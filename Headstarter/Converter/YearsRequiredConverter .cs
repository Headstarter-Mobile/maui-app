using System.Globalization;

namespace Headstarter.Converter;
public class YearsRequiredConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Protos.PositionYearsRequired years)
        {
            return $"{years.From} - {years.To}";
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}