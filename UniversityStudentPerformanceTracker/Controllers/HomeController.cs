using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityStudentPerformanceTracker.Models;
using System.Linq; // Include Linq for .FirstOrDefault()

namespace UniversityStudentPerformanceTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = InMemoryDatabase.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username)
                        // Add more claims as needed
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        // Customize properties if needed
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Dashboard", "Home"); // Redirect to dashboard after login
                }

                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }

            // If we got this far, something failed; redisplay form with errors
            return View(model);
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            // Ensure user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login"); // Redirect to login page if not authenticated
            }

            // Retrieve user details from authentication context
            var username = User.Identity.Name;
            var user = InMemoryDatabase.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return NotFound(); // Handle case where user is not found
            }

            return View(user); // Pass user model to the Dashboard view
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
