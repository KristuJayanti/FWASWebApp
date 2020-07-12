namespace FWASWebApp.ResponseModels.Manager {
    public class Programme {
        #nullable enable
        public string? Id { get; set; }
        public string? programmeName { get; set; }
        public string? programmeCode { get; set; }
        public int? semesterNumber { get; set; }
        public bool? IsUG { get; set; }
        public int? workingHoursPerDay { get; set; }
        public int? workingHoursPerSaturday { get; set; }
        public int? workingHoursPerWeek { get; set; }
        public int? totalWorkingHours { get; set; }
        public int? studentStrength { get; set; }
    }
}