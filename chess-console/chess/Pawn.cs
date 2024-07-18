

using board;

namespace chess
{
    internal class Pawn : Piece
    {
        public Pawn(Color color, Board board) : base(color, board)
        {
        }

        public override bool[,] PossibleMovements()
        {
            throw new NotImplementedException();
        }

        public override string? ToString()
        {
            return "\u2659";
        }
    }
}
