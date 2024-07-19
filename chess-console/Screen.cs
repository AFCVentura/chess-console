using board;
using chess;
using chess_console.chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_console
{
    internal class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            Console.Clear();
            Console.WriteLine();
            PrintBoard(match.Board);
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine($"Turn: {match.Turn}");
            if(!match.IsFinished)
            {
                Console.WriteLine($"Waiting for {match.CurrentPlayer} Move");
                if (match.Check)
                {
                    Console.WriteLine("Check!");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine($"Winner: {match.CurrentPlayer}");
            }

        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured Pieces:");
            Console.Write("White: ");
            PrintSet(match.CapturedPieces(Color.White));
            Console.Write("Black: ");
            PrintSet(match.CapturedPieces(Color.Black));
        }

        public static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach (Piece piece in set)
            {
                Console.Write(piece + " ");
            }
            Console.WriteLine("]");
        }

        public static void PrintBoard(Board board)
        {
            bool whiteSquare = true;
            for (int i = 0; i < board.Row; i++)
            {
                whiteSquare = !whiteSquare;

                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Column; j++)
                {
                    ConsoleColor aux = Console.BackgroundColor;
                    if (whiteSquare)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                         Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    PrintPiece(board.Piece(i,j));
                        
                    whiteSquare = !whiteSquare;
                    Console.BackgroundColor = aux;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");

        }

        public static void PrintBoard(Board board, bool[,] possiblePositions)
        {
            bool whiteSquare = true;

            ConsoleColor highlightedColor = ConsoleColor.DarkBlue;

            for (int i = 0; i < board.Row; i++)
            {
                whiteSquare = !whiteSquare;

                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Column; j++)
                {
                    ConsoleColor aux = Console.BackgroundColor;
                    
                    if (possiblePositions[i,j])
                    {
                        Console.BackgroundColor = highlightedColor;
                    }
                    else
                    {
                        if (whiteSquare)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        }
                    }
                            
                    PrintPiece(board.Piece(i, j));

                    whiteSquare = !whiteSquare;
                    Console.BackgroundColor = aux;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");

        }

        public static void PrintPiece(Piece piece)
        {

            if (piece == null)
            {
                Console.Write("  ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else if (piece.Color == Color.Black) 
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(column, row);
        }

    }
}
