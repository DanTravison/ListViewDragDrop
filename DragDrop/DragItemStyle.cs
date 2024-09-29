using ListViewDragDrop.Resources;
using Syncfusion.Maui.ListView;

namespace ListViewDragDrop.DragDrop;

/// <summary>
/// Provides a style for a drag item to indicate its valid or invalid state.
/// </summary>
public sealed class DragItemStyle : BindableObject
{
    #region Constants

    /// <summary>
    /// Defines the default <see cref="ValidColor"/>.
    /// </summary>
    public static readonly Color DefaultValidColor = Colors.Green;

    /// <summary>
    /// Defines the default <see cref="ValidColor"/>.
    /// </summary>
    public static readonly Color DefaultInvalidColor = Colors.Red;

    /// <summary>
    /// Gets the default glyph to indicate a valid drag item.
    /// </summary>
    public const string DefaultValidGlyph = FluentUI.ArrowSortFilled;

    /// <summary>
    /// Gets the default glyph to indicate an invalid drag item.
    /// </summary>
    public const string DefaultInvalidGlyph = FluentUI.PresenceBlocked;

    #endregion Constants

    #region Fields

    string _dragGlyph = DefaultValidGlyph;
    Color _dragColor = DefaultValidColor;

    #endregion Fields

    /// <summary>
    /// Initializes a new instance of this class.
    /// </summary>
    public DragItemStyle()
    {
    }

    /// <summary>
    /// Sets the drag item to the valid or invalid state.
    /// </summary>
    /// <param name="isValid"></param>
    public void DragState(bool isValid)
    {
        if (isValid)
        {
            DragGlyph = ValidGlyph;
            DragColor = ValidColor;
        }
        else
        {
            DragGlyph = InvalidGlyph;
            DragColor = InvalidColor;
        }
    }

    #region Properties

    // NOTE: These properties are read-only and are updated by the class.
    // They are bound to the DragDropItem via data binding
    // instead of using a BindableProperty.

    /// <summary>
    /// Gets the current glyph to display for the drag item.
    /// </summary>
    public string DragGlyph
    {
        get => _dragGlyph;
        private set
        {
            if (value != _dragGlyph)
            {
                _dragGlyph = value;
                OnPropertyChanged(nameof(DragGlyph));
            }
        }
    }

    /// <summary>
    /// Gets the current color to highlight the drag item.
    /// </summary>
    public Color DragColor
    {
        get => _dragColor;
        private set
        {
            if (value == null || _dragColor.ToInt() != value.ToInt())
            {
                _dragColor = value;
                OnPropertyChanged(nameof(DragColor));
            }
        }
    }

    #endregion Properties

    #region Bindable Properties

    #region ValidColor

    /// <summary>
    /// Gets or sets the color to highlight the drag item when it is valid to drag or drop.
    /// </summary>
    /// <remarks>
    /// The default value is <see cref="DefaultValidColor"/>.
    /// </remarks>
    public Color ValidColor
    {
        get => GetValue(ValidColorProperty) as Color;
        set => SetValue(ValidColorProperty, value);
    }

    /// <summary>
    /// Provides a <see cref="BindableProperty"/> for <see cref="ValidColor"/>.
    /// </summary>
    public static readonly BindableProperty ValidColorProperty = BindableProperty.Create
    (
        nameof(ValidColor),
        typeof(Color),
        typeof(DragItemStyle),
        DefaultValidColor
    );

    #endregion ValidColor

    #region ValidGlyph

    /// <summary>
    /// Gets or sets the glyph to highlight the drag item when it is valid to drag or drop.
    /// </summary>
    /// <remarks>
    /// The default value is <see cref="DefaultValidColor"/>.
    /// </remarks>
    public string ValidGlyph
    {
        get => GetValue(ValidGlyphProperty) as string;
        set => SetValue(ValidGlyphProperty, value);
    }

    /// <summary>
    /// Provides a <see cref="BindableProperty"/> for <see cref="ValidGlyph"/>.
    /// </summary>
    public static readonly BindableProperty ValidGlyphProperty = BindableProperty.Create
    (
        nameof(ValidGlyph),
        typeof(string),
        typeof(DragItemStyle),
        DefaultValidGlyph
    );

    #endregion ValidGlyph

    #region InvalidColor

    /// <summary>
    /// Gets or sets the color to highlight the drag item when it is valid to drag or drop.
    /// </summary>
    /// <remarks>
    /// The default value is <see cref="DefaultValidColor"/>.
    /// </remarks>
    public Color InvalidColor
    {
        get => GetValue(InvalidColorProperty) as Color;
        set => SetValue(InvalidColorProperty, value);
    }

    /// <summary>
    /// Provides a <see cref="BindableProperty"/> for <see cref="InvalidColor"/>.
    /// </summary>
    public static readonly BindableProperty InvalidColorProperty = BindableProperty.Create
    (
        nameof(InvalidColor),
        typeof(Color),
        typeof(DragItemStyle),
        DefaultInvalidColor
    );

    #endregion InvalidColor

    #region InvalidGlyph

    /// <summary>
    /// Gets or sets the glyph to highlight the drag item when it is valid to drag or drop.
    /// </summary>
    /// <remarks>
    /// The default value is <see cref="DefaultValidColor"/>.
    /// </remarks>
    public string InvalidGlyph
    {
        get => GetValue(InvalidGlyphProperty) as string;
        set => SetValue(InvalidGlyphProperty, value);
    }

    /// <summary>
    /// Provides a <see cref="BindableProperty"/> for <see cref="InvalidGlyph"/>.
    /// </summary>
    public static readonly BindableProperty InvalidGlyphProperty = BindableProperty.Create
    (
        nameof(InvalidGlyph),
        typeof(string),
        typeof(DragItemStyle),
        DefaultInvalidGlyph
    );

    #endregion InvalidGlyph

    #endregion Bindable Properties

    #region Attached DragItemStyle Property

    /// <summary>
    /// Provides an attached <see cref="DragItemStyle"/> <see cref="SfListView"/> attached property.
    /// </summary>
    public static readonly BindableProperty DragItemStyleProperty = BindableProperty.CreateAttached
    (
        nameof(DragItemStyle),
        typeof(DragItemStyle),
        typeof(SfListView),
        new DragItemStyle(),
        BindingMode.OneWay
    );

    /// <summary>
    /// Provides a get method for <see cref="DragItemStyle"/> attached <see cref="SfListView"/> property.
    /// </summary>
    /// <param name="view">The <see cref="BindableObject"/> to query.</param>
    /// <returns>The <see cref="DragItemStyle"/> defined for the <paramref name="view"/>.</returns>
    public static DragItemStyle GetDragItemStyle(BindableObject view)
    {
        return view.GetValue(DragItemStyleProperty) as DragItemStyle;
    }

    /// <summary>
    /// Provides the set method for <see cref="DragItemStyle"/> attached <see cref="SfListView"/> property..
    /// </summary>
    /// <param name="view">The <see cref="BindableObject"/> to update.</param>
    /// <returns>The <see cref="DragItemStyle"/> to set on the view <paramref name="view"/>.</returns>
    public static void SetDragItemStyle(BindableObject view, DragItemStyle value)
    {
        view.SetValue(DragItemStyleProperty, value);
    }

    #endregion Attached DragItemStyle Property   
}
