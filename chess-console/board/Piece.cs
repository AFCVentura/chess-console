using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace board
{
    abstract internal class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public Board Board { get; protected set; }
        public int MovementCount { get; protected set; }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            Board = board;
            MovementCount = 0;
        }

        public void IncrementMovementCount()
        {
            MovementCount++;
        }

        public bool AreTherePossibleMoves()
        {
            bool[,] mat = PossibleMovements();
            for (int i = 0; i < Board.Row; i++)
            {
                for (int j = 0; j < Board.Column; j++)
                {
                    if (mat[i, j])
                    {
                        return true;   
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position position)
        {
            return PossibleMovements()[position.Row, position.Column];
        }

        public abstract bool[,] PossibleMovements();
    }
}
