using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TGenWebApp.ResponseModels.Manager;

namespace TGenWebApp.Services.AllocationApi {
    public class SelectedCourseApi {
        public static async Task<bool> Add(string userId, List<string> courseIds) {
            Logger.Log($"Called /SelectedCourse:Add for {userId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}SelectedCourse") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = new RestRequest(Method.POST) {
                    RequestFormat = DataFormat.Json
                }.AddHeader("Access-Control-Allow-Origin", "*")
                .AddHeader("Accept", "application/json")
                .AddJsonBody(JsonConvert.SerializeObject
                    (new SelectedCourse{selectedCourseIds = courseIds, userId = userId})
                );
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful) {
                return true;
            }

            Logger.Log($"API Server failed when adding SelectedCourse {courseIds[0]},... to {userId}.",
                LogMode.Error);
            return false;
        }

        public static async Task<SelectedCourse> Get(string userId) {
            Logger.Log($"Called /SelectedCourse:get for {userId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}SelectedCourse") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = ApiBase
                .GenerateRequest($@"{{""userId"":""{userId}""}}");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<SelectedCourse>(response.Content);
            Logger.Log($"API Server failed when getting SelectedCourse for {userId}.", LogMode.Error);
            return null;
        }
    }
}