

using board;

namespace chess
{
    internal class King : Piece
    {
        private ChessMatch _match;
        public King(Color color, Board board, ChessMatch chessMatch) : base(color, board)
        {
            _match = chessMatch;
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        private bool TestTowerToCastle(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece is Tower && piece.Color == Color && piece.MovementCount == 0;
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

            // #SpecialMove Castle
            if (MovementCount == 0 && !_match.Check) 
            {
                // Castle Kingside
                Position kingTowerPosition = new Position(Position.Row, Position.Column + 3);
                if (TestTowerToCastle(kingTowerPosition)) 
                {
                    Position kingEmptySquare = new Position(Position.Row, Position.Column + 1);
                    Position towerEmptySquare = new Position(Position.Row, Position.Column + 2);
                    if (Board.Piece(kingEmptySquare) == null && Board.Piece(towerEmptySquare) == null)
                    {
                        mat[Position.Row, Position.Column + 2] = true;
                    }
                }

                // Castle Queenside
                Position queenTowerQueensidePosition = new Position(Position.Row, Position.Column - 4);
                if (TestTowerToCastle(queenTowerQueensidePosition))
                {
                    Position kingEmptySquare = new Position(Position.Row, Position.Column - 1);
                    Position middleEmptySquare = new Position(Position.Row, Position.Column - 2);
                    Position towerEmptySquare = new Position(Position.Row, Position.Column - 3);
                    if (Board.Piece(kingEmptySquare) == null && Board.Piece(middleEmptySquare) == null && Board.Piece(towerEmptySquare) == null)
                    {
                        mat[Position.Row, Position.Column - 2] = true;
                    }
                }
            }   


                
            return mat;
        }

        public override string ToString()
        {
            return "\u2654";
        }


    }
}
