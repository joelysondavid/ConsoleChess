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

            // Diagonal
            for (int i = Board.Rows - 1; i >= 0; i--)
            {
                for (int j = Board.Columns - 1; j >= 0; j--)
                {
                    position.SetValues(i, j);
                    if (Board.IsPositionValid(position) && CanMove(position) &&
                        Position.Row - i == Position.Column - j && (i + j < Position.Row + Position.Column))
                    {
                        possiblesMovements[i, j] = true;
                        if (Board.ExistsPiece(position))
                            i = -1;
                    }
                }
            }

            for (int i = 0; i <= Board.Rows; i++)
            {
                for (int j = 0; j <= Board.Columns; j++)
                {
                    position.SetValues(i, j);
                    if (Board.IsPositionValid(position) && CanMove(position) &&
                        Position.Row - i == Position.Column - j && (i + j > Position.Row + Position.Column))
                    {
                        possiblesMovements[i, j] = true;
                        if (Board.ExistsPiece(position))
                            i = Board.Rows;
                    }
                }
            }

            // Inverse diagonal
            for (int i = Board.Rows - 1; i >= 0; i--)
            {
                for (int j = Board.Columns - 1; j >= 0; j--)
                {
                    position.SetValues(i, j);
                    if (Board.IsPositionValid(position) && CanMove(position) &&
                        Position.Row + Position.Column == i + j && (i < Position.Row))
                    {
                        possiblesMovements[i, j] = true;
                        if (Board.ExistsPiece(position))
                            i = -1;
                    }
                }
            }

            for (int i = 0; i <= Board.Rows; i++)
            {
                for (int j = 0; j <= Board.Columns; j++)
                {
                    position.SetValues(i, j);
                    if (Board.IsPositionValid(position) && CanMove(position) &&
                        Position.Row + Position.Column == i + j && (j < Position.Column))
                    {
                        possiblesMovements[i, j] = true;
                        if (Board.ExistsPiece(position))
                            i = Board.Rows;
                    }
                }
            }

            return possiblesMovements;
        }
    }
}
