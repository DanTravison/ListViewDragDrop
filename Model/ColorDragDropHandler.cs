using ListViewDragDrop.DragDrop;
using Syncfusion.Maui.ListView;
using System.Collections.ObjectModel;

namespace ListViewDragDrop.Model;

/// <summary>
/// Provides an <see cref="IDragDropHandler"/> for a <see cref="ObservableCollection{NamedColor}"/>.
/// </summary>
public sealed class ColorDragDropHandler : IDragDropHandler
{
    readonly ObservableCollection<NamedColor> _colors;

    /// <summary>
    /// Initializes a new instance of this class.
    /// </summary>
    /// <param name="colors">The <see cref="ObservableCollection{NamedColor}"/> to manage.</param>
    /// <param name="updateSource">The <see cref="UpdateSource"/> value.</param>
    public ColorDragDropHandler(ObservableCollection<NamedColor> colors, bool updateSource = true)
    {
        _colors = colors;
        UpdateSource = updateSource;
    }

    /// <summary>
    /// Gets the value indicating if the <see cref="SfListView"/> should
    /// automatically update the source collection when Drop is handled.
    /// </summary>
    /// <value>true to enable updating the collection source by <see cref="SfListView"/>;
    /// otherwise, false to have <see cref="Drop"/> update the source collection.</value>
    public bool UpdateSource
    {
        get;
    }

    /// <summary>
    /// Determines if an item can be dragged.
    /// </summary>
    /// <param name="item">The item to drag.</param>
    /// <param name="itemIndex">The zero-based index of the item to drag.</param>
    /// <returns>true if the item can be dragged; otherwise, false.</returns>
    public bool CanDrag(object item, int itemIndex)
    {
        return (item is NamedColor);
    }

    static bool CanDrop(NamedColor color, NamedColor targetColor)
    {
        // simulate disabling drop for selected _players.
        return color.Name[0] != targetColor.Name[0];
    }

    /// <summary>
    /// Determines if an item can be dropped on a target.
    /// </summary>
    /// <param name="item">The dragged item.</param>
    /// <param name="itemIndex">The zero-based index of the dragged item.</param>
    /// <param name="target">The target item.</param>
    /// <param name="targetIndex">The zero-based index of the target item.</param>
    /// <returns>true if the item can be dropped; otherwise, false.</returns>
    public bool CanDrop(object item, int itemIndex, object target, int targetIndex)
    {
        bool result = false;
        if (item is NamedColor color && target is NamedColor targetColor)
        {
            result = itemIndex == targetIndex || CanDrop(color, targetColor);
        }
        return result;
    }

    /// <summary>
    /// Drops an item on a target.
    /// </summary>
    /// <param name="item">The item to drop.</param>
    /// <param name="itemIndex">The zero-based index of the item to drop.</param>
    /// <param name="target">The target item.</param>
    /// <param name="targetIndex">The zero-based index of the target item.</param>
    /// <returns>true if the item was dropped; otherwise, false.</returns>
    public bool Drop(object item, int itemIndex, object target, int targetIndex)
    {
        bool result = CanDrop(item, itemIndex, target, targetIndex);
        // NOTE: If UpdateSource is true, the SfListView will update the source collection;
        // otherwise, we update the source collection here.
        if (result && !UpdateSource && itemIndex != targetIndex)
        {
            _colors.Move(itemIndex, targetIndex);
        }
        return result;
    }
}
