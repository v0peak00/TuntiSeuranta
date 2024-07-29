namespace Helpers
{
    public class SessionManager
    {
        private readonly IFileHandler _fileHandler;
        private readonly InputHandler _inputHandler;

        public SessionManager(IFileHandler fileHandler, InputHandler inputHandler)
        {
            _fileHandler = fileHandler;
            _inputHandler = inputHandler;
        }

        public void RunSession()
        {
            _fileHandler.DisplayPreviousHours();
            if (!_inputHandler.TryGetDateInput(out DateTime dateInput)) return;
            bool hoursForDateExists = _fileHandler.HasHoursForDate(dateInput);

            while (hoursForDateExists)
            {
                _inputHandler.PromptUserToModifyHoursForDate(dateInput);
                _fileHandler.DisplayPreviousHours();
                if (!_inputHandler.TryGetDateInput(out dateInput)) return;
                hoursForDateExists = _fileHandler.HasHoursForDate(dateInput);
            }

            if (!_inputHandler.TryGetTimeInput("Töiden aloitus (tt:mm): ", out TimeSpan startTimeInput)) return;
            if (!_inputHandler.TryGetTimeInput("Töiden lopetus (tt:mm): ", out TimeSpan endTimeInput)) return;
            WorkHours workHours = HoursCalculator.CalculateHours(dateInput, startTimeInput, endTimeInput);
            _fileHandler.SaveResultToFile(workHours);
        }
    }
}