using board;

namespace chess
{
    /// <summary>
    /// Represents a bishop
    /// </summary>
    public class Bishop : Piece
    {
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="board"></param>
        /// <param name="color"></param>
        public Bishop(Board board, Color color) : base(board, color) { }

        /// <summary>
        /// Overrides string
        /// </summary>
        /// <returns>String that represents a bishop on board</returns>
        public override string ToString()
        {
            return "B";
        }

        /// <summary>
        /// Sets possibles movements by piece
        /// </summary>
        /// <returns>Possibles movements</returns>
        public override bool[,] PossibleMovements()
        {
            bool[,] possiblesMovements = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);

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
