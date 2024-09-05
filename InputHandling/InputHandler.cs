using TuntiSeuranta.ConsoleHandling;
using TuntiSeuranta.InputHandling.Validators;

namespace TuntiSeuranta.InputHandling;

public class InputHandler
{
    private readonly IConsoleHandler _consoleHandler;

    public InputHandler(IConsoleHandler consoleHandler)
    {
        _consoleHandler = consoleHandler ?? throw new ArgumentNullException(nameof(consoleHandler));
    }

    public DateTime GetValidatedDate(string prompt)
    {
        while (true)
        {
            string input = _consoleHandler.ReadInput(prompt);
            if (input.Trim().ToLower() == "q")
            {
                return default;
            }

            if (ValidationHelper.TryValidateDate(input, out DateTime date))
            {
                return date;
            }

            _consoleHandler.WriteMessage($"Syöte:'{input}' on virheellinen, käytä muotoa dd.mm.");
        }
    }

    public TimeSpan GetValidatedTime(string prompt)
    {
        while (true)
        {
            string input = _consoleHandler.ReadInput(prompt);
            if (input.Trim().ToLower() == "q")
            {
                return default;
            }

            if (ValidationHelper.TryValidateTime(input, out TimeSpan time))
            {
                return time;
            }

            _consoleHandler.WriteMessage($"Syöte:'{input}' on virheellinen, käytä muotoa hh:mm.");
        }
    }

    public bool GetModifyHoursSelection(string prompt)
    {
        while (true)
        {
            string input = _consoleHandler.ReadInput(prompt);
            if (input.Trim().ToLower() == "q")
            {
                return false;
            }

            if (input.Trim().ToLower() == "n")
            {
                return false;
            }

            if (input.Trim().ToLower() == "y")
            {
                return true;
            }
        }
    }
}