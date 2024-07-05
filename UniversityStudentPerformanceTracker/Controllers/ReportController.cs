using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using UniversityStudentPerformanceTracker.Models;

namespace UniversityStudentPerformanceTracker.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Generate(DateTime startDate, DateTime endDate)
        {
            // Retrieve the authenticated user's ID from the claims
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
            {
                // Handle the case where the UserId claim is not found
                return Unauthorized();
            }

            int userId = int.Parse(userIdClaim.Value);
            var user = InMemoryDatabase.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                // Handle the case where the user is not found in the in-memory database
                return NotFound();
            }

            // Ensure sessions and breaks are not null
            var filteredSessions = user.StudySessions?
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .ToList() ?? new List<StudySession>();

            var filteredBreaks = user.Breaks?
                .Where(b => b.StartTime >= startDate && b.EndTime <= endDate)
                .ToList() ?? new List<Break>();

            var reportViewModel = new ReportViewModel
            {
                WeekStartDate = startDate,
                WeekEndDate = endDate,
                StudySessions = filteredSessions,
                Breaks = filteredBreaks
            };

            return View(reportViewModel);
        }
    }
}
