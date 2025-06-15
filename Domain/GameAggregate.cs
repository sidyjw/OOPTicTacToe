namespace Domain;

public class GameAggregate : Entity
{
    public Grid Grid { get; init; }
    public PlayerX PlayerX { get; init; }
    public PlayerO PlayerO { get; init; }
    public Player CurrentPlayer => _currentPlayer;
    private Player _currentPlayer;

    public GameAggregate(Grid grid, PlayerX playerX, PlayerO playerO)
    {
        Grid = grid;
        PlayerX = playerX;
        PlayerO = playerO;
        _currentPlayer = RandomizePlayers();
    }

    public void MakeMove(Marker marker)
    {
        if (marker.Player.Id != _currentPlayer.Id)
            throw new InvalidOperationException("It's not your turn to play.");

        Grid.PlaceMarker(marker);

        SwitchPlayer();
    }

    private Player RandomizePlayers()
    {
        if (new Random().Next(2) == 0)
            return PlayerX;

        return PlayerO;
    }

    private void SwitchPlayer()
    {
        _currentPlayer = _currentPlayer == PlayerX ? PlayerO : PlayerX;
    }
}
