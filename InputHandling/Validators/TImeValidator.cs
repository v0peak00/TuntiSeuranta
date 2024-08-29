using System.Globalization;
namespace TuntiSeuranta.InputHandling.Validators;

public class TimeValidator : IInputValidator
{
    private IInputValidator? _nextValidator;

    public void SetNext(IInputValidator nextValidator)
    {
        _nextValidator = nextValidator;
    }

    public bool Validate(string input, out string errorMessage)
    {
        errorMessage = "Valid time format.";
        if (!TimeSpan.TryParse(input, CultureInfo.InvariantCulture, out _))
        {
            errorMessage = "Invalid time format. Please enter the time in HH:mm format.";
            return false;
        }

        // Pass the validation to the next in the chain if exists
        if (_nextValidator != null)
        {
            return _nextValidator.Validate(input, out errorMessage);
        }

        return true;
    }
}