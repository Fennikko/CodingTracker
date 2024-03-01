using Spectre.Console;

namespace CodingTracker;

public class UserInput
{
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
                        "Manual Session", "Get Session History",
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
                //case "Table Test":
                //    CodingController.TableTest();
                //    break;
                case "Exit":
                    appRunning = false;
                    Environment.Exit(0);
                    break;
            }
        } while (appRunning);
    }
}