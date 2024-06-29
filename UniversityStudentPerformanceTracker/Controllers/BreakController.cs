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
            return View(Breaks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Break breakSession)
        {
            breakSession.BreakId = Breaks.Count > 0 ? Breaks.Max(b => b.BreakId) + 1 : 1; // Assign a new BreakId
            Breaks.Add(breakSession);
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
