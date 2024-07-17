using board;
using chess;
using chess_console.chess;

namespace chess_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            try
            {
                Board b = new Board(8, 8);
                // test
                b.PutPiece(new Tower(Color.Black, b), new Position(0, 0));
                b.PutPiece(new Tower(Color.Black, b), new Position(1, 0));
                b.PutPiece(new King(Color.White, b), new Position(0, 2));
                b.PutPiece(new King(Color.White, b), new Position(1, 2));
                b.PutPiece(new Pawn(Color.Black, b), new Position(0, 4));
                Screen.PrintBoard(b);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
