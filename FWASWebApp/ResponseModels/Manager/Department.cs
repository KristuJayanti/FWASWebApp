using System.Collections.Generic;

namespace TGenWebApp.ResponseModels.Manager {
    public class Department {
        public string id { get; set; }

        public string deaneryId { get; set; }

        public string departmentName { get; set; }
        public string departmentCode { get; set; }
        public string roomId { get; set; }

        public bool IsUG { get; set; }
        public bool IsPG { get; set; }

        public List<string> accessDesignation { get; set; }
    }
}