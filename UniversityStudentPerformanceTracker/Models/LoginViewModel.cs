namespace UniversityStudentPerformanceTracker.Models;
public class LoginViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }

    public LoginViewModel()
    {
        Username = string.Empty; // or provide a default value
        Password = string.Empty; // or provide a default value
    }
}
