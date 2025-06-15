namespace Tests;

public class TestGameAggregate
{
    [Fact]
    public void MakeMove_CannotMakeMoveWhenGameIsFinished()
    {
        var player1 = new PlayerX("Player1");
        var player2 = new PlayerO("Player2");
        var grid = new Grid();
        var game = new GameAggregate(grid, player1, player2);

        game.MakeMove(new Marker(0, 0, game.CurrentPlayer));
        game.MakeMove(new Marker(1, 0, game.CurrentPlayer));
        game.MakeMove(new Marker(0, 1, game.CurrentPlayer));
        game.MakeMove(new Marker(1, 1, game.CurrentPlayer));
        game.MakeMove(new Marker(0, 2, game.CurrentPlayer));

        Assert.Throws<InvalidOperationException>(() =>
            game.MakeMove(new Marker(1, 1, game.CurrentPlayer))
        );
    }

    [Fact]
    public void MakeMove_CannotMakeMoveWhenNotPlayerTurn()
    {
        var player1 = new PlayerX("Player1");
        var player2 = new PlayerO("Player2");
        var grid = new Grid();
        var game = new GameAggregate(grid, player1, player2);
        Player previusPlayer = game.CurrentPlayer == player1 ? player1 : player2;

        game.MakeMove(new Marker(0, 0, game.CurrentPlayer));

        Assert.Throws<InvalidOperationException>(() =>
            game.MakeMove(new Marker(1, 1, previusPlayer))
        );
    }
}
