using TuntiSeuranta.InputHandling;
using TuntiSeuranta.WorkHoursManagement;

public class SessionManager
{
    private readonly InputHandler _inputHandler;
    private readonly WorkHoursDisplay _workHoursDisplay;

    public SessionManager(InputHandler inputHandler, WorkHoursDisplay workHoursDisplay)
    {
        _inputHandler = inputHandler;

        _workHoursDisplay = workHoursDisplay;
    }

    public void RunSession()
    {
        while (true)
        {
            _workHoursDisplay.DisplayPreviousHours();

            if (!_inputHandler.TryGetDateInput(out DateTime dateInput)) return;

            if (!_inputHandler.HandleDateInput(dateInput))
            {
                return; // Exit if user chose to quit
            }
        }
    }
}
