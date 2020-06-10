﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using TGenWebApp.ResponseModels.Core;

namespace TGenWebApp.Services {
    /// <summary>
    /// Authentication APIs are implemented in this class. 
    /// </summary>
    public static class AuthApi {
        /// <summary>
        /// Implementation of Endpoint /CheckUsername
        /// </summary>
        /// <param name="username">An Email Address or username.</param>
        /// <returns>Returns if the email or username is registered.</returns>
        public static async Task<bool> IsValidUsername(string username) {
            await Logger.Log($"Called /CheckUsername for {username}", LogMode.Information);
            var client = new RestClient($"{Constants.BaseUrl}CheckUsername") {Timeout = -1};
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
            var client = new RestClient($"{Constants.BaseUrl}UserAuth") {Timeout = -1};
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
                Id = result["userId"],
                UserType = result["userType"] == "institution" ? UserType.Institution : UserType.User
            };
            await CompleteSession(sess);
            return await SessionManager.AddSession(sess);
        }

        private static async Task<UserBasicResponseModel> CompleteSession(Session session) {
            await Logger.Log($"Called /UserBasic for {session.Id}", LogMode.Information);
            var client = new RestClient($"{Constants.BaseUrl}UserBasic") {Timeout = -1};
            var request = ApiBase.GenerateRequest($@"{{""userId"":""{session.UserType}"", 
                                                    ""userType"":""{session.UserType}""}}");
            var response = await client.ExecuteAsync<UserBasicResponseModel>(request);
            MapClass(session, response.Data);
            return response.Data;
        }

        private static void MapClass(Session session, UserBasicResponseModel model) {
            session.Name = model.name;
            if (session.UserType == UserType.User) {
                session.InstitutionId = model.institutionId;
                session.InstitutionName = model.addressLocation.name;
            } else {
                session.InstitutionId = session.Id;
                session.InstitutionName = session.Name;
            }
        }
    }
}