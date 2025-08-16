using YojigenPoint.VaultPrime.Extensions;
using FluentAssertions;

namespace YojigenPoint.VaultPrime.Tests.Extensions
{
    public class DateTimeExtensionTests
    {
        [Theory]
        [InlineData(-1, true)] // 1 day ago is new
        [InlineData(-2, true)] // 2 days ago is new
        [InlineData(-3, false)] // 3 days ago is not new
        [InlineData(1, false)] // 1 day in the future is not new
        public void IsNew_ShouldReturnCorrectValue(int daysOffset, bool expected)
        {
            // Arrange
            DateTime date = DateTime.UtcNow.AddDays(daysOffset);

            // Act
            bool result = date.IsNew();

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-1, true)] // 1 minute ago is expired
        [InlineData(0, true)] // now is expired
        [InlineData(1, false)] // 1 day in the future is expired
        public void IsExpired_ShouldReturnCorrectValue(int minutesOffset, bool expected)
        {
            // Arrange
            DateTime date = DateTime.UtcNow.AddMinutes(minutesOffset);

            // Act
            bool result = date.IsExpired();

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(6, 7, true)] // Expires in 6 days, period is 7 -> true
        [InlineData(7, 7, true)] // Expires in 7 days, period is 7 -> true
        [InlineData(8, 7, false)] // Expires in 8 days, period is 7 -> false
        [InlineData(-1, 7, false)] // Already expired -> false
        public void IsExpiringSoon_ShouldReturnCorrectValue(int daysOffset, int period, bool expected)
        {
            // Arrange
            DateTime date = DateTime.UtcNow.AddDays(daysOffset);

            // Act
            bool result = date.IsExpiringSoon();

            // Assert
            result.Should().Be(expected);
        }
    }
}
