namespace ListViewDragDrop.Model;

using System.ComponentModel;

/// <summary>
/// Provides a player object for the roster.
/// </summary>
public sealed class Player : RosterEntry, IComparable
{
    Team _team;

    public Player(string name)
    {
        Name = name;
    }

    public Team Team
    {
        get => _team;
        set => SetProperty(ref _team, value, ReferenceEqualityComparer.Instance, TeamChangedEventArgs);
    }

    static readonly PropertyChangedEventArgs TeamChangedEventArgs = new(nameof(Team));

    #region IComparable 

    public int CompareTo(object obj)
    {
        return base.CompareTo(obj as Player);  
    }

    #endregion IComparable 
}
