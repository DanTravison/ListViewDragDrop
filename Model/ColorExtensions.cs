using System.Reflection;

namespace ListViewDragDrop.Model;

/// <summary>
/// Provides <see cref="Color"/> extensions
/// </summary>
public static class ColorExtensions
{
    #region Fields

    static readonly Dictionary<string, string> _idToName = [];
    static readonly Dictionary<string, Color> _nameToColor = [];

    /// <summary>
    /// Defines a set of custom colors.
    /// </summary>
    static readonly Tuple<string, Color>[] _custom =
    [
         new("Cerulean", Color.FromRgb(0, 143, 190))
    ];

    #endregion Fields

    static ColorExtensions()
    {
        List<string> names = [];
        foreach (FieldInfo info in typeof(Microsoft.Maui.Graphics.Colors).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            if (info.FieldType == typeof(Color))
            {
                object infoValue = info.GetValue(null);
                if (infoValue == null)
                {
                    continue;
                }
                Color color = (Color)infoValue;
                string id = color.ToArgbHex();
                if (!_idToName.ContainsKey(id))
                {
                    _idToName.Add(id, info.Name);
                }
                _nameToColor.Add(info.Name, color);
                names.Add(info.Name);
            }
        }

        foreach (Tuple<string, Color> tuple in _custom)
        {
            string id = tuple.Item2.ToArgbHex();
            if (!_idToName.ContainsKey(id))
            {
                _idToName.Add(id, tuple.Item1);
            }
            _nameToColor.Add(tuple.Item1, tuple.Item2);
        }
        Names = names;
    }

    /// <summary>
    /// Gets a string name for a <see cref="Color"/>.
    /// </summary>
    /// <param name="c">The <see cref="Color"/> to query.</param>
    /// <returns>The string name of for the color; otherwise, the RGB hex string.</returns>
    public static string Name(this Color c)
    {
        string name;
        if (c != null)
        {
            string id = c.ToHex();
            if (!_idToName.TryGetValue(id, out name))
            {
                name = id;
            }
        }
        else
        {
            name = "[NULL]";
        }
        return name;
    }

    /// <summary>
    /// Gets an <see cref="IEnumerable{String}"/> of all <see cref="Color"/> properties
    /// in <see cref="Colors"/>.
    /// </summary>
    public static IEnumerable<string> Names
    {
        get;
    }

    /// <summary>
    /// Gets a color by it's name.
    /// </summary>
    /// <param name="name">The name or ARGB hex string of the color to get.</param>
    /// <param name="color">The <see cref="Color"/> for the specified <paramref name="name"/>, on success.</param>
    /// <returns>A <see cref="Color"/> for the specified <paramref name="name"/></returns>
    public static bool FromName(string name, out Color color)
    {
        return _nameToColor.TryGetValue(name, out color);
    }
}
