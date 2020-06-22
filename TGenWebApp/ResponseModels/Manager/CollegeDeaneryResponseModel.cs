using System.Collections.Generic;

namespace TGenWebApp.ResponseModels.Manager {
    public class CollegeDeaneryResponseModel {
        public string id { get; set; }

        public string deaneryName { get; set; }
        public string deaneryCode { get; set; }

        public string roomId { get; set; }
        public bool IsUG { get; set; }
        public bool IsPG { get; set; }

        public List<string> accessDesignation { get; set; }
    }
}