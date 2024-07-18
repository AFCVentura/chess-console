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
                ChessMatch match = new ChessMatch();
                while (!match.IsFinished)
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Board);

                    Console.WriteLine();

                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    bool[,] possiblePositions = match.Board.Piece(origin).PossibleMovements();

                    Console.Clear();

                    Screen.PrintBoard(match.Board, possiblePositions);


                    Console.Write("Target: ");
                    Position target = Screen.ReadChessPosition().ToPosition();

                    match.Move(origin, target);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
