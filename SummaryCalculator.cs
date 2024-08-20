using TuntiSeuranta.WorkHoursManagement;

public static class SummaryCalculator
{
    public static (double TotalYlityot, double Total100, double Total150, double Total200) CalculateSummary(WorkHoursService workHoursService)
    {
        var workHours = workHoursService.GetAllWorkHours();
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

    public static void DisplaySummary(WorkHoursService workHoursService)
    {
        var summary = CalculateSummary(workHoursService);
        Console.WriteLine($"Yhteenveto:");
        Console.WriteLine($"Ylityöt: {summary.TotalYlityot}h");
        Console.WriteLine($"100%: {summary.Total100}h");
        Console.WriteLine($"150%: {summary.Total150}h");
        Console.WriteLine($"200%: {summary.Total200}h\n");
    }
}
