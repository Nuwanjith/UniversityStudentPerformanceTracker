using Microsoft.AspNetCore.Mvc;

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

            double predictedGrade = _gradePredictor.PredictedGrade;
            int futureStudyHours = _gradePredictor.FutureStudyHours;

 
            return View();
        }
    }
}
