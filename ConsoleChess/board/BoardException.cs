using System;

namespace board
{
    /// <summary>
    /// Board exceptions
    /// </summary>
    public class BoardException : Exception
    {
        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="message">Message to be shown</param>
        public BoardException(string message) : base(message) { }

    }
}
