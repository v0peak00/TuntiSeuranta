public interface IFileHandler
{
    void SaveResultToFile(WorkHours workHours);
    void DisplayPreviousHours();
    void DisplayHoursForDate();
    bool HasHoursForDate(DateTime date);
    void ModifyHoursForDate(TimeSpan start, TimeSpan end);
    string GetFilePath();
    List<WorkHours> ReadWorkHoursFromFile();
}