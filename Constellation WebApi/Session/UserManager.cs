using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace Constellation_WebApi.SessionHandling
{
    public static class UserManager
    {
        /// <summary>
        ///  Attempts to log in user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>bool</returns>

        public static async Task<bool> Login(string username, string password) 
        {
            //Should have been hashed much earlier (like on the client side, but we'll get there)
            password = hash(username+password);

            var response = await CouchDB.HandleLogin(username, password);
            //var created = await client.CreateDatabaseAsync("test");
            //var db = await client.GetDatabaseAsync("users");
            //var userDoc = db.GetAsync(username);
            return true;
        }
        public static async Task<bool> Create(string username, string password, string course)
        {
            //Should have been hashed much earlier (like on the client side, but we'll get there)
            password = hash(username+password);

            UserDocument user = new UserDocument() {
                username = username,
                password = password,
                type = 0,
                course = course
            };
            
            var response = await CouchDB.Put("users", user);
            return true;
        }
        static string hash(string input)
        {
            var test = SHA1.HashData(System.Text.Encoding.UTF8.GetBytes(input));
            var sb = new System.Text.StringBuilder();
            foreach (byte x in test)
            {
                sb.Append(x.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}