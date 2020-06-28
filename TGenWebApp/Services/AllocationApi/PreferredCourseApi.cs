using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TGenWebApp.ResponseModels.Manager;
using TGenWebApp.Services.ManagerApi;

namespace TGenWebApp.Services.AllocationApi {
    public class PreferredCourseApi {
        public static async Task<bool> Add(string userId, List<string> courseIds) {
            Logger.Log($"Called /PreferredCourse:Add for {userId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}PreferredCourse") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = new RestRequest(Method.POST) {
                    RequestFormat = DataFormat.Json
                }.AddHeader("Access-Control-Allow-Origin", "*")
                .AddHeader("Accept", "application/json")
                .AddJsonBody(JsonConvert.SerializeObject
                    (new PreferredCourse{preferredCourseIds = courseIds, userId = userId})
                );
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful) {
                return true;
            }

            Logger.Log($"API Server failed when adding Preferred Course {courseIds[0]},... to {userId}.",
                LogMode.Error);
            return false;
        }

        public static async Task<PreferredCourse> Get(string userId) {
            Logger.Log($"Called /PreferredCourse:get for {userId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}PreferredCourse") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = ApiBase
                .GenerateRequest($@"{{""userId"":""{userId}""}}");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<PreferredCourse>(response.Content);
            Logger.Log($"API Server failed when getting PreferredCourse for {userId}.", LogMode.Error);
            return null;
        }
    }
}