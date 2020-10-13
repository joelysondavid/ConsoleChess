using board;
using System;

namespace ConsoleChess.chess.pieces
{
    /// <summary>
    /// Represents the knight piece
    /// </summary>
    public class Knight : Piece
    {
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="board">Board</param>
        /// <param name="color">Color of knight</param>
        public Knight(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "N";
        }

        /// <summary>
        /// Possibles movements
        /// </summary>
        /// <returns>Possibles movements of kinight</returns>
        public override bool[,] PossibleMovements()
        {
            bool[,] possiblesMovements = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);

            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    position.SetValues(i, j);
                    if ((Board.IsPositionValid(position) && CanMove(position)) && (Position.Row - i == 2 && Position.Column - j == 1) ||
                        (Board.IsPositionValid(position) && CanMove(position)) && (Position.Row - i == 2 && j - Position.Column == 1) ||
                        (Board.IsPositionValid(position) && CanMove(position)) && (i - Position.Row == 2 && j - Position.Column == 1) ||
                        (Board.IsPositionValid(position) && CanMove(position)) && (i - Position.Row == 2 && Position.Column - j == 1) ||
                        (Board.IsPositionValid(position) && CanMove(position)) && (Position.Row - i == 1 && Position.Column - j == 2) ||
                        (Board.IsPositionValid(position) && CanMove(position)) && (Position.Row - i == 1 && j - Position.Column == 2) ||
                        (Board.IsPositionValid(position) && CanMove(position)) && (i - Position.Row == 1 && j - Position.Column == 2) ||
                        (Board.IsPositionValid(position) && CanMove(position)) && (i - Position.Row == 1 && Position.Column - j == 2))
                        possiblesMovements[i, j] = true;

                    else possiblesMovements[i, j] = false;
                }
            }
            return possiblesMovements;
        }
    }
}
