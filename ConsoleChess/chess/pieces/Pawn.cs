using board;
using Microsoft.VisualBasic;

namespace ConsoleChess.chess.pieces
{
    /// <summary>
    /// Represents a pawn
    /// </summary>
    public class Pawn : Piece
    {
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="board">Board</param>
        /// <param name="color">Color</param>
        public Pawn(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "P";
        }

        /// <summary>
        /// Implements the possibles movements
        /// </summary>
        /// <returns>Possibles movements</returns>
        public override bool[,] PossibleMovements()
        {
            bool[,] possiblesMovements = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);

            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    position.SetValues(i, j);
                    if (Color is Color.White)
                    {
                        if (Board.IsPositionValid(position) && CanMove(position) &&
                           (!Board.ExistsPiece(position) && Position.Row - i == 1 && Position.Column == j) ||
                           (!Board.ExistsPiece(position) && (NumberOfMovements < 1) && Position.Row - i == 2 && Position.Column == j) ||
                           (Board.ExistsPiece(position) && Position.Row - i == 1 && Position.Column - j == 1) ||
                           (Board.ExistsPiece(position) && Position.Row - i == 1 && j - Position.Column == 1))
                            possiblesMovements[i, j] = true;

                        else possiblesMovements[i, j] = false;
                    }
                    else if (Color is Color.Black)
                    {
                        if (Board.IsPositionValid(position) && CanMove(position) &&
                           (!Board.ExistsPiece(position) && i - Position.Row == 1 && Position.Column == j) ||
                           (!Board.ExistsPiece(position) && (NumberOfMovements < 1) && i - Position.Row == 2 && Position.Column == j) ||
                           (Board.ExistsPiece(position) && i - Position.Row == 1 && j - Position.Column == 1) ||
                           (Board.ExistsPiece(position) && i - Position.Row == 1 && Position.Column - i == 1))
                            possiblesMovements[i, j] = true;

                        else possiblesMovements[i, j] = false;
                    }

                }
            }

            return possiblesMovements;
        }
    }
}
