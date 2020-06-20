using System.Collections.Generic;
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

        // /!\ BLOCKING
        public ViewCollegeConfigResponseModel CollegeConfig {
            get { 
                _config ??= ViewApi.ViewCollegeConfig(InstitutionId).Result;
                return _config;
            }
        }
        
        // /!\ BLOCKING
        public List<ViewCollegeFacultiesResponseModel> CollegeFaculties {
            get {
                _faculties ??= ViewApi.ViewCollegeFaculties(InstitutionId).Result;
                return _faculties;
            }
        }
    }
}