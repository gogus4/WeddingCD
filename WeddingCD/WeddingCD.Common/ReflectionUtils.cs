using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common
{
    /// <summary>
    /// Contains some nice methods to work with reflection.
    /// </summary>
    public static class ReflectionUtils
    {
        /// <summary>
        /// Extracts the name of a string property from an expression.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">An expression returning the property's name.</param>
        /// <returns>
        /// The name of the property returned by the expression.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The propertyExpression parameter is null</exception>
        /// <exception cref="System.ArgumentException">Argument is not a property</exception>
        /// <exception cref="ArgumentNullException">If the expression is null.</exception>
        /// <exception cref="ArgumentException">If the expression does not represent a property.</exception>
        public static PropertyInfo GetProperty<TObject, TProperty>(Expression<Func<TObject, TProperty>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            MemberExpression body = propertyExpression.Body as MemberExpression;
            if (body == null)
            {
                var unaryExp = propertyExpression.Body as UnaryExpression;
                if (unaryExp != null)
                {
                    body = ((UnaryExpression)unaryExp).Operand as MemberExpression;
                }
            }

            if (body == null)
            {
                throw new ArgumentException("Invalid argument", "propertyExpression");
            }

            var property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException("Argument is not a property", "propertyExpression");
            }

            return property;
        }
    }
}