namespace TGenWebApp.Drivers {
    public class Session {
        // For easy access to navbar name
        public string Name { get; set; }
        
        // In InitialSetup?
        public bool IsInitialSetup { get; set; }
        
        // Authentication level defines how many things a user can access
        public AuthLevel Auth { get; set; }
        
        // InstitutionID lets quick access to institution data
        public string InstitutionId { get; set; }
        
        // InstitutionName is for cosmetic purpose. Can be omitted
        public string InstitutionName { get; set; }
    }

    public enum AuthLevel {
        Faculty,
        DeptCoord,
        Head,
        DeanCoord
    }
}