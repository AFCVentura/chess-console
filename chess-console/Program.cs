using board;
using board.exception;
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
                    try
                    {
                        Screen.PrintMatch(match);

                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);


                        bool[,] possiblePositions = match.Board.Piece(origin).PossibleMovements();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possiblePositions);


                        Console.Write("Target: ");
                        Position target = Screen.ReadChessPosition().ToPosition();
                        match.ValidateTargetPosition(origin, target);

                        match.PerformMove(origin, target);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
