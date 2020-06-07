namespace TGenWebApp.Services {
    public class Session {
        public string Id { get; set; }
        public string CollegeId { get; set; }
        public string LoginType { get; set; }
        public string Name { get; set; }
        public bool IsInitialSetup { get; set; }
        public LoginLevel LoginLevel { get; set; }
    }

    public enum LoginLevel {
        Institution,
        Faculty,
        Dean,
        Admin
    }
}