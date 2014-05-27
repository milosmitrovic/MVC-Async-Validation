using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace mUtility.Validation.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public class ConditionlRequiredAttribute : ValidationAttribute
    {
        /// <summary>
        /// The conditional field name
        /// </summary>
        string conditionalFieldName = string.Empty;
        /// <summary>
        /// The expected value
        /// </summary>
        object expectedValue = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionlRequiredAttribute"/> class.
        /// </summary>
        /// <param name="conditionalFieldName">Name of the conditional field.</param>
        public ConditionlRequiredAttribute(string conditionalFieldName)
        {
            this.conditionalFieldName = conditionalFieldName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionlRequiredAttribute"/> class.
        /// </summary>
        /// <param name="conditionalFieldName">Name of the conditional field.</param>
        /// <param name="expectedValue">The expected value.</param>
        public ConditionlRequiredAttribute(string conditionalFieldName, object expectedValue)
        {
            this.conditionalFieldName = conditionalFieldName;
            this.expectedValue = expectedValue;
        }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(conditionalFieldName))
            {
                var conditionalField = validationContext.ObjectType.GetProperty(conditionalFieldName);
                var conditionalFieldValue = conditionalField.GetValue(validationContext.ObjectInstance, null);

                if (expectedValue == null)
                {
                    if (value == null && conditionalFieldValue != null && !string.IsNullOrWhiteSpace(conditionalFieldValue.ToString()))
                    {
                        return new ValidationResult(base.ErrorMessageString, new List<string> { validationContext.MemberName });
                    }
                }
                else
                {
                    if (conditionalFieldValue.Equals(expectedValue) && value == null)
                    {
                        return new ValidationResult(base.ErrorMessageString, new List<string> { validationContext.MemberName });
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
