using TuntiSeuranta.FileHandling;
namespace TuntiSeuranta.WorkHoursManagement;

public class WorkHoursRepository
{
    private readonly IFileHandler _fileHandler;

    public WorkHoursRepository(IFileHandler fileHandler)
    {
        _fileHandler = fileHandler ?? throw new ArgumentNullException(nameof(fileHandler));
    }

    public List<WorkHours> GetAllWorkHours()
    {
        return _fileHandler.ReadWorkHours();
    }

    public WorkHours? GetWorkHoursByDate(DateTime date)
    {
        return GetAllWorkHours().Find(wh => wh.Date.Date == date.Date);
    }

    public void SaveAllWorkHours(List<WorkHours> workHours)
    {
        _fileHandler.SaveWorkHours(workHours);
    }

    public void UpdateWorkHours(WorkHours updatedWorkHours)
    {
        List<WorkHours> allWorkHours = GetAllWorkHours();
        int index = allWorkHours.FindIndex(wh => wh.Date.Date == updatedWorkHours.Date.Date);

        if (index >= 0)
        {
            allWorkHours[index] = updatedWorkHours;
            SaveAllWorkHours(allWorkHours);
        }
        else
        {
            throw new InvalidOperationException("WorkHours entry not found.");
        }
    }
}
