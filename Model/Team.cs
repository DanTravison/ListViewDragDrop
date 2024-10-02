namespace ListViewDragDrop.Model;

/// <summary>
/// Provides a team object for the roster.  
/// </summary>
public sealed class Team :  RosterEntry
{
    public Team(string name)
    {
        Name = name;
    }
}
