using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace TGenWebApp.Services {
    public static class SessionManager {
        private static readonly Dictionary<string, Session> NameMap = new Dictionary<string, Session>();

        private static string GetRandomString(int size = 14) {
            return new string(Enumerable.Repeat("abcdef1234567890", size)
                .Select(s => {
                    var cryptoResult = new byte[4];
                    using (var cryptoProvider = new RNGCryptoServiceProvider())
                        cryptoProvider.GetBytes(cryptoResult);

                    return s[new Random(BitConverter.ToInt32(cryptoResult, 0)).Next(s.Length)];
                })
                .ToArray());
        }

        /// <summary>
        ///  Add a session to the scope.
        /// </summary>
        /// <param name="session">A valid session object.</param>
        /// <returns>Session ID</returns>
        public static string AddSession(Session session) {
            Logger.Log($"Created session for {session.Id}");
            var random = GetRandomString();
            while (NameMap.ContainsKey(random))
                random = GetRandomString();
            NameMap.Add(random, session);
            return random;                                                            
        }
        
        /// <summary>
        /// Get a session object upon requesting for a valid sessionID.
        /// </summary>
        /// <param name="session">A session ID</param>
        /// <returns>Session object if the session ID is valid.</returns>
        public static Session GetSession(string session) {
            return IsValidSession(session) ? NameMap[session] : null;
        }

        public static string GetName(string sessionId) {
            return NameMap[sessionId].Name;
        }

        public static void RemoveSession(string sessionId) {
            NameMap.Remove(sessionId);
        }

        public static bool IsValidSession(string sessionId) {
            return !string.IsNullOrEmpty(sessionId) && NameMap.ContainsKey(sessionId);
        }
    }
}