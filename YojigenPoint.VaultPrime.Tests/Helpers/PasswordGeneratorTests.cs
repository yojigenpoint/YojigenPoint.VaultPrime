using YojigenPoint.VaultPrime.Helpers;
using FluentAssertions;

namespace YojigenPoint.VaultPrime.Tests.Helpers
{
    public class PasswordGeneratorTests
    {
        [Fact]
        public void Generate_ReturnCorrectLength()
        {
            // Arrange
            int length = 16;

            // Act
            string password = PasswordGenerator.Generate(length);

            // Assert
            password.Length.Should().Be(16);
        }

        [Theory]
        [InlineData(7)] // Too short
        [InlineData(129)] // Too long
        public void Generate_ThrowsException_WhenLengthIsInvalid(int length)
        {
            // Action
            Action act = () => PasswordGenerator.Generate(length);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Generate_ThrowsException_WhenNoCharacterIsSelected()
        {
            // Arrange
            int length = 16;

            // Action
            Action act = () => PasswordGenerator.Generate(length, false, false, false, false, false);

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Evaluate_ReturnsWeak_ForSimplePassword()
        {
            // Arrange
            string password = "password";

            // Act
            var strength = PasswordGenerator.Evaluate(password);

            // Assert
            strength.Should().Be(PasswordStrength.Weak);
        }

        [Fact]
        public void Evaluate_ReturnsMedium_ForRegularPassword()
        {
            // Arrange
            string password = "Password";

            // Act
            var strength = PasswordGenerator.Evaluate(password);

            // Assert
            strength.Should().Be(PasswordStrength.Medium);
        }

        [Fact]
        public void Evaluate_ReturnsStrong_ForAveragePassword()
        {
            // Arrange
            string password = "Yojigenpoint1";

            // Act
            var strength = PasswordGenerator.Evaluate(password);

            // Assert
            strength.Should().Be(PasswordStrength.Strong);
        }

        [Fact]
        public void Evaluate_ReturnsVeryStrong_ForStrongPassword()
        {
            // Arrange
            string password = "Yojigen!Point123#";

            // Act
            var strength = PasswordGenerator.Evaluate(password);

            // Assert
            strength.Should().Be(PasswordStrength.VeryStrong);
        }
    }
}
