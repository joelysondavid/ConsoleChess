using board;

namespace chess
{
    /// <summary>
    /// Tower
    /// </summary>
    class Tower : Piece
    {
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="board">Board</param>
        /// <param name="color">Color of piece</param>
        public Tower(Board board, Color color) : base(board, color) { }

        /// <summary>
        /// To string override
        /// </summary>
        /// <returns>Word representing the Tower chess piece</returns>
        public override string ToString()
        {
            return "T";
        }

        /// <summary>
        /// Checks the possibles movements of a piece
        /// </summary>
        /// <returns>List of possible moviments</returns>
        public override bool[,] PossibleMovements()
        {
            bool[,] movements = new bool[Board.Rows, Board.Columns];

            Position position = new Position(0, 0);

            // N
            for (int i = Position.Row - 1; i >= 0; i--)
            {
                position.SetValues(i, Position.Column);
                if (Board.IsPositionValid(position) && CanMove(position))
                {
                    movements[position.Row, position.Column] = true;
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
                    movements[position.Row, position.Column] = true;
                }

                if ((Board.Piece(position) != null && Board.Piece(position).Color != Color) || (Board.Piece(position) != null && Board.Piece(position).Color == Color))
                    break;
            }

            // S
            for (int i = Position.Row+1; i <= 7; i++)
            {
                position.SetValues(i, Position.Column);
                if (Board.IsPositionValid(position) && CanMove(position))
                {
                    movements[position.Row, position.Column] = true;
                }

                if (Board.Piece(position) != null && Board.Piece(position).Color != Color || (Board.Piece(position) != null && Board.Piece(position).Color == Color))
                    break;
            }

            // W
            for (int i = Position.Column-1; i >= 0; i--)
            {
                position.SetValues(Position.Row, i);
                if (Board.IsPositionValid(position) && CanMove(position))
                {
                    movements[position.Row, position.Column] = true;
                }

                if (Board.Piece(position) != null && Board.Piece(position).Color != Color || (Board.Piece(position) != null && Board.Piece(position).Color == Color))
                    break;
            }

            return movements;
        }
    }
}
