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

        public override string ToString()
        {
            return string.Concat(Row, ", ", Column);
        }
    }
}
