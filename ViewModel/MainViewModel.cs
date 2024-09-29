namespace ListViewDragDrop.ViewModel;

using Syncfusion.Maui.ListView;
using ListViewDragDrop.Model;
using ListViewDragDrop.ObjectModel;
using System.Collections.ObjectModel;
using ListViewDragDrop.DragDrop;
using System.ComponentModel;

/// <summary>
/// Provides a view model for illustrating drag and drop logic.
/// </summary>
internal class MainViewModel : ObservableObject, IDragDropHandler
{
    /// <summary>
    /// Determines if <see cref="IDragDropHandler.Drop"/> should update the source
    /// collection.
    /// </summary>
    bool _updateSource;

    /// <summary>
    /// Initializes a new instance of this class.
    /// </summary>
    /// <param name="updateSource">true to update the source collection (Colors) when
    /// Drop is handled; otherwise, false when the SfListView updates the source collection
    /// directly.
    /// <para>
    /// Specify false when <see cref="DragDropController.UpdateSource"/> is set to true, the default; 
    /// otherwise, specify true to have <see cref="IDragDropHandler.Drop"/> update the 
    /// source collection.
    /// </para>
    /// </param>
    public MainViewModel(bool updateSource = false)
    {
        _updateSource = updateSource;
        Colors = new(NamedColor.All);
    }

    /// <summary>
    /// Gets the <see cref="NamedColor"/> collection.
    /// </summary>
    public ObservableCollection<NamedColor> Colors
    {
        get;
    }

    /// <summary>
    /// Gets or sets the value indicating if the <see cref="SfListView"/> should
    /// automatically update the source collection when Drop is handled.
    /// </summary>
    /// <value>true to enable updating the collection source by <see cref="SfListView"/>;
    /// otherwise, false to have <see cref="Drop"/> update the source collection.</value>
    public bool UpdateSource
    {
        get => _updateSource;
        set => SetProperty(ref _updateSource, value, UpdateSourceChangedEventArgs);
    }

    #region IDragDropHandler

    public bool CanDrag(object item, int itemIndex)
    {
        return (item is NamedColor);
    }

    static bool CanDrop(NamedColor color, NamedColor targetColor)
    {
        // simulate disabling drop for selected items.
        return color.Name[0] != targetColor.Name[0];
    }

    public bool CanDrop(object item, int itemIndex, object target, int targetIndex)
    {
        bool result = false;
        if (item is NamedColor color && target is NamedColor targetColor)
        {
            result = itemIndex == targetIndex || CanDrop(color, targetColor);
        }
        return result;
    }

    public bool Drop(object item, int itemIndex, object target, int targetIndex)
    {
        bool result = CanDrop(item, itemIndex, target, targetIndex);
        // NOTE: If UpdateSource is true, the SfListView will update the source collection;
        // otherwise, we update the source collection here.
        if (result && !UpdateSource && itemIndex != targetIndex)
        {
            Colors.Move(itemIndex, targetIndex);
        }
        return result;
    }

    #endregion IDragDropHandler

    static readonly PropertyChangedEventArgs UpdateSourceChangedEventArgs = new(nameof(UpdateSource));
}
