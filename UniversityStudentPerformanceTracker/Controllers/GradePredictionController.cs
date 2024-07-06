using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UniversityStudentPerformanceTracker.Models;

namespace UniversityStudentPerformanceTracker.Controllers
{
    [Authorize]
    public class GradePredictionController : Controller
    {
        private readonly GradePredictor _gradePredictor;

        public GradePredictionController(GradePredictor gradePredictor)
        {
            _gradePredictor = gradePredictor;
        }

        [HttpGet]
        public IActionResult PredictGrade()
        {
            return View(new GradePredictor()); // Use GradePredictionViewModel as the model for the view
        }

        [HttpPost]
        public IActionResult PredictGrade(GradePredictor model)
        {
            if (ModelState.IsValid)
            {
                // Perform grade prediction using GradePredictor
                model.PredictedGrade = _gradePredictor.PredictGrade(model.FutureStudyHours);
                ViewBag.PredictedGrade = model.PredictedGrade;
            }

            return View(model);
        }
    }
}
