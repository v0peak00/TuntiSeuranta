namespace TuntiSeuranta.WorkHoursManagement;

public class WorkHoursDisplay
{
    private readonly WorkHoursService _workHoursService;

    public WorkHoursDisplay(WorkHoursService workHoursService)
    {
        _workHoursService = workHoursService ?? throw new ArgumentNullException(nameof(workHoursService));
    }

    public void DisplayPreviousHours()
    {
        List<WorkHours> allWorkHours = _workHoursService.GetAllWorkHours();
        if (allWorkHours.Count != 0)
        {
            Console.WriteLine("Previous hours:");
            foreach (WorkHours workHours in allWorkHours)
            {
                Console.WriteLine(workHours.ToString());
            }
        }
        else
        {
            Console.WriteLine("No previous hours found.");
        }
    }

    public void DisplayHoursForDate(DateTime date)
    {
        var workHoursForDate = _workHoursService.GetWorkHoursForDate(date);
        if (workHoursForDate != null)
        {
            Console.WriteLine(workHoursForDate.ToString());
        }
        else
        {
            Console.WriteLine($"No hours found for the date: {date:dd.MM.yyyy}");
        }
    }
}
