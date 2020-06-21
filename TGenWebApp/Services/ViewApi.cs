using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TGenWebApp.ResponseModels.Manager;
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
        
        /// <summary>
        /// Get Faculty details. Endpoint for /ViewCollegeFaculties
        /// </summary>
        /// <param name="institutionId">Institution ID</param>
        /// <returns>List of Faculties</returns>
        public static async Task<List<ViewCollegeFacultiesResponseModel>> ViewCollegeFaculties(string institutionId) {
            Logger.Log($"Called /ViewCollegeFaculties for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}ViewCollegeFaculties") {
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
        
        /// <summary>
        /// Get a list of buildings available in the institution.
        /// </summary>
        /// <param name="institutionId">Institution ID</param>
        /// <returns>A list with all the buildings.</returns>
        public static async Task<List<CollegeInfrastructureBuildingResponseModel>>
            ViewCollegeBuildings(string institutionId) {
            Logger.Log($"Called /ViewCollegeInfrastructure:Building for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}ViewCollegeInfrastructure") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = ApiBase
                .GenerateRequest($@"{{""institutionID"":""{institutionId}""}}, {{""isRoom"":""false""}}");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<List<CollegeInfrastructureBuildingResponseModel>>(response.Content);
            Logger.Log($"API Server failed when getting College Infrastructure for {institutionId}.", LogMode.Error);
            return null;
        }

        /// <summary>
        /// Get a list of rooms available in the institution.
        /// </summary>
        /// <param name="institutionId">Institution ID</param>
        /// <returns>A list containing all the rooms.</returns>
        public static async Task<List<CollegeInfrastructureRoomResponseModel>> ViewCollegeRooms(string institutionId) {
            Logger.Log($"Called /ViewCollegeInfrastructure:Building for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}ViewCollegeInfrastructure") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = ApiBase
                .GenerateRequest($@"{{""institutionID"":""{institutionId}""}}, {{""isRoom"":""true""}}");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<List<CollegeInfrastructureRoomResponseModel>>(response.Content);
            Logger.Log($"API Server failed when getting College Infrastructure for {institutionId}.", LogMode.Error);
            return null;
        }
    }
}