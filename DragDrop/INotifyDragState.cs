namespace ListViewDragDrop.DragDrop;

/// <summary>
/// Provides an interface for notifying of drag state changes.
/// </summary>
public interface INotifyDragState
{
    /// <summary>
    /// Notifies the state of the drag operation.
    /// </summary>
    /// <param name="isValid">true if the current state is valid; otherwise, false.</param>
    void StateChanged(bool isValid);
}
