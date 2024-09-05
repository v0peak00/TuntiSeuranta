using System.Globalization;

namespace TuntiSeuranta.InputHandling.Validators;

public static class ValidationHelper
{
    public static bool TryValidateDate(string input, out DateTime date)
    {
        return DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
    }

    public static bool TryValidateTime(string input, out TimeSpan time)
    {
        return TimeSpan.TryParse(input, CultureInfo.InvariantCulture, out time);
    }
}