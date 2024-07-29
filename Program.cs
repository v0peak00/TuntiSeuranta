using Helpers;

class Program
{
    static void Main(string[] args)
    {
        IFileHandler fileHandler = new FileHandler();
        InputHandler inputHandler = new InputHandler(fileHandler);
        SessionManager sessionManager = new SessionManager(fileHandler, inputHandler);

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
                    SummaryCalculator.DisplaySummary(fileHandler);
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
