using NSubstitute;
using NSubstitute.ClearExtensions;

namespace Tests;

public class GameSessionTest : IDisposable
{
    IGameRepository? _gameRepository;
    private readonly List<GameAggregate> _gameAggregates = new();

    public GameSessionTest()
    {
        _gameRepository = Substitute.For<IGameRepository>();

        _gameRepository
            .GetById(Arg.Any<Guid>())
            .Returns((id) =>
                _gameAggregates
                .FirstOrDefault(g => g.Id == id.Arg<Guid>())
            );

        _gameRepository.Save(Arg.Any<GameAggregate>())
            .Returns((gameAggregate) =>
            {
                _gameAggregates.Add(gameAggregate.Arg<GameAggregate>());
                return gameAggregate.Arg<GameAggregate>();
            });
    }

    public void Dispose()
    {
        _gameRepository.ClearSubstitute();
        _gameAggregates.Clear();
    }

    [Fact]
    public void StartGame_ShouldInitializeGameAggregateWithPlayersAndGrid()
    {
        // Arrange
        var gameSession = new GameSession(_gameRepository!);
        var _playerX = new PlayerX("Player X");
        var _playerO = new PlayerO("Player O");
        // Act
        var gameAggregate = gameSession.StartGame(_playerX.Name, _playerO.Name);

        // Assert
        Assert.NotNull(gameAggregate);
        Assert.IsType<GameAggregate>(gameAggregate);
        Assert.Equal(_playerX.Name, gameAggregate.PlayerX.Name);
        Assert.Equal(_playerO.Name, gameAggregate.PlayerO.Name);
        Assert.NotNull(gameAggregate.Grid);
    }

    [Fact]
    public void MakeMove_ShouldThrownIfGameIsNotFound()
    {
        // Arrange
        var gameSession = new GameSession(_gameRepository!);
        var gameId = Guid.NewGuid();
        var _playerX = new PlayerX("Player X");

        var marker = new Marker(0, 0, _playerX);

        // Act
        // Assert
        Assert.Throws<InvalidOperationException>(() =>
            gameSession.MakeMove(gameId, marker)
        );
    }

    [Fact]
    public void MakeMove_ShouldPlaceMarker()
    {
        // Arrange
        var gameSession = new GameSession(_gameRepository!);
        var _playerX = new PlayerX("Player X");
        var _playerO = new PlayerO("Player O");
        var gameAggregate = gameSession.StartGame(_playerX.Name, _playerO.Name);
        // Act
        var moveResult = gameSession.MakeMove(gameAggregate.Id, new Marker(0, 0, gameAggregate.CurrentPlayer));

        // Assert
        Assert.IsType<GameAggregate>(moveResult);
        Assert.Equal(gameAggregate.Id, moveResult.Id);
    }

    [Fact]
    public void MakeMove_ShouldSwithPlayers()
    {
        // Arrange
        var gameSession = new GameSession(_gameRepository!);

        var gameAggregate = gameSession.StartGame("Player 1", "Player 2");
        Player nextPlayer =
            gameAggregate.CurrentPlayer.Id == gameAggregate.PlayerX.Id ? gameAggregate.PlayerO : gameAggregate.PlayerX;

        // Act
        var moveResult = gameSession.MakeMove(gameAggregate.Id, new Marker(0, 0, gameAggregate.CurrentPlayer));

        // Assert
        Assert.IsType<GameAggregate>(moveResult);
        Assert.Equal(gameAggregate.Id, moveResult.Id);
        Assert.Equal(nextPlayer.Id, moveResult.CurrentPlayer.Id);
    }
}
