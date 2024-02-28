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
        var startTime = userInput.GetDateInput();
        var endTime = userInput.GetDateInput();
        using var connection = new SqliteConnection(ConnectionString);
        var startSession = connection.Execute($"INSERT INTO coding_tracker(StartTime, EndTime) VALUES('{startTime}','{endTime}')");
        Console.WriteLine($"Affected Rows: {startSession}");
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