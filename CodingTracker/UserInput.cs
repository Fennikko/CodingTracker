using Spectre.Console;

namespace CodingTracker;

public class UserInput
{

    //public static void GetUserInput()
    //{
    //    Console.Clear();
    //    var appRunning = true;
    //    do
    //    {
    //        Console.WriteLine("""
    //                          MAIN MENU
    //                          ----------------------------------
    //                          Coding Tracker

    //                          ----------------------------------

    //                          0 - Close application
    //                          1 - View all coding sessions
    //                          2 - Start coding session
    //                          3 - End coding session
    //                          """);
    //        Console.WriteLine("----------------------------------");

    //        var command = Console.ReadLine()?.Trim();

    //        switch (command)
    //        {
    //            case "0":
    //                Console.WriteLine("Thank you for using the coding tracker!");
    //                Thread.Sleep(3000);
    //                appRunning = false;
    //                break;
    //            case "1":
    //                CodingController.GetAllSessions();
    //                break;
    //            case "2":
    //                CodingController.Session();
    //                break;
    //            default:
    //                Console.WriteLine("Invalid command, please select a command from the menu");
    //                Thread.Sleep(3000);
    //                Console.Clear();
    //                break;
    //        }
    //    } while (appRunning);
    //}

    public static void SpectreGetUserInput()
    {
        AnsiConsole.Clear();
        var appRunning = true;
        do
        {
            var functionSelect = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a [blue]function[/].")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Manual Session", "Get Session History","Table Test",
                        "Exit"
                    }));
            switch (functionSelect)
            {
                case "Manual Session":
                    CodingController.Session();
                    break;
                case "Get Session History":
                    CodingController.GetAllSessions();
                    break;
                case "Table Test":
                    CodingController.TableTest();
                    break;
                case "Exit":
                    appRunning = false;
                    Environment.Exit(0);
                    break;
            }
        } while (appRunning);
    }
}