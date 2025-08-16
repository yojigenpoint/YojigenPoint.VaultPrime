using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace YojigenPoint.VaultPrime.Extensions
{
    /// <summary>
    /// Provides a set of robust and performant extension methods for the string class.
    /// </summary>
    public static class StringExtensions
    {
        // A thread-safe cache for compiled Regex objects to boost performance.
        private static readonly ConcurrentDictionary<string, Regex> RegexCache = new();

        /// <summary>
        /// Determines if a string is null, empty, or consists only of white-space characters.
        /// This is a convenience wrapper for string.IsNullOrWhiteSpace for fluent syntax.
        /// </summary>
        /// <param name="input">The string to test.</param>
        /// <returns>true if the string is null or whitespace; otherwise, false.</returns>
        public static bool IsNullOrWhiteSpace(this string? input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Replaces the first occurrence of a specified substring with another string.
        /// This method is case-sensitive.
        /// </summary>
        /// <param name="input">The original string.</param>
        /// <param name="segmentToReplace">The substring to replace.</param>
        /// <param name="replacement">The string to replace the first occurrence of segmentToReplace.</param>
        /// <returns>A new string with the first occurrence replaced.</returns>
        public static string ReplaceFirst(this string input, string segmentToReplace, string replacement)
        {
            if (input.IsNullOrWhiteSpace() || string.IsNullOrEmpty(segmentToReplace))
            {
                return input;
            }

            var pattern = Regex.Escape(segmentToReplace);
            var regex = RegexCache.GetOrAdd(pattern, p => new Regex(p, RegexOptions.Compiled));

            return regex.Replace(input, replacement, 1);
        }

        /// <summary>
        /// Replaces the last occurrence of a specified substring with another string.
        /// This method is case-sensitive.
        /// </summary>
        /// <param name="input">The original string.</param>
        /// <param name="segmentToReplace">The substring to replace.</param>
        /// <param name="replacement">The string to replace the last occurrence of segmentToReplace.</param>
        /// <returns>A new string with the last occurrence replaced.</returns>
        public static string ReplaceLast(this string input, string segmentToReplace, string replacement)
        {
            if (input.IsNullOrWhiteSpace() || string.IsNullOrEmpty(segmentToReplace))
            {
                return input;
            }

            int place = input.LastIndexOf(segmentToReplace, StringComparison.Ordinal);

            return place == -1
                ? input
                : input.Remove(place, segmentToReplace.Length).Insert(place, replacement);
        }

        /// <summary>
        /// Gets the file name from a full file path, without its extension.
        /// This method is cross-platform safe.
        /// </summary>
        /// <param name="filePath">The full file path.</param>
        /// <returns>The file name without the extension.</returns>
        public static string GetFileNameWithoutExtension(this string filePath)
        {
            // This is the correct, robust, and cross-platform way to get a file name.
            return Path.GetFileNameWithoutExtension(filePath);
        }
    }

#if NET10_0
    /// <summary>
    /// EXPERIMENTAL: Provides extension methods for the string class using the
    /// proposed "implicit extension" syntax.
    /// </summary>
    public implicit extension StringExtensions for string
    {
        // A thread-safe cache for compiled Regex objects to boost performance.
        private static readonly ConcurrentDictionary<string, Regex> RegexCache = new();
        
        /// <summary>
        /// Determines if a string is null, empty, or consists only of white-space characters.
        /// </summary>
        public bool IsNullOrWhiteSpace()
        {
            return string.IsNullOrWhiteSpace(this);
        }

        /// <summary>
        /// Replaces the first occurrence of a specified substring with another string.
        /// </summary>
        public string ReplaceFirst(string segmentToReplace, string replacement)
        {
            if (this.IsNullOrWhiteSpace() || string.IsNullOrEmpty(segmentToReplace))
            {
                return this;
            }
            
            var pattern = Regex.Escape(segmentToReplace);
            var regex = RegexCache.GetOrAdd(pattern, p => new Regex(p, RegexOptions.Compiled));

            return regex.Replace(this, replacement, 1);
        }

        /// <summary>
        /// Replaces the last occurrence of a specified substring with another string.
        /// </summary>
        public string ReplaceLast(string segmentToReplace, string replacement)
        {
            if (this.IsNullOrWhiteSpace() || string.IsNullOrEmpty(segmentToReplace))
            {
                return this;
            }

            int place = this.LastIndexOf(segmentToReplace, StringComparison.Ordinal);

            return place == -1 
                ? this 
                : this.Remove(place, segmentToReplace.Length).Insert(place, replacement);
        }

        /// <summary>
        /// Gets the file name from a full file path, without its extension.
        /// </summary>
        public string GetFileNameWithoutExtension()
        {
            return Path.GetFileNameWithoutExtension(this);
        }
    }
#endif
}