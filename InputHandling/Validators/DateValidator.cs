using System.Globalization;
namespace TuntiSeuranta.InputHandling.Validators;

public class DateValidator : IInputValidator
{
    private IInputValidator? _nextValidator;

    public void SetNext(IInputValidator nextValidator)
    {
        _nextValidator = nextValidator;
    }

    public bool Validate(string input, out string errorMessage)
    {
        errorMessage = "Valid date format.";
        if (!DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            errorMessage = "Invalid date format. Please enter the date in dd.MM.yyyy format.";
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