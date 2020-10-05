using ConsoleChess.board;
using ConsoleChess.chess;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8,8);
            board.PutPiece(new Tower(board, Color.White), new Position(0, 0));
            board.PutPiece(new King(board, Color.White), new Position(7, 4));

            Canvas.PrintBoard(board);
        }
    }
}
