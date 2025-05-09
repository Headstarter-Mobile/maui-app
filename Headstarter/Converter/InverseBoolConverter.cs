﻿using System.Globalization;

namespace Headstarter.Converter;

public class InverseBoolConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        => !(bool)value;

    public object? ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
        => !(bool)value;
}
