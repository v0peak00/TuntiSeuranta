using TuntiSeuranta.ConsoleHandling;
using TuntiSeuranta.FileHandling;
using TuntiSeuranta.InputHandling;
using TuntiSeuranta.WorkHoursManagement;

class Program
{
    static void Main(string[] args)
    {
        IFileHandler fileHandler = new FileHandler();
        WorkHoursRepository workHoursRepository = new WorkHoursRepository(fileHandler);
        WorkHoursService workHoursService = new WorkHoursService(workHoursRepository);
        WorkHoursDisplay workHoursDisplay = new WorkHoursDisplay(workHoursService);
        IConsoleHandler consoleHandler = new ConsoleHandler();
        InputHandler inputHandler = new InputHandler(consoleHandler);
        SessionManager sessionManager = new SessionManager(workHoursService, workHoursDisplay, inputHandler, consoleHandler);

        while (true)
        {
            Console.WriteLine("Valitse toiminto:");
            Console.WriteLine("1. Lisää/Muokkaa työaikoja");
            Console.WriteLine("2. Näytä yhteenveto");
            Console.WriteLine("3. Poistu");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    sessionManager.RunSession();
                    break;
                case "2":
                    SummaryCalculator.DisplaySummary(workHoursService);
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Virheellinen valinta. Yritä uudelleen.");
                    break;
            }
        }
    }
}
