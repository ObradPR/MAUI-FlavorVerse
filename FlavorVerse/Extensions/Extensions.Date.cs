namespace FlavorVerse.Extensions
{
    public static partial class Extensions
    {
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static bool BeInThePast(DateOnly? date)
        {
            if (!date.HasValue)
            {
                return false;
            }

            return date < DateOnly.FromDateTime(DateTime.Now.Date);
        }

        public static bool BeAValidAge(DateOnly? date)
        {
            if (!date.HasValue)
            {
                return false;
            }

            return date > DateOnly.FromDateTime(DateTime.Now.AddYears(-100)) && date < DateOnly.FromDateTime(DateTime.Now.Date.AddYears(-12));
        }
    }
}