# ListViewDragDrop
A test project demonstrating SfListView ItemDragging with custom drag item visualization.

The goal of the sample is as follows:

1: Provide visual feedback when an item is dragged that indicates
if the target is a valid drop target.

2: Move the logic for determining if an item is draggable and if the 
target item is a valid drop target to the view model.

3: Support updating the source collection from either the SfListView or the view model.
Although the default behavior in the SfListView is useful for most cases, there are 
cases where a simple item move is not sufficient.

4: Ensure a clean separation between the View, ViewModel, and Model.

## The ViewModel
The view model has the following responsibilities:

* Determine if an item is draggable.
* Determine if a target item is a valid drop target.
* Determine if a drop is valid.
* Optionally update the source collection in the drop logic.

The contract is defined by the IDragDropHandler interface which has three methods:
 * bool CanDrag(object item, object itemIndex)
 This method returns true if the item is draggable.

 * bool CanDrop(object item, object itemIndex, object targetItem, object targetIndex)
 This method returns true if the item can be dropped at the target item or index.

 * bool Drop(object item, object itemIndex, object targetItem, object targetIndex)
 This method calls CanDrop to verify the drop and optionally updates the source collection.

IDragDropHandler is implemented by MainViewModel.

In addition to implementing IDragDropHandler, MainViewModel also defines the following properties:

* Colors: Provides the SfListView.ItemsSource

* UpdateSource: provides the value for SfListView.DragDropController.UpdateSource.
MainViewModel.CanDrop uses this property value to determine if it should update the source collection.
 
# Visual Feedback
The goal of the visual feedback is to be able to present the item being dragged with 
addditional visual cues that indicate if the target is a valid drop target.

## DragItemView
The sample accomplishes this using the DragItemView control template. The visual feedback is 
rather simple, it uses a ContentPresenter to display the dragged item with an adjacent
colored Glyph that indicates the current drop status.  It is intended as an example of writing
a custom drag item view.

It is consumed in the SfListView.DragItemTemplate DataTemplate property. The sample
does this in a SfListView style:

```xaml
<Setter Property="DragItemTemplate">
    <DataTemplate>
        <!-- DragItemView uses ListView.ItemTemplate to populate
             the embedded item view and the ListView.DragItemStyle
             attached property to provide drag/drop visual feedback. 
        -->
        <views:DragItemView ListView="{Binding Source={x:Reference ColorList}}"/>
    </DataTemplate>
</Setter>
```
*NOTE:* The DragItemView requires a reference to the SfListView. The reference is used to
retrieve the SfListView.ItemTemplate to render the dragged item.

## DragItemStyle
This is a companion class to the DragItemView and defines properties used for the visual feedback
as well as a method for the application to indicate the current drag state.

The purpose if this class is as follows:

* Defines the Glyphs and associated colors to use for the visual feedback by DragItemView.
  * Other custom dragItemView implementations will have different UI requirements.
* Provides a method to invoke by the application to update the current drag state.
* Defines an attached property, DragItemStyle, to attach it to the associated SfListView.

An instance of the class is typically created in Xaml and the sample application does this
in MainPage.xaml in the SfListView style:

```xaml
<Setter Property="views:DragItemStyle.DragItemStyle">
    <views:DragItemStyle InvalidColor="IndianRed"
                         InvalidGlyph="{x:Static res:FluentUI.PresenceBlocked}"
                         ValidColor="SpringGreen"
                         ValidGlyph="{x:Static res:FluentUI.ArrowSortFilled}"
                         />
</Setter>
```

# Visualizer Helper classes
Two abstract base classes are defined in the DragDrop folder to provide a common interface for creating
the drag item visualizer and it's associated style.

## ListViewDragItemStyle
Provides the abstract base class for a drag item style. It is a bindable object and requires the derived
class to implement INotifyDragState.StateChanged.  

This is where the DragItemStyle attached property is declared.

## ListViewDragItemView<T>
Provides a strongly typed abstract base class for implementing a drag item visualizer.
It provides the core logic for binding the visualizer to the associated drag item style 
and the visualizer to the dragged item. 

* Defines a strongly DragItemStyle property for use in the derived visualizer.
  * The derived class accesses it in Xaml using TemplateBinding DragItemStyle.  
  * An instance of the strongly typed style property is read from the DragItemStyle attached property.

* Defines the property ContentPresenter ItemPresenter.
  * The derived class is expected to name its ContentPresenter with the name 'ItemPresenter'.
  * ContentPresenter.View is set by creating the view from the ListView.ItemTemplate.

* Defines a ListView BindableProperty
  * This is used by the base class to access the ListView.ItemTemplate.
  * The property is set in the consumer's DragItemTemplate DataTemplate

The derived class is used in Xaml when declaring the DragItemTemplate. As shown above, and the sample 
application does this in MainPage.xaml in a SfListView style:

# Putting the pieces together
The sample application demonstrates how to use the DragItemView and DragItemStyle in the SfListView.

MainPage.xaml sets the typical properties on SfListView such as ItemTemplate, ItemsSource, SelectionMode, etc.

For drag and drop, it adds the following:
* It subscribes to the ItemDragging event with OnItemDragging.
* Sets DragStartMode to OnHold.
* Defines and uses a SfListView.Style that includes the DragItemTemplate and DragItemStyle.

MainPage.OnItemDragging proxies the ItemDraggingEventArgs to the appropriate IDragDropHandler method.
For DragAction.Dragging and DragAction.Drop actions, it also retrieves the data object for the target item.

After calling the handler, it sets ItemDraggingEventArgs.Cancel if a cancel is needed and then calls
DragItemStyle.DragState to update the visual feedback.

## Integration Notes/Experience

After successfully integrating this into my production project, I have the following observations:

1: The ItemDragging handler in Mainpage.xaml.cs should be moved to a separate class. For every use case I have,
the code is the same.

In my production code, I already have a derived SfListView class and it seemed the logic place for the logic.

Instead of attempting to replace the ItemDragging event, I added a new property, DragDropHandler that can be 
used instead of ItemDragging. The consumer can use the ItemDragging event directly or set the DragDropHandler. 
When setting the DragDropHandler, the derived ListView will handle the ItemDragging event and DragDropHandler the handler.

This left two open issues, setting DragDropController.UpdateSource and handling drag state changes.

To address DragDropController.UpdateSource, IDragDropHandler has been extended with a read-only 
UpdateSource property. 

The ListView will set DragDropController.UpdateSource to this value when the handler is set. This makes
sense since the handler is the one that knows if it should update the source collection.

To address drag state changes, I changed DragItemStyle to be an abstract base class named
ListViewDragItemStyle and implemented the INotifyDragState interface. 

ListView reads the attached property as an INotifyDragState and uses it, when set, 
to notify state changes. Custom visualizers access the attached property using their strongly 
typed DragItemStyle.

Since ListView depends on DragItemStyle and INotifyDragState, these have been moved to the 
ListView namespace.  In practice, all classes and interfaces in the sample's DragDrop folder
are relocated to my ListView namespace.

For the view models, I define a IDragDropHandler property and set it to an implementation class
specific to the underlying model. 

For the visualizer, most of my use cases use the a common visualizer, similar to the one
in the sample. For my complex case, I'm still deciding if I need a different visualizer or if
I can use the common visualizer.

The visualizer abstraction is really starting to grow on me. It's trivial to experiment with
different alternate visualizers or the same visualizer with various styles.