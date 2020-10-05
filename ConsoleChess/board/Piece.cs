namespace board
{
    /// <summary>
    /// Piece class
    /// </summary>
    public class Piece
    {
        /// <summary>
        /// Position porperty
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Color property
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Quantity of movements
        /// </summary>
        public int NumberOfMovements { get; set; }

        /// <summary>
        /// Porperty board
        /// </summary>
        public Board Board { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="board">Board param</param>
        /// <param name="color">Color param</param>
        public Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            NumberOfMovements = 0;
            Board = board;
        }
    }
}
