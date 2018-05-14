namespace RC.Common.Infrastructure.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// A converter class which converts a bool value
    /// </summary>
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class ZeroToNonVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Initializes a new instance of the ZeroToNonVisibilityConverter class
        /// </summary>
        public ZeroToNonVisibilityConverter()
        {
            // set default values
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        /// <summary>
        /// Gets or sets the Visibility for true value (value is not equal to zero)
        /// /summary>
        public Visibility TrueValue { get; set; }

        /// <summary>
        /// Gets or sets the Visibility for false value (value is equal to zero)
        /// /summary>
        public Visibility FalseValue { get; set; }

        /// <summary>
        /// Converts an value to Visibility
        /// </summary>
        /// <param name="value">the value produced by the binding source</param>
        /// <param name="targetType">the type of the binding target property</param>
        /// <param name="parameter">the converter parameter to use.</param>
        /// <param name="culture">the culture to use in the converter</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return System.Convert.ToInt32(value) != 0 ? TrueValue : FalseValue;
        }

        /// <summary>
        /// Converts Back the Visibility value
        /// </summary>
        /// <param name="value">The value that is produced by the binding target</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("ZeroToNonVisibilityConverter cannot convert back!");
        }
    }
}
