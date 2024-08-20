using System.Globalization;
using TuntiSeuranta.WorkHoursManagement;

namespace TuntiSeuranta.InputHandling;

public class InputHandler
{
    private readonly WorkHoursService _workHoursService;
    private readonly WorkHoursDisplay _workHoursDisplay;

    public InputHandler(WorkHoursService workHoursService, WorkHoursDisplay workHoursDisplay)
    {
        _workHoursService = workHoursService ?? throw new ArgumentNullException(nameof(workHoursService));
        _workHoursDisplay = workHoursDisplay ?? throw new ArgumentNullException(nameof(workHoursDisplay));
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

    public bool HandleDateInput(DateTime date)
    {
        if (_workHoursService.WorkHoursExistsForDate(date))
        {
            return HandleExistingWorkHours(date);
        }
        else
        {
            return HandleNewWorkHours(date);
        }
    }

    private bool HandleExistingWorkHours(DateTime date)
    {
        Console.WriteLine($"Tunteja löytyi PVM: {date:dd.MM.yyyy}");
        _workHoursDisplay.DisplayHoursForDate(date);

        Console.Write("Haluatko muokata näitä tunteja? Y/N tai 'q' palataksesi päävalikkoon: ");
        string decision = Console.ReadLine().ToUpper();

        while (decision != "Y" && decision != "N" && decision != "Q")
        {
            Console.Write("Virheellinen valinta. Syötä 'Y' muokataksesi tunteja, 'N' palataksesi takaisin, tai 'q' palataksesi päävalikkoon: ");
            decision = Console.ReadLine().ToUpper();
        }

        if (decision == "Y")
        {
            return ModifyWorkHours(date);
        }
        else if (decision == "Q")
        {
            return false; // Exit to menu
        }

        return true; // Continue to allow user input
    }

    private bool HandleNewWorkHours(DateTime date)
    {
        Console.WriteLine($"Ei löytynyt työaikamerkintöjä päivämäärälle: {date:dd.MM.yyyy}");

        if (!TryGetTimeInput("Töiden aloitus (tt:mm): ", out TimeSpan startTime)) return false;
        if (!TryGetTimeInput("Töiden lopetus (tt:mm): ", out TimeSpan endTime)) return false;

        _workHoursService.AddNewWorkHours(date, startTime, endTime);
        return true;
    }

    private bool ModifyWorkHours(DateTime date)
    {
        if (!TryGetTimeInput("Töiden aloitus (tt:mm): ", out TimeSpan startTime)) return false;
        if (!TryGetTimeInput("Töiden lopetus (tt:mm): ", out TimeSpan endTime)) return false;

        _workHoursService.ModifyWorkHours(date, startTime, endTime);
        return true;
    }
}
