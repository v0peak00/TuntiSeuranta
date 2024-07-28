class Program
{
    static void Main(string[] args)
    {
        IFileHandler fileHandler = new FileHandler();
        InputHandler inputHandler = new InputHandler(fileHandler);

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
                    RunTimeTrackingSession(fileHandler, inputHandler);
                    break;
                case "2":
                    DisplaySummary(fileHandler);
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Virheellinen valinta. Yritä uudelleen.");
                    break;
            }
        }
    }
                

    private static void RunTimeTrackingSession(IFileHandler fileHandler, InputHandler inputHandler)
    {
        fileHandler.DisplayPreviousHours();
        
        if (!inputHandler.TryGetDateInput(out DateTime dateInput)) return;
        bool hoursForDateExists = fileHandler.HasHoursForDate(dateInput);

        while (hoursForDateExists)
        {
            inputHandler.PromptUserToModifyHoursForDate(dateInput);
            fileHandler.DisplayPreviousHours(); // To reflect the changes
            if (!inputHandler.TryGetDateInput(out dateInput)) return;
            hoursForDateExists = fileHandler.HasHoursForDate(dateInput);
        }

        if (!inputHandler.TryGetTimeInput("Töiden aloitus (tt:mm): ", out TimeSpan startTimeInput)) return;
        if (!inputHandler.TryGetTimeInput("Töiden lopetus (tt:mm): ", out TimeSpan endTimeInput)) return;

        WorkHours workHours = HoursCalculator.CalculateHours(dateInput, startTimeInput, endTimeInput);
        
        fileHandler.SaveResultToFile(workHours);
    }

     private static void DisplaySummary(IFileHandler fileHandler)
    {
        var summary = SummaryCalculator.CalculateSummary(fileHandler.GetFilePath());
        SummaryCalculator.DisplaySummary(summary);
    }
}
