namespace YojigenPoint.VaultPrime.Extensions
{
    /// <summary>
    /// Provides useful extension methods for the DateTime struct.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Determines if a given creation time is within a specified recent period.
        /// </summary>
        /// <param name="createdTime">The UTC time of creation.</param>
        /// <param name="periodInDays">How many days previous from now counts as new. Default is 2 days.</param>
        /// <returns>True if the time is within the period and not in the future.</returns>
        public static bool IsNew(this DateTime createdTime, int periodInDays = 2)
        {
            var diff = DateTime.UtcNow - createdTime;
            return diff.TotalDays >= 0 && diff.TotalDays <= periodInDays;
        }

        /// <summary>
        /// Determines if a given expiration time is approaching.
        /// </summary>
        /// <param name="expiryTime">The UTC time of expiry.</param>
        /// <param name="periodInDays">How many days from now counts as expiring soon. Default is 7 days.</param>
        /// <returns>True if the time is within the period and has not already passed.</returns>
        public static bool IsExpiringSoon(this DateTime expiryTime, int periodInDays = 7)
        {
            var diff = expiryTime - DateTime.UtcNow;
            return diff.TotalDays > 0 && diff.TotalDays <= periodInDays;
        }

        /// <summary>
        /// Determines if a content has expired based on a given expiry time.
        /// </summary>
        /// <param name="expiryTime">The UTC time of expiry.</param>
        /// <returns>True if the expiry time is in the past.</returns>
        public static bool IsExpired(this DateTime expiryTime)
        {
            return expiryTime <= DateTime.UtcNow;
        }
    }
}