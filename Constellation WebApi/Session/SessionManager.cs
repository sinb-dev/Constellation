using System;
using System.Collections.Generic;
namespace Constellation_WebApi.Sesion
{
    public class SessionManager
    {
        Dictionary<string, Session> sessions = new();
        /// <summary>
        ///  Adds another session to the manager.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>

        public void Add(Session session) {
            sessions[session.SessionId] = session;
        }

        /// <summary>
        /// Checks if a session id exists and not expired
        /// </summary>
        /// <param name="string">The id of the user session</param>
        /// <returns>bool</returns>
        public bool Check(string sessionId) {
            if (!sessions.ContainsKey(sessionId))
            {
                return false;
            }
            if (sessions[sessionId].IsExpired())
            {
                sessions.Remove(sessionId);
                return false;
            }   
            return true;
        }
    }
}