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
        // Add some initial users
        Users.Add(new User { UserId = 1, Username = "john.doe", Password = "password123" });
        Users.Add(new User { UserId = 2, Username = "jane.doe", Password = "password456" });
    }
}
