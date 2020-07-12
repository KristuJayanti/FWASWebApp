using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using FWASWebApp.ResponseModels.Core;
using FWASWebApp.ResponseModels.Manager;

namespace FWASWebApp.Services.ManagerApi {
    public class FacultyApi {
         public static async Task<bool> Add(string institutionId, Faculty faculty) {
            Logger.Log($"Called /CollegeProgramme:Add for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}CollegeFaculty") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = new RestRequest(Method.POST) {
                    RequestFormat = DataFormat.Json
                }.AddHeader("Access-Control-Allow-Origin", "*")
                .AddHeader("Accept", "application/json")
                .AddJsonBody(
                    JsonConvert.SerializeObject(faculty));
            Console.WriteLine(JsonConvert.SerializeObject(faculty));
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful) {
                var resp = JsonConvert.DeserializeObject<FacultyResponseModel>(response.Content);
                if (resp.IsSuccess) {
                    var inst = InstitutionManager.GetInstitution(institutionId);
                    inst.ResetCollegeFaculties();
                    return true;
                }
            }

            Logger.Log(
                $"API Server failed when adding Faculty {faculty.userID},... to {institutionId}.",
                LogMode.Error);
            return false;
        }
    }

    public class FacultyResponseModel {
        public bool IsSuccess { get; set; }
        public string message { get; set; }
        
        #nullable enable

        public UserBasicResponseModel? UserBasicResponseModel { get; set; } = new UserBasicResponseModel();

        public string? userID { get; set; }

        public string? institutionId { get; set; }

        public string? departmentId { get; set; }

        public string? jobId { get; set; }
        public int? MaxTeachingHoursPerWeak { get; set; }
        public bool? IsTeaching { get; set; }
        public List<string>? designationIds { get; set; }
    }
}