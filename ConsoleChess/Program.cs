using board;
using chess;
using System;
using System.Runtime.InteropServices;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.FinishedMatch)
                {
                    try
                    {
                        Canvas.PrintMatch(match);
                    }
                    catch (BoardException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Try again.");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
            }
            catch (BoardException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
