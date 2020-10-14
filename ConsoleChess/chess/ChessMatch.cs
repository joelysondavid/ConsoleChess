using board;
using System.Collections.Generic;

namespace chess
{
    /// <summary>
    /// represents a game of chess
    /// </summary>
    public class ChessMatch
    {
        /// <summary>
        /// Board
        /// </summary>
        public Board Board { get; private set; }

        /// <summary>
        /// Turn
        /// </summary>
        public int TotalMoves { get; private set; }

        /// <summary>
        /// Current player
        /// </summary>
        public Color CurrentPlayer { get; private set; }

        /// <summary>
        /// Checks the match is finished
        /// </summary>
        public bool FinishedMatch { get; private set; }

        /// <summary>
        /// All pieces
        /// </summary>
        public HashSet<Piece> Pieces { get; private set; }

        /// <summary>
        /// Captured pieces
        /// </summary>
        public HashSet<Piece> CapturedPieces { get; private set; }

        /// <summary>
        /// This match is check?
        /// </summary>
        public bool IsCheck { get; set; }

        /// <summary>
        /// En Passant
        /// </summary>
        public Piece EnableEnPassant { get; private set; }

        /// <summary>
        /// builder to start the match
        /// </summary>
        public ChessMatch()
        {
            Board = new Board(8, 8);
            TotalMoves = 1;
            CurrentPlayer = Color.White;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            IsCheck = false;
            EnableEnPassant = null;
            BuildPieces();
        }

        /// <summary>
        /// Validates the origin position
        /// </summary>
        /// <param name="origin">Origin to validate</param>
        public void ValidateOriginPosition(Position origin)
        {
            if (!Board.IsPositionValid(origin))
                throw new BoardException($"Position {origin} is invalid.");

            Piece piece = Board.Piece(origin);

            if (piece == null)
                throw new BoardException("There is not a piece in this chosen position.");

            if (piece.Color != CurrentPlayer)
                throw new BoardException("This piece does not belong to you.");

            if (piece.IsPieceStuck())
                throw new BoardException("This piece is stuck.");
        }

        /// <summary>
        /// Validates target position
        /// </summary>
        /// <param name="position">Target position</param>
        public void ValidatesTargetPosition(Position origin, Position target)
        {
            Piece piece = Board.Piece(origin);

            if (!piece.CanMove(target))
                throw new BoardException("Target position is invalid");

            bool[,] movements = piece.PossibleMovements();

            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (movements[i, j] == true && (target.Row == i && target.Column == j))
                        return;
                }
            }

