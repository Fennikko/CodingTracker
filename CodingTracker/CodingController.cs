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

    public List<CodingSession> SessionRecords()
    {
        using var connection = new SqliteConnection(ConnectionString);
        var reader = connection.ExecuteReader("SELECT * FROM coding_tracker");
        List<CodingSession> tableData = new();
        while (reader.Read())
        {
            tableData.Add(
                new CodingSession
                {
                    Id = reader.GetInt32(0),
                    StartTime = DateTime.ParseExact(reader.GetString(1),"dd-MM-yy HH:mm", new CultureInfo("en-US")),
                    EndTime = DateTime.ParseExact(reader.GetString(2),"dd-MM-yy HH:mm", new CultureInfo("en-US"))
                });
        }
        return tableData;
    }

    public void GetAllRecords()
    {
        var tableData = SessionRecords();
        foreach (var session in tableData)
        {
            Console.WriteLine($"Id: {session.Id} Start Time: {session.StartTime:dd-MMM-yyyy HH:mm} End Time: {session.EndTime:dd-MMM-yyyy HH:mm}");
        }
    }

}