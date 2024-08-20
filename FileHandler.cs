using TuntiSeuranta.WorkHoursManagement;
namespace TuntiSeuranta.FileHandling;

public class FileHandler : IFileHandler
{
    private readonly string _filePath = "TuntiSeuranta.txt";

    public FileHandler()
    {
        EnsureFileExistsElseCreateFile();
    }

    private void EnsureFileExistsElseCreateFile()
    {
        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Close();
        }
    }

    public List<WorkHours> ReadWorkHours()
    {
        if (!File.Exists(_filePath))
        {
            return new List<WorkHours>();
        }

        var workHoursList = File.ReadAllLines(_filePath)
                                .Where(line => line.StartsWith("PVM"))
                                .Select(line => WorkHours.ParseWorkHoursFromString(line))
                                .ToList();

        return workHoursList;
    }

    public void SaveWorkHours(List<WorkHours> workHoursList)
    {
        using StreamWriter writer = new(_filePath, false);
        foreach (var workHours in workHoursList)
        {
            writer.WriteLine(workHours.ToString());
            writer.WriteLine("-----");
        }
    }
}
