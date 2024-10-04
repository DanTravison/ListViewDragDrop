namespace ListViewDragDrop.Model;

/// <summary>
/// Provides a team object for the roster.  
/// </summary>
public sealed class Team :  RosterEntry, IComparable
{
    public Team(string name)
    {
        Name = name;
    }

    #region IComparable 

    public int CompareTo(object obj)
    {
        return base.CompareTo(obj as Team);
    }

    #endregion IComparable 

}
