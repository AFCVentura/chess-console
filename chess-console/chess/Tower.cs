﻿

using board;

namespace chess
{
    internal class Tower : Piece
    {
        public Tower(Color color, Board board) : base(color, board)
        {
        }
        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Row, Board.Column];

            Position position = new Position(0, 0);

            // forward
            position.DefiningValues(Position.Row - 1, Position.Column);
            while(Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.DefiningValues(position.Row - 1, position.Column);
            }

            // right
            position.DefiningValues(Position.Row, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.DefiningValues(position.Row, position.Column + 1);
            }

            // back
            position.DefiningValues(position.Row + 1, Position.Column);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.DefiningValues(position.Row + 1, position.Column);
            }
  
            // left
            position.DefiningValues(Position.Row, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.DefiningValues(position.Row, position.Column - 1);
            }

            return mat;
        }

        public override string ToString()
        {
            return "\u2656";
        }
    }
}
