using ListViewDragDrop.DragDrop;
using ListViewDragDrop.ViewModel;
using Syncfusion.Maui.ListView;

namespace ListViewDragDrop;

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
    /// <remarks>
    /// The logic contained here could be refactored into a reusable class.
    /// 1: Define an IDragState interface with an DragState(bool isValid) method
    /// and implement it in DragItemStyle. 
    /// 2: Construct the class with the IDragDropHandler implementation.
    /// 3: Define an public method such as ProcessDragEvent(IDragState, ItemDraggingEventArgs)
    /// 4: Retrieve the DragItemStyle from the SfListView.
    /// 4: Call ProcessDragEvent from here.
    /// </remarks>
    void OnItemDragging(object sender, ItemDraggingEventArgs e)
    {
        object target = null;
        int targetIndex = -1;

        if (e.Action == DragAction.Dragging || e.Action == DragAction.Drop)
        {
            targetIndex = e.NewIndex;
            // Get the target data item. This allows the IDragDropHandler
            // to make decisions based on either the target item or the target index.
            target = ColorList.DataSource.DisplayItems[e.NewIndex];
        }
        bool result = false;

        switch (e.Action)
        {
            case DragAction.Start:
                result = _model.CanDrag(e.DataItem, e.NewIndex);
                e.Cancel = !result;
                break;

            case DragAction.Dragging:

                if (e.OldIndex != e.NewIndex)
                {
                    result = _model.CanDrop(e.DataItem, e.OldIndex, target, targetIndex);
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
                    result = _model.Drop(e.DataItem, e.OldIndex, target, targetIndex);
                }
                // Cancel the request if the drop location is not valid or if the drop location
                // is the original location of the dragged item.
                e.Cancel = !result;
                break;
        }
        // NOTE: Assuming the DragItemStyle can change because it is a
        // BindableProperty.  As such, request it on each call.
        DragItemStyle.GetDragItemStyle(ColorList).DragState(result);
    }
}