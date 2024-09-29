namespace ListViewDragDrop.DragDrop;

/// <summary>
/// Provides an interface for handling drag-and-drop operations in a view model.
/// </summary>
public interface IDragDropHandler
{
    /// <summary>
    /// Determines if an item can be dragged.
    /// </summary>
    /// <param name="item">The item to drag.</param>
    /// <param name="itemIndex">the zero-based index of the <paramref name="item"/>.</param>
    /// <returns>true if the <paramref name="item"/> can be dragged.</returns>
    bool CanDrag(object item, int itemIndex);

    /// <summary>
    /// Determines if an item can be dropped on a target.
    /// </summary>
    /// <param name="item">The item to drag.</param>
    /// <param name="itemIndex">the zero-based index of the <paramref name="item"/>.</param>
    /// <param name="target">The target of the drop.</param>
    /// <param name="targetIndex">The zero-based index of the <paramref name="target"/>.</param>
    /// <returns>true if the <paramref name="item"/> can be dropped on the <paramref name="target"/>;
    /// otherwise, false.</returns>
    bool CanDrop(object item, int itemIndex, object target, int targetIndex);

    /// <summary>
    /// Drops an item on a target.
    /// </summary>
    /// <param name="item">The item to drag.</param>
    /// <param name="itemIndex">the zero-based index of the <paramref name="item"/>.</param>
    /// <param name="target">The target of the drop.</param>
    /// <param name="targetIndex">The zero-based index of the <paramref name="target"/>.</param>
    /// <returns>true if the <paramref name="item"/> was dropped on the <paramref name="target"/>;
    /// otherwise, false.</returns>
    bool Drop(object item, int itemIndex, object target, int targetIndex);
}
