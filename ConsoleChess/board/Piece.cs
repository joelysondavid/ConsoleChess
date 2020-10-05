using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChess.board
{
    public class Piece
    {
        public Position Position { get; set; }
        public Color Color{ get; set; }
        public int NumberOfMovements { get; set; }
        public Board Board{ get; set; }

        public Piece(Position position, Color color, int numberOfMovements, Board board)
        {
            Position = position;
            Color = color;
            NumberOfMovements = numberOfMovements;
            Board = board;
        }
    }
}
