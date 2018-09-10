using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.MapPath
{
    /// <summary>
    /// Interface for path mapper
    /// </summary>
    public interface IPathMapper
    {
        /// <summary>
        /// Equivalent to Server.MapPath
        /// </summary>
        /// <param name="relativePath">The relative path.</param>
        /// <returns>The path.</returns>
        string MapPath(string relativePath);
    }
}