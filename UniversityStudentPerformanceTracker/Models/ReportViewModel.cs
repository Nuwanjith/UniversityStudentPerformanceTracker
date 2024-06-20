using System;
using System.Collections.Generic;

namespace UniversityStudentPerformanceTracker.Models
{
    public class ReportViewModel
    {
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
        public List<StudySession> StudySessions { get; set; } = new List<StudySession>(); // Initialize with default value
        public List<Break> Breaks { get; set; } = new List<Break>(); // Initialize with default value
    }
}
