using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.Exceptions
{
    /// <summary>
    /// Represents a Thermibox exception that is used in the normal code flow.
    /// </summary>
    [Serializable]
    public abstract class NormalCodeFlowThermiboxException : WeddingCDException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NormalCodeFlowThermiboxException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public NormalCodeFlowThermiboxException(string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalCodeFlowThermiboxException"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public NormalCodeFlowThermiboxException(string errorMessage)
            : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NormalCodeFlowThermiboxException"/> class.
        /// </summary>
        public NormalCodeFlowThermiboxException()
        {
        }
    }
}