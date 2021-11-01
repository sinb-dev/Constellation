using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;
using System.Net.Http.Headers;
using CouchDB.Client.FluentMango;

namespace Constellation_WebApi
{
    public class Database
    {
        const string host = "https://sofa.hoxer.net:6984/";
        static readonly HttpClient client = new HttpClient(new HttpClientHandler(){
            Credentials = new NetworkCredential("admin", "123hemlig"),
        });
        static Database()
        {
            var encoded = Convert.ToBase64String( System.Text.Encoding.ASCII.GetBytes(
                String.Format( "{0}:{1}", "admin", "123hemlig" ) ) );
            
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue( "Basic", encoded );
        }
        public static async Task<string> HandleLogin(string username, string password)
        {
            var user = await GetDoc<UserDocument>("users",username);
            Console.WriteLine(user.password+"=="+password);
            return "";
        }

        public static async Task<bool> Put(string database, Document doc)
        {
            string request = $"{host}{database}/{doc._id}";
            JsonContent content = null;
            if (doc.GetType() == typeof(UserDocument)) {
                content = JsonContent.Create(doc as UserDocument);
            }
            
            try {
                HttpResponseMessage response = await client.PutAsync(request,content);
            }
            catch(NullReferenceException e)
            {
                Logger.Error("Could save an unknown document type",e);
            }
            catch(Exception e)
            {
                Logger.Error("Could not put document",e);
            }
            return true;
            
        }

        public static async Task<T> GetDoc<T>(string database, string id)
        {
           
            try 
            {
                string request = $"{host}{database}/{id}";
                HttpResponseMessage response = await client.GetAsync(request);
                string responseText = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseText);
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("Request failed: "+e.Message);
                return default(T);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: "+e.Message);
                return default(T);
            }
        }


        public static async Task<List<T>> GetList<T>(string database, string query) 
        {
            
            List<T> result = new List<T>();
            try 
            {
                string request = $"{host}{database}/_find";
                StringContent content = new StringContent(Query());
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                
                HttpResponseMessage response = await client.PostAsync(request,content);
                string responseText = await response.Content.ReadAsStringAsync();
                QueryResponse<T> queryResponse = JsonSerializer.Deserialize<QueryResponse<T>>(responseText) ;
                result = queryResponse.docs;
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("Request failed: "+e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: "+e.Message);
            }
            return result;
        }
        public static async Task<List<UserDocument>> ListUsers()
        {
            return await GetList<UserDocument>("_users", Query());
        }
        public static string Query()
        {
            
            var query = new FindBuilder()
                .AddSelector("roles", SelectorOperator.In, new string[] {"user" });
            
            return query.ToString();

        }
        public static async Task<bool> DeleteUser(string id)
        {
            UserDocument doc = await UserDocument.Load(id);
            if (string.IsNullOrEmpty(doc.name))
            {
                return false;
            }
            string request = $"{host}_users/org.couchdb.user:{id}?rev="+doc._rev;
            HttpResponseMessage response = await client.DeleteAsync(request);
            return response.StatusCode == HttpStatusCode.OK;
        }
        public static async Task<bool> UpdateUser(string id, string password)
        {
            //First find the database user
            string request = $"{host}_users/org.couchdb.user:{id}";

            UserDocument doc = await UserDocument.Load(id);
            if (string.IsNullOrEmpty(doc.name))
            {
                return false;
            }
            doc.password = password;

            JsonContent content = JsonContent.Create(doc);
            
            HttpResponseMessage response = await client.PutAsync(request,content);

            return response.StatusCode == HttpStatusCode.Created;
        }

        public static async Task<bool> CreateUser(string id, string password, bool admin)
        {
            //First create the database user
            string request = $"{host}_users/org.couchdb.user:{id}";
            List<string> roles = admin ? new List<string>{ "user", "admin" } : new List<string>{"user"};
            string type = admin ? "admin" : "user";
            UserDocument doc = new UserDocument() {
                name        = id,
                password    = password,
                type        = type,
                roles       = roles
            };

            JsonSerializerOptions options  = new() {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
            JsonContent content = JsonContent.Create(doc,null,options);
            
            HttpResponseMessage response = await client.PutAsync(request,content);

            return response.StatusCode == HttpStatusCode.Created;
        }

        public static async void GetContainerDefinitions() 
        {
            
        }
    }
    public class QueryResponse<T> {
        public List<T> docs {get;set;}
    }
    public abstract class Document {
        public abstract string _id {get;set;}
        public abstract string _rev {get;set;}
    }
    public class UserDocument : Document
    {
        public override string _id {get;set;}
        public override string _rev {get;set;}
        public string name {get;set;}
        public string password {get;set;}
        public string type {get;set;}
        public List<string> roles {get;set;} = new();
        public static async Task<UserDocument> Load(string id)
        {
            UserDocument doc = null;
            id = $"org.couchdb.user:{id}";
            try
            {
                doc = await Database.GetDoc<UserDocument>("_users",id);
            }
            catch(Exception e)
            {
                Logger.Error($"Could not retrieve user document for {id}",e);
            }
            return doc;
        }
    }
    public class ConfigurationDocument : Document
    {
        public override string _id {get;set;}
        public override string _rev {get;set;}
        public string username {get;set;}
        public string password {get;set;}
        public string course {get;set;}
        public List<ContainerDefinition> container_defititions {get;set;} = new List<ContainerDefinition>();


    }
    public class ContainerDefinition
    {
        public string image {get;set;}
        public string prefix {get;set;}
        public int port {get;set;}
    }
}