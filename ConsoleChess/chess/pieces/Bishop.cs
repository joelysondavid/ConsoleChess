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
            for (int i = Position.Row - 1; i >= 0; i--)
            {
                for (int j = Position.Column - 1; j >= 0; j--)
                {
                    position.SetValues(i, j);
                    Piece p = Board.Piece(i, j);
                    if (Board.IsPositionValid(position) && Board.ExistsPiece(position) && (p != null & p != this && p.Color == Color))
                    {
                        i = -1;
                        break;
                    }
                    else if ((Board.IsPositionValid(position) && CanMove(position)) && (
                        Position.Row - i == Position.Column - j && (i + j < Position.Row + Position.Column)))
                    {

                        possiblesMovements[i, j] = true;
                    }
                }
            }

            // SE
            for (int i = Position.Row + 1; i < Board.Rows; i++)
            {
                for (int j = Position.Column + 1; j < Board.Columns; j++)
                {
                    position.SetValues(i, j);
                    Piece p = Board.Piece(i, j);
                    if (Board.IsPositionValid(position) && Board.ExistsPiece(position) && (p != null & p != this && p.Color == Color))
                    {
                        i = Board.Rows;
                        break;
                    }
                    else if (Board.IsPositionValid(position) && CanMove(position) &&
                        Position.Row - i == Position.Column - j && (i + j > Position.Row + Position.Column))
                    {
                        possiblesMovements[i, j] = true;
                    }
                }
            }

            // NE
            for (int i = Position.Row - 1; i >= 0; i--)
            {
                for (int j = Position.Column + 1; j < Board.Columns; j++)
                {
                    position.SetValues(i, j);
                    Piece p = Board.Piece(i, j);
                    if (Board.ExistsPiece(position) && (p != null & p != this && p.Color == Color))
                    {
                        i = -1;
                        break;
                    }
                    if (Board.IsPositionValid(position) && CanMove(position) &&
                        Position.Row + Position.Column == i + j && (i < Position.Row))
                    {
                        possiblesMovements[i, j] = true;
                    }
                }
            }

            // SW
            for (int i = Position.Row + 1; i < Board.Rows; i++)
            {
                for (int j = Position.Column + 1; j > 0; j--)
                {
                    position.SetValues(i, j);
                    Piece p = Board.Piece(i, j);
                    if (Board.IsPositionValid(position) && Board.ExistsPiece(position) && (p != null & p != this && p.Color == Color))
                    {
                        i = Board.Rows;
                        break;
                    }
                    if (Board.IsPositionValid(position) && CanMove(position) &&
                        Position.Row + Position.Column == i + j && (j < Position.Column))
                    {
                        possiblesMovements[i, j] = true;
                    }
                }
            }

            return possiblesMovements;
        }
    }
}
