namespace TuntiSeuranta.InputHandling.Validators;

public interface IInputValidator
{
    void SetNext(IInputValidator nextValidator);
    bool Validate(string input, out string errorMessage);
}