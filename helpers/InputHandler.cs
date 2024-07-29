using System.Globalization;

public class InputHandler
{
    private readonly IFileHandler _fileHandler;

    public InputHandler(IFileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

    private bool GetInputWithExitOption(string prompt, Func<string, bool> tryParse, out string result)
    {
        Console.Write(prompt + " tai 'q' palataksesi päävalikkoon: ");
        result = Console.ReadLine();

        if (result.Trim().ToLower() == "q")
        {
            return false;
        }

        while (!tryParse(result))
        {
            Console.Write("Virheellinen muoto. " + prompt + " tai 'q' palataksesi päävalikkoon: ");
            result = Console.ReadLine();

            if (result.Trim().ToLower() == "q")
            {
                return false;
            }
        }

        return true;
    }

    public bool TryGetDateInput(out DateTime date)
    {
        DateTime tempDate = default;
        bool success = GetInputWithExitOption("Syötä PVM (pp.kk.vvvv)", input =>
        {
            bool parseSuccess = DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tempDate);
            return parseSuccess;
        }, out string _);

        date = success ? tempDate : default;
        return success;
    }

    public bool TryGetTimeInput(string prompt, out TimeSpan time)
    {
        TimeSpan tempTime = default;
        bool success = GetInputWithExitOption(prompt, input =>
        {
            bool parseSuccess = TimeSpan.TryParse(input, out tempTime);
            return parseSuccess;
        }, out string _);

        time = success ? tempTime : default;
        return success;
    }

    public bool PromptUserToModifyHoursForDate(DateTime date)
    {
        Console.WriteLine($"Olemassa olevia tunteja löytyi PVM: {date:dd.MM.yyyy}");
        _fileHandler.DisplayHoursForDate();
        Console.Write("Haluatko muokata näitä tunteja Y/N tai 'q' palataksesi päävalikkoon? ");
        string decision = Console.ReadLine().ToUpper();

        while (decision != "Y" && decision != "N" && decision != "Q")
        {
            Console.Write("Virheellinen valinta. Syötä 'Y' muokataksesi tunteja, 'N' palataksesi takaisin, tai 'q' palataksesi päävalikkoon: ");
            decision = Console.ReadLine().ToUpper();
        }

        if (decision == "Y")
        {
            if (!TryGetTimeInput("Töiden aloitus (tt:mm): ", out TimeSpan startTime)) return false;
            if (!TryGetTimeInput("Töiden lopetus (tt:mm): ", out TimeSpan endTime)) return false;
            _fileHandler.ModifyHoursForDate(startTime, endTime);
            return true; // Continue modifying hours
        }
        else if (decision == "N")
        {
            Console.WriteLine("Palataan työaikamerkintä näkymään.");
            return true; // Continue to modify or add hours
        }
        else if (decision == "Q")
        {
            return false; // Exit to menu
        }

        return true;
    }
}
