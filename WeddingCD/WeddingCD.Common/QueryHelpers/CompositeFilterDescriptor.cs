using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.QueryHelpers
{
    /// <summary>
    /// Logical operator used for filter descriptor composition
    /// </summary>
    public enum FilterCompositionLogicalOperator
    {
        /// <summary>
        /// Combines filters with logical AND
        /// </summary>
        And = 0,

        /// <summary>
        /// Combines filters with logical OR
        /// </summary>
        Or = 1,
    }

    /// <summary>
    /// Composite filter descriptor
    /// </summary>
    public class CompositeFilterDescriptor : IFilterDescriptor
    {
        /// <summary>
        /// Gets or sets the filter descriptors.
        /// </summary>
        /// <value>
        /// The filter descriptors.
        /// </value>
        public IList<IFilterDescriptor> FilterDescriptors { get; set; }

        /// <summary>
        /// Gets or sets the logical operator.
        /// </summary>
        /// <value>
        /// The logical operator.
        /// </value>
        public FilterCompositionLogicalOperator LogicalOperator { get; set; }
    }
}