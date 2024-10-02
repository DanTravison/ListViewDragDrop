using ListViewDragDrop.Resources;
using System.Globalization;

namespace ListViewDragDrop.ViewModel;

/// <summary>
/// Provides an <see cref="IValueConverter"/> to convert a <see cref="bool"/> value to a expand/collapse glyph.
/// </summary>
public sealed class BoolToGlyphConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value)
        {
            return FluentUI.ChevronUpFilled;
        }
        return FluentUI.ChevronDownFilled;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
