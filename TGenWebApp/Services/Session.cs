namespace TGenWebApp.Services {
    public class Session {
        public string Id { get; set; }
        public string CollegeId { get; set; }
        public string CollegeName { get; set; }
        public string Name { get; set; }
        public bool IsInitialSetup { get; set; }
        public UserType UserType { get; set; }
    }

    public enum UserType {
        Institution,
        User
    }
}