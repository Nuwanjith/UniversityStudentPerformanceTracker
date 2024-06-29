using Microsoft.AspNetCore.Mvc;
using UniversityStudentPerformanceTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace UniversityStudentPerformanceTracker.Controllers
{
    public class StudySessionController : Controller
    {
        public static List<StudySession> Sessions = new List<StudySession>(); // Changed to public static for accessibility

        public IActionResult Index()
        {
            return View(Sessions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudySession session)
        {
            session.SessionId = Sessions.Count > 0 ? Sessions.Max(s => s.SessionId) + 1 : 1; // Assign a new SessionId
            Sessions.Add(session);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var session = Sessions.FirstOrDefault(s => s.SessionId == id);
            return View(session);
        }

        [HttpPost]
        public IActionResult Edit(StudySession session)
        {
            var existingSession = Sessions.FirstOrDefault(s => s.SessionId == session.SessionId);
            if (existingSession != null)
            {
                existingSession.Subject = session.Subject;
                existingSession.Duration = session.Duration;
                existingSession.StartTime = session.StartTime;
                existingSession.EndTime = session.EndTime;
                existingSession.Date = session.Date;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var session = Sessions.FirstOrDefault(s => s.SessionId == id);
            return View(session);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var session = Sessions.FirstOrDefault(s => s.SessionId == id);
            if (session != null)
            {
                Sessions.Remove(session);
            }
            return RedirectToAction("Index");
        }
    }
}
