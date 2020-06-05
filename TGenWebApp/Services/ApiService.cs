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
        private const string BaseUrl = "https://localhost:5001/";
        
        private static IRestRequest GenerateRequest(string jsonBody) {
            return new RestRequest(Method.POST) { RequestFormat = DataFormat.Json}
                .AddHeader("Access-Control-Allow-Origin", "*")
                .AddHeader("Accept", "application/json")
                .AddJsonBody(jsonBody);
        }

        private static Dictionary<string, string> GetDict(string response) {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
        }
        
        public static async Task<bool> IsValidUsername(string username) {
            var client = new RestClient($"{BaseUrl}CheckUsername") {Timeout = -1};
            var request = GenerateRequest($@"{{""username"":""{username}""}}");
            var response = await client.ExecuteAsync(request);
            var re = GetDict(response.Content);
            return bool.Parse(re["isUsernameExists"]);
        }

        public static async Task<string> Login(string username, string password) {
            var client = new RestClient($"{BaseUrl}UserAuth") {Timeout = -1};
            var request = GenerateRequest($@"{{""username"":""{username}"", 
                                                        ""password"":""{password}""}}");
            var response = await client.ExecuteAsync(request);
            var result = GetDict(response.Content);
            if (result["validationMessage"] == "Validation Success") {
                var sess = new Session() {
                    Id = result["userId"],
                };
                return SessionManager.AddSession(sess);
            }
            return null;
        }
    }
}