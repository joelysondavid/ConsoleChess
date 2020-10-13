using board;

namespace chess
{
    /// <summary>
    /// Piece class
    /// </summary>
    public abstract class Piece
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
        /// Possible exits
        /// </summary>
        public int PossiblesExits { get; set; }

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

        /// <summary>
        /// Increments this movements count
        /// </summary>
        public void IncrementMovements()
        {
            NumberOfMovements++;
        }

        /// <summary>
        /// Decrements this movements count
        /// </summary>
        public void DecrementMovements()
        {
            NumberOfMovements--;
        }

        /// <summary>
        /// Checks if the piece is pray
        /// </summary>
        /// <returns>If the piece has any possible movement</returns>
        public bool IsPieceStuck()
        {
            bool[,] exists = PossibleMovements();

            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (exists[i, j])
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the possibles movements of a piece
        /// </summary>
        /// <returns>List of possible moviments</returns>
        public abstract bool[,] PossibleMovements();

        /// <summary>
        /// Checks target
        /// </summary>
        /// <param name="position">Position target</param>
        /// <returns>If that piece can move to target</returns>
        internal bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || (piece.Color != Color);
        }
    }
}
