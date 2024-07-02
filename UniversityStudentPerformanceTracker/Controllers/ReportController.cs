using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using UniversityStudentPerformanceTracker.Models;

namespace UniversityStudentPerformanceTracker.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Generate(DateTime weekStartDate)
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

            var weekEndDate = weekStartDate.AddDays(7);
            var weeklySessions = user.StudySessions.Where(s => s.Date >= weekStartDate && s.Date <= weekEndDate).ToList();
            var weeklyBreaks = user.Breaks.Where(b => b.StartTime >= weekStartDate && b.EndTime <= weekEndDate).ToList();

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
