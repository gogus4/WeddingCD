using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.QueryHelpers
{
    /// <summary>
    /// List of supported filter operators
    /// </summary>
    public enum FilterOperator
    {
        /// <summary>
        /// Left operand must be smaller than the right one
        /// </summary>
        IsLessThan = 0,

        /// <summary>
        /// Left operand must be smaller than or equal to the right one
        /// </summary>
        IsLessThanOrEqualTo = 1,

        /// <summary>
        /// Left operand must be equal to the right one
        /// </summary>
        IsEqualTo = 2,

        /// <summary>
        /// Left operand must be different from the right one
        /// </summary>
        IsNotEqualTo = 3,

        /// <summary>
        /// Left operand must be larger than the right one
        /// </summary>
        IsGreaterThanOrEqualTo = 4,

        /// <summary>
        /// Left operand must be larger than or equal to the right one
        /// </summary>
        IsGreaterThan = 5,

        /// <summary>
        /// Left operand must start with the right one
        /// </summary>
        StartsWith = 6,

        /// <summary>
        /// Left operand must end with the right one
        /// </summary>
        EndsWith = 7,

        /// <summary>
        /// Left operand must contain the right one
        /// </summary>
        Contains = 8,

        /// <summary>
        /// Left operand must be contained in the right one
        /// </summary>
        IsContainedIn = 9,

        /// <summary>
        /// Left operand must not contain the right one
        /// </summary>
        DoesNotContain = 10,
    }

    /// <summary>
    /// Describes a filter.
    /// </summary>
    public class FilterDescriptor : IFilterDescriptor
    {
        /// <summary>
        /// Gets or sets the name of the member.
        /// </summary>
        /// <value>
        /// The name of the member.
        /// </value>
        public string MemberName { get; set; }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>
        /// The operator.
        /// </value>
        public FilterOperator Operator { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public object Value { get; set; }
    }
}