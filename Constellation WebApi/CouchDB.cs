using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;
using System.Net.Http.Headers;

namespace Constellation_WebApi
{
    public class CouchDB
    {
        const string host = "https://sofa.hoxer.net:6984/";
        static readonly HttpClient client = new HttpClient(new HttpClientHandler(){
            Credentials = new NetworkCredential("admin", "123hemlig"),
        });
        static CouchDB()
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
        public static async Task<bool> CreateUser(string username, string password)
        {
            
            string request = $"{host}_users/org.couchdb.user:{username}";
            UserDocument doc = new UserDocument() {
                name        = username,
                password    = password,
                type        = "user"
            };
            JsonContent content = JsonContent.Create(doc);
            
            HttpResponseMessage response = await client.PutAsync(request,content);
            return true;
        }
    }
    public abstract class Document {
        public abstract string _id {get;set;}
    }
    public class UserDocument : Document
    {
        public override string _id {get {return name;}set { name = value;}}
        public string name {get;set;}
        public string password {get;set;}
        //public string course {get;set;}
        public string type {get;set;}
        public List<string> roles {get;set;} = new();
        public static async Task<UserDocument> Load(string username)
        {
            UserDocument doc = null;
            try
            {
                doc = await CouchDB.GetDoc<UserDocument>("users",username);
            }
            catch(Exception e)
            {
                Logger.Error($"Could not retrieve user document for {username}",e);
            }
            return doc;
        }
    }
}