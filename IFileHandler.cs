using TuntiSeuranta.WorkHoursManagement;
namespace TuntiSeuranta.FileHandling;

public interface IFileHandler
{
    List<WorkHours> ReadWorkHours();
    void SaveWorkHours(List<WorkHours> workHoursList);
}