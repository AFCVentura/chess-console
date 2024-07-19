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
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _capturedPieces;
        public bool Check { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsFinished = false;
            Check = false;
            _pieces = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
            PutPieces();
        }

        public Piece Move(Position origin, Position target)
        {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementMovementCount();
            Piece capturedPiece = Board.RemovePiece(target);
            Board.PutPiece(piece, target);
            if (capturedPiece != null)
            {
                _capturedPieces.Add(capturedPiece);
            }
            return capturedPiece;

        }

        public void UndoMove(Position origin, Position target, Piece capturedPiece) 
        {
            Piece piece = Board.RemovePiece(target);
            piece.DecrementMovementCount();
            if(capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, target);
                _capturedPieces.Remove(capturedPiece);
            }
            Board.PutPiece(piece, origin);
        }

        public void PerformMove(Position origin, Position target)
        {
            Piece capturedPiece = Move(origin, target);
            if (IsInCheck(CurrentPlayer))
            {
                UndoMove(origin, target, capturedPiece);
                throw new BoardException("This move will put your king in check");
            }

            if (IsInCheck(WhoIsTheOpponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            Turn++;
            ChangePlayer();
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

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in _capturedPieces)
            {
                if (piece.Color == color)
                {
                    aux.Add(piece);
                }
            }

            return aux;
        }

        public HashSet<Piece> InGamePieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in _pieces)
            {
                if (piece.Color == color)
                {
                    aux.Add(piece);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        private Color WhoIsTheOpponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White; 
            }
        }

        private Piece King(Color color)
        {
            foreach(Piece piece in InGamePieces(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece king = King(color);
            if (king == null)
            {
                throw new BoardException("There's no king of the specified color");
            }
            foreach(Piece piece in InGamePieces(WhoIsTheOpponent(color))) {
                bool[,] mat = piece.PossibleMovements();
                if (mat[king.Position.Row, king.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }


        public void PutNewPiece(char column, int row, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(column, row).ToPosition());
            _pieces.Add(piece);
        }

        private void PutPieces()
        {
            PutNewPiece('d', 1, new King(Color.White, Board));
            PutNewPiece('c', 1, new Tower(Color.White, Board));
            PutNewPiece('c', 2, new Tower(Color.White, Board));
            PutNewPiece('d', 2, new Tower(Color.White, Board));
            PutNewPiece('e', 2, new Tower(Color.White, Board));
            PutNewPiece('e', 1, new Tower(Color.White, Board));

            PutNewPiece('d', 8, new King(Color.Black, Board));
            PutNewPiece('c', 8, new Tower(Color.Black, Board));
            PutNewPiece('c', 7, new Tower(Color.Black, Board));
            PutNewPiece('d', 7, new Tower(Color.Black, Board));
            PutNewPiece('e', 7, new Tower(Color.Black, Board));
            PutNewPiece('e', 8, new Tower(Color.Black, Board));

            /*
            // White strong pieces
            PutNewPiece('a', 1, new Tower(Color.White, Board));
            PutNewPiece('b', 1, new Knight(Color.White, Board));
            PutNewPiece('c', 1, new Bishop(Color.White, Board));
            PutNewPiece('d', 1, new Queen(Color.White, Board));
            PutNewPiece('e', 1, new King(Color.White, Board));
            PutNewPiece('f', 1, new Bishop(Color.White, Board));
            PutNewPiece('g', 1, new Knight(Color.White, Board));
            PutNewPiece('h', 1, new Tower(Color.White, Board));

            // White pawns
            PutNewPiece('a', 2, new Pawn(Color.White, Board));
            PutNewPiece('b', 2, new Pawn(Color.White, Board));
            PutNewPiece('c', 2, new Pawn(Color.White, Board));
            PutNewPiece('d', 2, new Pawn(Color.White, Board));
            PutNewPiece('e', 2, new Pawn(Color.White, Board));
            PutNewPiece('f', 2, new Pawn(Color.White, Board));
            PutNewPiece('g', 2, new Pawn(Color.White, Board));
            PutNewPiece('h', 2, new Pawn(Color.White, Board));

            // Black strong pieces
            PutNewPiece('a', 8, new Tower(Color.Black, Board));
            PutNewPiece('b', 8, new Knight(Color.Black, Board));
            PutNewPiece('c', 8, new Bishop(Color.Black, Board));
            PutNewPiece('d', 8, new Queen(Color.Black, Board));
            PutNewPiece('e', 8, new King(Color.Black, Board));
            PutNewPiece('f', 8, new Bishop(Color.Black, Board));
            PutNewPiece('g', 8, new Knight(Color.Black, Board));
            PutNewPiece('h', 8, new Tower(Color.Black, Board));

            // Black pawns
            PutNewPiece('a', 7, new Pawn(Color.White, Board));
            PutNewPiece('b', 7, new Pawn(Color.White, Board));
            PutNewPiece('c', 7, new Pawn(Color.White, Board));
            PutNewPiece('d', 7, new Pawn(Color.White, Board));
            PutNewPiece('f', 7, new Pawn(Color.White, Board));
            PutNewPiece('g', 7, new Pawn(Color.White, Board));
            PutNewPiece('h', 7, new Pawn(Color.White, Board));
            */
        }
    }
}
