namespace TuntiSeuranta.ConsoleHandling;

public class ConsoleHandler : IConsoleHandler
{
    public void WriteMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void WritePrompt(string prompt)
    {
        Console.Write(prompt);
    }

    public string ReadInput(string prompt)
    {
        WritePrompt(prompt);
        string? userInput = Console.ReadLine(); // Nullable string
        return string.IsNullOrWhiteSpace(userInput) ? string.Empty : userInput;
    }
}
