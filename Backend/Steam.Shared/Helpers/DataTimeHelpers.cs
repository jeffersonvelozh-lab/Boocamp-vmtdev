namespace Steam.Shared.Helpers
{
    public class DataTimeHelpers
    {
        public static DateTime UtcNow()
        {
            return DateTimeOffset.UtcNow.DateTime;
        }
    }
}
