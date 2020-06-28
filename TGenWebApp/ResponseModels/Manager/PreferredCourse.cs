using System.Collections.Generic;

namespace TGenWebApp.ResponseModels.Manager {
    public class PreferredCourse {
        public string userId { get; set; }
        #nullable enable
        public List<string>? preferredCourseIds { get; set; }
    }
}