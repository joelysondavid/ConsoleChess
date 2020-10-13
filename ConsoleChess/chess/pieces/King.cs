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
            PossiblesExits = 0;
            bool[,] movements = new bool[Board.Rows, Board.Columns];

            Position position = new Position(0, 0);

            // N
            position.SetValues(Position.Row - 1, Position.Column);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // NE
            position.SetValues(Position.Row - 1, Position.Column + 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // E
            position.SetValues(Position.Row, Position.Column + 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // SE
            position.SetValues(Position.Row + 1, Position.Column + 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // S
            position.SetValues(Position.Row + 1, Position.Column);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // SW
            position.SetValues(Position.Row + 1, Position.Column - 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // W
            position.SetValues(Position.Row, Position.Column - 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            // NW
            position.SetValues(Position.Row - 1, Position.Column - 1);
            if (Board.IsPositionValid(position) && CanMove(position))
            {
                movements[position.Row, position.Column] = true;
                PossiblesExits++;
            }

            return movements;
        }
    }
}
