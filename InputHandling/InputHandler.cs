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

    public bool TryGetValidatedInput(out string input)
    {
        input = string.Empty;

        // Create validators
        IInputValidator dateValidator = new DateValidator();
        IInputValidator timeValidator = new TimeValidator();

        dateValidator.SetNext(timeValidator);

        input = _consoleHandler.ReadInput("Enter date (dd.MM.yyyy): ");

        if (!dateValidator.Validate(input, out string errorMessage))
        {
            _consoleHandler.WriteMessage(errorMessage);
            return false;
        }

        return true;
    }
}

