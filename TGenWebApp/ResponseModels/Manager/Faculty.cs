using System.Collections.Generic;
using TGenWebApp.ResponseModels.Core;

namespace TGenWebApp.ResponseModels.Manager {
    public class Faculty {
        public string institutionID { get; set; }
        public string emailId { get; set; }
        #nullable enable
        public string? userID { get; set; }

        public string? name { get; set; }
        public string? photo { get; set; }
        
        public bool? passwordReset { get; set; } = false;
        
        public string? password { get; set; }
        public long? phoneNumber { get; set; }
        public string? address { get; set; }
        public int? pincode { get; set; }

        public string? jobId { get; set; }
        public int? MaxHoursPerWeek { get; set; }
        public bool? IsTeaching { get; set; }
        public List<string>? designationIds { get; set; }
        public string? departmentId { get; set; }
    }
}