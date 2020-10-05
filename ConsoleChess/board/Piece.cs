namespace ConsoleChess.board
{
    public class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; set; }
        public int NumberOfMovements { get; set; }
        public Board Board { get; set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            NumberOfMovements = 0;
            Board = board;
        }
    }
}
