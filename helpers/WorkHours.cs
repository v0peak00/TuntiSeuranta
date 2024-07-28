using System.Globalization;

public class WorkHours
{
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public double TotalHours { get; set; }
    public double RegularHours { get; set; }
    public double OvertimeHours { get; set; }
    public double Overtime150Hours { get; set; }
    public double Overtime200Hours { get; set; }

    public override string ToString()
    {
        return $"PVM: {Date:dd.MM.yyyy} | " +
               $"Aloitus: {StartTime} | " +
               $"Lopetus: {EndTime} | " +
               $"Työtunnit: {TotalHours}h | " +
               $"Ylityöt: {OvertimeHours}h | " +
               $"100%: {RegularHours} | " +
               $"150%: {Overtime150Hours} | " +
               $"200%: {Overtime200Hours} | ";
    }

    public void Display()
    {
        Console.WriteLine(ToString());
    }

    public static WorkHours ParseWorkHoursFromString(string workHours)
    {
        WorkHours workHoursObject = new WorkHours();

        string[]? splitStrings = workHours.Split(new[] { " | " }, StringSplitOptions.None);

        foreach (string splitString in splitStrings)
        {
            string[]? keyValue = splitString.Split(new[] { ": " }, StringSplitOptions.None);
            if (keyValue.Length != 2)
            {
                continue;
            }

            string key = keyValue[0].Trim();
            string value = keyValue[1].Trim();

            switch (key)
            {
                case "PVM":
                    workHoursObject.Date = DateTime.ParseExact(value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    break;
                case "Aloitus":
                    workHoursObject.StartTime = TimeSpan.Parse(value);
                    break;
                case "Lopetus":
                    workHoursObject.EndTime = TimeSpan.Parse(value);
                    break;
                case "Työtunnit":
                    workHoursObject.TotalHours = double.Parse(value.Replace("h", ""));
                    break;
                case "Ylityöt":
                    workHoursObject.OvertimeHours = double.Parse(value.Replace("h", ""));
                    break;
                case "100%":
                    workHoursObject.RegularHours = double.Parse(value);
                    break;
                case "150%":
                    workHoursObject.Overtime150Hours = double.Parse(value);
                    break;
                case "200%":
                    workHoursObject.Overtime200Hours = double.Parse(value);
                    break;
            }
        }
        return workHoursObject;
    }
}