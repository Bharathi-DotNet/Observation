namespace Enquiry.Web.Extensions
{
    public static class DatetimeExtension
    {
        public static DateTime ToDateTime(DateTimeOffset value)
        {
            DateTime date = DateTime.Parse(value.ToString("g"));
            return date;
        }

        public static DateTime ToIndiaStandardTime(DateTime dt)
        {
            return TimeZoneInfo.ConvertTime(dt, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")); ;
        }
        public static DateTime IndiaStandardTimeNow()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")); ;
        }
    }
}
