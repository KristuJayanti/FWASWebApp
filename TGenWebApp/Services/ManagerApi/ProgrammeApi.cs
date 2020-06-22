using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TGenWebApp.ResponseModels.Manager;

namespace TGenWebApp.Services.ManagerApi {
    public class ProgrammeApi {
        public static async Task<bool> Add(string institutionId, List<Programme> programmes) {
            Logger.Log($"Called /CollegeProgramme:Add for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}CollegeProgramme") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = new RestRequest(Method.POST) {
                    RequestFormat = DataFormat.Json
                }.AddHeader("Access-Control-Allow-Origin", "*")
                .AddHeader("Accept", "application/json")
                .AddJsonBody(
                    JsonConvert.SerializeObject(
                        new CollegeProgrammeRequestModel(institutionId, programmes)));
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful) {
                var inst = InstitutionManager.GetInstitution(institutionId);
                inst.ResetProgrammes();
                return true;
            }

            Logger.Log(
                $"API Server failed when adding Programme {programmes[0].programmeName},... to {institutionId}.",
                LogMode.Error);
            return false;
        }

        public static async Task<bool> Add(string institutionId, Programme programmes) {
            return await Add(institutionId, new List<Programme> {programmes});
        }


        public static async Task<List<Programme>> Get(string institutionId) {
            Logger.Log($"Called /CollegeProgramme:get for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}CollegeProgramme") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = ApiBase
                .GenerateRequest($@"{{""institutionId"":""{institutionId}""}}");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<List<Programme>>(response.Content);
            Logger.Log($"API Server failed when getting College Programmes for {institutionId}.", LogMode.Error);
            return null;
        }
    }

    public class CollegeProgrammeRequestModel {
        public string institutionId { get; set; }
        public List<Programme>? programmes { get; set; }

        public CollegeProgrammeRequestModel(string institutionId, List<Programme> programmes) {
            this.institutionId = institutionId;
            this.programmes = programmes;
        }
    }
}