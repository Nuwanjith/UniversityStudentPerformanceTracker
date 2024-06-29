using System;
using System.Collections.Generic;
using UniversityStudentPerformanceTracker.Models;

public static class InMemoryDatabase
{
    public static List<User> Users = new List<User>();

    static InMemoryDatabase()
    {
        InitializeData();
    }

    private static void InitializeData()
    {
        // Users initialization
        var johnDoe = new User { UserId = 1, Username = "john.doe", Password = "password123" };
        var janeDoe = new User { UserId = 2, Username = "jane.doe", Password = "password456" };

        // Study sessions for John Doe
        johnDoe.StudySessions = new List<StudySession>
        {
            new StudySession { SessionId = 1, UserId = 1, Subject = "Math", Duration = 60, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), Date = DateTime.Today },
            new StudySession { SessionId = 2, UserId = 1, Subject = "Science", Duration = 45, StartTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now.AddDays(-1).AddMinutes(45), Date = DateTime.Today.AddDays(-1) },
            // Add more study sessions for John Doe as needed
        };

        // Study sessions for Jane Doe
        janeDoe.StudySessions = new List<StudySession>
        {
            new StudySession { SessionId = 3, UserId = 2, Subject = "History", Duration = 30, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(30), Date = DateTime.Today },
            new StudySession { SessionId = 4, UserId = 2, Subject = "Literature", Duration = 75, StartTime = DateTime.Now.AddDays(-2), EndTime = DateTime.Now.AddDays(-2).AddHours(1).AddMinutes(15), Date = DateTime.Today.AddDays(-2) },
            // Add more study sessions for Jane Doe as needed
        };

        var johnDoeBreaks = new List<Break>
        {
            new Break { BreakId = 1, UserId = 1, Description = "Short break", StartTime = DateTime.Now.AddHours(-1), EndTime = DateTime.Now.AddHours(-1).AddMinutes(10) },
            new Break { BreakId = 2, UserId = 1, Description = "Lunch break", StartTime = DateTime.Now.AddDays(-1).AddHours(12), EndTime = DateTime.Now.AddDays(-1).AddHours(13) }
            // Add more breaks for John Doe as needed
        };
        johnDoe.Breaks = johnDoeBreaks;
        
        var janeDoeBreaks = new List<Break>
        {
            new Break { BreakId = 3, UserId = 2, Description = "Coffee break", StartTime = DateTime.Now.AddHours(-2), EndTime = DateTime.Now.AddHours(-2).AddMinutes(15) },
            new Break { BreakId = 4, UserId = 2, Description = "Study group break", StartTime = DateTime.Now.AddDays(-2).AddHours(15), EndTime = DateTime.Now.AddDays(-2).AddHours(16) }
            // Add more breaks for Jane Doe as needed
        };
        


        // Add users to the database
        Users.Add(johnDoe);
        Users.Add(janeDoe);
    }
}
