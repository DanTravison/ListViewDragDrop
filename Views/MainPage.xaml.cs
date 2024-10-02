using ListViewDragDrop.DragDrop;
using ListViewDragDrop.ViewModel;
using Syncfusion.Maui.ListView;

namespace ListViewDragDrop.Views;

public partial class MainPage : ContentPage
{
    readonly MainViewModel _model;

    public MainPage()
    {
        BindingContext = _model = new MainViewModel();
        InitializeComponent();
    }

    /// <summary>
    /// Handles the <see cref="SfListView.ItemDragging"/> event.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The <see cref="ItemDraggingEventArgs"/> containing
    /// the event args.</param>
    void OnItemDragging(object sender, ItemDraggingEventArgs e)
    {
        IDragDropHandler handler;
        SfListView listView;

        object target = null;
        int targetIndex = -1;

        if (ReferenceEquals(sender, ColorList))
        {
            handler = _model.ColorDragHandler;
            listView = ColorList;
        }
        else
        {
            handler = _model.PlayerDragHandler;
            listView = PlayerList;
        }

        if (e.Action == DragAction.Dragging || e.Action == DragAction.Drop)
        {
            targetIndex = e.NewIndex;
            // Get the target data item. This allows the IDragDropHandler
            // to make decisions based on either the target item or the target index.
            target = listView.DataSource.DisplayItems[e.NewIndex];
        }
        bool result = false;

        switch (e.Action)
        {
            case DragAction.Start:
                result = handler.CanDrag(e.DataItem, e.NewIndex);
                e.Cancel = !result;
                break;

            case DragAction.Dragging:

                if (e.OldIndex != e.NewIndex)
                {
                    result = handler.CanDrop(e.DataItem, e.OldIndex, target, targetIndex);
                }
                else
                {
                    // NOTE: SfListView raises a DragAction.Dragging event immediately after
                    // the DragAction.Starting event. We consider this a valid
                    // drop location; otherwise, the drag visual shows invalid on the 
                    // initial drag or when the item is dragged back to its original location.
                    result = true;
                }
                break;

            case DragAction.Drop:
                // NOTE: Dropping an item at it's original location
                // is handled as a NOP request.
                if (e.OldIndex != e.NewIndex)
                {
                    result = handler.Drop(e.DataItem, e.OldIndex, target, targetIndex);
                    /* FUTURE: Expand the target if it is a group and not expanded.
                    if (result && target is GroupResult group && !group.IsExpand)
                    {
                        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(100), () => ExpandGroup(listView, group.Key));
                    }
                    */
                }
                // Cancel the request if the drop location is not valid or if the drop location
                // is the original location of the dragged item.
                e.Cancel = !result;
                break;
        }

        // NOTE: Assuming the DragItemStyle can change because it is a
        // BindableProperty.  As such, request it on each call.
        if (DragItemStyle.GetDragItemStyle(ColorList) is INotifyDragState notify)
        {
            notify.StateChanged(result);
        }
    }

    void ExpandGroup(SfListView listView, object key)
    {
        foreach (var group in listView.DataSource.Groups)
        {
            if (ReferenceEquals(group.Key, key))
            {
                listView.ExpandGroup(group);
                break;
            }
        }
    }
}