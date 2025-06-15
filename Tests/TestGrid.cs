namespace Tests;

public class TestGrid
{
    [Fact]
    public void PlaceMarker_CanPlaceMarkerWhenGameIsNotFinished()
    {
        var grid = new Grid();

        grid.PlaceMarker(new Marker(0, 0, new PlayerX("Player1")));
        grid.PlaceMarker(new Marker(0, 1, new PlayerO("Player2")));

        Assert.False(grid.HasWinner);
        Assert.False(grid.Drawn);
    }

    [Fact]
    public void PlaceMarker_CannotPlaceMarkerWhenGameIsFinished()
    {
        var grid = new Grid();
        var player1 = new PlayerX("Player1");
        var player2 = new PlayerX("Player2");

        grid.PlaceMarker(new Marker(0, 0, player1));
        grid.PlaceMarker(new Marker(0, 1, player1));
        grid.PlaceMarker(new Marker(0, 2, player1));

        Assert.True(grid.HasWinner);
        Assert.True(grid.Winner?.Id == player1.Id);
        Assert.Throws<InvalidOperationException>(() =>
            grid.PlaceMarker(new Marker(1, 1, player2))
        );
        Assert.Throws<InvalidOperationException>(() =>
            grid.PlaceMarker(new Marker(1, 1, player1))
        );
    }

    [Fact]
    public void PlaceMarker_Player1IsTheWinner()
    {
        var grid = new Grid();
        var player1 = new PlayerX("Player1");

        grid.PlaceMarker(new Marker(0, 0, player1));
        grid.PlaceMarker(new Marker(0, 1, player1));
        grid.PlaceMarker(new Marker(0, 2, player1));

        Assert.True(grid.HasWinner);
        Assert.True(grid.Winner?.Id == player1.Id);
    }

    [Fact]
    public void PlaceMarker_Player2IsTheWinner()
    {
        var grid = new Grid();
        var player2 = new PlayerO("Player2");

        grid.PlaceMarker(new Marker(0, 0, player2));
        grid.PlaceMarker(new Marker(0, 1, player2));
        grid.PlaceMarker(new Marker(0, 2, player2));

        Assert.True(grid.HasWinner);
        Assert.True(grid.Winner?.Id == player2.Id);
    }

    [Fact]
    public void PlaceMarker_AllWinningPositions()
    {
        var player = new PlayerX("Winner");

        // Test all rows
        for (int row = 0; row < 3; row++)
        {
            var grid = new Grid();
            for (int col = 0; col < 3; col++)
                grid.PlaceMarker(new Marker(row, col, player));
            Assert.True(grid.HasWinner);
            Assert.True(grid.Winner?.Id == player.Id);
        }

        // Test all columns
        for (int col = 0; col < 3; col++)
        {
            var grid = new Grid();
            for (int row = 0; row < 3; row++)
                grid.PlaceMarker(new Marker(row, col, player));
            Assert.True(grid.HasWinner);
            Assert.True(grid.Winner?.Id == player.Id);
        }

        // Test main diagonal
        {
            var grid = new Grid();
            for (int i = 0; i < 3; i++)
                grid.PlaceMarker(new Marker(i, i, player));
            Assert.True(grid.HasWinner);
            Assert.True(grid.Winner?.Id == player.Id);
        }

        // Test anti-diagonal
        {
            var grid = new Grid();
            for (int i = 0; i < 3; i++)
                grid.PlaceMarker(new Marker(i, 2 - i, player));
            Assert.True(grid.HasWinner);
            Assert.True(grid.Winner?.Id == player.Id);
        }
    }

    [Fact]
    public void PlaceMarker_CannotPlaceMarkerWhenPositionIsOccupied()
    {
        var grid = new Grid();
        var player1 = new PlayerX("Player1");
        var player2 = new PlayerO("Player2");

        grid.PlaceMarker(new Marker(0, 0, player1));

        Assert.Throws<InvalidOperationException>(() =>
            grid.PlaceMarker(new Marker(0, 0, player2))
        );
    }

    [Fact]
    public void PlaceMarker_CannotPlaceMarkerWhenDrawn()
    {
        var grid = new Grid();
        var player1 = new PlayerX("Player1");
        var player2 = new PlayerO("Player2");

        // Fill the grid without a winner
        grid.PlaceMarker(new Marker(0, 0, player1));
        grid.PlaceMarker(new Marker(0, 1, player2));
        grid.PlaceMarker(new Marker(0, 2, player1));
        grid.PlaceMarker(new Marker(1, 0, player2));
        grid.PlaceMarker(new Marker(1, 1, player1));
        grid.PlaceMarker(new Marker(1, 2, player2));
        grid.PlaceMarker(new Marker(2, 0, player1));
        grid.PlaceMarker(new Marker(2, 1, player2));
        grid.PlaceMarker(new Marker(2, 2, player1));

        Assert.True(grid.Drawn);

        Assert.Throws<InvalidOperationException>(() =>
            grid.PlaceMarker(new Marker(0, 0, player1))
        );
    }
}