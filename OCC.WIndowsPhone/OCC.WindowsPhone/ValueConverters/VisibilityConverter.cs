using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OCC.WindowsPhone.ValueConverters
{
    /// <summary>
    /// A type converter for visibility and boolean values.
    /// </summary>
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            var visibility = (bool)value;
            var not=false;
            if (parameter != null)
                bool.TryParse(parameter.ToString(), out not);

            if (not)
                return visibility ?Visibility.Collapsed : Visibility.Visible;
            
            return visibility ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            var visibility = (Visibility)value;
            return (visibility == Visibility.Visible);
        }
    }
}