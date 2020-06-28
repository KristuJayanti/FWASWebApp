namespace TGenWebApp.Services {
    public class Session {
        public UserType userType { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string InstitutionId { get; set; }
        public string InstitutionName { get; set; }
        public bool IsInitialSetup { get; set; }
    }

    public enum UserType {
        institution,
        user
    }

    public static class Constants {
        public const string SessionId = "sessionId";
        public const string BaseUrl = "https://localhost:5001/";
    }
}