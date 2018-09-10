using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.QueryHelpers
{
    /// <summary>
    /// Contains method to support entity binding functionalities, such as Map...
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    public class QueryBinder<TEntity>
    {
        /// <summary>
        /// The member binders.
        /// </summary>
        private readonly Dictionary<string, MemberBinder<TEntity>> memberBinders = new Dictionary<string, MemberBinder<TEntity>>();

        /// <summary>
        /// The query
        /// </summary>
        private IQueryable<TEntity> query;

        /// <summary>
        /// The query parameters
        /// </summary>
        private QueryParameters<TEntity> queryParameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBinder{TEntity}" /> class.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="queryParameters">The query parameters.</param>
        public QueryBinder(IQueryable<TEntity> query, QueryParameters<TEntity> queryParameters)
        {
            this.query = query;
            this.queryParameters = queryParameters;
        }

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public IQueryable<TEntity> Query
        {
            get { return this.query; }
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <returns>The Task to be awaited. Result is the list of entities</returns>
        public async Task<List<TEntity>> ExecuteAsync()
        {
            return await this.query.ToListAsync();
        }

        /// <summary>
        /// Maps the specified property between the model and the entity.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TModelProperty">The type of the model property.</typeparam>
        /// <typeparam name="TEntityProperty">The type of the entity property.</typeparam>
        /// <param name="modelPropertyExpression">The model property expression.</param>
        /// <param name="entityPropertyExpression">The entity property expression.</param>
        /// <returns>The instance for fluent usage</returns>
        public QueryBinder<TEntity> Map<TModel, TModelProperty, TEntityProperty>(
            Expression<Func<TModel, TModelProperty>> modelPropertyExpression,
            Expression<Func<TEntity, TEntityProperty>> entityPropertyExpression)
        {
            string modelPath = modelPropertyExpression.ExtractPath();
            var binder = new MemberBinderOverride<TEntity, TEntityProperty>(entityPropertyExpression, modelPath);
            this.memberBinders[modelPath] = binder;

            return this;
        }

        /// <summary>
        /// Computes the pagination.
        /// </summary>
        public void Paginate()
        {
            if (this.queryParameters.PageSize > 0)
            {
                this.query = this.query.Skip((this.queryParameters.Page - 1) * this.queryParameters.PageSize).Take(this.queryParameters.PageSize);
            }
        }

        /// <summary>
        /// Computes the sorts.
        /// </summary>
        public void Sort()
        {
            IQueryable<TEntity> sortedQuery = null;

            if (this.queryParameters.Sorts != null)
            {
                bool isMultiple = false;
                foreach (SortDescriptor sort in this.queryParameters.Sorts)
                {
                    MemberBinder<TEntity> binder = this.GetBinding(sort.MemberName);
                    sortedQuery = binder.Sort(sortedQuery ?? this.query, sort, isMultiple);
                    isMultiple = true;
                }
            }

            if (sortedQuery != null)
            {
                this.query = sortedQuery;
            }
        }

        /// <summary>
        /// Computes the filters.
        /// </summary>
        public void Filter()
        {
            foreach (var filter in this.queryParameters.Filters)
            {
                this.Filter(filter);
            }
        }

        /// <summary>
        /// Filters using the specified filter descriptor.
        /// </summary>
        /// <param name="filterDescriptor">The filter descriptor.</param>
        private void Filter(IFilterDescriptor filterDescriptor)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity));
            Expression body = this.CreateFilterExpression(parameter, filterDescriptor);
            if (body != null)
            {
                Expression<Func<TEntity, bool>> lambdaExpression = Expression.Lambda<Func<TEntity, bool>>(body, parameter);
                this.query = this.query.Where(lambdaExpression);
            }
        }

        /// <summary>
        /// Creates the filter expression.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="filterDescriptor">The filter descriptor.</param>
        /// <returns>The created expression</returns>
        private Expression CreateFilterExpression(ParameterExpression parameter, IFilterDescriptor filterDescriptor)
        {
            var compositeDescriptor = filterDescriptor as CompositeFilterDescriptor;
            if (compositeDescriptor != null)
            {
                return this.CreateFilterExpression(parameter, compositeDescriptor);
            }

            var singleDescriptor = filterDescriptor as FilterDescriptor;
            if (singleDescriptor != null)
            {
                return this.CreateFilterExpression(parameter, singleDescriptor);
            }

            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Creates the filter expression.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="filterDescriptor">The filter descriptor.</param>
        /// <returns>The created expression</returns>
        private Expression CreateFilterExpression(ParameterExpression parameter, FilterDescriptor filterDescriptor)
        {
            MemberBinder<TEntity> binder = this.GetBinding(filterDescriptor.MemberName);
            return binder.CreateFilterExpression(parameter, filterDescriptor);
        }

        /// <summary>
        /// Creates the filter expression.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="compositeFilterDescriptor">The composite filter descriptor.</param>
        /// <param name="columnNamesForFilterClean">The column names for filter clean.</param>
        /// <returns>The created expression</returns>
        private Expression CreateFilterExpression(
           ParameterExpression parameter,
           CompositeFilterDescriptor compositeFilterDescriptor,
           params string[] columnNamesForFilterClean)
        {
            FilterCompositionLogicalOperator logicalOperator = compositeFilterDescriptor.LogicalOperator;

            Expression parametreInit;

            switch (logicalOperator)
            {
                case FilterCompositionLogicalOperator.And:
                    parametreInit = Expression.Constant(true);
                    break;

                case FilterCompositionLogicalOperator.Or:
                    parametreInit = Expression.Constant(false);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return compositeFilterDescriptor.FilterDescriptors.Aggregate(
                parametreInit,
                (previous, descriptor) =>
                {
                    Expression newFilter = this.CreateFilterExpression(parameter, descriptor);
                    if (previous == null)
                    {
                        return newFilter;
                    }

                    if (newFilter == null)
                    {
                        return Expression.AndAlso(previous, Expression.Constant(true));
                    }

                    switch (logicalOperator)
                    {
                        case FilterCompositionLogicalOperator.And:
                            return Expression.AndAlso(previous, newFilter);

                        case FilterCompositionLogicalOperator.Or:
                            return Expression.OrElse(previous, newFilter);

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                });
        }

        /// <summary>
        /// Returns a ModelBinder object for the given model path, using cache.
        /// </summary>
        /// <param name="modelPath">The model path.</param>
        /// <returns>The ModelBinder</returns>
        private MemberBinder<TEntity> GetBinding(string modelPath)
        {
            if (this.memberBinders.ContainsKey(modelPath))
            {
                return this.memberBinders[modelPath];
            }

            var binder = new MemberBinder<TEntity>(modelPath);
            this.memberBinders[modelPath] = binder;
            return binder;
        }
    }
}