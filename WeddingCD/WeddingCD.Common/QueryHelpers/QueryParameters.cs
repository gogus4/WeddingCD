using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.QueryHelpers
{
    /// <summary>
    /// Class that represents all the parameters of a query
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class QueryParameters<TEntity>
    {
        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the list of sorts.
        /// </summary>
        /// <value>
        /// The list of sorts.
        /// </value>
        public IList<SortDescriptor> Sorts { get; set; }

        /// <summary>
        /// Gets or sets the list of filters.
        /// </summary>
        /// <value>
        /// The list of filters.
        /// </value>
        public IList<IFilterDescriptor> Filters { get; set; }

        /// <summary>
        /// Executes the query asynchronously.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The Task to be awaited. Result is the query result
        /// </returns>
        public async Task<QueryResult<TEntity>> ExecuteQueryAsync(IQueryable<TEntity> source)
        {
            if (this.Sorts == null || !this.Sorts.Any())
            {
                throw new ArgumentException("A sort is required for the pagination to work");
            }

            var binder = new QueryBinder<TEntity>(source, this);

            // Now filters query
            binder.Filter();

            // Computes the total number of items for pagination
            var total = await binder.Query.CountAsync();

            // Now sorts the output
            binder.Sort();

            // Now computes pagination
            binder.Paginate();

            // Now fetches data
            var data = await binder.ExecuteAsync();

            return new QueryResult<TEntity> { Count = total, Data = data.ToList() };
        }

        /// <summary>
        /// Executes the query asynchronously.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="mappingFunction">The mapping function.</param>
        /// <param name="binders">The binders.</param>
        /// <returns>
        /// The Task to be awaited. Result is the query result
        /// </returns>
        public async Task<QueryResult<TModel>> ExecuteQueryAsync<TModel>(
            IQueryable<TEntity> source,
            Func<TEntity, TModel> mappingFunction,
            Action<QueryBinder<TEntity>> binders = null)
        {
            if (this.Sorts == null || !this.Sorts.Any())
            {
                throw new ArgumentException("A sort is required for the pagination to work");
            }

            var binder = new QueryBinder<TEntity>(source, this);
            if (binders != null)
            {
                binders.Invoke(binder);
            }

            // Now filters query
            binder.Filter();

            // Computes the total number of items for pagination
            var total = await binder.Query.CountAsync();

            // Now sorts the output
            binder.Sort();

            // Now computes pagination
            binder.Paginate();

            // Now fetches data
            var data = await binder.ExecuteAsync();

            return new QueryResult<TModel> { Count = total, Data = data.Select(mappingFunction).ToList() };
        }
    }
}