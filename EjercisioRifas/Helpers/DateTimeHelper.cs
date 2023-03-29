
public static class DateTimeHelper
{
	public static DateTime ToMexicoTime(this DateTime dateTime)
	{
		return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, "Central Standard Time");
	}
}

