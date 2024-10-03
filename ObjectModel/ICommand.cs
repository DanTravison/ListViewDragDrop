using System.ComponentModel;
using Input = System.Windows.Input;

namespace ListViewDragDrop.ObjectModel;

/// <summary>
/// Provides an extended <see cref="Input"/> interface.
/// </summary>
public interface ICommand : Input.ICommand, INotifyPropertyChanged
{
    #region Properties

    /// <summary>
    /// Gets or sets the value indicating if the item is enabled.
    /// </summary>
    /// <value>
    /// true if the item is enabled; otherwise, false.
    /// </value>
    bool IsEnabled
    {
        get;
        set;
    }

    /// <summary>
    /// Gets the parameter that was passed to <see cref="Input.ICommand.Execute"/>
    /// </summary>
    /// <value>
    /// The parameter passed to <see cref="Input.ICommand.Execute"/>; otherwise, a null reference.
    /// </value>
    public object Parameter
    {
        get;
    }

    /// <summary>
    /// Gets or sets the display text for the command.
    /// </summary>
    public string Text
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the description of the command.
    /// </summary>
    public string Description
    {
        get;
        set;
    }

    /// <summary>
    /// Gets <see cref="Action{ICommand}"/> to invoke.
    /// </summary>
    public Action<ICommand> Action
    {
        get;
    }

    #endregion Properties
}