using System.Collections.Generic;

namespace TGenWebApp.ResponseModels.Manager {
    public class Deanery {
        #nullable enable
        public string? id { get; set; }
        #nullable disable

        public string deaneryName { get; set; }
        public string deaneryCode { get; set; }

        public string roomId { get; set; }
        public bool IsUG { get; set; }
        public bool IsPG { get; set; }

        public List<string> accessDesignation { get; set; }
    }
}