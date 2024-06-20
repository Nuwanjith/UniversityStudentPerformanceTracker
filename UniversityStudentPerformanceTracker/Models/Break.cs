namespace UniversityStudentPerformanceTracker.Models
{
    public class Break
    {
        public int BreakId { get; set; }
        public int UserId { get; set; }
        public int Duration { get; set; } // Duration in minutes
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
    }
}
