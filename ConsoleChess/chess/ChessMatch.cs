using board;
using System.Collections.Generic;

namespace chess
{
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
        private HashSet<Piece> pieces;

        /// <summary>
        /// Captured pieces
        /// </summary>
        private HashSet<Piece> capturedPieces;

        /// <summary>
        /// builder to start the match
        /// </summary>
        public ChessMatch()
        {
            Board = new Board(8, 8);
            TotalMoves = 1;
            CurrentPlayer = Color.White;
            pieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
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
            ExecuteMove(origin, target);
            TotalMoves++;

            CurrentPlayer = CurrentPlayer == Color.Black ? Color.White : Color.Black;
        }

        /// <summary>
        /// Execute move of chess piece
        /// </summary>
        /// <param name="origin">Origin of piece</param>
        /// <param name="target">Target position</param>
        public void ExecuteMove(Position origin, Position target)
        {
            Piece piece = Board.RemovePiece(origin);
            Piece capturedPiece = Board.RemovePiece(target);
            Board.PutPiece(piece, target);
            piece.IncrementMovements();

            if (capturedPiece != null)
                capturedPieces.Add(capturedPiece);
        }


        /// <summary>
        /// Get captured pices by informed color
        /// </summary>
        /// <param name="color">Desired color</param>
        /// <returns>Pieces captured by desired color</returns>
        public HashSet<Piece> GetCapturedPiecesByColor(Color color)
        {
            HashSet<Piece> pieces = new HashSet<Piece>();
            foreach (Piece piece in capturedPieces)
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
            foreach (Piece piece in pieces)
            {
                if (piece.Color == color)
                    pieces.Add(piece);
            }

            pieces.ExceptWith(GetCapturedPiecesByColor(color));
            return pieces;
        }

        /// <summary>
        /// Sets initial position of pices on the board.
        /// </summary>
        private void BuildPieces()
        {
            // Whites
            PutPiece(new Tower(Board, Color.White), 'a', 8);
            PutPiece(new Tower(Board, Color.White), 'h', 8);
            PutPiece(new King(Board, Color.White), 'd', 8);


            // Blacks
            PutPiece(new Tower(Board, Color.Black), 'a', 1);
            PutPiece(new Tower(Board, Color.Black), 'h', 1);
            PutPiece(new King(Board, Color.Black), 'c', 1);
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
            pieces.Add(piece);
        }
    }
}
