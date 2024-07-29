public class FileHandler : IFileHandler
{
    private readonly string _filePath = "tunnit.txt";
    private string[] linesFromFile;
    private WorkHours existingWorkHours;

    public FileHandler()
    {
        EnsureFileExists();
        ReadFileToStringArray();
    }

    private void EnsureFileExists()
    {
        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Close();
        }
    }

    private void ReadFileToStringArray()
    {
        linesFromFile = File.Exists(_filePath) ? File.ReadAllLines(_filePath) : new string[0];
    }

    public void SaveResultToFile(WorkHours workHours)
    {
        using (StreamWriter writer = new StreamWriter(_filePath, true))
        {
            writer.WriteLine(workHours.ToString());
            writer.WriteLine("-----");
        }
        // Refresh lines from file after writing
        ReadFileToStringArray();
    }

    public void DisplayPreviousHours()
    {
        if (linesFromFile.Length > 0)
        {
            Console.WriteLine("Previous hours:");
            foreach (string line in linesFromFile)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine("No previous hours found.");
        }
    }

    public bool HasHoursForDate(DateTime date)
    {
        string targetDate = $"PVM: {date:dd.MM.yyyy}";
        foreach (string line in linesFromFile)
        {
            if (line.Contains(targetDate))
            {
                existingWorkHours = WorkHours.ParseWorkHoursFromString(line);
                return true;
            }
        }
        return false;
    }

    public void DisplayHoursForDate()
    {
        if (existingWorkHours != null)
        {
            Console.WriteLine(existingWorkHours.ToString());
        }
        else
        {
            Console.WriteLine("No hours found for the given date.");
        }
    }

    public void ModifyHoursForDate(TimeSpan start, TimeSpan end)
    {
        if (existingWorkHours != null)
        {
            existingWorkHours.StartTime = start;
            existingWorkHours.EndTime = end;
            existingWorkHours = HoursCalculator.CalculateHours(existingWorkHours.Date, start, end);

            // Update the entry in the file
            for (int i = 0; i < linesFromFile.Length; i++)
            {
                if (linesFromFile[i].Contains($"PVM: {existingWorkHours.Date:dd.MM.yyyy}"))
                {
                    linesFromFile[i] = existingWorkHours.ToString();
                    break;
                }
            }
            File.WriteAllLines(_filePath, linesFromFile);
            // Refresh lines from file after writing
            ReadFileToStringArray();
        }
    }

    public string GetFilePath()
    {
        return _filePath;
    }

    public List<WorkHours> ReadWorkHoursFromFile()
    {
        var workHoursList = new List<WorkHours>();

        foreach (var line in linesFromFile)
        {
            if (line.StartsWith("PVM"))
            {
                workHoursList.Add(WorkHours.ParseWorkHoursFromString(line));
            }
        }

        return workHoursList;
    }
}
