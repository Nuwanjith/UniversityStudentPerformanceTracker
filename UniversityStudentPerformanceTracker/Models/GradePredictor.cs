namespace UniversityStudentPerformanceTracker.Models
{
    public class GradePredictor
    {
        public double PredictedGrade { get; set; }
        public int FutureStudyHours { get; set; }

        // Method to calculate predicted grade based on future study hours
        public double CalculatePredictedGrade(string subject, int futureStudyHours)
        {
            // Example logic for grade prediction
            // Adjust this logic based on your specific prediction model

            // Base grade for each subject, you may fetch or calculate this from a database or configuration
            double baseGrade = GetBaseGradeForSubject(subject);

            // Simple linear model: each hour of study improves the grade by a fixed amount
            double improvementPerHour = 1.5; // Example value: 1.5 points per hour
            double predictedGrade = baseGrade + (futureStudyHours * improvementPerHour);

            // Ensure grade is within a reasonable range (e.g., 0 to 100)
            predictedGrade = Math.Clamp(predictedGrade, 0, 100);

            return predictedGrade;
        }

        // Example method to get a base grade for a subject (could be replaced with a more complex logic or database call)
        private double GetBaseGradeForSubject(string subject)
        {
            // Simple example base grades
            return subject switch
            {
                "Math" => 70,
                "Science" => 65,
                "History" => 60,
                "Literature" => 75,
                _ => 50 // Default base grade
            };
        }
    }
}
