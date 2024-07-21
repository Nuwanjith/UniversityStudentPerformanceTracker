using System;
using System.Collections.Generic;
using System.Linq;
using UniversityStudentPerformanceTracker.Models;
using UniversityStudentPerformanceTrackerApi.Services;

public static class InMemoryDatabase
{
    private static readonly XMLStorageService _xmlStorageService = new XMLStorageService();
    private static string usersFilePath = "Data/users.xml";

    public static List<User> Users => _xmlStorageService.LoadData<User>(usersFilePath);

    static InMemoryDatabase()
    {
        InitializeData();
    }

    private static void InitializeData()
    {
        var users = _xmlStorageService.LoadData<User>(usersFilePath);

        if (users.Count == 0)
        {
            // Initial users setup
            var johnDoe = new User { UserId = 1, Username = "john.doe", Password = "password123" };
            var janeDoe = new User { UserId = 2, Username = "jane.doe", Password = "password456" };

            // Study sessions for John Doe
            johnDoe.StudySessions = new List<StudySession>
            {
                new StudySession { SessionId = 1, UserId = 1, Subject = "Math", Duration = 60, StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1), Date = DateTime.Today },
                new StudySession { SessionId = 2, UserId = 1, Subject = "Science", Duration = 45, StartTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now.AddDays(-1).AddMinutes(45), Date = DateTime.Today.AddDays(-1) }
            };

            // Study sessions for Jane Doe
            janeDoe.StudySessions = new List<StudySession>
            {
                new StudySession { SessionId = 3, UserId = 2, Subject = "History", Duration = 30, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(30), Date = DateTime.Today },
                new StudySession { SessionId = 4, UserId = 2, Subject = "Literature", Duration = 75, StartTime = DateTime.Now.AddDays(-2), EndTime = DateTime.Now.AddDays(-2).AddHours(1).AddMinutes(15), Date = DateTime.Today.AddDays(-2) }
            };

            // Breaks for John Doe
            johnDoe.Breaks = new List<Break>
            {
                new Break { BreakId = 1, UserId = 1, Description = "Short break", StartTime = DateTime.Now.AddHours(-1), EndTime = DateTime.Now.AddHours(-1).AddMinutes(10) },
                new Break { BreakId = 2, UserId = 1, Description = "Lunch break", StartTime = DateTime.Now.AddDays(-1).AddHours(12), EndTime = DateTime.Now.AddDays(-1).AddHours(13) }
            };

            // Breaks for Jane Doe
            janeDoe.Breaks = new List<Break>
            {
                new Break { BreakId = 3, UserId = 2, Description = "Coffee break", StartTime = DateTime.Now.AddHours(-2), EndTime = DateTime.Now.AddHours(-2).AddMinutes(15) },
                new Break { BreakId = 4, UserId = 2, Description = "Study group break", StartTime = DateTime.Now.AddDays(-2).AddHours(15), EndTime = DateTime.Now.AddDays(-2).AddHours(16) }
            };

            users.Add(johnDoe);
            users.Add(janeDoe);

            _xmlStorageService.SaveData(usersFilePath, users);
        }
    }

    public static List<User> GetAllUsers()
    {
        return _xmlStorageService.LoadData<User>(usersFilePath);
    }

    public static User GetUserById(int userId)
    {
        var users = GetAllUsers();
        return users.FirstOrDefault(u => u.UserId == userId);
    }

    public static void AddUser(User user)
    {
        var users = GetAllUsers();
        users.Add(user);
        SaveUsers(users);
    }

    public static void UpdateUser(User user)
    {
        var users = GetAllUsers();
        var existingUser = users.FirstOrDefault(u => u.UserId == user.UserId);
        if (existingUser != null)
        {
            users.Remove(existingUser);
            users.Add(user);
            SaveUsers(users);
        }
    }

    public static void DeleteUser(int userId)
    {
        var users = GetAllUsers();
        var user = users.FirstOrDefault(u => u.UserId == userId);
        if (user != null)
        {
            users.Remove(user);
            SaveUsers(users);
        }
    }

    private static void SaveUsers(List<User> users)
    {
        _xmlStorageService.SaveData(usersFilePath, users);
    }
}
