using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace UniversityStudentPerformanceTracker.Controllers
{
    public class GradePredictionController : Controller
    {
        private readonly UniversityStudentPerformanceTracker.Models.GradePredictor _gradePredictor;

        public GradePredictionController()
        {
            _gradePredictor = new UniversityStudentPerformanceTracker.Models.GradePredictor();
        }

        public IActionResult PredictGrade()
        {
            // Replace this with your logic to retrieve available subjects.
            ViewBag.Subjects = GetAvailableSubjects();

            // If you have specific logic to calculate predicted grade and future study hours,
            // include that here and pass the result to the view if needed.

            return View();
        }

        [HttpPost]
        public IActionResult PredictGrade(string subject, int futureStudyHours)
        {
            // Implement your grade prediction logic using the _gradePredictor and inputs.
            var predictedGrade = _gradePredictor.CalculatePredictedGrade(subject, futureStudyHours);

            // Pass the predicted grade to the view.
            ViewBag.PredictedGrade = predictedGrade;

            // Repopulate ViewBag.Subjects to ensure the form can be displayed correctly after a post.
            ViewBag.Subjects = GetAvailableSubjects();

            return View();
        }

        private List<string> GetAvailableSubjects()
        {
            // Replace this with your logic to retrieve available subjects.
            return new List<string> { "Math", "Science", "History", "Literature" };
        }
    }
}
