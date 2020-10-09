using board;
using chess;
using System;
using System.ComponentModel;

namespace ConsoleChess
{
    /// <summary>
    /// Object to represents the screen
    /// </summary>
    public class Canvas
    {
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
            Console.WriteLine("-| A B C D E F G H");
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
            Console.WriteLine("-| A B C D E F G H");
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
