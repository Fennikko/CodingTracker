using System.Globalization;

public class UserInput
{

    public void GetUserInput()
    {
        Console.Clear();
        var appRunning = true;
        do
        {
            Console.WriteLine("""
                              MAIN MENU
                              ----------------------------------
                              Coding Tracker
                              
                              ----------------------------------
                              
                              0 - Close application
                              1 - View all coding sessions
                              2 - Start coding session
                              3 - End coding session
                              """);
            Console.WriteLine("----------------------------------");

            var command = Console.ReadLine()?.Trim();

            switch (command)
            {
                case "0":
                    Console.WriteLine("Thank you for using the coding tracker!");
                    Thread.Sleep(3000);
                    break;
                //case "1":
                //    GetAllRecords();
                //    break;
                //case "2":
                //    Session();
                //    break;
                default:
                    Console.WriteLine("Invalid command, please select a command from the menu");
                    Thread.Sleep(3000);
                    Console.Clear();
                    break;
            }
        } while (appRunning);
    }

    public string? GetDateInput()
    {
        Console.WriteLine("Please insert the date and time: (Format dd-mm-yy HH:mm) Type 0 to return to the main menu");
        var dateInput = Console.ReadLine();
        if (dateInput == "0") GetUserInput();

        while (!DateTime.TryParseExact(dateInput, "dd-MM-yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            Console.WriteLine("Invalid Date. (Format dd-mm-yy HH:mm) Type 0 to return to the main menu");
            dateInput = Console.ReadLine();
        }

        return dateInput;
    }
}