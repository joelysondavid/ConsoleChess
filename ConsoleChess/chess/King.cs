using ConsoleChess.board;

namespace ConsoleChess.chess
{
    public class King : Piece
    {
        public King(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "K";
        }
    }
}
