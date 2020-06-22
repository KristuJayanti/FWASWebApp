using System.Collections.Generic;
using TGenWebApp.ResponseModels.Core;

namespace TGenWebApp.ResponseModels.Manager {
    public class Faculty {
        public bool IsSuccess { get; set; }
        public string message { get; set; }

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