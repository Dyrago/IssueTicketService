namespace IssueTicketService.Application.Helpers
{
    public static class DateTimeFormater
    {
        public static DateTime ToPolishTime(DateTime dateTime)
        {
            var polishTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw");
            return TimeZoneInfo.ConvertTime(dateTime, polishTimeZone);
        }
    }
}