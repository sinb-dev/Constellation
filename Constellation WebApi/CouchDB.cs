using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;
using System.Net.Http.Headers;

namespace Constellation_WebApi
{
    public class CouchDB
    {
        static readonly HttpClient client = new HttpClient(new HttpClientHandler(){
            Credentials = new NetworkCredential("admin", "123hemlig"),
        });
        static CouchDB()
        {
            //client.BaseAddress = new Uri("http://localhost:10000");
            
        }
        public static async Task<string> HandleLogin(string username, string password)
        {
            var user = await GetDoc<UserDocument>("users",username);
            Console.WriteLine(user.password+"=="+password);
            return "";
        }

        public static async Task<T> GetDoc<T>(string database, string id)
        {
            var encoded = Convert.ToBase64String( System.Text.Encoding.ASCII.GetBytes(
                String.Format( "{0}:{1}", "admin", "123hemlig" ) ) );
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue( "Basic", encoded );
            try 
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:10000/"+database+"/"+id+"/");
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

    }        
    public class UserDocument 
    {
        public string username {get;set;}
        public string password {get;set;}
        public int type {get;set;}
    }
}