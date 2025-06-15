namespace Domain;

public class Grid : Entity
{
    public bool HasWinner { get; private set; } = false;
    public bool Drawn { get; private set; } = false;
    public Player? Winner => _winner;
    private Player? _winner = null;
    public Marker[,] Board => _board;
    private Marker[,] _board = new Marker[3, 3];

    public void PlaceMarker(Marker position)
    {
        if (HasWinner || Drawn)
            throw new InvalidOperationException("Cannot place a marker when the game is already finished.");

        if (position.Row is < 0 or >= 3 || position.Column is < 0 or >= 3)
            throw new ArgumentOutOfRangeException("Row and column must be between 0 and 2.");

        if (_board[position.Row, position.Column] is not null)
            throw new InvalidOperationException("This position is already occupied.");

        _board[position.Row, position.Column] = position;

        CheckWinner();
    }
    private void SetWinner(Player player)
    {
        if (HasWinner)
            throw new InvalidOperationException("A winner has already been set.");

        _winner = player;
        HasWinner = true;
    }
    private void SetDrawn()
    {
        if (HasWinner)
            throw new InvalidOperationException("Cannot set drawn state when there is already a winner.");

        Drawn = true;
    }

    public void CheckWinner()
    {
        // Check rows
        for (int i = 0; i < 3; i++)
        {
            if (_board[i, 0]?.Player == _board[i, 1]?.Player && _board[i, 1]?.Player == _board[i, 2]?.Player && _board[i, 0] is not null)
            {
                SetWinner(_board[i, 0].Player);
                return;
            }
        }

        // Check columns
        for (int i = 0; i < 3; i++)
        {
            if (_board[0, i]?.Player == _board[1, i]?.Player && _board[1, i]?.Player == _board[2, i]?.Player && _board[0, i] is not null)
            {
                SetWinner(_board[0, i].Player);
                return;
            }
        }

        // Check diagonals
        if (_board[0, 0]?.Player == _board[1, 1]?.Player && _board[1, 1]?.Player == _board[2, 2]?.Player && _board[0, 0] is not null)
        {
            SetWinner(_board[0, 0].Player);
            return;
        }

        if (_board[0, 2]?.Player == _board[1, 1]?.Player && _board[1, 1]?.Player == _board[2, 0]?.Player && _board[0, 2] is not null)
        {
            SetWinner(_board[0, 2].Player);
            return;
        }

        // Check for draw
        if (IsFull())
        {
            SetDrawn();
        }
    }
    private bool IsFull()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (_board[i, j] is null)
                    return false;
            }
        }
        return true;
    }
}
public record Marker(int Row, int Column, Player Player);