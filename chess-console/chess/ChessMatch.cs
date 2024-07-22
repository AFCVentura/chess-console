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
        public Piece VulnerableToEnPassant { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            IsFinished = false;
            Check = false;
            VulnerableToEnPassant = null; 
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

            // #SpecialMove Castle Kingside
            if (piece is King && target.Column == origin.Column + 2)
            {
                Position towerOrigin = new Position(origin.Row, origin.Column + 3);
                Position towerTarget = new Position(origin.Row, origin.Column + 1);
                Piece tower = Board.RemovePiece(towerOrigin);
                tower.IncrementMovementCount();
                Board.PutPiece(tower, towerTarget);
            }

            // #SpecialMove Castle Queenside
            if (piece is King && target.Column == origin.Column - 2)
            {
                Position towerOrigin = new Position(origin.Row, origin.Column - 4);
                Position towerTarget = new Position(origin.Row, origin.Column - 1);
                Piece tower = Board.RemovePiece(towerOrigin);
                tower.IncrementMovementCount();
                Board.PutPiece(tower, towerTarget);
            }

            // #SpecialMove En Passant
            if (piece is Pawn)
            {
                if (origin.Column != target.Column && capturedPiece == null)
                {
                    Position positionPawn;
                    if (piece.Color == Color.White)
                    {
                        positionPawn = new Position(target.Row + 1 , target.Column);
                    }
                    else
                    {
                        positionPawn = new Position(target.Row - 1, target.Column);
                    }
                    capturedPiece = Board.RemovePiece(positionPawn);
                    _capturedPieces.Add(capturedPiece);
                }
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

            // #SpecialMove Castle Kingside
            if (piece is King && target.Column == origin.Column + 2)
            {
                Position towerOrigin = new Position(origin.Row, origin.Column + 3);
                Position towerTarget = new Position(origin.Row, origin.Column + 1);
                Piece tower = Board.RemovePiece(towerTarget);
                tower.DecrementMovementCount();
                Board.PutPiece(tower, towerOrigin);
            }

            // #SpecialMove Castle Queenside
            if (piece is King && target.Column == origin.Column - 2)
            {
                Position towerOrigin = new Position(origin.Row, origin.Column - 4);
                Position towerTarget = new Position(origin.Row, origin.Column - 1);
                Piece tower = Board.RemovePiece(towerTarget);
                tower.DecrementMovementCount();
                Board.PutPiece(tower, towerOrigin);
            }

            // #SpecialMove En Passant
            if (piece is Pawn)
            {
                if (origin.Column != target.Column && capturedPiece == VulnerableToEnPassant)
                {
                    Piece pawn = Board.RemovePiece(target);
                    Position positionPawn;
                    if (piece.Color == Color.White)
                    {
                        positionPawn = new Position(3, target.Column);
                    }
                    else
                    {
                        positionPawn = new Position(4, target.Column);
                    }
                    Board.PutPiece(pawn, positionPawn);
                }
            }

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
            
            if(TestCheckMate(WhoIsTheOpponent(CurrentPlayer)))
            {
                IsFinished = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }

            Piece p = Board.Piece(target);

            // #SpecialMove En Passant
            if (p is Pawn && (target.Row == origin.Row - 2 || target.Row == origin.Row + 2)) 
            {
                VulnerableToEnPassant = p;   
            }
            else
            {
                VulnerableToEnPassant = null;
            }


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
            if (!Board.Piece(origin).PossibleMove(target))
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

        public bool TestCheckMate(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }
            foreach (Piece piece in InGamePieces(color))
            {
                bool[,] mat = piece.PossibleMovements();
                for (int i = 0; i < Board.Row; i++)
                {
                    for (int j = 0; j < Board.Column; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = piece.Position;
                            Position target = new Position(i,j);
                            Piece capturedPiece = Move(origin, target);
                            bool testCheck = IsInCheck(color);
                            UndoMove(origin, target, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }      
                    }
                }
            }
            return true;
        }


        public void PutNewPiece(char column, int row, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(column, row).ToPosition());
            _pieces.Add(piece);
        }

        private void PutPieces()
        {
            // White strong pieces
            PutNewPiece('a', 1, new Tower(Color.White, Board));
            PutNewPiece('b', 1, new Knight(Color.White, Board));
            PutNewPiece('c', 1, new Bishop(Color.White, Board));
            PutNewPiece('d', 1, new Queen(Color.White, Board));
            PutNewPiece('e', 1, new King(Color.White, Board, this));
            PutNewPiece('f', 1, new Bishop(Color.White, Board));
            PutNewPiece('g', 1, new Knight(Color.White, Board));
            PutNewPiece('h', 1, new Tower(Color.White, Board));

            // White pawns
            PutNewPiece('a', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('b', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('c', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('d', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('e', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('f', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('g', 2, new Pawn(Color.White, Board, this));
            PutNewPiece('h', 2, new Pawn(Color.White, Board, this));

            // Black strong pieces
            PutNewPiece('a', 8, new Tower(Color.Black, Board));
            PutNewPiece('b', 8, new Knight(Color.Black, Board));
            PutNewPiece('c', 8, new Bishop(Color.Black, Board));
            PutNewPiece('d', 8, new Queen(Color.Black, Board));
            PutNewPiece('e', 8, new King(Color.Black, Board, this));
            PutNewPiece('f', 8, new Bishop(Color.Black, Board));
            PutNewPiece('g', 8, new Knight(Color.Black, Board));
            PutNewPiece('h', 8, new Tower(Color.Black, Board));

            // Black pawns
            PutNewPiece('a', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('b', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('c', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('d', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('e', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('f', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('g', 7, new Pawn(Color.Black, Board, this));
            PutNewPiece('h', 7, new Pawn(Color.Black, Board, this));

        }
    }
}
