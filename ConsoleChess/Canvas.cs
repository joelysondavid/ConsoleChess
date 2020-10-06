using board;
using System;
using System.ComponentModel;

namespace ConsoleChess
{
    public class Canvas
    {
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(board.Rows - i + "| ");
                for (int j = 0; j < board.Columns; j++)
                {
                    Console.BackgroundColor = PrintBackChess(i, j);
                    if (board.Piece(i, j) == null)
                    {

                        Console.Write("- ");
                    }
                    else
                    {
                        PrintPiece(board.Piece(i, j));
                        Console.Write(" ");
                    }
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
            Console.WriteLine("-| A B C D E F G H");
        }

        /// <summary>
        /// Sets the background color of the field
        /// </summary>
        /// <param name="line">Line field</param>
        /// <param name="column">Column field</param>
        /// <returns></returns>
        private static ConsoleColor PrintBackChess(int line, int column)
        {
            if ((line % 2 == 0 && column % 2 == 0) || (line % 2 != 0 && column % 2 != 0))
                return Console.BackgroundColor = ConsoleColor.White;
            else
                return Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Print piece on board
        /// </summary>
        /// <param name="piece">Piece</param>
        public static void PrintPiece(Piece piece)
        {
            if (piece.Color == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
