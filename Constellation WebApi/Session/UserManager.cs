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

            var response = await Database.HandleLogin(username, password);
            //var created = await client.CreateDatabaseAsync("test");
            //var db = await client.GetDatabaseAsync("users");
            //var userDoc = db.GetAsync(username);
            return true;
        }
        public static async Task<bool> Create(string username, string password, string course)
        {
            if (username.IndexOf('_') != -1) 
            {
                Logger.Log("Attempted to create user {username} but it contains illegal character '_'");
                return false;
            }

            //Should have been hashed much earlier (like on the client side, but we'll get there)
            //password = hash(username+password);
            var isAdmin = course == "teacher";
            
            var response = await Database.CreateUser($"{username}_{course}", password, isAdmin);
            return true;
        }

        public static async Task<List<UserDocument>> List()
        {
            return await Database.ListUsers();
        }

        public static async Task<bool> Update(string id, string password)
        {
            return await Database.UpdateUser(id, password);
        }
        public static async Task<bool> Remove(string id)
        {
            return await Database.DeleteUser(id);
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
        public static string Hex(string text) 
        {
            
            byte[] ba = System.Text.Encoding.Default.GetBytes(text);
            var hexString = BitConverter.ToString(ba);
            return hexString.Replace("-","");
        }

        public static async Task<List<ContainerDefinition>> GetContainerDefinitions(string username, string course) 
        {
            return await GetContainerDefinitions(username+"_"+course);
            
        }
        public static async Task<List<ContainerDefinition>> GetContainerDefinitions(string userid) 
        {
            string user_db = "userdb-" + Hex(userid);
            user_db = user_db.ToLower();
            ConfigurationDocument config = await Database.GetDoc<ConfigurationDocument>(user_db,"user");
            if (config != null) {
                return config.container_definitions;
            }
            return new List<ContainerDefinition>();
        }
    }
}