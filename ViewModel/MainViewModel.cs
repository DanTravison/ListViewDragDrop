namespace ListViewDragDrop.ViewModel;
using ListViewDragDrop.Model;
using ListViewDragDrop.ObjectModel;
using System.Collections.ObjectModel;
using ListViewDragDrop.DragDrop;

/// <summary>
/// Provides a view model for illustrating drag and drop logic.
/// </summary>
internal class MainViewModel : ObservableObject
{
    /// <summary>
    /// Initializes a new instance of this class.
    /// </summary>
    public MainViewModel()
    {
        Colors = new(NamedColor.All);
        Players = new(_allPlayers);

        ColorDragHandler = new ColorDragDropHandler(Colors, updateSource:true);
        PlayerDragHandler = new PlayerDragDropHandler(updateSource:true);
    }

    /// <summary>
    /// Gets the <see cref="NamedColor"/> collection.
    /// </summary>
    public ObservableCollection<NamedColor> Colors
    {
        get;
    }

    public IDragDropHandler ColorDragHandler
    {
        get;
    }

    public ObservableCollection<Player> Players
    {
        get;
    }

    public IDragDropHandler PlayerDragHandler
    {
        get;
    }

    #region Static Initialization - create the teams and players

    static readonly Team[] _allTeams =
    [
        new Team("Team 1"),
        new Team("Team 2"),
        new Team("Team 3"),
        new Team("Team 4"),
        new Team("Team 5"),
        new Team("Team 6"),
    ];

    static readonly Player[] _allPlayers =
    [
        new Player("John Doe"),
        new Player("Jane Doe"),
        new Player("Sam Smith"),
        new Player("Tom Brown"),
        new Player("Lucy White"),
        new Player("Bob Green"),
        new Player("Alice Black"),
        new Player("Charlie Orange"),
        new Player("Eve Red"),
        new Player("Frank Purple"),
        new Player("Grace Yellow"),
        new Player("Henry Blue"),
        new Player("Ivy Pink"),
        new Player("Jack Gray"),
        new Player("Kate Silver"),
        new Player("Larry Gold"),
        new Player("Molly Copper"),
        new Player("Ned Nickel"),
        new Player("Olive Brass"),
        new Player("Pete Zinc"),
        new Player("Quinn Iron"),
        new Player("Rose Lead"),
        new Player("Stan Mercury"),
        new Player("Tina Platinum"),
        new Player("Uma Titanium"),
        new Player("Vince Uranium"),
        new Player("Wendy Tungsten"),
        new Player("Xavier Silver"),
        new Player("Yvonne Gold"),
        new Player("Zack Copper")
    ];

    static MainViewModel()
    {
        for (int x = 0; x < _allPlayers.Length; x++)
        {
            _allPlayers[x].Team = _allTeams[x % _allTeams.Length];
        }
    }

    #endregion Static Initialization - create the teams and players

}
