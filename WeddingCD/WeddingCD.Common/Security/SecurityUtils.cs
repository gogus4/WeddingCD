using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WeddingCD.Common.Security
{
    /// <summary>
    /// Contains general methods related to security.
    /// </summary>
    public static class SecurityUtils
    {
        /// <summary>
        /// Generates an authentication token.
        /// </summary>
        /// <returns>The token</returns>
        public static string GenerateAuthenticationToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[16 * 4];
                rng.GetBytes(tokenData);

                return HttpServerUtility.UrlTokenEncode(tokenData);
            }
        }

        /// <summary>
        /// Generates a confirmation identifier.
        /// </summary>
        /// <returns>The confirmation ID</returns>
        public static string GenerateConfirmationID()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[16 * 4];
                rng.GetBytes(tokenData);

                return HttpServerUtility.UrlTokenEncode(tokenData);
            }
        }

        /// <summary>
        /// Generates a new API key.
        /// </summary>
        /// <returns>The API key</returns>
        public static string GenerateAPIKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[16 * 4];
                rng.GetBytes(tokenData);

                return BitConverter.ToString(tokenData).Replace("-", string.Empty);
            }
        }
    }
}