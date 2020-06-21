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

        /// <summary>
        /// Cache of College Configuration. Look at <see cref="ViewApi.ViewCollegeConfig(string)"/>
        /// </summary>
        /// <returns>Latest College Configuration Object. </returns>
        public async Task<ViewCollegeConfigResponseModel> GetCollegeConfigResponse() {
           return _config ??= await ViewApi.ViewCollegeConfig(InstitutionId);
        }
        
        /// <summary>
        /// Resets Configuration cache.
        /// </summary>
        public void ResetCollegeConfigResponse() {
            _config = null;
        }
        
        /// <summary>
        /// Cache of College Faculty Data. Look at <see cref="ViewApi.ViewCollegeFaculties(string)"/>
        /// </summary>
        /// <returns>List of Faculties.</returns>
        public async Task<List<ViewCollegeFacultiesResponseModel>> GetCollegeFaculties() {
            return _faculties ??= await ViewApi.ViewCollegeFaculties(InstitutionId);
        }

        /// <summary>
        /// Resets Faculty cache. 
        /// </summary>
        public void ResetCollegeFaculties() {
            _faculties = null;
        }
        
        /// <summary>
        /// Cache of College Building layout. Look at <see cref="ViewApi.ViewCollegeBuildings(string)"/>
        /// </summary>
        /// <returns>List of Buildings.</returns>
        public async Task<List<CollegeInfrastructureBuildingResponseModel>> GetCollegeBuildings() {
            return _buildings ??= await ViewApi.ViewCollegeBuildings(InstitutionId);
        }

        /// <summary>
        /// Resets College Building Layout cache.
        /// </summary>
        public void ResetCollegeBuildings() {
            _buildings = null;
        }

        /// <summary>
        /// Cache of College rooms layout. Look at <see cref="ViewApi.ViewCollegeRooms(string)"/>
        /// </summary>
        /// <returns>List of College rooms.</returns>
        public async Task<List<CollegeInfrastructureRoomResponseModel>> GetCollegeRooms() {
            return _rooms ??= await ViewApi.ViewCollegeRooms(InstitutionId);
        }

        /// <summary>
        /// Resets College Room cache.
        /// </summary>
        public void ResetCollegeRooms() {
            _rooms = null;
        }
    }
}