using Microsoft.AspNetCore.Mvc;
using UniversityStudentPerformanceTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace UniversityStudentPerformanceTracker.Controllers
{
    public class BreakController : Controller
    {
        public static List<Break> Breaks = new List<Break>(); // Changed to public static for accessibility
    public IActionResult Index()
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

        var breaks = user.Breaks;

        return View(breaks);
    }

        public IActionResult Create()
        {
            return View();
        }

[HttpPost]
public IActionResult CreateBreak(Break breakSession)
{
    // Example initialization of Breaks if not already done
    if (Breaks == null)
    {
        Breaks = new List<Break>();
    }

    // Assign a new BreakId
    breakSession.BreakId = Breaks.Count > 0 ? Breaks.Max(b => b.BreakId) + 1 : 1;

    // Add the new Break to the collection
    Breaks.Add(breakSession);

    // Redirect to the Index action method of the current controller
    return RedirectToAction("Index");
}

        public IActionResult Edit(int id)
        {
            var breakSession = Breaks.FirstOrDefault(b => b.BreakId == id);
            return View(breakSession);
        }

        [HttpPost]
        public IActionResult Edit(Break breakSession)
        {
            var existingBreak = Breaks.FirstOrDefault(b => b.BreakId == breakSession.BreakId);
            if (existingBreak != null)
            {
                existingBreak.Duration = breakSession.Duration;
                existingBreak.StartTime = breakSession.StartTime;
                existingBreak.EndTime = breakSession.EndTime;
                existingBreak.Date = breakSession.Date;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var breakSession = Breaks.FirstOrDefault(b => b.BreakId == id);
            return View(breakSession);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var breakSession = Breaks.FirstOrDefault(b => b.BreakId == id);
            if (breakSession != null)
            {
                Breaks.Remove(breakSession);
            }
            return RedirectToAction("Index");
        }
    }
}
