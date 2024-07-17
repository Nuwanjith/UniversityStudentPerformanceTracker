using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UniversityStudentPerformanceTracker.Models;
using UniversityStudentPerformanceTrackerApi.Services;

namespace UniversityStudentPerformanceTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly XMLStorageService _xmlStorageService;
        private static string usersFilePath = "path/to/users.xml"; // Define the path to the XML file

        public UserController(XMLStorageService xmlStorageService)
        {
            _xmlStorageService = xmlStorageService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            var users = _xmlStorageService.LoadData<User>(usersFilePath);
            users.Add(user);
            _xmlStorageService.SaveData(usersFilePath, users);

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var users = _xmlStorageService.LoadData<User>(usersFilePath);
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
