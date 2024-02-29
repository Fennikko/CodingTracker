using System.Globalization;
using Spectre.Console;

namespace CodingTracker;

public class Validation
{
    public static string GetDateInput(string message1, string message2, string message3)
    {
        //Console.WriteLine(message);
        //var dateInput = Console.ReadLine();
        var dateInput = AnsiConsole.Ask<string>($"{message1}[green]{message2}[/]{message3}");
        if (dateInput == "0") UserInput.SpectreGetUserInput();

        while (!DateTime.TryParseExact(dateInput, "dd-MM-yy HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            Console.WriteLine("Invalid Date. (Format dd-mm-yy HH:mm) Type 0 to return to the main menu");
            dateInput = Console.ReadLine();
        }

        return dateInput;
    }

}