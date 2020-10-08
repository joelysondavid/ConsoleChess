using board;
using System;
using System.Collections.Generic;
using System.Text;

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
        private int turn;

        /// <summary>
        /// Current player
        /// </summary>
        private Color currentPlayer;

        /// <summary>
        /// Checks the match is finished
        /// </summary>
        public bool FinishedMatch { get; private set; }

        /// <summary>
        /// builder to start the match
        /// </summary>
        public ChessMatch()
        {
            Board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            BuildPieces();
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
        }

        private void BuildPieces()
        {
            // whites
            Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('a', 8).ToPosition());
            Board.PutPiece(new Tower(Board, Color.White), new ChessPosition('h', 8).ToPosition());

            Board.PutPiece(new King(Board, Color.White), new ChessPosition('d', 8).ToPosition());


            // blacks
            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('h', 7).ToPosition());
            Board.PutPiece(new Tower(Board, Color.Black), new ChessPosition('b', 2).ToPosition());

            Board.PutPiece(new King(Board, Color.Black), new ChessPosition('c', 1).ToPosition());
        }
    }
}
