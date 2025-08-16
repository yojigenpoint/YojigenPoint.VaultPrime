using System.Security.Cryptography;
using System.Text;

namespace YojigenPoint.VaultPrime.Helpers
{
    /// <summary>
    /// Represents the evaluated strength of a password.
    /// </summary>
    public enum PasswordStrength
    {
        Weak,
        Medium,
        Strong,
        VeryStrong
    }

    /// <summary>
    /// Provides functionality to generate and evaluate cryptographically strong passwords.
    /// </summary>
    public static class PasswordGenerator
    {
        private const string LowercaseCharacters = "abcdefghijklmnopqrstuvwxyz";
        private const string UppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NumericCharacters = "0123456789";
        private const string SpecialCharacters = "!#$%&*@\\";
        private const string AmbiguousCharacters = "l1O0";

        /// <summary>
        /// Generates a random password based on specified rules.
        /// </summary>
        /// <param name="length">Length of the password. Must be between 8 and 128.</param>
        /// <param name="includeLowercase">Whether to include lowercase characters.</param>
        /// <param name="includeUppercase">Whether to include uppercase characters.</param>
        /// <param name="includeNumeric">Whether to include numeric characters.</param>
        /// <param name="includeSpecial">Whether to include special characters.</param>
        /// <param name="excludeAmbiguous">Whether to exclude ambiguous characters (l, 1, O, 0).</param>
        /// <returns>A cryptographically strong random password.</returns>
        public static string Generate(
            int length = 12,
            bool includeLowercase = true,
            bool includeUppercase = true,
            bool includeNumeric = true,
            bool includeSpecial = true,
            bool excludeAmbiguous = true)
        {
            if (length < 8 || length > 128)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Password length must be between 8 and 128.");
            }

            var charSetBuilder = new StringBuilder();
            if (includeLowercase) charSetBuilder.Append(LowercaseCharacters);
            if (includeUppercase) charSetBuilder.Append(UppercaseCharacters);
            if (includeNumeric) charSetBuilder.Append(NumericCharacters);
            if (includeSpecial) charSetBuilder.Append(SpecialCharacters);

            if (charSetBuilder.Length == 0)
            {
                throw new ArgumentException("At least one character type must be selected.");
            }

            string charSet = charSetBuilder.ToString();
            if (excludeAmbiguous)
            {
                charSet = new string(charSet.Where(c => !AmbiguousCharacters.Contains(c)).ToArray());
            }

            var password = new StringBuilder(length);
            while (password.Length < length)
            {
                password.Append(GetRandomChar(charSet));
            }

            // Shuffle the password to ensure randomness of character positions
            return new string(password.ToString().ToCharArray().OrderBy(x => RandomNumberGenerator.GetInt32(int.MaxValue)).ToArray());
        }

        /// <summary>
        /// Evaluates the strength of a given password based on a simple scoring model.
        /// Note: This is a basic check and not a substitute for entropy calculation.
        /// </summary>
        /// <param name="password">The password to evaluate.</param>
        /// <returns>An enum representing the password's strength.</returns>
        public static PasswordStrength Evaluate(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return PasswordStrength.Weak;
            }

            int score = 0;
            if (password.Length >= 8) score++;
            if (password.Length >= 12) score++;
            if (password.Any(char.IsLower)) score++;
            if (password.Any(char.IsUpper)) score++;
            if (password.Any(char.IsDigit)) score++;
            if (password.Any(c => SpecialCharacters.Contains(c))) score++;

            return score switch
            {
                <= 2 => PasswordStrength.Weak,
                3 or 4 => PasswordStrength.Medium,
                5 => PasswordStrength.Strong,
                _ => PasswordStrength.VeryStrong,
            };
        }

        private static char GetRandomChar(string source)
        {
            return source[RandomNumberGenerator.GetInt32(source.Length)];
        }
    }
}