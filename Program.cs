class Program
{
    static void Main(string[] args)
    {
        IFileHandler fileHandler = new FileHandler();
        InputHandler inputHandler = new InputHandler(fileHandler);
        
        while (true)
        {
            RunTimeTrackingSession(fileHandler, inputHandler);
        }
    }

    private static void RunTimeTrackingSession(IFileHandler fileHandler, InputHandler inputHandler)
    {
        fileHandler.DisplayPreviousHours();
        
        DateTime dateInput = inputHandler.GetDateInputFromUser();
        bool hoursForDateExists = fileHandler.HasHoursForDate(dateInput);

        while (hoursForDateExists)
        {
            inputHandler.PromptUserToModifyHoursForDate(dateInput);
            fileHandler.DisplayPreviousHours(); // To reflect the changes
            dateInput = inputHandler.GetDateInputFromUser();
            hoursForDateExists = fileHandler.HasHoursForDate(dateInput);
        }

        TimeSpan startTimeInput = inputHandler.GetTimeInputFromUser("Töiden aloitus (tt:mm): ");
        TimeSpan endTimeInput = inputHandler.GetTimeInputFromUser("Töiden lopetus (HH:mm): ");

        WorkHours workHours = HoursCalculator.CalculateHours(dateInput, startTimeInput, endTimeInput);
        
        fileHandler.SaveResultToFile(workHours);
    }
}
