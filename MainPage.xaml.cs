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

    void OnItemDragging(object sender, ItemDraggingEventArgs e)
    {
        object target = null;
        int targetIndex = -1;

        if (e.Action == DragAction.Dragging || e.Action == DragAction.Drop)
        {
            targetIndex = e.NewIndex;
            // get the target data item
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
                    // We get a Start call right after the Dragging call
                    // with the same item index. Avoid setting the state to invalid.
                    result = true;
                }
                break;

            case DragAction.Drop:
                result = _model.Drop(e.DataItem, e.OldIndex, target, targetIndex);
                e.Cancel = !result;
                break;
        }

        DragItemStyle.GetDragItemStyle(ColorList).DragState(result);
    }
}