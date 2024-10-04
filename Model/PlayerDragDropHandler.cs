using ListViewDragDrop.DragDrop;
using Syncfusion.Maui.DataSource.Extensions;

namespace ListViewDragDrop.Model;

/// <summary>
/// Provides a <see cref="Player"/> <see cref="IDragDropHandler"/>
/// </summary>
/// <remarks>
/// This class supports dragging and dropping <see cref="Player"/> objects.
/// The source collection can contain just Player objects or a mix of Player and Team objects.
/// The drop target can be a <see cref="Player"/>, <see cref="Team"/>, or <see cref="GroupResult"/>.
/// GroupResult handles the path where the UI presents a list of all players and the 
/// group property is <see cref="Player.Team"/>.
/// Note that only <see cref="Player"/> objects can be dragged.
/// </remarks>
public sealed class PlayerDragDropHandler : IDragDropHandler
{
    public PlayerDragDropHandler(bool updateSource)
    {
        UpdateSource = updateSource;
    }

    /// <summary>
    /// Don't update the source collection.
    /// </summary>
    /// <remarks>
    /// <see cref="Drop"/> changes the Player.Team value and doesn't require updating the source collection.
    /// </remarks>
    public bool UpdateSource
    {
        get;
    } = false;

    /// <summary>
    /// Determines if an item can be dragged.
    /// </summary>
    /// <param name="item">The item to drag.</param>
    /// <param name="itemIndex">The zero-based index of the item to drag.</param>
    /// <returns>true if the item is a <see cref="Player"/>; otherwise, false.</returns>
    public bool CanDrag(object item, int itemIndex)
    {
        return item is Player;
    }

    /// <summary>
    /// Determines the associated <see cref="Team"/> from a drop target object.
    /// </summary>
    /// <param name="target">The target object to query.</param>
    /// <param name="team">The team associated with the <paramref name="target"/>.</param>
    /// <returns>A resolved <see cref="Team"/> object; otherwise, a null reference.</returns>
    static bool ResolveTeam(object target, out Team team)
    {
        if (target is Team targetTeam)
        {
            team = targetTeam;
        }
        else if (target is Player player)
        {
            team = player.Team;
        }
        else if (target is GroupResult result && result.Key is Team groupTeam)
        {
            team = groupTeam;
        }
        else
        {
            team = null;
        }
        return team != null;
    }

    /// <summary>
    /// Determines if an item can be dropped on a target.
    /// </summary>
    /// <param name="item">The dragged item.</param>
    /// <param name="itemIndex">The zero-based index of the dragged item.</param>
    /// <param name="target">The target item.</param>
    /// <param name="targetIndex">The zero-based index of the target item.</param>
    /// <returns>true if the item can be dropped; otherwise, false.</returns>
    public bool CanDrop(object item, int itemIndex, object target, int targetIndex)
    {
        if (item is Player player)
        {
            return ResolveTeam(target, out _);
        }
        return false;
    }

    /// <summary>
    /// Drops an item on a target.
    /// </summary>
    /// <param name="item">The item to drop.</param>
    /// <param name="itemIndex">The zero-based index of the item to drop.</param>
    /// <param name="target">The target item.</param>
    /// <param name="targetIndex">The zero-based index of the target item.</param>
    /// <returns>true if the item was dropped; otherwise, false.</returns>
    public bool Drop(object item, int itemIndex, object target, int targetIndex)
    {
        bool result = false;

        if (item is Player player)
        {
            if (ResolveTeam(target, out Team team))
            {
                // Change the player's team.
                player.Team = team;
                result = true;
            }
        }

        return result;
    }
}
