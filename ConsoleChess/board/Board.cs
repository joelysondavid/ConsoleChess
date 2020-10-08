namespace board
{
    /// <summary>
    /// Object respresenting a Board
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Number of lines
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Number of columns
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// List of pieces
        /// </summary>
        private Piece[,] Pieces { get; set; }

        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public Board(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            Pieces = new Piece[rows, columns];
        }

        /// <summary>
        /// Method return a piece
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        /// <returns>Piece</returns>
        public Piece Piece(int row, int column)
        {
            return Pieces[row, column];
        }

        /// <summary>
        /// Return a piece override
        /// </summary>
        /// <param name="position">Position</param>
        /// <returns>Piece</returns>
        public Piece Piece(Position position)
        {
            return Pieces[position.Row, position.Column];
        }

        /// <summary>
        /// Checks if there is a piece in the informed position
        /// </summary>
        /// <param name="position">Position</param>
        /// <returns>Existence of a piece</returns>
        public bool ExistsPiece(Position position)
        {
            ValidatePosition(position);
            return Piece(position) != null;
        }

        /// <summary>
        /// Removes a piece from the table
        /// </summary>
        /// <param name="piece">Piece</param>
        /// <param name="position">Position</param>
        public void PutPiece(Piece piece, Position position)
        {
            if (ExistsPiece(position))
                throw new BoardException("There's already a piece in this position!");

            Pieces[position.Row, position.Column] = piece;
            piece.Position = position;
        }

        /// <summary>
        /// Remove a piece of the table
        /// </summary>
        /// <param name="position"></param>
        public Piece RemovePiece(Position position)
        {
            if (Piece(position) == null)
                return null;

            Piece aux = Piece(position);
            aux.Position = null;
            Pieces[position.Row, position.Column] = null;
            return aux;
        }

        /// <summary>
        /// Checks whether a position is valid
        /// </summary>
        /// <param name="position">Position</param>
        /// <returns>Whether the position is valid or not</returns>
        public bool PositionIsValid(Position position)
        {
            if ((position.Row < 0 || position.Row >= Rows) || (position.Column < 0 || position.Column >= Columns))
                return false;

            return true;
        }

        /// <summary>
        /// Exception 
        /// </summary>
        /// <param name="position">Position</param>
        public void ValidatePosition(Position position)
        {
            if (!PositionIsValid(position))
                throw new BoardException("Position is invalid!");
        }
    }
}