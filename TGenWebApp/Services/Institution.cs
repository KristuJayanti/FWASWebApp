using System.Collections.Generic;
using System.Threading.Tasks;
using TGenWebApp.ResponseModels.Manager;
using TGenWebApp.ResponseModels.View;

namespace TGenWebApp.Services {
    public class Institution {

        private ViewCollegeConfigResponseModel _config;
        private List<ViewCollegeFacultiesResponseModel> _faculties;
        private List<CollegeInfrastructureBuildingResponseModel> _buildings;
        private List<CollegeInfrastructureRoomResponseModel> _rooms;

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

        public async Task<List<CollegeInfrastructureBuildingResponseModel>> GetCollegeBuildings() {
            return _buildings ??= await ViewApi.ViewCollegeBuildings(InstitutionId);
        }

        public async Task<List<CollegeInfrastructureRoomResponseModel>> GetCollegeRooms() {
            return _rooms ??= await ViewApi.ViewCollegeRooms(InstitutionId);
        }
    }
}