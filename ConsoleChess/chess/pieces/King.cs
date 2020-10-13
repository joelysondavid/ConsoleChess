using board;
using System;

namespace chess
{
    /// <summary>
    /// Object that represents a King
    /// </summary>
    public class King : Piece
    {
        /// <summary>
        /// Match
        /// </summary>
        private ChessMatch Match { get; set; }

        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="board">Board</param>
        /// <param name="color">Color of piece</param>
        /// <param name="match">Match</param>
        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
        }

        /// <summary>
        /// To string override
        /// </summary>
        /// <returns>Word representing the King chess piece</returns>
        public override string ToString()
        {
            return "K";
        }

        /// <summary>
        /// Checks the possibles movements of a piece
        /// </summary>
        /// <returns>List of possible moviments</returns>
        public override bool[,] PossibleMovements()
        {
            PossiblesExits = 0;
            bool[,] possiblesMovements = new bool[Board.Rows, Board.Columns];

            Position position = new Position(0, 0);

            // N
            position.SetValues(Position.Row - 1, Position.Column);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                possiblesMovements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // NE
            position.SetValues(Position.Row - 1, Position.Column + 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                possiblesMovements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // E
            position.SetValues(Position.Row, Position.Column + 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                possiblesMovements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // SE
            position.SetValues(Position.Row + 1, Position.Column + 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                possiblesMovements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // S
            position.SetValues(Position.Row + 1, Position.Column);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                possiblesMovements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // SW
            position.SetValues(Position.Row + 1, Position.Column - 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                possiblesMovements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // W
            position.SetValues(Position.Row, Position.Column - 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                possiblesMovements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // NW
            position.SetValues(Position.Row - 1, Position.Column - 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                possiblesMovements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // #SpecialPlay - Castling
            if (NumberOfMovements == 0 && !Match.IsCheck)
            {
                // little castling
                Position posT0 = new Position(Position.Row, Position.Column + 3);
                if (TowerToCastling(posT0))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Board.Piece(p1) is null && Board.Piece(p2) is null)
                    {
                        possiblesMovements[p2.Row, p2.Column] = true;
                    }
                }

                // big castling
                Position posT1 = new Position(Position.Row, Position.Column - 4);
                if (TowerToCastling(posT1))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Board.Piece(p1) is null && Board.Piece(p2) is null && Board.Piece(p3) is null)
                    {
                        possiblesMovements[p2.Row, p2.Column] = true;
                    }
                }
            }

            return possiblesMovements;
        }

        /// <summary>
        /// Tawer is avaliable to castling
        /// </summary>
        /// <param name="position">Position</param>
        /// <returns>If this piece is avaliable to playmake</returns>
        private bool TowerToCastling(Position position)
        {
            if (Board.IsPositionValid(position))
            {
                Piece piece = Board.Piece(position);

                return piece != null && piece is Tower && piece.NumberOfMovements == 0 && piece.Color == Color;
            }
            return false;
        }
    }
}
