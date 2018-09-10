using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.QueryHelpers
{
    /// <summary>
    /// Represents the result of a Query
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    public class QueryResult<TEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResult{TEntity}"/> class.
        /// </summary>
        public QueryResult()
        {
            this.Data = new List<TEntity>();
            this.Count = 0;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public IList<TEntity> Data { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public int Count { get; set; }
    }
}