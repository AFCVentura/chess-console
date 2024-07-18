using board;
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

                    if (board.Piece(i,j) == null)
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        PrintPiece(board.Piece(i,j));
                        Console.Write(" ");
                    }
                    whiteSquare = !whiteSquare;
                    Console.BackgroundColor = aux;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");

        }

        public static void PrintPiece(Piece piece)
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
