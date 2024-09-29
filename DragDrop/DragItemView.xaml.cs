namespace ListViewDragDrop.DragDrop;

using Syncfusion.Maui.ListView;

/// <summary>
/// Provides a custom drag item view for use in the <see cref="SfListView.DragItemTemplate"/>.
/// </summary>
/// <remarks>
/// This view embeds the item's view created by the <see cref="SfListView.ItemTemplate"/>
/// and displays it in a grid with a Label indicating the drag state.
/// </remarks>
public partial class DragItemView : ContentView
{
    ContentPresenter _itemPresenter;

    public DragItemView()
	{
		InitializeComponent();
	}

    protected override void OnApplyTemplate()
    {
        _itemPresenter = GetTemplateChild("ItemPresenter") as ContentPresenter;
        base.OnApplyTemplate();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (BindingContext != null && ItemTemplate != null)
        {
            // Create the item's view and set it as the content of the ContentPresenter.
            _itemPresenter.Content = ItemTemplate.CreateView(this, BindingContext);
        }
        else
        {
            _itemPresenter.Content = null;
        }
    }

    DataTemplate ItemTemplate
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
        typeof(DragItemView),
        null,
        BindingMode.OneWay,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (bindableObject is DragItemView view)
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
            OnPropertyChanged(DragItemStyle.DragItemStyleProperty.PropertyName);
        }
    }

    private void OnListViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == DragItemStyle.DragItemStyleProperty.PropertyName)
        {
            OnPropertyChanged(e.PropertyName);
        }
    }

    #endregion ListView

    #region DragItemStyle

    public DragItemStyle DragItemStyle
    {
        get
        {
            if (ListView != null)
            {
                return DragItemStyle.GetDragItemStyle(ListView);
            }
            return null;
        }
    }

    #endregion DragItemStyle
}