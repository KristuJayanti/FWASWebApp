using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGenWebApp.ResponseModels.Manager;
using TGenWebApp.ResponseModels.View;
using TGenWebApp.Services.ManagerApi;

namespace TGenWebApp.Services {
    public class Institution {

        private ViewCollegeConfigResponseModel _config;
        private List<ViewCollegeFacultiesResponseModel> _faculties;
        private List<Designations> _designations;
        private List<Building> _buildings;
        private List<CollegeInfrastructureRoomResponseModel> _rooms;
        private List<Deanery> _deanery;
        private List<Department> _departments;
        private List<Course> _courses;
        private List<Programme> _programmes;

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
        public async Task<List<Building>> GetCollegeBuildings() {
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

        public async Task<List<Designations>> GetDesignations() {
            if (_designations != null) return _designations;
            var viewDesignation = await ViewApi.ViewDesignation(InstitutionId);
            return _designations ??= viewDesignation.designations;
        }

        public void ResetDesignations() {
            _designations = null;
        }

        public async Task<List<Deanery>> GetDeaneries() {
            return _deanery ??= await DeaneryApi.Get(InstitutionId);
        }

        public void ResetDeaneries() {
            _deanery = null;
        }

        public Deanery GetDeaneryById(string deaneryId) {
            return deaneryId == null ? new Deanery() : _deanery.Find(match: deanery => deanery.id == deaneryId);
        }

        public async Task<List<Department>> GetDepartments() {
            return _departments ??= await DepartmentApi.Get(InstitutionId);
        }

        public void ResetDepartments() {
            _departments = null;
        }

        public async Task<List<Course>> GetCourses() {
            return _courses ??= await CourseApi.Get(InstitutionId);
        }

        public void ResetCourses() {
            _courses = null;
        }

        public async Task<List<Programme>> GetProgrammes() {
            return _programmes ??= await ProgrammeApi.Get(InstitutionId);
        }

        public void ResetProgrammes() {
            _programmes = null;
        }
    }
}