namespace ListViewDragDrop.Model;

using ListViewDragDrop.ObjectModel;
using System.ComponentModel;

/// <summary>
/// Provided an abstract base class for a <see cref="Player"/> or <see cref="Team"/>
/// </summary>
public abstract class RosterEntry : ObservableObject
{
    private string _name;

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value, NameChangedEventArgs);
    }

    public override string ToString()
    {
        return _name;
    }

    static readonly PropertyChangedEventArgs NameChangedEventArgs = new(nameof(Name));
}
