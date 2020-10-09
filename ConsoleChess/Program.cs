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
                    try
                    {
                        Console.Clear();
                        Canvas.PrintBoard(match.Board);

                        Console.WriteLine();
                        Console.WriteLine("Turn: " + match.TotalMoves);
                        Console.WriteLine($"Waiting {match.CurrentPlayer} player");

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position postionOrigin = Canvas.ReadChessPosition().ToPosition();

                        match.ValidateOriginPosition(postionOrigin);

                        bool[,] possibleMovements = match.Board.Piece(postionOrigin).PossibleMovements();

                        Console.Clear();
                        Canvas.PrintBoard(match.Board, possibleMovements);

                        Console.WriteLine();
                        Console.Write($"Target: ");
                        Position positionTarget = Canvas.ReadChessPosition().ToPosition();

                        match.ValidatesTargetPosition(postionOrigin, positionTarget);

                        match.MakeChessMove(postionOrigin, positionTarget);

                        Console.WriteLine();
                    }
                    catch (BoardException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Try again.");
                        Console.ReadLine();
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
