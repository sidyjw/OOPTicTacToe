namespace Application;

public class GameSession
{
    private readonly IGameRepository _gameRepository;

    public GameSession(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public GameAggregate StartGame(string playerXName, string playerOName)
    {
        // Initialize game aggregate with players and grid
        var playerX = new PlayerX(playerXName);
        var playerO = new PlayerO(playerOName);
        var grid = new Grid();

        var gameAggregate = new GameAggregate(grid, playerX, playerO);
        _gameRepository.Save(gameAggregate);

        return gameAggregate;
    }

    public GameAggregate MakeMove(Guid gameId, Marker marker)
    {
        // Logic to retrieve the game aggregate by gameId
        // and make a move using the marker
        var gameAggregate = _gameRepository.GetById(gameId);

        if (gameAggregate == null)
            throw new InvalidOperationException("Game not found.");

        gameAggregate.MakeMove(marker);
        _gameRepository.Update(gameAggregate);

        return gameAggregate;
    }
}

public interface IGameRepository
{
    public GameAggregate GetById(Guid gameId);
    public GameAggregate Save(GameAggregate gameAggregate);
    public GameAggregate Update(GameAggregate gameAggregate);
}