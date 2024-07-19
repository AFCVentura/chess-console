
using board;

namespace chess
{
    internal class Knight : Piece
    {
        public Knight(Color color, Board board) : base(color, board)
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

            // forward right
            position.DefiningValues(Position.Row - 2, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // right forward
            position.DefiningValues(Position.Row - 1, Position.Column + 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // right back
            position.DefiningValues(Position.Row + 1, Position.Column + 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // back right
            position.DefiningValues(Position.Row + 2, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // back left
            position.DefiningValues(Position.Row + 2, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // left back
            position.DefiningValues(Position.Row + 1, Position.Column - 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // left forward
            position.DefiningValues(Position.Row - 1, Position.Column - 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            // forward left
            position.DefiningValues(Position.Row - 2, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                mat[position.Row, position.Column] = true;
            }

            return mat;
        }

        public override string? ToString()
        {
            return "\u2658";
        }
    }
}
