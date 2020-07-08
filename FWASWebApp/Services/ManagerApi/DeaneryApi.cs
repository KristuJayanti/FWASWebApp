using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TGenWebApp.ResponseModels.Manager;

namespace TGenWebApp.Services.ManagerApi {
    public static class DeaneryApi {

        public static async Task<bool> Add(string institutionId, List<Deanery> deaneries) {
            Logger.Log($"Called /CollegeDeanery:Add for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}CollegeDeanery") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = new RestRequest(Method.POST) {
                    RequestFormat = DataFormat.Json
                }.AddHeader("Access-Control-Allow-Origin", "*")
                .AddHeader("Accept", "application/json")
                .AddJsonBody(JsonConvert.SerializeObject(new CollegeDeaneryRequestModel(institutionId, deaneries)));
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful) {
                var inst = InstitutionManager.GetInstitution(institutionId);
                inst.ResetDeaneries();
                return true;
            }
            Logger.Log($"API Server failed when adding Deanery {deaneries[0].deaneryName},... to {institutionId}.",
                LogMode.Error);
            return false;
        }

        public static async Task<bool> Add(string institutionId, Deanery deanery) {
            return await Add(institutionId, new List<Deanery> {deanery});
        }
        
        
        public static async Task<List<Deanery>> Get(string institutionId) {
            Logger.Log($"Called /CollegeDeanery:get for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}CollegeDeanery") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = ApiBase
                .GenerateRequest($@"{{""institutionId"":""{institutionId}""}}");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<List<Deanery>>(response.Content);
            Logger.Log($"API Server failed when getting College Deanery for {institutionId}.", LogMode.Error);
            return null;
        }
    }
    
    public class CollegeDeaneryRequestModel {
        public string institutionId { get; set; }
        public List<Deanery> deaneries { get; set; }

        public CollegeDeaneryRequestModel(string institutionId, List<Deanery> deaneries) {
            this.institutionId = institutionId;
            this.deaneries = deaneries;
        }
    }
}