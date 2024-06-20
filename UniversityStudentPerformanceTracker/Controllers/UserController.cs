using Microsoft.AspNetCore.Mvc;
using UniversityStudentPerformanceTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace UniversityStudentPerformanceTracker.Controllers
{
    public class UserController : Controller
    {
        private static List<User> users = new List<User>();

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            users.Add(user);
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var existingUser = users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (existingUser != null)
            {
                // Redirect to user dashboard or home page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }
    }
}
