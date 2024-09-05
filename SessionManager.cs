using TuntiSeuranta.ConsoleHandling;
using TuntiSeuranta.InputHandling;
using TuntiSeuranta.WorkHoursManagement;

public class SessionManager
{
    private readonly WorkHoursDisplay _workHoursDisplay;
    private readonly WorkHoursService _workHoursService;
    private readonly InputHandler _inputHandler;
    private readonly IConsoleHandler _consoleHandler;

    public SessionManager(WorkHoursService workHoursService, WorkHoursDisplay workHoursDisplay, InputHandler inputHandler, IConsoleHandler consoleHandler)
    {
        _workHoursService = workHoursService ?? throw new ArgumentNullException(nameof(workHoursService));
        _workHoursDisplay = workHoursDisplay ?? throw new ArgumentNullException(nameof(workHoursDisplay));
        _inputHandler = inputHandler ?? throw new ArgumentNullException(nameof(inputHandler));
        _consoleHandler = consoleHandler ?? throw new ArgumentNullException(nameof(consoleHandler));
    }

    public void RunSession()
    {
        while (true)
        {
            _workHoursDisplay.DisplayPreviousHours();

            DateTime dateInput = _inputHandler.GetValidatedDate("Syötä PVM (pp.kk.vvvv) tai 'q' palataksesi");
            if (dateInput == default)
            {
                return;
            }

            if (_workHoursService.WorkHoursExistsForDate(dateInput))
            {
                HandleExistingWorkHours(dateInput);
            }
            else
            {
                HandleAddNewWorkHours(dateInput);
            }
        }
    }

    private void HandleExistingWorkHours(DateTime dateInput)
    {
        _consoleHandler.WriteMessage($"Tunteja löytyi PVM: {dateInput:dd.MM.yyyy}");
        _workHoursDisplay.DisplayHoursForDate(dateInput);

        bool modifyHours = _inputHandler.GetModifyHoursSelection(
            "Haluatko muokata näitä tunteja? Y/N tai 'q' palataksesi päävalikkoon: "
        );

        if (!modifyHours)
        {
            _consoleHandler.ExitModifyingHours();
            return;
        }

        HandleModifyWorkHours(dateInput);
    }

    private void HandleModifyWorkHours(DateTime dateInput)
    {
        if (GetValidatedStartAndEndTimes(out TimeSpan startTimeInput, out TimeSpan endTimeInput))
        {
            _workHoursService.ModifyWorkHours(dateInput, startTimeInput, endTimeInput);
            _consoleHandler.WriteMessage($"Työaika päivitetty päivälle {dateInput:dd.MM.yyyy}");
        }
    }

    private void HandleAddNewWorkHours(DateTime dateInput)
    {
        if (GetValidatedStartAndEndTimes(out TimeSpan startTimeInput, out TimeSpan endTimeInput))
        {
            _workHoursService.AddNewWorkHours(dateInput, startTimeInput, endTimeInput);
            _consoleHandler.WriteMessage($"Uudet työajat lisätty päivälle {dateInput:dd.MM.yyyy}");
        }
    }

    private bool GetValidatedStartAndEndTimes(out TimeSpan startTimeInput, out TimeSpan endTimeInput)
    {
        startTimeInput = _inputHandler.GetValidatedTime("Syötä aloitus aika (hh:mm) tai 'q' palataksesi.");
        endTimeInput = default;
        if (startTimeInput == default)
        {
            _consoleHandler.ExitModifyingHours();
            return false;
        }

        endTimeInput = _inputHandler.GetValidatedTime("Syötä lopetus aika (hh:mm) tai 'q' palataksesi.");
        if (endTimeInput == default)
        {
            _consoleHandler.ExitModifyingHours();
            return false;
        }

        return true;
    }
}
