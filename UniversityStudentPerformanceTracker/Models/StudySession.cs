namespace UniversityStudentPerformanceTracker.Models
{
    public class StudySession
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public string Subject { get; set; } = string.Empty; // Initialize with default value
        public int Duration { get; set; } // Duration in minutes
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
    }
}
