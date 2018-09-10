using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.QueryHelpers
{
    /// <summary>
    /// Provides Helpers to build includes.
    /// </summary>
    public static class Entity
    {
        /// <summary>
        /// Builds member path of an entity.
        /// </summary>
        /// <typeparam name="T">Type of the entity</typeparam>
        /// <param name="expression">Member expression</param>
        /// <param name="basePath">Path from which to start</param>
        /// <returns>
        /// The member path built
        /// </returns>
        public static string Path<T>(
            Expression<Func<T, object>> expression,
            string basePath = null)
        {
            return GetMemberPathRecursivly(expression.Body);
        }

        /// <summary>
        /// Builds member path of an entity.
        /// </summary>
        /// <typeparam name="T">Type of the entity</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="basePath">The base path.</param>
        /// <returns>The member path built</returns>
        public static string Path<T>(this T entity, Expression<Func<T, object>> expression, string basePath = null)
        {
            return Path(expression, basePath);
        }

        /// <summary>
        /// Builds member path of an entity
        /// </summary>
        /// <typeparam name="T1">Type of the entity 1</typeparam>
        /// <typeparam name="T2">Type of the entity 2</typeparam>
        /// <param name="expression1">Member expression</param>
        /// <param name="expression2">The expression 2.</param>
        /// <param name="basePath">Path from which to start</param>
        /// <returns>The member path built</returns>
        public static string Path<T1, T2>(
            Expression<Func<T1, IEnumerable<T2>>> expression1,
            Expression<Func<T2, object>> expression2,
            string basePath = null)
        {
            return string.Format(
                "{0}.{1}",
                GetMemberPathRecursivly(expression1.Body),
                GetMemberPathRecursivly(expression2.Body));
        }

        /// <summary>
        /// Builds member path of an entity
        /// </summary>
        /// <typeparam name="T1">Type of the entity 1</typeparam>
        /// <typeparam name="T2">Type of the entity 2</typeparam>
        /// <typeparam name="T3">Type of the entity 3</typeparam>
        /// <param name="expression1">Member expression</param>
        /// <param name="expression2">The expression 2.</param>
        /// <param name="expression3">The expression 3.</param>
        /// <param name="basePath">Path from which to start</param>
        /// <returns>
        /// The member path built
        /// </returns>
        public static string Path<T1, T2, T3>(
            Expression<Func<T1, IEnumerable<T2>>> expression1,
            Expression<Func<T2, IEnumerable<T3>>> expression2,
            Expression<Func<T3, object>> expression3,
            string basePath = null)
        {
            return string.Format(
                "{0}.{1}.{2}",
                GetMemberPathRecursivly(expression1.Body),
                GetMemberPathRecursivly(expression2.Body),
                GetMemberPathRecursivly(expression3.Body));
        }

        /// <summary>
        /// Builds member path of an entity
        /// </summary>
        /// <typeparam name="T1">Type of the entity 1</typeparam>
        /// <typeparam name="T2">Type of the entity 2</typeparam>
        /// <typeparam name="T3">Type of the entity 3</typeparam>
        /// <typeparam name="T4">Type of the entity 4</typeparam>
        /// <param name="expression1">Member expression</param>
        /// <param name="expression2">The expression 2.</param>
        /// <param name="expression3">The expression 3.</param>
        /// <param name="expression4">The expression 4.</param>
        /// <param name="basePath">Path from which to start</param>
        /// <returns>
        /// The member path built
        /// </returns>
        public static string Path<T1, T2, T3, T4>(
            Expression<Func<T1, IEnumerable<T2>>> expression1,
            Expression<Func<T2, IEnumerable<T3>>> expression2,
            Expression<Func<T3, IEnumerable<T4>>> expression3,
            Expression<Func<T4, object>> expression4,
            string basePath = null)
        {
            return string.Format(
                "{0}.{1}.{2}.{3}",
                GetMemberPathRecursivly(expression1.Body),
                GetMemberPathRecursivly(expression2.Body),
                GetMemberPathRecursivly(expression3.Body),
                GetMemberPathRecursivly(expression4.Body));
        }

        /// <summary>
        /// Builds member path of an entity
        /// </summary>
        /// <typeparam name="T1">Type of the entity 1</typeparam>
        /// <typeparam name="T2">Type of the entity 2</typeparam>
        /// <typeparam name="T3">Type of the entity 3</typeparam>
        /// <typeparam name="T4">Type of the entity 4</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <param name="expression1">Member expression</param>
        /// <param name="expression2">The expression 2.</param>
        /// <param name="expression3">The expression 3.</param>
        /// <param name="expression4">The expression 4.</param>
        /// <param name="expression5">The expression5.</param>
        /// <param name="basePath">Path from which to start</param>
        /// <returns>
        /// The member path built
        /// </returns>
        public static string Path<T1, T2, T3, T4, T5>(
            Expression<Func<T1, IEnumerable<T2>>> expression1,
            Expression<Func<T2, IEnumerable<T3>>> expression2,
            Expression<Func<T3, IEnumerable<T4>>> expression3,
            Expression<Func<T4, IEnumerable<T5>>> expression4,
            Expression<Func<T5, object>> expression5,
            string basePath = null)
        {
            return string.Format(
                "{0}.{1}.{2}.{3}.{4}",
                GetMemberPathRecursivly(expression1.Body),
                GetMemberPathRecursivly(expression2.Body),
                GetMemberPathRecursivly(expression3.Body),
                GetMemberPathRecursivly(expression4.Body),
                GetMemberPathRecursivly(expression5.Body));
        }

        /// <summary>
        /// Build member path from Member expressions recursivly.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="basePath">The base path.</param>
        /// <returns>
        /// The node built member path
        /// </returns>
        private static string GetMemberPathRecursivly(Expression expression, string basePath = null)
        {
            var memberExpression = expression as MemberExpression;

            // Check whether we reached the leaf of the member expression tree
            if (memberExpression != null)
            {
                // Go deeper in recursivity
                basePath = GetMemberPathRecursivly(memberExpression.Expression, basePath);

                // Core action of the recursivity
                if (string.IsNullOrWhiteSpace(basePath))
                {
                    basePath += memberExpression.Member.Name;
                }
                else
                {
                    basePath = string.Format("{0}.{1}", basePath, memberExpression.Member.Name);
                }
            }

            // Leave the recursivity step by step
            return basePath ?? string.Empty;
        }
    }
}