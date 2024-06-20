using Microsoft.AspNetCore.Mvc;
using UniversityStudentPerformanceTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace UniversityStudentPerformanceTracker.Controllers
{
    public class StudySessionController : Controller
    {
        public static List<StudySession> sessions = new List<StudySession>(); // Change to public

        public IActionResult Index()
        {
            return View(sessions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudySession session)
        {
            sessions.Add(session);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var session = sessions.FirstOrDefault(s => s.SessionId == id);
            return View(session);
        }

        [HttpPost]
        public IActionResult Edit(StudySession session)
        {
            var existingSession = sessions.FirstOrDefault(s => s.SessionId == session.SessionId);
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
            var session = sessions.FirstOrDefault(s => s.SessionId == id);
            return View(session);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var session = sessions.FirstOrDefault(s => s.SessionId == id);
            if (session != null)
            {
                sessions.Remove(session);
            }
            return RedirectToAction("Index");
        }
    }
}
