namespace ListViewDragDrop.Model;

/// <summary>
/// Provides an <see cref="IComparer{NamedColor}"/> for comparing two <see cref="NamedColor"/>.
/// </summary>
public class NamedColorComparer : IComparer<NamedColor>
{
    /// <summary>
    /// Provides a <see cref="NamedColorComparer"/> instance that compares by name.
    /// </summary>
    public static readonly NamedColorComparer ByName = new(true);
    /// <summary>
    /// Provides a <see cref="NamedColorComparer"/> instance that compares by ARGB.
    /// </summary>
    public static readonly NamedColorComparer ByARGB = new(false);

    bool _byName;

    private NamedColorComparer(bool byName)
    {
        _byName = byName;
    }

    /// <summary>
    /// Compares two <see cref="NamedColor"/> instances.
    /// </summary>
    /// <param name="x">The first <see cref="NamedColor"/> to compare.</param>
    /// <param name="y">The second <see cref="NamedColor"/> to compare.</param>
    /// <returns>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Value</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term>Less than zero</term>
    ///         <description><paramref name="x"/> is less than <paramref name="y"/>.</description>
    ///     </item>
    ///     <item>
    ///         <term>Zero</term>
    ///         <description>This <paramref name="x"/> is equal to <paramref name="y"/>.</description>
    ///     </item>
    ///     <item>
    ///         <term>Greater than zero.</term>
    ///         <description><paramref name="x"/> is greater than <paramref name="y"/>.</description>
    ///     </item>
    /// </list>
    /// </returns>		
    public int Compare(NamedColor x, NamedColor y)
    {
        int result;
        if (x == null && y == null)
        {
            result = 0;
        }
        else if (x == null)
        {
            result = -1;
        }
        else if (y == null)
        {
            result = 1;
        }
        else if (_byName)
        {
            result = StringComparer.InvariantCultureIgnoreCase.Compare(x.Name, y.Name);
        }
        else
        {
            int diff = x.Color.ToInt() - y.Color.ToInt();
            if (diff < 0)
            {
                result = -1;
            }
            else if (diff > 0)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }
        }
        return result;
    }
}
