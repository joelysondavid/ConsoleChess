using board;
using chess;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            board.PutPiece(new Tower(board, Color.Black), new Position(0, 0));
            board.PutPiece(new Tower(board, Color.Black), new Position(0, 7));
            board.PutPiece(new Tower(board, Color.Black), new Position(7, 0));

            Canvas.PrintBoard(board);
        }
    }
}
