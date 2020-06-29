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
            var client = new RestClient($"{Constants.BaseUrl}ViewDesignation") {
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
            var client = new RestClient($"{Constants.BaseUrl}ViewCollegeConfig") {
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
        public static async Task<List<Building>>
            ViewCollegeBuildings(string institutionId) {
            Logger.Log($"Called /ViewCollegeInfrastructure:Building for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}ViewCollegeInfrastructure") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = ApiBase
                .GenerateRequest($@"{{""institutionId"":""{institutionId}"", ""IsRoom"":false}}");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<List<Building>>(response.Content);
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
                .GenerateRequest($@"{{""institutionId"":""{institutionId}"", ""IsRoom"":true}}");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<List<CollegeInfrastructureRoomResponseModel>>(response.Content);
            Logger.Log($"API Server failed when getting College Infrastructure for {institutionId}.", LogMode.Error);
            return null;
        }

        public static async Task<bool> AddCollegeBuilding(string institutionId,
            List<Building> buildings) {
            Logger.Log($"Called /CollegeInfrastructureBuilding:Add for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}CollegeInfrastructureBuilding") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = new RestRequest(Method.POST) {
                    RequestFormat = DataFormat.Json
                }.AddHeader("Access-Control-Allow-Origin", "*")
                .AddHeader("Accept", "application/json")
                .AddJsonBody(JsonConvert.SerializeObject(new BuildingRequest(institutionId, buildings)));
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful) {
                var inst = InstitutionManager.GetInstitution(institutionId);
                inst.ResetCollegeBuildings();
                return true;
            }

            Logger.Log($"API Server failed when adding Buildings {buildings[0].buildingName},... to {institutionId}.",
                LogMode.Error);
            return false;
        }

        public static async Task<bool> AddCollegeBuilding(string institutionId, Building building) {
            return await AddCollegeBuilding(institutionId, new List<Building> {building});
        }

        class BuildingRequest {
            public string institutionId { get; set; }

            public List<Building> buildings { get; set; }

            public BuildingRequest(string institutionId, List<Building> buildings) {
                this.institutionId = institutionId;
                this.buildings = buildings;
            }
        }

        public static async Task<bool> AddCollegeRoom(string institutionId, List<RoomRequest> rooms) {
            Logger.Log($"Called /CollegeInfrastructureRoom:Add for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}CollegeInfrastructureRoom") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = new RestRequest(Method.POST) {
                    RequestFormat = DataFormat.Json
                }.AddHeader("Access-Control-Allow-Origin", "*")
                .AddHeader("Accept", "application/json")
                .AddJsonBody(
                    JsonConvert.SerializeObject(new CollegeInfrastructureRoomRequestModel(institutionId, rooms)));
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful) {
                var inst = InstitutionManager.GetInstitution(institutionId);
                inst.ResetCollegeRooms();
                return true;
            }

            Logger.Log($"API Server failed when adding Rooms {rooms[0].roomCode},... to {institutionId}.",
                LogMode.Error);
            return false;
        }

        public static async Task<bool> AddCollegeRoom(string institutionId, RoomRequest room) {
            return await AddCollegeRoom(institutionId, new List<RoomRequest> {room});
        }

        public class CollegeInfrastructureRoomRequestModel {
            public string institutionId { get; set; }
            public List<RoomRequest> rooms { get; set; }

            public CollegeInfrastructureRoomRequestModel(string institutionId, List<RoomRequest> rooms) {
                this.institutionId = institutionId;
                this.rooms = rooms;
            }
        }

        public static async Task<bool> UpdateConfig(CollegeConfigRequestModel requestModel) {
            Logger.Log($"Called /CollegeConfig:Update for {requestModel.institutionID}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}CollegeConfig") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = new RestRequest(Method.POST) {
                    RequestFormat = DataFormat.Json
                }.AddHeader("Access-Control-Allow-Origin", "*")
                .AddHeader("Accept", "application/json")
                .AddJsonBody(JsonConvert.SerializeObject(requestModel));
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful) {
                var inst = InstitutionManager.GetInstitution(requestModel.institutionID);
                inst.ResetCollegeConfigResponse();;
                return true;
            }

            Logger.Log($"API Server failed when adding Configuration to,... to {requestModel.institutionID}.",
                LogMode.Error);
            return false;
        }

        public class CollegeConfigRequestModel {
            public string institutionID { get; set; }

            public bool IsUG { get; set; }
            public bool IsPG { get; set; }
            public int minimumWorkingDaysOddSemester { get; set; }
            public int minimumWorkingDaysEvenSemester { get; set; }
            public List<Designation> designations { get; set; } = new List<Designation>();
        }

        public class Designation {
            public List<int> codes { get; set; }
            public string title { get; set; }
            public string? id { get; set; }
        }
    }
}