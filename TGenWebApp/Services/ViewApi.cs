using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TGenWebApp.ResponseModels.View;

namespace TGenWebApp.Services {
    public static class ViewApi {
        public static async Task<ViewDesignationResponseModel> ViewDesignation(string institutionId) {
            await Logger.Log($"Called /ViewDesignation for {institutionId}", LogMode.Info);
            var client = new RestClient($"{Constants.BaseUrl}ViewDesignation")  {
                Timeout = -1,
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            var request = ApiBase.GenerateRequest($@"{{""institutionID"":""{institutionId}""}}");
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
                return JsonConvert.DeserializeObject<ViewDesignationResponseModel>(response.Content);
            await Logger.Log($"API Server failed when getting designations for {institutionId}.", LogMode.Error);
            return null;
        }
    }
}