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
