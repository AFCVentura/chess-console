

using board;

namespace chess
{
    internal class Pawn : Piece
    {
        public Pawn(Color color, Board board) : base(color, board)
        {
        }
        private bool IsThereAnEnemy(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }

        private bool IsFreeToMove(Position position)
        {
            return Board.Piece(position) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Row, Board.Column];

            Position position = new Position(0, 0);

            if (Color == Color.White)
            {
                // forward one square
                position.DefiningValues(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(position) && IsFreeToMove(position))
                {
                    mat[position.Row, position.Column] = true;
                }

                // forward two squares (first move)
                position.DefiningValues(Position.Row - 2, Position.Column);
                if (Board.ValidPosition(position) && IsFreeToMove(position) && MovementCount == 0)
                {
                    mat[position.Row, position.Column] = true;
                }

                // capturing left
                position.DefiningValues(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPosition(position) && IsThereAnEnemy(position))
                {
                    mat[position.Row, position.Column] = true;
                }

                // capturing right
                position.DefiningValues(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(position) && IsThereAnEnemy(position))
                {
                    mat[position.Row, position.Column] = true;
                }
            }
            else
            {
                // forward one square
                position.DefiningValues(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(position) && IsFreeToMove(position))
                {
                    mat[position.Row, position.Column] = true;
                }

                // forward two squares (first move)
                position.DefiningValues(Position.Row + 2, Position.Column);
                if (Board.ValidPosition(position) && IsFreeToMove(position) && MovementCount == 0)
                {
                    mat[position.Row, position.Column] = true;
                }

                // capturing left
                position.DefiningValues(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPosition(position) && IsThereAnEnemy(position))
                {
                    mat[position.Row, position.Column] = true;
                }

                // capturing right
                position.DefiningValues(Position.Row + 1, Position.Column + 1);
                if (Board.ValidPosition(position) && IsThereAnEnemy(position))
                {
                    mat[position.Row, position.Column] = true;
                }
            }
            return mat;
        }

        public override string? ToString()
        {
            return "\u2659";
        }
    }
}
