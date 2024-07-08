public static class HoursCalculator
{
    public static WorkHours CalculateHours(DateTime date, TimeSpan startTime, TimeSpan endTime)
    {
        var workHours = new WorkHours
        {
            Date = date,
            StartTime = startTime,
            EndTime = endTime
        };

        if (workHours.EndTime < workHours.StartTime)
        {
            workHours.EndTime = workHours.EndTime.Add(new TimeSpan(1, 0, 0, 0));
        }

        double exactTotalHours = CalculateTotalHours(workHours.StartTime, workHours.EndTime);
        workHours.TotalHours = RoundToNearestQuarterHour(exactTotalHours);

        CalculateRegularAndOvertimeHours(workHours);

        return workHours;
    }

    private static double CalculateTotalHours(TimeSpan startTime, TimeSpan endTime)
    {
        double totalHours = 0;
        TimeSpan current = startTime;

        while (current < endTime)
        {
            TimeSpan nextSegment = current.Add(TimeSpan.FromMinutes(15));
            if (nextSegment > endTime)
            {
                nextSegment = endTime;
            }

            double segmentHours = (nextSegment - current).TotalHours;

            if ((current.Hours >= 0 && current.Hours < 6) || (current.Hours >= 18 && current.Hours < 24))
            {
                totalHours += segmentHours * 2;
            }
            else
            {
                totalHours += segmentHours;
            }

            current = nextSegment;
        }

        return totalHours;
    }

    private static void CalculateRegularAndOvertimeHours(WorkHours workHours)
    {
        workHours.RegularHours = Math.Min(workHours.TotalHours, 8.0);
        double overtimeHours = Math.Max(0, workHours.TotalHours - 8.0);

        workHours.Overtime150Hours = RoundToNearestQuarterHour(Math.Min(overtimeHours, 2.0));
        workHours.Overtime200Hours = RoundToNearestQuarterHour(Math.Max(0, overtimeHours - 2.0));
        workHours.OvertimeHours = workHours.Overtime150Hours + workHours.Overtime200Hours;
    }

    private static double RoundToNearestQuarterHour(double hours)
    {
        double totalMinutes = hours * 60;
        double roundedMinutes = Math.Round(totalMinutes / 15.0) * 15;
        return roundedMinutes / 60.0;
    }
}
