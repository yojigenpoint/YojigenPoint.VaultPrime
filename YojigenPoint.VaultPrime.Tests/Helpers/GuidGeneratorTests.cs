using YojigenPoint.VaultPrime.Helpers;
using FluentAssertions;

namespace YojigenPoint.VaultPrime.Tests.Helpers
{
    public class GuidGeneratorTests
    {
        [Fact]
        public void GenerateCombGuid_ReturnValidGuid()
        {
            // Act
            Guid combGuid = GuidGenerator.GenerateCombGuid();

            // Assert
            Guid.TryParse(combGuid.ToString(), out _).Should().BeTrue();
        }

        [Fact]
        public void GenerateCombGuid_ReturnSequentialGuids()
        {
            // Act
            Guid guid1 = GuidGenerator.GenerateCombGuid();
            Guid guid2 = GuidGenerator.GenerateCombGuid();

            // Assert
            guid2.CompareTo(guid1).Should().Be(1);
        }
    }
}
