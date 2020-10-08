using board;
using System.Threading;

namespace chess
{
    /// <summary>
    /// Object representing positions in the chess game
    /// </summary>
    public class ChessPosition
    {
        /// <summary>
        /// Property that represents a column
        /// </summary>
        public char Column { get; set; }
        /// <summary>
        /// Property that represents a row
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Simple cosntructor
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        public ChessPosition(char column, int row)
        {
            Column = column;
            Row = row;
        }

        /// <summary>
        /// Conversion to position
        /// </summary>
        /// <returns>Position</returns>
        public Position ToPosition()
        {
            return new Position(8 - Row, Column - 'a');
        }


        /// <summary>
        /// To string override
        /// </summary>
        /// <returns>Position in chess</returns>
        public override string ToString()
        {
            return "" + Column + Row;
        }
    }
}
