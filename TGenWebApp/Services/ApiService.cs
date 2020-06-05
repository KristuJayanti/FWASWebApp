using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;

namespace TGenWebApp.Services {
    public static class ApiService {
        public static async Task<bool> IsValidUsername(string username) {
            var client = new RestClient($"{ApiBase.BaseUrl}CheckUsername") {Timeout = -1};
            var request = ApiBase.GenerateRequest($@"{{""username"":""{username}""}}");
            var response = await client.ExecuteAsync(request);
            var re = ApiBase.GetDict(response.Content);
            return bool.Parse(re["isUsernameExists"]);
        }

        public static async Task<string> Login(string username, string password) {
            var client = new RestClient($"{ApiBase.BaseUrl}UserAuth") {Timeout = -1};
            var request = ApiBase.GenerateRequest($@"{{""username"":""{username}"", 
                                                        ""password"":""{password}""}}");
            var response = await client.ExecuteAsync(request);
            var result = ApiBase.GetDict(response.Content);
            if (result["validationMessage"] == "Validation Success") {
                var sess = new Session {
                    Id = result["userId"]
                };
                return SessionManager.AddSession(sess);
            }
            return null;
        }
    }
}