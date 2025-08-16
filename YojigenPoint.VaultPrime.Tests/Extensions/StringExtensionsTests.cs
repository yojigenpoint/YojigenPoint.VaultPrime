using YojigenPoint.VaultPrime.Extensions;
using FluentAssertions;

namespace YojigenPoint.VaultPrime.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void IsNullOrWhiteSpace_ShouldReturnTrue_ForNullInput()
        {
            // Arrange
            string? input = null;

            // Act
            bool result = input.IsNullOrWhiteSpace();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsNullOrWhiteSpace_ShouldReturnTrue_ForEmptyInput()
        {
            // Arrange
            string input = string.Empty;

            // Act
            bool result = input.IsNullOrWhiteSpace();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsNullOrWhiteSpace_ShouldReturnTrue_ForWhitespaceInput()
        {
            // Arrange
            string input = "    ";

            // Act
            bool result = input.IsNullOrWhiteSpace();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsNullOrWhiteSpace_ShouldReturnFalse_ForValidInput()
        {
            // Arrange
            string input = "Yojigen";

            // Act
            bool result = input.IsNullOrWhiteSpace();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ReplaceFirst_ReplacesOnlyFirstOccurrence()
        {
            // Arrange
            string input = "A B C B D";
            string oldValue = "B";
            string newValue = "X";
            // Act
            string result = input.ReplaceFirst(oldValue, newValue);
            // Assert
            result.Should().Be("A X C B D");
        }

        [Fact]
        public void ReplaceLast_ReplacesOnlyLastOccurrence()
        {
            // Arrange
            string input = "A B C B D";
            string oldValue = "B";
            string newValue = "X";
            // Act
            string result = input.ReplaceLast(oldValue, newValue);
            // Assert
            result.Should().Be("A B C X D");
        }

        [Fact]
        public void GetFileNameWithoutExtension_HandlesWindowsPath()
        {
            // Arrange
            string filePath = @"C:\dir\file.txt";
            // Act
            string result = filePath.GetFileNameWithoutExtension();
            // Assert
            result.Should().Be("file");
        }

        [Fact]
        public void GetFileNameWithoutExtension_HandlesLinuxPath()
        {
            // Arrange
            string filePath = @"/home/user/file.pdf";
            // Act
            string result = filePath.GetFileNameWithoutExtension();
            // Assert
            result.Should().Be("file");
        }
    }
}
