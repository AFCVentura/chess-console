using board;
using chess;

namespace chess_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board b = new Board(8, 8);

            b.PutPiece(new Tower(Color.Black, b), new Position(0, 0));
            b.PutPiece(new King(Color.Black, b), new Position(0, 2));
            b.PutPiece(new Pawn(Color.Black, b), new Position(0, 4));
            Screen.PrintBoard(b);
        }
    }
}
