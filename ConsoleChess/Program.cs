using board;
using chess;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessPositon pos = new ChessPositon('a', 1);
            System.Console.WriteLine(pos);
            System.Console.WriteLine(pos.ToPosition());
        }
    }
}
