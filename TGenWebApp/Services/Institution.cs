using System.Collections.Generic;
using TGenWebApp.ResponseModels.View;

namespace TGenWebApp.Services {
    public class Institution {

        private ViewCollegeConfigResponseModel _config;
        private List<ViewCollegeFacultiesResponseModel> _faculties;
        
        public string InstitutionId { get; }
        public string InstitutionName { get; }

        public Institution(string institutionId, string institutionName) {
            InstitutionId = institutionId;
            InstitutionName = institutionName;
        }

        // /!\ BLOCKING
        public ViewCollegeConfigResponseModel CollegeConfig {
            get { return _config ??= ViewApi.ViewCollegeConfig(InstitutionId).Result; }
        }
        
        // /!\ BLOCKING
        public List<ViewCollegeFacultiesResponseModel> CollegeFaculties {
            get { return _faculties ??= ViewApi.ViewCollegeFaculties(InstitutionId).Result; }
        }
    }
}