using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.QueryHelpers
{
    /// <summary>
    /// Class that allows to extract a path from an expression.
    /// </summary>
    public static class ExpressionPathExtractor
    {
        #region Public Methods and Operators

        /// <summary>
        /// Extracts the path of an expression.
        /// Only supports members (properties, fields), array indexes [N], and constants.
        /// </summary>
        /// <param name="ex">The expression.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public static string ExtractPath(this Expression ex)
        {
            var builder = new StringBuilder();
            ExtractPath(ex, builder);
            return builder.ToString();
        }

        /// <summary>
        /// Extracts the path for the given expression.
        /// </summary>
        /// <param name="ex">The expression.</param>
        /// <param name="output">The output.</param>
        public static void ExtractPath(this Expression ex, StringBuilder output)
        {
            var visitor = new ExpressionPathVisitor(output);
            visitor.Visit(ex);
        }

        #endregion

        /// <summary>
        /// Stacks the MemberExpressions of an expression tree.
        /// </summary>
        private class ExpressionPathVisitor : ExpressionVisitor
        {
            #region Fields

            /// <summary>
            /// The builder.
            /// </summary>
            private readonly StringBuilder builder;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="ExpressionPathVisitor"/> class.
            /// </summary>
            /// <param name="builder">The builder.</param>
            public ExpressionPathVisitor(StringBuilder builder)
            {
                this.builder = builder;
            }

            #endregion

            #region Methods

            /// <summary>
            /// The visit binary.
            /// </summary>
            /// <param name="node">The node.</param>
            /// <returns>
            /// The <see cref="Expression" />.
            /// </returns>
            protected override Expression VisitBinary(BinaryExpression node)
            {
                if (node.NodeType == ExpressionType.ArrayIndex)
                {
                    node.Left.ExtractPath(this.builder);
                    this.builder.Append("[");
                    node.Right.ExtractPath(this.builder);
                    this.builder.Append("]");
                    return node;
                }

                return base.VisitBinary(node);
            }

            /// <summary>
            /// The visit constant.
            /// </summary>
            /// <param name="node">The node.</param>
            /// <returns>
            /// The <see cref="Expression" />.
            /// </returns>
            protected override Expression VisitConstant(ConstantExpression node)
            {
                this.builder.Append(node.Value);

                return base.VisitConstant(node);
            }

            /// <summary>
            /// The visit member.
            /// </summary>
            /// <param name="node">The node.</param>
            /// <returns>
            /// The <see cref="Expression" />.
            /// </returns>
            protected override Expression VisitMember(MemberExpression node)
            {
                if (node.Member.MemberType == MemberTypes.Field && node.Expression is ConstantExpression)
                {
                    this.builder.Append(((FieldInfo)node.Member).GetValue(((ConstantExpression)node.Expression).Value));
                    return node;
                }

                Expression ex = base.VisitMember(node);
                if (!(node.Expression is ParameterExpression))
                {
                    this.builder.Append(".");
                }

                this.builder.Append(node.Member.Name);
                return ex;
            }

            #endregion
        }
    }
}