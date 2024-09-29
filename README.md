# ListViewEvents
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

*NOTE:* When the SfListView owns updating the source collection, the view model only verifies
the drop is valid.

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
a custom drag item view..

It is consumed in the SfListView.DragItemTemplate DataTemplate property. The sample
does this in a SfListView style:

```xaml
<Setter Property="DragItemTemplate">
    <DataTemplate>
        <!-- DragItemView uses ListView.ItemTemplate to populate
             the embedded item view and the ListView.DragItemStyle
             attached property to provide drag/drop visual feedback. 
        -->
        <dragdrop:DragItemView ListView="{Binding Source={x:Reference ColorList}}"/>
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
in a a SfListView style:

```xaml
<Setter Property="dragdrop:DragItemStyle.DragItemStyle">
    <dragdrop:DragItemStyle InvalidColor="IndianRed"
                            InvalidGlyph="{x:Static res:FluentUI.PresenceBlocked}"
                            ValidColor="SpringGreen"
                            ValidGlyph="{x:Static res:FluentUI.ArrowSortFilled}"
                            />
</Setter>
```
*NOTE:* If the attached property is not set, a default instance is used.

## Putting the pieces together
The sample application demonstrates how to use the DragItemView and DragItemStyle in the SfListView.

MainPage.xaml sets the typical properties on SfListView such as ItemTemplate, ItemsSource, SelectionMode, etc.

For drag and drop, it sets the following:
* It subscribes to the ItemDragging event with OnItemDragging.
* Sets DragStartMode to OnHold.
* Sets a custom style that includes the DragItemTemplate and DragItemStyle.

MainPage.OnItemDragging proxies the ItemDraggingEventArgs to the appropriate IDragDropHandler method.
For DragAction.Dragging and DragAction.Drop actions, it also retrieves the data object for the target item.

After calling the handler, it sets ItemDraggingEventArgs.Cancel if a cancel is needed and then calls
DragItemStyle.DragState update the visual feedback.

## Possible Enhancements
There are a few areas where logic could be better encapsulated or improved:

1: Define an IDragState interface that has an DragState property or method.
The goal is to provide a clear path for the consumer to update the drag state regardless of the drag item view.
In other words, the SfListView.ItemDragging handler should be able to update the state without 
requiring knowledge of the drag item view.

This would also allow passing it to the IDragDropHandler to set the drag state without exposing the view.

2: Move the logic in MainPage.OnItemDragging to a separate class or static method.
MainPage.OnItemDragging would route the call the method with the SfListView.DataSource, the ItemDraggingEventArgs 
and the IDragDropHandler.

When combinded with IDragState, it would allow completely encapsulating drag/drop event logic in a separate class
making the MainPage's ItemDragging event handler a simple passthrough.

## Caveats
* Syncfusion's drag and drop support in SfListView is very good but has one usability issue (In my opinion). 
To initiate a drag, it appears a long press is required. As a user, I find this takes getting used to, esspecially
when using a mouse on the desktop.  I would prefer a click and drag to initiate a drag but I have not found a way to
accomplish this without bypassing SfListView's built-in drag and drop support.

* I have made no attempt to prototype dragging items between different SfListViews.

* The current code has only been tested on Windows. Since I need this to be cross-platform, I will be testing it on
Android, iOS, and MacCatalyst.

* I have no plans to test on Tizen.
