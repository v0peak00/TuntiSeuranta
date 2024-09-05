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
        string? userInput = Console.ReadLine();
        return string.IsNullOrWhiteSpace(userInput) ? string.Empty : userInput;
    }

    public void ExitToMenu()
    {
        Console.WriteLine("Palataan päävalikkoon");
    }

    public void ExitModifyingHours()
    {
        Console.WriteLine("Palataan tuntinäkymään");
    }

}
