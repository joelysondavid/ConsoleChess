using board;
using System;

namespace chess
{
    /// <summary>
    /// Represents a pawn
    /// </summary>
    public class Pawn : Piece
    {
        /// <summary>
        /// Match
        /// </summary>
        private ChessMatch Match { get; set; }

        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="board">Board</param>
        /// <param name="color">Color</param>
        public Pawn(Board board, Color color, ChessMatch match) : base(board, color) { Match = match; }

        /// <summary>
        /// Checks if the reported position has an enemy
        /// </summary>
        /// <param name="position">Position to checks</param>
        /// <returns>If there is an enemy</returns>
        private bool ExistsEnemy(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }

        /// <summary>
        /// Represents pawn with string
        /// </summary>
        /// <returns>String 'P'</returns>
        public override string ToString()
        {
            return "P";
        }

        /// <summary>
        /// Implements the possibles movements
        /// </summary>
        /// <returns>Possibles movements</returns>
        public override bool[,] PossibleMovements()
        {
            bool[,] possiblesMovements = new bool[Board.Rows, Board.Columns];
            Position position = new Position(0, 0);

            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    position.SetValues(i, j);
                    if (Color is Color.White)
                    {
                        if ((Board.IsPositionValid(position) && CanMove(position) && Position != null) && (
                           (!Board.ExistsPiece(position) && Position.Row - i == 1 && Position.Column == j) ||
                           (!Board.ExistsPiece(position) && (NumberOfMovements < 1) && Position.Row - i == 2 && Position.Column == j) ||
                           (ExistsEnemy(position) && Position.Row - i == 1 && Position.Column - j == 1) ||
                           (ExistsEnemy(position) && Position.Row - i == 1 && j - Position.Column == 1)))
                            possiblesMovements[i, j] = true;
                    }
                    else if (Color is Color.Black)
                    {
                        if ((Board.IsPositionValid(position) && CanMove(position) && Position != null) && (
                           (!Board.ExistsPiece(position) && i - Position.Row == 1 && Position.Column == j) ||
                           (!Board.ExistsPiece(position) && (NumberOfMovements < 1) && i - Position.Row == 2 && Position.Column == j) ||
                           (ExistsEnemy(position) && i - Position.Row == 1 && j - Position.Column == 1) ||
                           (ExistsEnemy(position) && i - Position.Row == 1 && Position.Column - j == 1)))
                            possiblesMovements[i, j] = true;
                    }
                }
            }
            // Checks enpassant
            if (Position != null)
            {
                Position left = new Position(Position.Row, Position.Column - 1);
                Position right = new Position(Position.Row, Position.Column + 1);
                Piece pieceLeft = null;
                Piece pieceRight = null;
                if (Board.IsPositionValid(left))
                    pieceLeft = Board.Piece(left);
                if (Board.IsPositionValid(right))
                    pieceRight = Board.Piece(right);

                // Black
                if (Position.Row == 4)
                {
                    if (Board.IsPositionValid(left) && ExistsEnemy(left) && Match.EnableEnPassant == pieceLeft)
                        possiblesMovements[left.Row + 1, left.Column] = true;
                    else if (Board.IsPositionValid(right) && ExistsEnemy(right) && Match.EnableEnPassant == pieceRight)
                        possiblesMovements[right.Row + 1, right.Column] = true;
                }
                // White
                else if (Position.Row == 3)
                {
                    if (Board.IsPositionValid(left) && ExistsEnemy(left) && Match.EnableEnPassant == pieceLeft)
                        possiblesMovements[left.Row - 1, left.Column] = true;
                    else if (Board.IsPositionValid(right) && ExistsEnemy(right) && Match.EnableEnPassant == pieceRight)
                        possiblesMovements[right.Row - 1, right.Column] = true;
                }
            }

            return possiblesMovements;
        }

        /// <summary>
        /// Checks is pawn can promotion
        /// </summary>
        /// <param name="color">To checks if a black or white</param>
        public void MakePromotion(Color color)
        {
            if (color is Color.Black)
            {
                if (Position.Row == 7)
                {
                    Console.WriteLine("This pawn was promote, you can promotion a new piece.");
                    Console.WriteLine("Choose any T: Tower, B: Bishop, N: Knight and Q: Queen: ");
                    string choose = Console.ReadLine().ToUpper();

                    Promotions promotion = 0;
                    if (Enum.TryParse<Promotions>(choose, out promotion))
                    {
                        Position pos = Position;
                        Board.RemovePiece(Position);
                        switch (promotion)
                        {
                            case Promotions.T:
                                Board.PutPiece(new Rook(Board, color), pos);
                                break;
                            case Promotions.B:
                                Board.PutPiece(new Bishop(Board, color), pos);
                                break;
                            case Promotions.N:
                                Board.PutPiece(new Knight(Board, color), pos);
                                break;
                            case Promotions.Q:
                                Board.PutPiece(new Queen(Board, color), pos);
                                break;
                        }
                    }
                    else
                        throw new BoardException("Invalid promotion");
                }
            }
            else if (color is Color.White)
            {
                if (Position.Row == 0)
                {
                    Console.WriteLine("This pawn was promote, you can promotion a new piece.");
                    Console.WriteLine("Choose any T: Tower, B: Bishop, N: Knight and Q: Queen: ");
                    string choose = Console.ReadLine().ToUpper();

                    Promotions promotion;
                    if (Enum.TryParse(choose, out promotion))
                    {
                        Position pos = Position;
                        Board.RemovePiece(Position);
                        switch (promotion)
                        {
                            case Promotions.T:
                                Board.PutPiece(new Rook(Board, color), pos);
                                break;
                            case Promotions.B:
                                Board.PutPiece(new Bishop(Board, color), pos);
                                break;
                            case Promotions.N:
                                Board.PutPiece(new Knight(Board, color), pos);
                                break;
                            case Promotions.Q:
                                Board.PutPiece(new Queen(Board, color), pos);
                                break;
                        }
                    }
                    else
                        throw new BoardException("Invalid promotion");
                }
            }
        }

        private enum Promotions : int
        {
            T,
            B,
            N,
            Q
        }
    }
}
