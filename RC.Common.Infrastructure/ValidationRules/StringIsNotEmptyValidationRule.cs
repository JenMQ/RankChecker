namespace RC.Common.Infrastructure.ValidationRules
{
    using System.Globalization;
    using System.Windows.Controls;

    /// <summary>
    /// A Validation Rule which checks if the string is empty
    /// </summary>
    public class StringIsNotEmptyValidationRule : ValidationRule
    {
        /// <summary>
        /// Validates a string expected not to be null or empty.
        /// </summary>
        /// <param name="value">The string to be validated</param>
        /// <param name="cultureInfo">The culture info</param>
        /// <returns>A ValidationResult which is true if the value is not null or epmty or false, otherwise.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;
            if (string.IsNullOrEmpty(input))
            {
                return new ValidationResult(false, "Field cannot be empty.");
            }

            return new ValidationResult(true, null);
        }
    }
}
