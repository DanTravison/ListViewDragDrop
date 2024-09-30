namespace ListViewDragDrop.DragDrop;

using Syncfusion.Maui.ListView;

/// <summary>
/// Provides a an abstract base class for a style consumed by a <see cref="ListViewDragItemView{T}"/>.
/// </summary>
public abstract class ListViewDragItemStyle : BindableObject, INotifyDragState
{
    /// <summary>
    /// Initializes a new instance of this class.
    /// </summary>
    protected ListViewDragItemStyle()
    {
    }

    /// <summary>
    /// Sets the drag item to the valid or invalid state.
    /// </summary>
    /// <param name="isValid">true if the state is valid; otherwise, false.</param>
    public abstract void StateChanged(bool isValid);

    #region Attached DragItemStyle Property

    /// <summary>
    /// Provides an attached <see cref="ListViewDragItemStyle"/> <see cref="ListView"/> attached property.
    /// </summary>
    public static readonly BindableProperty DragItemStyleProperty = BindableProperty.CreateAttached
    (
        "DragItemStyle",
        typeof(ListViewDragItemStyle),
        typeof(SfListView),
        null,
        BindingMode.OneWay
    );

    /// <summary>
    /// Provides a get method for <see cref="ListViewDragItemStyle"/> attached <see cref="ListView"/> property.
    /// </summary>
    /// <param name="view">The <see cref="BindableObject"/> to query.</param>
    /// <returns>The <see cref="ListViewDragItemStyle"/> defined for the <paramref name="view"/>.</returns>
    public static ListViewDragItemStyle GetDragItemStyle(BindableObject view)
    {
        return view.GetValue(DragItemStyleProperty) as ListViewDragItemStyle;
    }

    /// <summary>
    /// Provides the set method for <see cref="ListViewDragItemStyle"/> attached <see cref="ListView"/> property..
    /// </summary>
    /// <param name="view">The <see cref="BindableObject"/> to update.</param>
    /// <returns>The <see cref="ListViewDragItemStyle"/> to set on the view <paramref name="view"/>.</returns>
    public static void SetDragItemStyle(BindableObject view, ListViewDragItemStyle value)
    {
        view.SetValue(DragItemStyleProperty, value);
    }

    #endregion Attached DragItemStyle Property   
}
