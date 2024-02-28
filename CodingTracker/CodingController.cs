using System.Configuration;
using System.Globalization;
using Dapper;
using Microsoft.Data.Sqlite;

public class CodingController
{
    private static readonly string ConnectionString = ConfigurationManager.AppSettings.Get("connectionString") + 
                                                      ConfigurationManager.AppSettings.Get("databasePath");


    public void DatabaseCreation()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Execute(
            """
            CREATE TABLE IF NOT EXISTS coding_tracker (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StartTime TEXT,
                    EndTime TEXT
                    )
            """);
    }

    public void Session()
    {
        var userInput = new UserInput();
        var startTime = userInput.GetDateInput("Please insert the date and time for your start time: (Format dd-mm-yy HH:mm) Type 0 to return to the main menu");
        var endTime = userInput.GetDateInput("Please insert the date and time for your end time: (Format dd-mm-yy HH:mm) Type 0 to return to the main menu");
        using var connection = new SqliteConnection(ConnectionString);
        var command = "INSERT INTO coding_tracker(StartTime, EndTime) VALUES(@StartTime, @EndTime)";
        var startSession = new { StartTime = startTime, EndTime = endTime };
        var rowsAffected = connection.Execute(command, startSession);
        Console.WriteLine($"{rowsAffected} row(s) inserted.");
        
    }

    public void GetAllRecords()
    {

        using var connection = new SqliteConnection(ConnectionString);
        var sql = "SELECT * FROM coding_tracker";
        var codingSessions = connection.Query<CodingSession>(sql);

        foreach (var session in codingSessions)
        {
            Console.WriteLine($"ID: {session.Id} {session.StartTime} {session.EndTime}");
        }
    }

}