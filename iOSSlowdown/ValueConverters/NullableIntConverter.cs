using System.Globalization;

namespace iOSSlowdown.ValueConverters;

public class NullableIntConverter :
    IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        if (targetType != typeof(string) && targetType != typeof(object)) // object is for SelectedItem binding in SettingsPage.
            return new object();

        return value switch
        {
            null  => parameter as string ?? string.Empty,
            int a => a.ToString(culture),
            _     => string.Empty
        };
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (targetType != typeof(int?))
            return null;

        bool success = int.TryParse(value as string, out int intValue);
        return success ? (int?)intValue : null;
    }
}
