namespace Reports.Api.Extension;

public static class ConvertDateToInt
{
    public static int ConvertToInt(this DateOnly dateOnly)
    {
        return dateOnly.Year * 10000 + dateOnly.Month * 100 + dateOnly.Day;
    }
}
