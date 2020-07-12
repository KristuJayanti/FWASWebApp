namespace FWASWebApp.ResponseModels.Manager {
    public class CollegeInfrastructureRoomResponseModel {
        public string id { get; set; }
        public string buildingId { get; set; }
        public string buildingCode { get; set; }
        public int floorNumber { get; set; }
        public string roomCode { get; set; }
        public bool IsLab { get; set; }
        public string labType { get; set; }
        public string roomType { get; set; }
        public int maxStrength { get; set; }
        public bool IsUG { get; set; }
        public bool IsPG { get; set; }
    }
}