            throw new BoardException("Target position is invalid");
        }

        /// <summary>
        /// Make a chess move and change the turn player
        /// </summary>
        /// <param name="origin">From</param>
        /// <param name="target">To</param>
        public void MakeChessMove(Position origin, Position target)
        {
            Piece captured = ExecuteMove(origin, target);

            if (KingIsCheck(CurrentPlayer))
            {
                UndoMovement(origin, target, captured);
                throw new BoardException($"You can't put yourself in check");
            }

            IsCheck = KingIsCheck(GetEnemy(CurrentPlayer)) ? true : false;

            TotalMoves++;
            CurrentPlayer = CurrentPlayer == Color.Black ? Color.White : Color.Black;

            // Checks if enpassan is enable
            Piece piece = Board.Piece(target);
            if (piece != null && piece is Pawn && (piece.Position.Row + 2 == origin.Row || piece.Position.Row - 2 == origin.Row))
                EnableEnPassant = piece;
            else EnableEnPassant = null;

            if (piece is Pawn)
            {
                Pawn pawn = piece as Pawn;
                pawn.MakePromotion(pawn.Color);
            }
        }

        /// <summary>
        /// Execute move of chess piece
        /// </summary>
        /// <param name="origin">Origin of piece</param>
        /// <param name="target">Target position</param>
        public Piece ExecuteMove(Position origin, Position target)
        {
            Piece piece = Board.RemovePiece(origin);
            Piece capturedPiece = Board.RemovePiece(target);
            Board.PutPiece(piece, target);

            piece.IncrementMovements();

            if (capturedPiece != null)
                CapturedPieces.Add(capturedPiece);

            if (Board.Piece(target) is King && ((target.Column + 2 == origin.Column) || (target.Column - 2 == origin.Column)))
                ExecuteCastling(Board.Piece(target));

            if (Board.Piece(target) != null && Board.Piece(target) is Pawn && origin.Column != target.Column && capturedPiece == null)
                capturedPiece = ExecuteEnPassant(Board.Piece(target), origin);

            return capturedPiece;
        }

        /// <summary>
        /// Checks if last play was a castling and move the respective tower
        /// </summary>
        /// <param name="king">King</param>
        private void ExecuteCastling(Piece king)
        {
            Piece tower = null;
            Position origin = null;
            Position target = null;
            if (king.Position.Column == 6)
            {
                tower = Board.Piece(king.Position.Row, king.Position.Column + 1);
                if (tower.NumberOfMovements == 0)
                {
                    origin = tower.Position;
                    target = new Position(king.Position.Row, king.Position.Column - 1);
                    Board.RemovePiece(origin);
                    Board.PutPiece(tower, target);
                    tower.IncrementMovements();
                }
            }
            else if (king.Position.Column == 2)
            {
                tower = Board.Piece(king.Position.Row, king.Position.Column - 2);
                if (tower.NumberOfMovements == 0)
                {
                    origin = tower.Position;
                    target = new Position(king.Position.Row, king.Position.Column + 1);
                    Board.RemovePiece(origin);
                    Board.PutPiece(tower, target);
                    tower.IncrementMovements();
                }
            }

            if (KingIsCheck(king.Color) && origin != null)
                UndoMovement(origin, target, null);
        }

        /// <summary>
        /// Playmake enpassant
        /// </summary>
        /// <param name="piece">Would was a pawn</param>
        private Piece ExecuteEnPassant(Piece pawn, Position origin)
        {
            Piece enemyPawn = Board.Piece(origin.Row, pawn.Position.Column);

            return Board.RemovePiece(enemyPawn.Position);
        }

        /// <summary>
        /// Undo the movement itself that would put in check
        /// </summary>
        /// <param name="piece">Current piece</param>
        /// <param name="capturedPiece">Captured piece</param>
        /// <param name="origin">Origin of current piece</param>
        private void UndoMovement(Position origin, Position target, Piece captured)
        {
            Piece piece = Board.RemovePiece(target);

            piece.DecrementMovements();

            if (captured != null)
            {
                if (captured != EnableEnPassant)
                {
                    Board.PutPiece(captured, target);
                    CapturedPieces.Remove(captured);
                }
                else
                {
                    Board.PutPiece(captured, new Position(origin.Row, target.Column));
                    CapturedPieces.Remove(captured);
                }
            }

            Board.PutPiece(piece, origin);
        }

        /// <summary>
        /// Go through a chess board checking if king is checkmate
        /// </summary>
        /// <returns>If the king is in check</returns>
        public bool KingIsCheck(Color color)
        {
            Piece king = GetKing(color);

            if (king == null)
                throw new BoardException($"There is no {color} king on board");

            foreach (Piece p in AvaliablePiecesByColor(GetEnemy(color)))
            {
                bool[,] movements = p.PossibleMovements();

                if (movements[king.Position.Row, king.Position.Column])
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the match is a checkmate
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool IsInCheckmate(Color color)
        {
            if (!KingIsCheck(color))
                return false;

            foreach (Piece p in AvaliablePiecesByColor(color))
            {
                bool[,] moves = p.PossibleMovements();

                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (moves[i, j])
                        {
                            Position origin = p.Position;
                            Position target = new Position(i, j);
                            Piece captured = ExecuteMove(p.Position, target);
                            if (!KingIsCheck(color))
                            {
                                UndoMovement(origin, target, captured);
                                return false;
                            }
                            UndoMovement(origin, target, captured);
                        }
                    }
                }
            }

            FinishedMatch = true;
            return true;
        }

        /// <summary>
        /// Get captured pices by informed color
        /// </summary>
        /// <param name="color">Desired color</param>
        /// <returns>Pieces captured by desired color</returns>
        public HashSet<Piece> GetCapturedPiecesByColor(Color color)
        {
            HashSet<Piece> pieces = new HashSet<Piece>();
            foreach (Piece piece in CapturedPieces)
            {
                if (piece.Color == color)
                    pieces.Add(piece);
            }

            return pieces;
        }

        /// <summary>
        /// Get avaliable pices by informed color
        /// </summary>
        /// <param name="color">Desired color</param>
        /// <returns>Pieces avaliables by desired color</returns>
        public HashSet<Piece> AvaliablePiecesByColor(Color color)
        {
            HashSet<Piece> pieces = new HashSet<Piece>();
            foreach (Piece piece in Pieces)
            {
                if (piece.Color == color)
                    pieces.Add(piece);
            }

            pieces.ExceptWith(GetCapturedPiecesByColor(color));
            return pieces;
        }

        /// <summary>
        /// Get enemy
        /// </summary>
        /// <param name="color">Current color</param>
        /// <returns>Enemy this current color</returns>
        public Color GetEnemy(Color color)
        {
            if (color is Color.Black)
                return Color.White;
            else return Color.Black;
        }

        /// <summary>
        /// Get King of desired color
        /// </summary>
        /// <param name="color">Desired color</param>
        /// <returns>King thos color</returns>
        public Piece GetKing(Color color)
        {
            foreach (Piece p in AvaliablePiecesByColor(color))
            {
                if (p is King)
                    return p;
            }

            return null;
        }

        /// <summary>
        /// Sets initial position of pices on the board.
        /// </summary>
        private void BuildPieces()
        {
            // Blacks
            PutPiece(new Rook(Board, Color.Black), 'a', 8);
            PutPiece(new Knight(Board, Color.Black), 'b', 8);
            PutPiece(new Bishop(Board, Color.Black), 'c', 8);
            PutPiece(new Queen(Board, Color.Black), 'e', 8);
            PutPiece(new King(Board, Color.Black, this), 'd', 8);
            PutPiece(new Bishop(Board, Color.Black), 'f', 8);
            PutPiece(new Knight(Board, Color.Black), 'g', 8);
            PutPiece(new Rook(Board, Color.Black), 'h', 8);
            PutPiece(new Pawn(Board, Color.Black, this), 'a', 7);
            PutPiece(new Pawn(Board, Color.Black, this), 'b', 7);
            PutPiece(new Pawn(Board, Color.Black, this), 'c', 7);
            PutPiece(new Pawn(Board, Color.Black, this), 'd', 7);
            PutPiece(new Pawn(Board, Color.Black, this), 'e', 7);
            PutPiece(new Pawn(Board, Color.Black, this), 'f', 7);
            PutPiece(new Pawn(Board, Color.Black, this), 'g', 7);
            PutPiece(new Pawn(Board, Color.Black, this), 'h', 7);

            // Whites
            PutPiece(new Rook(Board, Color.White), 'a', 1);
            PutPiece(new Knight(Board, Color.White), 'b', 1);
            PutPiece(new Bishop(Board, Color.White), 'c', 1);
            PutPiece(new Queen(Board, Color.White), 'd', 1);
            PutPiece(new King(Board, Color.White, this), 'e', 1);
            PutPiece(new Bishop(Board, Color.White), 'f', 1);
            PutPiece(new Knight(Board, Color.White), 'g', 1);
            PutPiece(new Rook(Board, Color.White), 'h', 1);
            PutPiece(new Pawn(Board, Color.White, this), 'a', 2);
            PutPiece(new Pawn(Board, Color.White, this), 'b', 2);
            PutPiece(new Pawn(Board, Color.White, this), 'c', 2);
            PutPiece(new Pawn(Board, Color.White, this), 'd', 2);
            PutPiece(new Pawn(Board, Color.White, this), 'e', 2);
            PutPiece(new Pawn(Board, Color.White, this), 'f', 2);
            PutPiece(new Pawn(Board, Color.White, this), 'g', 2);
            PutPiece(new Pawn(Board, Color.White, this), 'h', 2);
        }

        /// <summary>
        /// Put piece
        /// </summary>
        /// <param name="piece">Piece</param>
        /// <param name="column">Column</param>
        /// <param name="line">Row</param>
        private void PutPiece(Piece piece, char column, int line)
        {
            Board.PutPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }
    }
}