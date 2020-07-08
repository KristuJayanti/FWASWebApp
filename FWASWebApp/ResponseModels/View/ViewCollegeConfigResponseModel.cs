using System.Collections.Generic;

namespace TGenWebApp.ResponseModels.View {
    public class ViewCollegeConfigResponseModel
    {
        public bool IsUG { get; set; }
        public bool IsPG { get; set; }
        public List<Designations> designations { get; set; } = new List<Designations>();
        public List<AccessCode> accessCodes { get; set; } = new List<AccessCode>();
    }
}