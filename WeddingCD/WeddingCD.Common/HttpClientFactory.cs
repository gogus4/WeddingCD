using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common
{
    /// <summary>
    /// Factory for HttpClient.
    /// </summary>
    public static class HttpClientFactory
    {
        /// <summary>
        /// The private member of the singleton
        /// </summary>
        private static readonly Lazy<HttpClient> InstancePrivate = new Lazy<HttpClient>(() => BuildHttpClientInstance());

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        /// <value>
        /// The singleton instance.
        /// </value>
        public static HttpClient Instance
        {
            get
            {
                return InstancePrivate.Value;
            }
        }

        /// <summary>
        /// Builds an HttpClient instance.
        /// </summary>
        /// <returns>The built HttpClient instance</returns>
        private static HttpClient BuildHttpClientInstance()
        {
            return new HttpClient(new HttpClientHandler { UseProxy = true });
        }
    }
}