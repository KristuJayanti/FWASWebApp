using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TGenWebApp.ResponseModels.View;

namespace TGenWebApp.Services {
    public static class ViewApi {
        /// <summary>
        /// Get all the designations from an institution
        /// </summary>
        /// <param name="institutionId">Institution ID</param>
        /// <returns>Instance of ViewDesignationResponseModel</returns>
        public static async Task<ViewDesignationResponseModel> ViewDesignation(string institutionId) {
            Logger.Log($"Called /ViewDesignation for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}ViewDesignation")  {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = ApiBase.GenerateRequest($@"{{""institutionID"":""{institutionId}""}}");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<ViewDesignationResponseModel>(response.Content);
            Logger.Log($"API Server failed when getting designations for {institutionId}.", LogMode.Error);
            return null;
        }

        /// <summary>
        /// Get College Config, endpoint for /ViewCollegeConfig
        /// </summary>
        /// <param name="institutionId">Institution ID</param>
        /// <returns>Instance of ViewCollegeConfigResponseModel</returns>
        public static async Task<ViewCollegeConfigResponseModel> ViewCollegeConfig(string institutionId) {
            Logger.Log($"Called /ViewCollegeConfig for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}ViewCollegeConfig")  {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = ApiBase.GenerateRequest($@"{{""institutionID"":""{institutionId}""}}");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<ViewCollegeConfigResponseModel>(response.Content);
            Logger.Log($"API Server failed when getting College Config for {institutionId}.", LogMode.Error);
            return null;
        }

        public static async Task<List<ViewCollegeFacultiesResponseModel>> ViewCollegeFaculties(string institutionId) {
            Logger.Log($"Called /ViewCollegeFaculties for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}ViewCollegeConfig")  {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = ApiBase.GenerateRequest($@"{{""institutionID"":""{institutionId}""}}");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<List<ViewCollegeFacultiesResponseModel>>(response.Content);
            Logger.Log($"API Server failed when getting College Faculties for {institutionId}.", LogMode.Error);
            return null;
        }
    }
}