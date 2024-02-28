using System.Configuration;
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

    public void StartSession()
    {
        var userInput = new UserInput();
        var startTime = userInput.GetDateInput();
        var endTime = userInput.GetDateInput();
        using var connection = new SqliteConnection(ConnectionString);
        var startSession = connection.Execute($"INSERT INTO coding_tracker(StartTime, EndTime) VALUES('{startTime}','{endTime}')");
        Console.WriteLine($"Affected Rows: {startSession}");
    }

    public void GetRecords()
    {
        using var connection = new SqliteConnection(ConnectionString);
        var reader = connection.ExecuteReader("SELECT * FROM coding_tracker");

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string startTime = reader.GetString(1);
            string endTime = reader.GetString(2);

            Console.WriteLine($"ID: {id} Start Time: {startTime} End Time: {endTime}");
        }
    }

}