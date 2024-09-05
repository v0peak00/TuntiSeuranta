namespace TuntiSeuranta.ConsoleHandling;

public interface IConsoleHandler
{
    void WriteMessage(string message);
    void WritePrompt(string prompt);
    string ReadInput(string prompt);
    void ExitToMenu();
    void ExitModifyingHours();
}
