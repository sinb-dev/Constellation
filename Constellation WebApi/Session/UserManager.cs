using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var response = await CouchDB.HandleLogin(username, password);
            
            //var created = await client.CreateDatabaseAsync("test");
            //var db = await client.GetDatabaseAsync("users");
            //var userDoc = db.GetAsync(username);
            return true;
        }
    }
}