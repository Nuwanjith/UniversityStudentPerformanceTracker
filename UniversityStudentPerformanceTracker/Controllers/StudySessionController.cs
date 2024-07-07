using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using UniversityStudentPerformanceTracker.Models;

namespace UniversityStudentPerformanceTracker.Controllers
{
    [Authorize]
    public class StudySessionController : Controller
    {   
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

            var sessions = user.StudySessions;

            return View(sessions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudySession session)
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

            if (user != null)
            {
                session.SessionId = user.StudySessions.Count > 0 ? user.StudySessions.Max(s => s.SessionId) + 1 : 1;
                session.UserId = userId;
                user.StudySessions.Add(session);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
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
                return NotFound();
            }

            var session = user.StudySessions.FirstOrDefault(s => s.SessionId == id);
            return View(session);
        }

        [HttpPost]
        public IActionResult Edit(StudySession session)
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
            var existingSession = user?.StudySessions.FirstOrDefault(s => s.SessionId == session.SessionId);

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
                return NotFound();
            }

            var session = user.StudySessions.FirstOrDefault(s => s.SessionId == id);
            return View(session);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
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

            if (user != null)
            {
                var session = user.StudySessions.FirstOrDefault(s => s.SessionId == id);
                if (session != null)
                {
                    user.StudySessions.Remove(session);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
