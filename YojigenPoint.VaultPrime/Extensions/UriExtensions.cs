using System.Collections.Specialized;
using System.Web;

namespace YojigenPoint.VaultPrime.Extensions
{
    /// <summary>
    /// Provides useful extension methods for the Uri class.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Gets the base URI without any query parameters.
        /// </summary>
        /// <param name="originalUri">The original Uri.</param>
        /// <returns>A new Uri object without the query string.</returns>
        public static Uri GetBaseUri(this Uri originalUri)
        {
            return new UriBuilder(originalUri) { Query = string.Empty }.Uri;
        }

        /// <summary>
        /// Parses the query string of a Uri and returns it as a NameValueCollection.
        /// </summary>
        /// <param name="uri">The original Uri.</param>
        /// <returns>A NameValueCollection of the query parameters.</returns>
        public static NameValueCollection ParseQueryString(this Uri uri)
        {
            return HttpUtility.ParseQueryString(uri.Query);
        }
    }
}