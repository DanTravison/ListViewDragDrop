using Syncfusion.Maui.ListView;

namespace ListViewDragDrop.DragDrop;

/// <summary>
/// Provides an abstract base class for a drag item visualizer
/// </summary>
/// <typeparam name="T">The type of <see cref="DragItemStyle"/> consumed by the derived visualizer.</typeparam>
public abstract class ListViewDragItemView<T> : ContentView
    where T : ListViewDragItemStyle
{
    /// <summary>
    /// Gets the content presenter.
    /// </summary>
    /// <remarks>
    /// Derived class must name their ContentPresenter "ItemPresenter" in their XAML.
    /// </remarks>
    protected ContentPresenter ItemPresenter
    {
        get;
        private set;
    }

    /// <summary>
    /// Handles apply template to resolve the <see cref="ItemPresenter"/>.
    /// </summary>
    protected override void OnApplyTemplate()
    {
        ItemPresenter = GetTemplateChild(nameof(ItemPresenter)) as ContentPresenter;
        base.OnApplyTemplate();
    }

    /// <summary>
    /// Handles changes to <see cref="BindingCondition"/> to update the item's view.
    /// </summary>
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (BindingContext != null && ItemTemplate != null)
        {
            // Create the item's view and set it as the content of the ContentPresenter.
            ItemPresenter.Content = ItemTemplate.CreateView(this, BindingContext);
        }
        else
        {
            ItemPresenter.Content = null;
        }
    }

    /// <summary>
    /// Gets the item <see cref="DataTemplate"/>.
    /// </summary>
    protected DataTemplate ItemTemplate
    {
        get
        {
            if (ListView != null)
            {
                return ListView.ItemTemplate;
            }
            return null;
        }
    }

    #region ListView

    /// <summary>
    /// Gets or sets the <see cref="ListView"/>.
    /// </summary>
    public SfListView ListView
    {
        get => GetValue(ListViewProperty) as SfListView;
        set => SetValue(ListViewProperty, value);
    }

    /// <summary>
    /// Provides a <see cref="BindableProperty"/> for <see cref="ListView"/>.
    /// </summary>
    public static readonly BindableProperty ListViewProperty = BindableProperty.Create
    (
        nameof(ListView),
        typeof(SfListView),
        typeof(ListViewDragItemView<T>),
        null,
        BindingMode.OneWay,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (bindableObject is ListViewDragItemView<T> view)
            {
                view.OnListViewChanged(oldValue as SfListView, newValue as SfListView);
            }
        }
    );

    void OnListViewChanged(SfListView oldView, SfListView newView)
    {
        if (oldView != null)
        {
            oldView.PropertyChanged -= OnListViewPropertyChanged;
        }
        if (newView != null)
        {
            newView.PropertyChanged += OnListViewPropertyChanged;
            OnPropertyChanged(ListViewDragItemStyle.DragItemStyleProperty.PropertyName);
        }
    }

    private void OnListViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == ListViewDragItemStyle.DragItemStyleProperty.PropertyName)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }

    #endregion ListView

    #region DragItemStyle

    /// <summary>
    /// Gets the <see cref="DragItemStyle"/>.
    /// </summary>
    public T DragItemStyle
    {
        get
        {
            if (ListView != null)
            {
                return ListViewDragItemStyle.GetDragItemStyle(ListView) as T;
            }
            return null;
        }
    }

    #endregion DragItemStyle
}
