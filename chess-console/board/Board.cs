using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace board
{
    internal class Board
    {
        public int Row { get; set; }
        public int Column { get; set; }
        private Piece[,] _pieces;

        public Board(int row, int column)
        {
            Row = row;
            Column = column;
            _pieces = new Piece[row, column];
        }
    }
}
