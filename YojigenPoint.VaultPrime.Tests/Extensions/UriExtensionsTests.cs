using YojigenPoint.VaultPrime.Extensions;
using FluentAssertions;

namespace YojigenPoint.VaultPrime.Tests.Extensions
{
    public class UriExtensionsTests
    {
        [Fact]
        public void GetBaseUri_ReturnsOnlyUriWithoutQueryWithUriAndQuery()
        {
            // Arrange
            var originalUri = new Uri("https://example.com/path?param1=value1&param2=value2");

            // Act
            var baseUri = originalUri.GetBaseUri();

            // Assert
            baseUri.ToString().Should().Be("https://example.com/path");
        }

        [Fact]
        public void GetBaseUri_ReturnsOnlyUriWithoutQuery_WithOnlyUri()
        {
            // Arrange
            var originalUri = new Uri("https://example.com/path");

            // Act
            var baseUri = originalUri.GetBaseUri();

            // Assert
            baseUri.ToString().Should().Be("https://example.com/path");
        }

        [Fact]
        public void ParseQueryString_ReturnsCorrectParameters()
        {
            // Arrange
            var uri = new Uri("https://example.com/path?param1=value1&param2=value2");

            // Act
            var queryParams = uri.ParseQueryString();

            // Assert
            queryParams["param1"].Should().Be("value1");
            queryParams["param2"].Should().Be("value2");
        }
    }
}
