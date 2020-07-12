using System.Collections.Generic;

namespace FWASWebApp.ResponseModels.View {
    public class ViewDesignationResponseModel {
        public List<Designations> designations { get; set; } = new List<Designations>();
    }

    public class Designations {
        public string title { get; set; }
        public List<AccessCode> codes { get; set; } = new List<AccessCode>();
        public string id { get; set; }
    }

    public class AccessCode {
        public int code { get; set; }
        public string name { get; set; }
    }
}