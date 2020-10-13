using board;
using chess;
using System;
using System.Collections.Generic;

namespace ConsoleChess
{
    /// <summary>
    /// Object that represents the screen
    /// </summary>
    public class Canvas
    {
        /// <summary>
        /// Print match
        /// </summary>
        /// <param name="match">Match</param>
        public static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Board);
            Console.WriteLine();

            ShowCapturedPieces(match.GetCapturedPiecesByColor(Color.White), Color.White);
            Console.WriteLine();
            ShowCapturedPieces(match.GetCapturedPiecesByColor(Color.Black), Color.Black);
            Console.WriteLine();

            match.IsInCheckmate(match.CurrentPlayer);

            Console.WriteLine();
            Console.WriteLine("Turn: " + match.TotalMoves);
            if (!match.FinishedMatch)
            {
                Console.WriteLine($"Waiting {match.CurrentPlayer} player");

                Console.WriteLine();

                if (match.IsCheck)
                {
                    Console.WriteLine("You are in check");
                }

                Console.WriteLine();
                Console.Write("Origin: ");
                Position postionOrigin = ReadChessPosition().ToPosition();

                match.ValidateOriginPosition(postionOrigin);

                bool[,] possibleMovements = match.Board.Piece(postionOrigin).PossibleMovements();

                Console.Clear();
                PrintBoard(match.Board, possibleMovements);

                Console.WriteLine();
                Console.Write($"Target: ");
                Position positionTarget = ReadChessPosition().ToPosition();

                match.ValidatesTargetPosition(postionOrigin, positionTarget);

                match.MakeChessMove(postionOrigin, positionTarget);
                Console.Clear();
            }
            else
            {
                Console.WriteLine("CHECKEMATE!");
                Console.WriteLine($"{match.CurrentPlayer} Wins!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="capturedPieces"></param>
        /// <param name="color"></param>
        public static void ShowCapturedPieces(HashSet<Piece> capturedPieces, Color color)
        {
            int cont = 0;

            if (color == Color.Black) Console.ForegroundColor = ConsoleColor.DarkRed;
            else Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Pieces {0} captured: [ ", color);
            foreach (var piece in capturedPieces)
            {
                Console.Write((cont > 0 ? ", " : "") + piece);
                cont++;
            }
            Console.Write(" ]");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Drawing the board
        /// </summary>
        /// <param name="board"></param>
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(board.Rows - i + "| ");
                for (int j = 0; j < board.Columns; j++)
                {
                    Console.BackgroundColor = PrintBackChess(i, j);
                    PrintPiece(board.Piece(i, j));
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
            Console.WriteLine("-| a b c d e f g h");
        }

        /// <summary>
        /// Drawing the board
        /// </summary>
        /// <param name="board"></param>
        public static void PrintBoard(Board board, bool[,] possiblesMovements)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(board.Rows - i + "| ");
                for (int j = 0; j < board.Columns; j++)
                {
                    Console.BackgroundColor = PrintBackChess(i, j, possiblesMovements[i, j]);
                    PrintPiece(board.Piece(i, j));
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
            Console.WriteLine("-| a b c d e f g h");
        }

        /// <summary>
        /// Read the position of chess
        /// </summary>
        /// <returns>Chess position</returns>
        public static ChessPosition ReadChessPosition()
        {
            string positionString = Console.ReadLine();

            char col = positionString[0];
            int line = 0;

            int.TryParse(positionString[1].ToString(), out line);

            if (line < 1)
                throw new BoardException("Invalid line!");

            return new ChessPosition(col, line);
        }

        /// <summary>
        /// Print piece on board
        /// </summary>
        /// <param name="piece">Piece</param>
        public static void PrintPiece(Piece piece)
        {
            ConsoleColor aux = Console.ForegroundColor;

            if (piece == null)
                Console.Write(" ");

            else
            {
                if (piece.Color == Color.White)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(piece);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(piece);
                }
            }

            Console.Write(" ");
            Console.ForegroundColor = aux;
        }

        /// <summary>
        /// Sets the background color of the field
        /// </summary>
        /// <param name="line">Line field</param>
        /// <param name="column">Column field</param>
        /// <returns></returns>
        private static ConsoleColor PrintBackChess(int line, int column, bool checksPath = false)
        {
            if (checksPath)
                return Console.BackgroundColor = ConsoleColor.DarkGray;

            if ((line % 2 == 0 && column % 2 == 0) || (line % 2 != 0 && column % 2 != 0))
                return Console.BackgroundColor = ConsoleColor.DarkYellow;
            else
                return Console.BackgroundColor = ConsoleColor.Red;
        }
    }
}
