using System.Collections.Generic;

namespace FWASWebApp.ResponseModels.Manager {
    public class SelectedCourse {
        public string userId { get; set; }
        #nullable enable
        public List<string>? selectedCourseIds { get; set; }
    }
}