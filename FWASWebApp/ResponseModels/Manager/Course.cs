namespace TGenWebApp.ResponseModels.Manager {
    public class Course {
        #nullable enable
        public string? Id { get; set; }
        public string? courseName { get; set; }
        public string? courseCode { get; set; }
        public bool? IsTheory { get; set; }
        public bool? IsPractical { get; set; }
        public bool? mandatoryLab { get; set; }
        public int? totalHours { get; set; }
        public int? hoursPerWeek { get; set; }
        public string? programmeId { get; set; }
        public string? departmentId { get; set; }
    }
}