namespace board
{
    /// <summary>
    /// Position class
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Row position
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Column position
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Constructor, to initialize the variables
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Set the row and column position
        /// </summary>
        /// <param name="row">Position row</param>
        /// <param name="column">Position column</param>
        public void SetValues(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// To string override
        /// </summary>
        /// <returns>Line and column position</returns>
        public override string ToString()
        {
            return string.Concat(Row, ", ", Column);
        }
    }
}
