using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UniversityStudentPerformanceTracker.Models;

namespace UniversityStudentPerformanceTracker.Controllers
{
    public class ReportController : Controller
    {
        private static List<StudySession> sessions = StudySessionController.sessions;
        private static List<Break> breaks = BreakController.breaks;

        public IActionResult Generate(DateTime weekStartDate)
        {
            var weekEndDate = weekStartDate.AddDays(7);
            var weeklySessions = sessions.Where(s => s.Date >= weekStartDate && s.Date <= weekEndDate).ToList();
            var weeklyBreaks = breaks.Where(b => b.Date >= weekStartDate && b.Date <= weekEndDate).ToList();

            var reportViewModel = new ReportViewModel
            {
                WeekStartDate = weekStartDate,
                WeekEndDate = weekEndDate,
                StudySessions = weeklySessions,
                Breaks = weeklyBreaks
            };

            return View(reportViewModel);
        }
    }
}
