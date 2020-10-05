using ConsoleChess.board;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8,8);
            Canvas.PrintBoard(board);
        }
    }
}
