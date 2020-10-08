using board;

namespace chess
{
    public class King : Piece
    {
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="board">Board</param>
        /// <param name="color">Color of piece</param>
        public King(Board board, Color color) : base(board, color) { }

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
            bool[,] movements = new bool[Board.Rows, Board.Columns];

            Position position = new Position(Position.Row, Position.Column);

            // N
            position.SetValues(Position.Row - 1, Position.Column);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
            }

            // NE
            position.SetValues(Position.Row - 1, Position.Column + 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
            }

            // E
            position.SetValues(Position.Row, Position.Column + 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
            }

            // SE
            position.SetValues(Position.Row + 1, Position.Column + 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
            }

            // S
            position.SetValues(Position.Row + 1, Position.Column);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
            }

            // SW
            position.SetValues(Position.Row + 1, Position.Column - 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
            }

            // W
            position.SetValues(Position.Row, Position.Column - 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
            }

            // NW
            position.SetValues(Position.Row - 1, Position.Column - 1);
            if (Board.PositionIsValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
            }

            return movements;
        }
    }
}
