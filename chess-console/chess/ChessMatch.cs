using board;
using board.exception;
using chess_console.chess;


namespace chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer {  get; private set; }
        public bool IsFinished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsFinished = false;
            PutPieces();
        }

        public void PerformMove(Position origin, Position target)
        {
            Move(origin, target);
            Turn++;
            ChangePlayer();
        }

        public void Move(Position origin, Position target)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementMovementCount();
            Piece capturedPiece = Board.RemovePiece(target);
            Board.PutPiece(piece, target);
        }

        public void ValidateOriginPosition(Position origin)
        {
            if (Board.Piece(origin) == null )
            {
                throw new BoardException("There are no pieces in the chosen origin square");
            }
            if (CurrentPlayer != Board.Piece(origin).Color)
            {
                throw new BoardException("The piece in the chosen origin square is not yours");
            }
            if (!Board.Piece(origin).AreTherePossibleMoves())
            {
                throw new BoardException("This piece can't be moved");
            }
        }

        public void ValidateTargetPosition(Position origin, Position target)
        {
            if (!Board.Piece(origin).CanMoveTo(target))
            {
                throw new BoardException("Invalid target square");
            }
        }

        private void ChangePlayer() 
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        private void PutPieces()
        {
            Board.PutPiece(new King(Color.White, Board), new ChessPosition('a', 1).ToPosition());
            Board.PutPiece(new Tower(Color.White, Board), new ChessPosition('a', 2).ToPosition());
            Board.PutPiece(new Tower(Color.White, Board), new ChessPosition('b', 1).ToPosition());
            Board.PutPiece(new Tower(Color.White, Board), new ChessPosition('b', 2).ToPosition());
            Board.PutPiece(new Queen(Color.Black, Board), new ChessPosition('a', 5).ToPosition());
            Board.PutPiece(new Knight(Color.White, Board), new ChessPosition('e', 1).ToPosition());
            Board.PutPiece(new King(Color.White, Board), new ChessPosition('d', 4).ToPosition());
            /*
            // White strong pieces
            Board.PutPiece(new Tower(Color.White, Board), new ChessPosition('a', 1).ToPosition());
            Board.PutPiece(new Knight(Color.White, Board), new ChessPosition('b', 1).ToPosition());
            Board.PutPiece(new Bishop(Color.White, Board), new ChessPosition('c', 1).ToPosition());
            Board.PutPiece(new Queen(Color.White, Board), new ChessPosition('d', 1).ToPosition());
            Board.PutPiece(new King(Color.White, Board), new ChessPosition('e', 1).ToPosition());
            Board.PutPiece(new Bishop(Color.White, Board), new ChessPosition('f', 1).ToPosition());
            Board.PutPiece(new Knight(Color.White, Board), new ChessPosition('g', 1).ToPosition());
            Board.PutPiece(new Tower(Color.White, Board), new ChessPosition('h', 1).ToPosition());

            // White pawns
            Board.PutPiece(new Pawn(Color.White, Board), new ChessPosition('a', 2).ToPosition());
            Board.PutPiece(new Pawn(Color.White, Board), new ChessPosition('b', 2).ToPosition());
            Board.PutPiece(new Pawn(Color.White, Board), new ChessPosition('c', 2).ToPosition());
            Board.PutPiece(new Pawn(Color.White, Board), new ChessPosition('d', 2).ToPosition());
            Board.PutPiece(new Pawn(Color.White, Board), new ChessPosition('e', 2).ToPosition());
            Board.PutPiece(new Pawn(Color.White, Board), new ChessPosition('f', 2).ToPosition());
            Board.PutPiece(new Pawn(Color.White, Board), new ChessPosition('g', 2).ToPosition());
            Board.PutPiece(new Pawn(Color.White, Board), new ChessPosition('h', 2).ToPosition());

            // Black strong pieces
            Board.PutPiece(new Tower(Color.Black, Board), new ChessPosition('a', 8).ToPosition());
            Board.PutPiece(new Knight(Color.Black, Board), new ChessPosition('b', 8).ToPosition());
            Board.PutPiece(new Bishop(Color.Black, Board), new ChessPosition('c', 8).ToPosition());
            Board.PutPiece(new Queen(Color.Black, Board), new ChessPosition('d', 8).ToPosition());
            Board.PutPiece(new King(Color.Black, Board), new ChessPosition('e', 8).ToPosition());
            Board.PutPiece(new Bishop(Color.Black, Board), new ChessPosition('f', 8).ToPosition());
            Board.PutPiece(new Knight(Color.Black, Board), new ChessPosition('g', 8).ToPosition());
            Board.PutPiece(new Tower(Color.Black, Board), new ChessPosition('h', 8).ToPosition());

            // Black pawns
            Board.PutPiece(new Pawn(Color.Black, Board), new ChessPosition('a', 7).ToPosition());
            Board.PutPiece(new Pawn(Color.Black, Board), new ChessPosition('b', 7).ToPosition());
            Board.PutPiece(new Pawn(Color.Black, Board), new ChessPosition('c', 7).ToPosition());
            Board.PutPiece(new Pawn(Color.Black, Board), new ChessPosition('d', 7).ToPosition());
            Board.PutPiece(new Pawn(Color.Black, Board), new ChessPosition('e', 7).ToPosition());
            Board.PutPiece(new Pawn(Color.Black, Board), new ChessPosition('f', 7).ToPosition());
            Board.PutPiece(new Pawn(Color.Black, Board), new ChessPosition('g', 7).ToPosition());
            Board.PutPiece(new Pawn(Color.Black, Board), new ChessPosition('h', 7).ToPosition());
            */
        }
    }
}
