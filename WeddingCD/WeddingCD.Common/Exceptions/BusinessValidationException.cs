using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingCD.Common.Exceptions
{
    /// <summary>
    /// Represents a business validation exception.
    /// </summary>
    [Serializable]
    public class BusinessValidationException : WeddingCDException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessValidationException"/> class.
        /// </summary>
        public BusinessValidationException()
        {
            this.ValidationErrors = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessValidationException" /> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="errorMessage">The error message.</param>
        public BusinessValidationException(string propertyName, string errorMessage)
        {
            this.ValidationErrors = new Dictionary<string, List<string>>();
            this.AddError(propertyName, errorMessage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessValidationException"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="errorMessages">The error messages.</param>
        public BusinessValidationException(string propertyName, List<string> errorMessages)
        {
            this.ValidationErrors = new Dictionary<string, List<string>>();
            foreach (var msg in errorMessages)
            {
                this.AddError(propertyName, msg);
            }
        }

        /// <summary>
        /// Gets or sets the validation errors.
        /// </summary>
        /// <value>
        /// The validation errors.
        /// </value>
        public Dictionary<string, List<string>> ValidationErrors { get; set; }

        /// <summary>
        /// Adds the error to the exception.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="errorMessage">The error message.</param>
        public void AddError(string propertyName, string errorMessage)
        {
            if (!this.ValidationErrors.ContainsKey(propertyName))
            {
                this.ValidationErrors.Add(propertyName, new List<string>());
            }

            if (!this.ValidationErrors[propertyName].Contains(errorMessage))
            {
                this.ValidationErrors[propertyName].Add(errorMessage);
            }
        }

        /// <summary>
        /// Replaces the name of the property by another name.
        /// </summary>
        /// <param name="oldPropertyName">Old name of the property.</param>
        /// <param name="newPropertyName">New name of the property.</param>
        /// <returns>true if we replaced something; otherwise false.</returns>
        public bool ReplacePropertyName(string oldPropertyName, string newPropertyName)
        {
            if (this.ValidationErrors.ContainsKey(oldPropertyName))
            {
                var errors = this.ValidationErrors[oldPropertyName];

                this.ValidationErrors.Remove(oldPropertyName);
                this.ValidationErrors.Add(newPropertyName, errors);

                return true;
            }

            return false;
        }
    }
}