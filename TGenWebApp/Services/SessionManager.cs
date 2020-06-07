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
        public static async Task<string> AddSession(Session session) {
            await Logger.Log($"Created session for {session.Id}");
            var random = GetRandomString();
            while (NameMap.ContainsKey(random))
                random = GetRandomString();
            NameMap.Add(random, session);
            return random;
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

        public static Session Login(string username, string password) {
            return new Session();
        }
    }
}