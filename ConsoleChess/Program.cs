using board;
using chess;
using System;

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
                    Console.Clear();
                    Canvas.PrintBoard(match.Board);
                    Console.Write("Origin: "); 
                    Position postionOrigin = Canvas.ReadChessPosition().ToPosition();

                    bool[,] possibleMovements = match.Board.Piece(postionOrigin).PossibleMovements();

                    Console.Clear();
                    Canvas.PrintBoard(match.Board, possibleMovements);

                    Console.Write($"Target: ");
                    Position positionTarget = Canvas.ReadChessPosition().ToPosition();

                    match.ExecuteMove(postionOrigin, positionTarget);

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }
        }
    }
}
