static void StartGame()
{
    char[,] board = new char[3, 3]
    {
            { 'X', 'O', ' ' },
            { ' ', 'X', ' ' },
            { 'O', ' ', 'X' }
    };

    int selRow = 0, selCol = 0;

    // Find first empty cell
    FindNextEmpty(board, ref selRow, ref selCol, 1, 0);

    while (true)
    {
        Console.Clear();
        DrawBoard(board, selRow, selCol);

        var key = Console.ReadKey(true);

        int prevRow = selRow, prevCol = selCol;

        if (key.Key == ConsoleKey.UpArrow) selRow = (selRow + 2) % 3;
        if (key.Key == ConsoleKey.DownArrow) selRow = (selRow + 1) % 3;
        if (key.Key == ConsoleKey.LeftArrow) selCol = (selCol + 2) % 3;
        if (key.Key == ConsoleKey.RightArrow) selCol = (selCol + 1) % 3;

        // Skip occupied cells
        if (board[selRow, selCol] != ' ')
        {
            int dRow = selRow - prevRow;
            int dCol = selCol - prevCol;
            FindNextEmpty(board, ref selRow, ref selCol, dRow, dCol);
        }

        if (key.Key == ConsoleKey.Enter)
        {
            if (board[selRow, selCol] == ' ')
            {
                board[selRow, selCol] = 'X'; // Example: always place 'X'
                                             // Optionally break or switch player here
            }
        }
    }
}

static void DrawBoard(char[,] board, int selRow, int selCol)
{
    Console.WriteLine("   0   1   2");
    for (int row = 0; row < 3; row++)
    {
        Console.Write($"{row} ");
        for (int col = 0; col < 3; col++)
        {
            if (row == selRow && col == selCol)
            {
                Console.Write("[");
                Console.Write(board[row, col] == ' ' ? ' ' : board[row, col]);
                Console.Write("]");
            }
            else
            {
                Console.Write(" ");
                Console.Write(board[row, col] == ' ' ? ' ' : board[row, col]);
                Console.Write(" ");
            }
            if (col < 2) Console.Write("|");
        }
        Console.WriteLine();
        if (row < 2)
            Console.WriteLine("  ---+---+---");
    }
}

static void FindNextEmpty(char[,] board, ref int row, ref int col, int dRow, int dCol)
{
    for (int i = 0; i < 9; i++)
    {
        row = (row + dRow + 3) % 3;
        col = (col + dCol + 3) % 3;
        if (board[row, col] == ' ')
            return;
    }
}

StartGame();