namespace TGenWebApp.Services {
    public class Session {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public bool IsInitialSetup { get; set; }

        public AuthLevel Auth { get; set; }
    }

    public enum AuthLevel {
        Faculty,
        DeptCoord,
        Head,
        DeanCoord
    }
}