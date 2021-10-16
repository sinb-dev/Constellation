using System;

namespace Constellation_WebApi.SessionHandling
{
    public class Session
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        TimeSpan expiration = TimeSpan.FromMinutes(15);
        public int UserId {get;set;}
        public string SessionId {get;set;}
        public DateTime LastUpdated {get;set;}

        public Session() : this(DateTime.Now) {}
        public Session(DateTime lastUpdated) 
        {
            SessionId = CreateSessionId();
            LastUpdated = lastUpdated;
        }

        public static string CreateSessionId()
        {
            int length = 32;
            Random r = new Random();
            
            char[] id = new char[length];
            for (int i = 0; i < length; i++)
            {
                id[i] = chars[r.Next(0,chars.Length)];
                
            }
            return string.Join("",id);
        }

        public bool IsExpired()
        {
            return DateTime.Now - LastUpdated > expiration;
        }
    }
}