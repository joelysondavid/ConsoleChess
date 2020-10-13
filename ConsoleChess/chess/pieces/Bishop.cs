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

            // SE
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

            // NE
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

            // SW
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
