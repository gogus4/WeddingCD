using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.QueryHelpers
{
    /// <summary>
    /// Contains method to support entity members binding functionalities, such as Sort, Filter...
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    public class MemberBinder<TEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberBinder{TEntity}"/> class.
        /// </summary>
        /// <param name="propertyPath">The property path.</param>
        public MemberBinder(string propertyPath)
        {
            this.PropertyPath = propertyPath;
        }

        /// <summary>
        /// Gets the property path.
        /// </summary>
        /// <value>
        /// The property path.
        /// </value>
        public string PropertyPath { get; private set; }

        /// <summary>
        /// Creates the filter expression.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="filterDescriptor">The filter descriptor.</param>
        /// <returns>The created expression</returns>
        public Expression CreateFilterExpression(ParameterExpression parameter, FilterDescriptor filterDescriptor)
        {
            Expression left = this.CreatePropertyExpression(parameter);
            Expression right = Expression.Constant(filterDescriptor.Value);
            Expression body = null;

            if (left != null)
            {
                if (left.Type != right.Type)
                {
                    if (filterDescriptor.Value is string && left.Type == typeof(long))
                    {
                        right = Expression.Constant(Convert.ToInt64(filterDescriptor.Value));
                    }
                    else if (filterDescriptor.Value is string && left.Type == typeof(Guid?))
                    {
                        Guid tmp;
                        if (Guid.TryParse((string)filterDescriptor.Value, out tmp))
                        {
                            right = Expression.Constant(tmp);
                        }
                        else
                        {
                            right = Expression.Constant(Guid.Empty);
                        }
                    }
                    else
                    {
                        right = Expression.Convert(right, left.Type);
                    }
                }

                switch (filterDescriptor.Operator)
                {
                    case FilterOperator.IsLessThan:
                        body = Expression.LessThan(left, right);
                        break;

                    case FilterOperator.IsLessThanOrEqualTo:
                        body = Expression.LessThanOrEqual(left, right);
                        break;

                    case FilterOperator.IsEqualTo:
                        body = Expression.Equal(left, right);
                        break;

                    case FilterOperator.IsNotEqualTo:
                        body = Expression.NotEqual(left, right);
                        break;

                    case FilterOperator.IsGreaterThanOrEqualTo:
                        body = Expression.GreaterThanOrEqual(left, right);
                        break;

                    case FilterOperator.IsGreaterThan:
                        body = Expression.GreaterThan(left, right);
                        break;

                    case FilterOperator.StartsWith:
                        body = this.Method(left, "StartsWith", right);
                        break;

                    case FilterOperator.EndsWith:
                        body = this.Method(left, "EndsWith", right);
                        break;

                    case FilterOperator.Contains:
                        body = this.Method(left, "Contains", right);
                        break;

                    case FilterOperator.IsContainedIn:
                        body = this.Method(right, "Contains", left);
                        break;

                    case FilterOperator.DoesNotContain:
                        body = Expression.Not(this.Method(left, "Contains", right));
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return body;
        }

        /// <summary>
        /// Sorts the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="sortDescriptor">The sort descriptor.</param>
        /// <param name="isMultiple">if set to <c>true</c> there are multiple sorts involved.</param>
        /// <returns>The query</returns>
        public IOrderedQueryable<TEntity> Sort(IQueryable<TEntity> query, SortDescriptor sortDescriptor, bool isMultiple)
        {
            Type type = typeof(TEntity);
            ParameterExpression parameterExpression = Expression.Parameter(type);
            Expression propertyExpression = this.CreatePropertyExpression(parameterExpression);
            LambdaExpression lambdaExpression = Expression.Lambda(propertyExpression, parameterExpression);

            ListSortDirection sortDirection = sortDescriptor.SortDirection;

            MethodInfo method =
                typeof(Queryable).GetMethods().First(
                    m => //// If ascending, invoke OrderBy, else OrderByDescending
                    (isMultiple
                         ? (sortDirection == ListSortDirection.Ascending
                                ? m.Name == "ThenBy"
                                : m.Name == "ThenByDescending")
                         : (sortDirection == ListSortDirection.Ascending
                                ? m.Name == "OrderBy"
                                : m.Name == "OrderByDescending")) && //// The method we want has 2 parameters
                    m.GetParameters().Length == 2);

            return
                (IOrderedQueryable<TEntity>)
                method.MakeGenericMethod(type, propertyExpression.Type).Invoke(
                    null, new object[] { query, lambdaExpression });
        }

        /// <summary>
        /// Creates the property expression.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The created expression
        /// </returns>
        protected virtual Expression CreatePropertyExpression(ParameterExpression source)
        {
            MemberExpression propertyExpression = null;
            string[] propertyNames = this.PropertyPath.Split('.');
            foreach (string propertyName in propertyNames)
            {
                if (propertyName != "null")
                {
                    propertyExpression = Expression.Property((Expression)propertyExpression ?? source, propertyName);
                }
            }

            return propertyExpression;
        }

        /// <summary>
        /// Creates an EF compatible method for the given method name.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The method call expression</returns>
        private MethodCallExpression Method(Expression source, string methodName, Expression parameter)
        {
            MethodInfo method =
                typeof(string).GetMethods().First(
                    m => m.Name == methodName && m.GetParameters().Length == 1 && m.IsPublic && !m.IsStatic);
            return Expression.Call(source, method, parameter);
        }
    }
}