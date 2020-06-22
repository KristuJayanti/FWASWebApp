using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TGenWebApp.ResponseModels.Manager;

namespace TGenWebApp.Services.ManagerApi {
    public class DepartmentApi {
        public static async Task<bool> Add(string institutionId, List<Department> departments) {
            Logger.Log($"Called /CollegeDepartment:Add for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}CollegeDepartment") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = new RestRequest(Method.POST) {
                    RequestFormat = DataFormat.Json
                }.AddHeader("Access-Control-Allow-Origin", "*")
                .AddHeader("Accept", "application/json")
                .AddJsonBody(JsonConvert.SerializeObject(new CollegeDepartmentRequestModel(institutionId, departments)));
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful) {
                var inst = InstitutionManager.GetInstitution(institutionId);
                inst.ResetDepartments();
                return true;
            }

            Logger.Log($"API Server failed when adding Deanery {departments[0].departmentName},... to {institutionId}.",
                LogMode.Error);
            return false;
        }

        public static async Task<bool> Add(string institutionId, Department department) {
            return await Add(institutionId, new List<Department> {department});
        }


        public static async Task<List<Department>> Get(string institutionId) {
            Logger.Log($"Called /CollegeDepartment:get for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}CollegeDepartment") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = ApiBase
                .GenerateRequest($@"{{""institutionId"":""{institutionId}""}}");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<List<Department>>(response.Content);
            Logger.Log($"API Server failed when getting College Deanery for {institutionId}.", LogMode.Error);
            return null;
        }

        private class CollegeDepartmentRequestModel {
            private string institutionId { get; set; }

            private List<Department>? departments { get; set; } = new List<Department>();

            public CollegeDepartmentRequestModel(string institutionId, List<Department> departments) {
                this.institutionId = institutionId;
                this.departments = departments;
            }
        }
    }
}