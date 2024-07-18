using board.exception;
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

        public Piece Piece(int row, int column)
        {
            return _pieces[row, column];
        }

        public Piece Piece(Position position)
        {
            return _pieces[position.Row, position.Column];
        }

        public bool IsThereAPiece(Position position)
        {
            ValidatingPosition(position);
            return Piece(position) != null;
        }

        public void PutPiece (Piece piece, Position position)
        {
            if (IsThereAPiece(position))
            {
                throw new BoardException("There's already a piece in this position");
            }
            _pieces[position.Row, position.Column] = piece;
            piece.Position = position;

        }

        public Piece RemovePiece(Position position)
        {
            if (Piece(position) == null)
            {
                return null;
            }
            Piece aux = Piece(position);
            aux.Position = null;
            _pieces[position.Row, position.Column] = null;
            return aux;
            


        }

        public bool ValidPosition(Position position)
        {
            if (position.Row < 0 || position.Column < 0 || position.Row >= Row || position.Column >= Column)
            {
                return false;
            }
            return true;
        }

        public void ValidatingPosition(Position position)
        {
            if(!ValidPosition(position))
            {
                throw new BoardException("Invalid Position!");
            }
        }
    }
}
