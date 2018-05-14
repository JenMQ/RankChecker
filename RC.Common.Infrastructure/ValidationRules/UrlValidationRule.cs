namespace RC.Common.Infrastructure.ValidationRules
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;

    /// <summary>
    /// A Validation rule which checks if the string provided is a valid URL string
    /// </summary>
    public class UrlValidationRule : ValidationRule
    {
        /// <summary>
        /// Validates if the string provided is a valid URL string.
        /// </summary>
        /// <param name="value">The string to be validated</param>
        /// <param name="cultureInfo">The culture info</param>
        /// <returns>A ValidationResult which is true if the value is a valid URL String or false, otherwise.</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string urlString = value as string;
            if (urlString == null)
            {
                return new ValidationResult(false, "Value cannot be converted to string.");
            }

            if (urlString == string.Empty)
            {
                return new ValidationResult(false, "URL cannot be empty.");
            }

            var isValid = Uri.IsWellFormedUriString(urlString, UriKind.RelativeOrAbsolute);
            if (isValid)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "URL is invalid.");
            }
        }
    }
}
