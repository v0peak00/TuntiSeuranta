namespace TuntiSeuranta.WorkHoursManagement;

public class WorkHoursService
{
    private readonly WorkHoursRepository _workHoursRepository;

    public WorkHoursService(WorkHoursRepository workHoursRepository)
    {
        _workHoursRepository = workHoursRepository ?? throw new ArgumentNullException(nameof(workHoursRepository));
    }

    public List<WorkHours> GetAllWorkHours()
    {
        return _workHoursRepository.GetAllWorkHours();
    }

    public bool WorkHoursExistsForDate(DateTime date)
    {
        return _workHoursRepository.GetWorkHoursByDate(date) != null;
    }

    public WorkHours GetWorkHoursForDate(DateTime date)
    {
        return _workHoursRepository.GetWorkHoursByDate(date);
    }

    public void ModifyWorkHours(DateTime date, TimeSpan start, TimeSpan end)
    {
        var existingWorkHours = _workHoursRepository.GetWorkHoursByDate(date);
        existingWorkHours.StartTime = start;
        existingWorkHours.EndTime = end;
        existingWorkHours = HoursCalculator.CalculateHours(existingWorkHours.Date, start, end);
        _workHoursRepository.UpdateWorkHours(existingWorkHours);
    }

    public void AddNewWorkHours(DateTime date, TimeSpan start, TimeSpan end)
    {
        var newWorkHours = HoursCalculator.CalculateHours(date, start, end);
        var allWorkHours = _workHoursRepository.GetAllWorkHours();
        allWorkHours.Add(newWorkHours);
        _workHoursRepository.SaveAllWorkHours(allWorkHours);
    }
}
