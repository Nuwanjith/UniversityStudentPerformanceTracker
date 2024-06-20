using Microsoft.AspNetCore.Mvc;
using UniversityStudentPerformanceTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace UniversityStudentPerformanceTracker.Controllers
{
    public class BreakController : Controller
    {
        public static List<Break> breaks = new List<Break>(); // Change to public

        public IActionResult Index()
        {
            return View(breaks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Break breakSession)
        {
            breaks.Add(breakSession);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var breakSession = breaks.FirstOrDefault(b => b.BreakId == id);
            return View(breakSession);
        }

        [HttpPost]
        public IActionResult Edit(Break breakSession)
        {
            var existingBreak = breaks.FirstOrDefault(b => b.BreakId == breakSession.BreakId);
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
            var breakSession = breaks.FirstOrDefault(b => b.BreakId == id);
            return View(breakSession);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var breakSession = breaks.FirstOrDefault(b => b.BreakId == id);
            if (breakSession != null)
            {
                breaks.Remove(breakSession);
            }
            return RedirectToAction("Index");
        }
    }
}
