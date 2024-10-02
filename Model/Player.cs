namespace ListViewDragDrop.Model;

using System.ComponentModel;

/// <summary>
/// Provides a player object for the roster.
/// </summary>
public sealed class Player : RosterEntry
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
}
