using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.QueryHelpers
{
    /// <summary>
    /// MemberBinder that allows rebinding.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    public class MemberBinderOverride<TEntity, TProperty> : MemberBinder<TEntity>
    {
        #region Fields

        /// <summary>
        /// The property expression.
        /// </summary>
        private readonly Expression<Func<TEntity, TProperty>> propertyExpression;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberBinderOverride{TEntity, TProperty}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="propertyPath">The property path.</param>
        public MemberBinderOverride(Expression<Func<TEntity, TProperty>> propertyExpression, string propertyPath)
            : base(propertyPath)
        {
            this.propertyExpression = propertyExpression;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the property expression.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The expressin</returns>
        protected override Expression CreatePropertyExpression(ParameterExpression source)
        {
            return this.Rebind(this.propertyExpression, source);
        }

        /// <summary>
        /// Rebinds the specified property expression.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The expression
        /// </returns>
        private Expression Rebind(Expression<Func<TEntity, TProperty>> propertyExpression, ParameterExpression source)
        {
            ParameterExpression oldParameter = propertyExpression.Parameters[0];
            return ParameterRebinder.ReplaceParameter(oldParameter, source, propertyExpression.Body);
        }

        #endregion
    }
}