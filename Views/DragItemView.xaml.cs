namespace ListViewDragDrop.Views;

using ListViewDragDrop.DragDrop;
using Syncfusion.Maui.ListView;

/// <summary>
/// Provides a concrete implementation of a <see cref="ListViewDragItemView{DragItemStyle}"/>
/// drag item visualizer.
/// </summary>
/// <remarks>
/// This visualizer embeds the item's view created by the <see cref="SfListView.ItemTemplate"/>
/// and displays it in a grid with a Label indicating the drag state.
/// </remarks>
public partial class DragItemView : ListViewDragItemView<DragItemStyle>
{
    public DragItemView()
	{
		InitializeComponent();
	}
}