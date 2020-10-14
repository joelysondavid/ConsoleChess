using board;

namespace chess
{
    /// <summary>
    /// Represents a queen
    /// </summary>
    public class Queen : Piece
    {
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="board">Board</param>
        /// <param name="color">Color</param>
        public Queen(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "Q";
        }

        /// <summary>
        /// Possibles movements
        /// </summary>
        /// <returns></returns>
        public override bool[,] PossibleMovements()
        {
            bool[,] possiblesMovements = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);

            // N
            for (int i = Position.Row - 1; i >= 0; i--)
            {
                position.SetValues(i, Position.Column);
                if (Board.IsPositionValid(position) && CanMove(position))
                {
                    possiblesMovements[position.Row, position.Column] = true;
                }

                if (Board.Piece(position) != null && Board.Piece(position).Color != Color || (Board.Piece(position) != null && Board.Piece(position).Color == Color))
                    break;
            }

            // E
            for (int i = Position.Column + 1; i <= 7; i++)
            {
                position.SetValues(Position.Row, i);
                if (Board.IsPositionValid(position) && CanMove(position))
                {
                    possiblesMovements[position.Row, position.Column] = true;
                }

                if ((Board.Piece(position) != null && Board.Piece(position).Color != Color) || (Board.Piece(position) != null && Board.Piece(position).Color == Color))
                    break;
            }

            // S
            for (int i = Position.Row + 1; i <= 7; i++)
            {
                position.SetValues(i, Position.Column);
                if (Board.IsPositionValid(position) && CanMove(position))
                {
                    possiblesMovements[position.Row, position.Column] = true;
                }

                if (Board.Piece(position) != null && Board.Piece(position).Color != Color || (Board.Piece(position) != null && Board.Piece(position).Color == Color))
                    break;
            }

            // W
            for (int i = Position.Column - 1; i >= 0; i--)
            {
                position.SetValues(Position.Row, i);
                if (Board.IsPositionValid(position) && CanMove(position))
                {
                    possiblesMovements[position.Row, position.Column] = true;
                }

                if (Board.Piece(position) != null && Board.Piece(position).Color != Color || (Board.Piece(position) != null && Board.Piece(position).Color == Color))
                    break;
            }

            // NW
            position.SetValues(Position.Row - 1, Position.Column - 1);
            while (Board.IsPositionValid(position) && CanMove(position))
            {
                possiblesMovements[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row - 1, position.Column - 1);
            }

            // NE
            position.SetValues(Position.Row - 1, Position.Column + 1);
            while (Board.IsPositionValid(position) && CanMove(position))
            {
                possiblesMovements[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row - 1, position.Column + 1);
            }

            // SE
            position.SetValues(Position.Row + 1, Position.Column + 1);
            while (Board.IsPositionValid(position) && CanMove(position))
            {
                possiblesMovements[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row + 1, position.Column + 1);
            }

            // SW
            position.SetValues(Position.Row + 1, Position.Column - 1);
            while (Board.IsPositionValid(position) && CanMove(position))
            {
                possiblesMovements[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.SetValues(position.Row + 1, position.Column - 1);
            }

            return possiblesMovements;
        }
    }
}
