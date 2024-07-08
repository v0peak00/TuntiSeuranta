using System.Globalization;

public class InputHandler
{
    private readonly IFileHandler _fileHandler;

    public InputHandler(IFileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

    public DateTime GetDateInputFromUser()
    {
        Console.Write("Syötä PVM (pp.kk.vvvv): ");
        string dateInput = Console.ReadLine();
        DateTime date;

        while (!DateTime.TryParseExact(dateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            Console.Write("Virheellinen PVM muoto. Syötä PVM (pp.kk.vvvv): ");
            dateInput = Console.ReadLine();
        }

        return date;
    }

    public TimeSpan GetTimeInputFromUser(string prompt)
    {
        Console.Write(prompt);
        string timeInput = Console.ReadLine();
        TimeSpan time;
        
        while (!TimeSpan.TryParse(timeInput, out time))
        {
            Console.Write("Virheellinen ajan muoto. " + prompt);
            timeInput = Console.ReadLine();
        }
        return time;
    }

    public void PromptUserToModifyHoursForDate(DateTime date)
    {
        Console.WriteLine($"Olemassa olevia tunteja löytyi PVM: {date:dd.MM.yyyy}");
        _fileHandler.DisplayHoursForDate();
        Console.Write("Haluatko muokata näitä tunteja Y/N? ");
        string decision = Console.ReadLine().ToUpper();

        while (decision != "Y" && decision != "N")
        {
            Console.Write("Virheellinen valinta. Syötä 'Y' muokataksesi tunteja tai 'N' palataksesi takaisin: ");
            decision = Console.ReadLine().ToUpper();
        }

        if (decision == "Y")
        {
            TimeSpan startTime = GetTimeInputFromUser("Töiden aloitus (tt:mm): ");
            TimeSpan endTime = GetTimeInputFromUser("Töiden lopetus (HH:mm): ");
            _fileHandler.ModifyHoursForDate(startTime, endTime);
        }
        else if(decision == "N")
        {
            Console.WriteLine("Palataan työaikamerkintä näkymään.");
            return;
        }
    }
}
