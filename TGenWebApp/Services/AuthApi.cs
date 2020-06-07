﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;

namespace TGenWebApp.Services {
    public static class AuthApi {
        
        /// <summary>
        /// Implementation of Endpoint /CheckUsername
        /// </summary>
        /// <param name="username">An Email Address or username.</param>
        /// <returns>Returns if the email or username is registered.</returns>
        public static async Task<bool> IsValidUsername(string username) {
            await Logger.Log($"Called /CheckUsername for {username}", LogMode.Information);
            var client = new RestClient($"{ApiBase.BaseUrl}CheckUsername") {Timeout = -1};
            var request = ApiBase.GenerateRequest($@"{{""username"":""{username}""}}");
            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful) { 
                await Logger.Log($"API Server failed when calling {username}.", LogMode.Error);
                return false;
            }
            var re = ApiBase.GetDict(response.Content);
            return bool.Parse(re["isUsernameExists"]);
        }

        /// <summary>
        /// Perform a login using the API Endpoint /UserAuth
        /// </summary>
        /// <param name="username">Login Email</param>
        /// <param name="password">Login Password</param>
        /// <returns>Session ID</returns>
        public static async Task<string> Login(string username, string password) {
            await Logger.Log($"Called /UserAuth for {username}", LogMode.Information);
            var client = new RestClient($"{ApiBase.BaseUrl}UserAuth") {Timeout = -1};
            var request = ApiBase.GenerateRequest($@"{{""username"":""{username}"", 
                                                        ""password"":""{password}""}}");
            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful) {
                await Logger.Log($"API Server failed when calling {username}.", LogMode.Error);
                return null;
            }
            var result = ApiBase.GetDict(response.Content);
            if (result["validationMessage"] != "Validation Success") return null;
            var sess = new Session {
                Id = result["userId"]
            };
            return await SessionManager.AddSession(sess);
        }
    }
}