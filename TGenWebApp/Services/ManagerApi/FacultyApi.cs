using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TGenWebApp.ResponseModels.Manager;

namespace TGenWebApp.Services.ManagerApi {
    public class FacultyApi {
         public static async Task<bool> Add(string institutionId, Faculty faculty) {
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
                    JsonConvert.SerializeObject(faculty));
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful) {
                var inst = InstitutionManager.GetInstitution(institutionId);
                inst.ResetCollegeFaculties();
                return true;
            }

            Logger.Log(
                $"API Server failed when adding Faculty {faculty.userID},... to {institutionId}.",
                LogMode.Error);
            return false;
        }
    }
}