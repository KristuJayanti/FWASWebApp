﻿using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;

namespace FWASWebApp.Services {
    internal static class ApiBase {

        internal static IRestRequest GenerateRequest(string jsonBody) {
            return new RestRequest(Method.POST) { RequestFormat = DataFormat.Json}
                .AddHeader("Access-Control-Allow-Origin", "*")
                .AddHeader("Accept", "application/json")
                .AddJsonBody(jsonBody);
        }

        internal static Dictionary<string, string> GetDict(string response) {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
        }
    }
}