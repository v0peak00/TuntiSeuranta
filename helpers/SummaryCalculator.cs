using System;
using System.Collections.Generic;
using System.IO;

public static class SummaryCalculator
{
    public static (double TotalYlityot, double Total100, double Total150, double Total200) CalculateSummary(string filePath)
    {
        var workHours = ReadWorkHoursFromFile(filePath);
        double totalYlityot = 0, total100 = 0, total150 = 0, total200 = 0;

        foreach (var entry in workHours)
        {
            totalYlityot += entry.OvertimeHours;
            total100 += entry.RegularHours;
            total150 += entry.Overtime150Hours;
            total200 += entry.Overtime200Hours;
        }

        return (totalYlityot, total100, total150, total200);
    }

    private static List<WorkHours> ReadWorkHoursFromFile(string filePath)
    {
        var workHoursList = new List<WorkHours>();
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            if (line.StartsWith("PVM"))
            {
                workHoursList.Add(WorkHours.ParseWorkHoursFromString(line));
            }
        }

        return workHoursList;
    }

    public static void DisplaySummary((double TotalYlityot, double Total100, double Total150, double Total200) summary)
    {
        Console.WriteLine($"Yhteenveto:");
        Console.WriteLine($"Ylityöt: {summary.TotalYlityot}h");
        Console.WriteLine($"100%: {summary.Total100}h");
        Console.WriteLine($"150%: {summary.Total150}h");
        Console.WriteLine($"200%: {summary.Total200}h\n");
    }
}
