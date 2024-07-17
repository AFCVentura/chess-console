using System;

namespace board.exception
{
    internal class BoardException : ApplicationException
    {
        public BoardException(string message) : base(message)
        {
        }
    }
}