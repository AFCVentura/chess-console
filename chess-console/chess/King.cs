

using board;

namespace chess
{
    internal class King : Piece
    {
        public King(Color color, Board board) : base(color, board)
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

            Position position = new Position(0,0);

            // forward
            position.DefiningValues(Position.Row - 1, Position.Column);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // forward right
            position.DefiningValues(Position.Row - 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // right
            position.DefiningValues(Position.Row, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // back right
            position.DefiningValues(Position.Row + 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // back
            position.DefiningValues(Position.Row + 1, Position.Column);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // back left
            position.DefiningValues(Position.Row + 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // left
            position.DefiningValues(Position.Row, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // forward left
            position.DefiningValues(Position.Row - 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            return mat;
        }

        public override string ToString()
        {
            return "\u2654";
        }


    }
}
