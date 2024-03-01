using System.Configuration;
using System.Globalization;
using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace CodingTracker;

public class CodingController
{
    private static readonly string ConnectionString = ConfigurationManager.AppSettings.Get("connectionString") + 
                                                      ConfigurationManager.AppSettings.Get("databasePath");


    public static void DatabaseCreation()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Execute(
            """
            CREATE TABLE IF NOT EXISTS coding_tracker (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StartTime TEXT,
                    EndTime TEXT,
                    Duration TEXT
                    )
            """);
    }

    public static void Session()
    {
        AnsiConsole.Clear();
        var startTime = Validation.GetDateInput("Please insert the date and time for your start time:", "(Format dd-mm-yy HH:mm)", "Type 0 to return to the main menu");
        var endTime = Validation.GetDateInput("Please insert the date and time for your end time:", "(Format dd-mm-yy HH:mm)", "Type 0 to return to the main menu");
        var startDateTime = DateTime.ParseExact(startTime, "dd-MM-yy HH:mm", new CultureInfo("en-US"));
        var endDateTime = DateTime.ParseExact(endTime, "dd-MM-yy HH:mm", new CultureInfo("en-US"));
        while (startDateTime >= endDateTime)
        {
            startTime = Validation.GetDateInput("Start time is after end time, please enter start time:", "(Format dd-mm-yy HH:mm)", "Type 0 to return to the main menu");
            startDateTime = DateTime.ParseExact(startTime, "dd-MM-yy HH:mm", new CultureInfo("en-US"));
        }
        using var connection = new SqliteConnection(ConnectionString);
        var command = "INSERT INTO coding_tracker(StartTime, EndTime, Duration) VALUES(@StartTime, @EndTime, @Duration)";
        var startSession = new CodingSession{ StartTime = startTime, EndTime = endTime };
        Console.WriteLine(startSession.Duration);
        var rowsAffected = connection.Execute(command, startSession);
        Console.WriteLine($"{rowsAffected} row(s) inserted.");
        
    }

    public static void GetAllSessions()
    {
        AnsiConsole.Clear();
        var sql = "SELECT * FROM coding_tracker";
        using var connection = new SqliteConnection(ConnectionString);
        var codingSessions = connection.Query<CodingSession>(sql);

        foreach (var session in codingSessions)
        {
            Console.WriteLine($"ID: {session.Id} Start Time: {session.StartTime} End Time: {session.EndTime} Duration: {session.Duration}");
        }
    }

    public static void TableTest()
    {
        AnsiConsole.Clear();
        var table = new Table();


        table.Title(new TableTitle("[blue]Coding Sessions[/]"));

        table.AddColumn(new TableColumn("[#FFA500]Id[/]").Centered());
        table.AddColumn(new TableColumn("[#104E1D]Start Time[/]").Centered());
        table.AddColumn(new TableColumn("[red]End Time[/]").Centered());
        table.AddColumn(new TableColumn("[#8F00FF]Duration[/]").Centered());

        AnsiConsole.Write(table);
    }



}