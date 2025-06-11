namespace Tests;

public class TestGrid
{
    [Fact]
    public void PlaceMarker_CanPlaceMarkerWhenGameIsNotFinished()
    {
        var grid = new Domain.Grid();

        grid.PlaceMarker(new Domain.Position(0, 0, new Domain.Player("Player1", Domain.PlayerType.X)));
        grid.PlaceMarker(new Domain.Position(0, 1, new Domain.Player("Player2", Domain.PlayerType.O)));

        Assert.False(grid.HasWinner);
        Assert.False(grid.Drawn);
    }

    [Fact]
    public void PlaceMarker_CannotPlaceMarkerWhenGameIsFinished()
    {
        var grid = new Domain.Grid();
        var player1 = new Domain.Player("Player1", Domain.PlayerType.X);
        var player2 = new Domain.Player("Player2", Domain.PlayerType.O);

        grid.PlaceMarker(new Domain.Position(0, 0, player1));
        grid.PlaceMarker(new Domain.Position(0, 1, player1));
        grid.PlaceMarker(new Domain.Position(0, 2, player1));

        Assert.True(grid.HasWinner);
        Assert.True(grid.Winner?.Id == player1.Id);
        Assert.Throws<InvalidOperationException>(() =>
            grid.PlaceMarker(new Domain.Position(1, 1, player2))
        );
        Assert.Throws<InvalidOperationException>(() =>
            grid.PlaceMarker(new Domain.Position(1, 1, player1))
        );
    }

    [Fact]
    public void PlaceMarker_Player1IsTheWinner()
    {
        var grid = new Domain.Grid();
        var player1 = new Domain.Player("Player1", Domain.PlayerType.X);

        grid.PlaceMarker(new Domain.Position(0, 0, player1));
        grid.PlaceMarker(new Domain.Position(0, 1, player1));
        grid.PlaceMarker(new Domain.Position(0, 2, player1));

        Assert.True(grid.HasWinner);
        Assert.True(grid.Winner?.Id == player1.Id);
    }

    [Fact]
    public void PlaceMarker_Player2IsTheWinner()
    {
        var grid = new Domain.Grid();
        var player2 = new Domain.Player("Player2", Domain.PlayerType.O);

        grid.PlaceMarker(new Domain.Position(0, 0, player2));
        grid.PlaceMarker(new Domain.Position(0, 1, player2));
        grid.PlaceMarker(new Domain.Position(0, 2, player2));

        Assert.True(grid.HasWinner);
        Assert.True(grid.Winner?.Id == player2.Id);
    }

    [Fact]
    public void PlaceMarker_AllWinningPositions()
    {
        var player = new Domain.Player("Winner", Domain.PlayerType.X);

        // Test all rows
        for (int row = 0; row < 3; row++)
        {
            var grid = new Domain.Grid();
            for (int col = 0; col < 3; col++)
                grid.PlaceMarker(new Domain.Position(row, col, player));
            Assert.True(grid.HasWinner);
            Assert.True(grid.Winner?.Id == player.Id);
        }

        // Test all columns
        for (int col = 0; col < 3; col++)
        {
            var grid = new Domain.Grid();
            for (int row = 0; row < 3; row++)
                grid.PlaceMarker(new Domain.Position(row, col, player));
            Assert.True(grid.HasWinner);
            Assert.True(grid.Winner?.Id == player.Id);
        }

        // Test main diagonal
        {
            var grid = new Domain.Grid();
            for (int i = 0; i < 3; i++)
                grid.PlaceMarker(new Domain.Position(i, i, player));
            Assert.True(grid.HasWinner);
            Assert.True(grid.Winner?.Id == player.Id);
        }

        // Test anti-diagonal
        {
            var grid = new Domain.Grid();
            for (int i = 0; i < 3; i++)
                grid.PlaceMarker(new Domain.Position(i, 2 - i, player));
            Assert.True(grid.HasWinner);
            Assert.True(grid.Winner?.Id == player.Id);
        }
    }
}