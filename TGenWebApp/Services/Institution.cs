using System.Collections.Generic;
using System.Threading.Tasks;
using TGenWebApp.ResponseModels.View;

namespace TGenWebApp.Services {
    public class Institution {

        private ViewCollegeConfigResponseModel _config;
        private List<ViewCollegeFacultiesResponseModel> _faculties;

        private string InstitutionId;
        private string InstitutionName;

        public Institution(string institutionId, string institutionName) {
            InstitutionId = institutionId;
            InstitutionName = institutionName;
        }

        public async Task<ViewCollegeConfigResponseModel> GetCollegeConfigResponse() {
           return _config ??= await ViewApi.ViewCollegeConfig(InstitutionId);
        }
        
        public async Task<List<ViewCollegeFacultiesResponseModel>> GetCollegeFaculties() {
            return _faculties ??= await ViewApi.ViewCollegeFaculties(InstitutionId);
        }
    }
}