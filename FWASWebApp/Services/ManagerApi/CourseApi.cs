using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using FWASWebApp.ResponseModels.Manager;

namespace FWASWebApp.Services.ManagerApi {
    public class CourseApi {
        public static async Task<bool> Add(string institutionId, List<Course> courses) {
            Logger.Log($"Called /CollegeDeanery:Add for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}CollegeCourse") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = new RestRequest(Method.POST) {
                    RequestFormat = DataFormat.Json
                }.AddHeader("Access-Control-Allow-Origin", "*")
                .AddHeader("Accept", "application/json")
                .AddJsonBody(JsonConvert.SerializeObject(new CollegeCourseRequestModel(institutionId, courses)));
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful) {
                var inst = InstitutionManager.GetInstitution(institutionId);
                inst.ResetCourses();
                return true;
            }

            Logger.Log($"API Server failed when adding Course {courses[0].courseName},... to {institutionId}.",
                LogMode.Error);
            return false;
        }

        public static async Task<bool> Add(string institutionId, Course course) {
            return await Add(institutionId, new List<Course> {course});
        }


        public static async Task<List<Course>> Get(string institutionId) {
            Logger.Log($"Called /CollegeDeanery:get for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}CollegeCourse") {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = ApiBase
                .GenerateRequest($@"{{""institutionId"":""{institutionId}""}}");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<List<Course>>(response.Content);
            Logger.Log($"API Server failed when getting College Course for {institutionId}.", LogMode.Error);
            return null;
        }
    }

    public class CollegeCourseRequestModel {
        public string institutionId { get; set; }
        public List<Course> courses { get; set; }

        public CollegeCourseRequestModel(string institutionId, List<Course> courses) {
            this.institutionId = institutionId;
            this.courses = courses;
        }
    }
}