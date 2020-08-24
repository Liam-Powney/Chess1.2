using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Board
    {
        const int TILE_SIZE = 128;

        public bool whitesTurn = true;
        public Piece selectedPiece;
        public List<Vector2> moves = new List<Vector2>();

        public List<Piece> Pieces = new List<Piece>();

        Texture2D lightSquareTexture;
        Texture2D darkSquareTexture;
        Texture2D lightBrownSquareTexture;
        Texture2D darkBrownSquareTexture;
        Texture2D selectCircle;

        public Board()
        {
        }

        public void LoadBoardTextures(ContentManager Content)
        {
            lightSquareTexture = Content.Load<Texture2D>("square_light");
            darkSquareTexture = Content.Load<Texture2D>("square_dark");
            lightBrownSquareTexture = Content.Load<Texture2D>("square_brown_light");
            darkBrownSquareTexture = Content.Load<Texture2D>("square_brown_dark");
            selectCircle = Content.Load<Texture2D>("select_circle");
        }

        public void DrawBoard(SpriteBatch sb)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Rectangle r = new Rectangle(i * TILE_SIZE, (-j + 7) * TILE_SIZE, TILE_SIZE, TILE_SIZE);
                    if ((i + j) % 2 == 0)
                    {
                        //draw black squares
                        sb.Draw(darkSquareTexture, r, Color.White);
                    }
                    else
                    {
                        //draw white squares
                        sb.Draw(lightSquareTexture, r, Color.White);
                    }
                }
            }
            //draw pieces
            foreach(Piece p in Pieces)
            {
                p.DrawPiece(sb);
            }
            //draw available moves for selected piece
            if (selectedPiece != null)
            {
                foreach(Vector2 vec2 in moves)
                {
                    sb.Draw(selectCircle, new Rectangle((int)vec2.X * TILE_SIZE + TILE_SIZE * 1 / 5, (-(int)vec2.Y + 7) * TILE_SIZE + TILE_SIZE * 1 / 5, TILE_SIZE * 3 / 5, TILE_SIZE * 3 / 5), Color.Red * 0.65f);
                }
            }
        }


        public void boardSetup()
        {

            //clear the board of pieces
            Pieces.Clear();

            //create board pieces here
            Pieces.Add(new Rook(0, 0, true));
            Pieces.Add(new Rook(7, 0, true));
            Pieces.Add(new Rook(0, 7, false));
            Pieces.Add(new Rook(7, 7, false));
            Pieces.Add(new King(4, 0, true));
            Pieces.Add(new King(4, 7, false));
            Pieces.Add(new Knight(1, 0, true));
            Pieces.Add(new Knight(6, 0, true));
            Pieces.Add(new Knight(1, 7, false));
            Pieces.Add(new Knight(6, 7, false));
            Pieces.Add(new Bishop(2, 0, true));
            Pieces.Add(new Bishop(5, 0, true));
            Pieces.Add(new Bishop(2, 7, false));
            Pieces.Add(new Bishop(5, 7, false));
            Pieces.Add(new Queen(3, 0, true));
            Pieces.Add(new Queen(3, 7, false));
            Pieces.Add(new Pawn(0, 1, true));
            Pieces.Add(new Pawn(1, 1, true));
            Pieces.Add(new Pawn(2, 1, true));
            Pieces.Add(new Pawn(3, 1, true));
            Pieces.Add(new Pawn(4, 1, true));
            Pieces.Add(new Pawn(5, 1, true));
            Pieces.Add(new Pawn(6, 1, true));
            Pieces.Add(new Pawn(7, 1, true));
            Pieces.Add(new Pawn(0, 6, false));
            Pieces.Add(new Pawn(1, 6, false));
            Pieces.Add(new Pawn(2, 6, false));
            Pieces.Add(new Pawn(3, 6, false));
            Pieces.Add(new Pawn(4, 6, false));
            Pieces.Add(new Pawn(5, 6, false));
            Pieces.Add(new Pawn(6, 6, false));
            Pieces.Add(new Pawn(7, 6, false));

        }

        //returns piece on a given board coord
        public Piece pieceOnCoord(int x, int y)
        {
            foreach (Piece p in Pieces)
            {
                if (p.x == x && p.y == y)
                {
                    return p;
                }
            }
            return null;
        }

        // selects a piece
        public void selectPiece(Piece p)
        {
            this.selectedPiece = p;
            this.moves = availableMoves();
        }

        // de-selects a peice
        public void deselectPiece()
        {
            this.selectedPiece = null;
            this.moves.Clear();
        }

        // returns whether mouse click is on a valid move square for the current selected piece
        public bool clickIsValidMove(int x, int y)
        {
            foreach (Vector2 vec2 in moves)
            {
                if ((int)vec2.X == x && (int)vec2.Y == y)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Vector2> availableMoves()
        {
            List<Vector2> availableMoves = new List<Vector2>();
            if (selectedPiece != null)
            {
                availableMoves = checklessMoves(this.selectedPiece.controlledSquares(Pieces));
            }
            return availableMoves;
        }

        // moves a piece, takes the piece if necessary, and changes who's turn it is
        public void Move(Piece p, int newX, int newY)
        {
            // track if a pawn double moved for en passant
            if (p is Pawn && Math.Abs(newY - p.y) == 2)
            {
                p.pawnLastDoulbeMove = true;
            }
            else if (p is Pawn && !(Math.Abs(newY - p.y) == 2))
            {
                p.pawnLastDoulbeMove = false;
            }

            // en passant 
            if (p is Pawn && p.x != newX && pieceOnCoord(newX, newY) == null)
            {
                if (p.isWhite)
                {
                    Pieces.Remove(pieceOnCoord(newX, newY - 1));
                    //Console.WriteLine("White took en passant!");
                }
                else
                {
                    Pieces.Remove(pieceOnCoord(newX, newY + 1));
                    //Console.WriteLine("Black took en passant!");
                }
            }

            // castling queenside
            if (p is King && (p.x - newX) == 2)
            {
                pieceOnCoord(0, p.y).x = pieceOnCoord(0, p.y).x + 3;
            }
            //castling kingside 
            else if (p is King && (p.x - newX) == -2)
            {
                pieceOnCoord(7, p.y).x = pieceOnCoord(7, p.y).x - 2;
            }

            // taking a piece
            if (pieceOnCoord(newX, newY) != null)
            {
                Pieces.Remove(pieceOnCoord(newX, newY));
                //Console.WriteLine(" a piece was taken!");
            }

            // move piece to new location
            p.x = newX;
            p.y = newY;
            p.hasMoved = true;

        }

        // returns the king piece for either player
        public Piece king(bool isWhite)
        {
            foreach (Piece p in Pieces)
            {
                if (p is King && p.isWhite == isWhite)
                {
                    return p;
                }
            }
            return null;
        }

        // returns list of all rooks of a given colour that haven't moved
        public List<Piece> Rooks(bool isWhite, List<Piece> pieceList)
        {
            List<Piece> Rooks = new List<Piece>();
            foreach(Piece p in pieceList)
            {
                if (p is Rook && p.isWhite==isWhite && !p.hasMoved)
                {
                    Rooks.Add(p);
                }
            }
            return Rooks;
        }

        //creates a copy of the current board instance
        public Board copiedBoard()
        {
            Board copiedBoard = new Board();
            copiedBoard.whitesTurn = whitesTurn;
            foreach(Piece p in this.Pieces)
            {
                copiedBoard.Pieces.Add(p.copyPiece());
            }

            int index = this.Pieces.IndexOf(selectedPiece);
            copiedBoard.selectedPiece = copiedBoard.Pieces[index];

            return copiedBoard;
        }

        // finds available moves from the controlled squares of a piece
        public List<Vector2> checklessMoves(List<Vector2> controlledSquares)
        {
            List<Vector2> checklessMoves = new List<Vector2>();
            Piece playersKing = null;

            foreach (Vector2 square in controlledSquares)
            {
                checklessMoves.Add(square);
            }

            foreach(Vector2 move in controlledSquares)
            {
                Board copiedBoard = this.copiedBoard();
                copiedBoard.Move(copiedBoard.selectedPiece, (int)move.X, (int)move.Y);

                foreach(Piece p in copiedBoard.Pieces)
                {
                    if (p is King && p.isWhite == copiedBoard.whitesTurn)
                    {
                        playersKing = p;
                        break;
                    }
                }

                foreach (Piece p in copiedBoard.Pieces) if (p.isWhite != copiedBoard.whitesTurn)
                {
                    foreach(Vector2 potentialControlledSquare in p.controlledSquares(copiedBoard.Pieces)) if (playersKing != null)
                    {
                        if ((int)potentialControlledSquare.X == playersKing.x && (int)potentialControlledSquare.Y == playersKing.y)
                        {
                            checklessMoves.Remove(move);
                            //Console.WriteLine("Square " + potentialControlledSquare.X.ToString() + ", " + potentialControlledSquare.Y.ToString() + " is not a legal move!");
                            break;
                        }
                        else
                        {
                            //Console.WriteLine("Square " + move.X.ToString() + ", " + move.Y.ToString() + " is fine!");
                            continue;
                        }
                    }
                }
            }

            return checklessMoves;
        }

        // checks for checkmate and stalemate
        public void checkmate()
        {
            bool playerHasMoves = false;
            bool kingIsAttacked = false;

            foreach (Piece p in Pieces) if (p.isWhite != whitesTurn)
                {
                    if (checklessMoves(p.controlledSquares(Pieces)).Count() != 0)
                    {
                        playerHasMoves = true;
                    }
                }

            foreach (Piece p in Pieces) if (p.isWhite == whitesTurn)
                {
                    foreach (Vector2 move in p.controlledSquares(Pieces))
                    {
                        if (king(!whitesTurn).x == (int)move.X && king(!whitesTurn).y == (int)move.Y)
                        {
                            kingIsAttacked = true;
                        }
                    }
                }

            if (!playerHasMoves && kingIsAttacked)
            {
                if (whitesTurn)
                {
                    Console.WriteLine("Checkmate! Black Wins!");
                }
                else
                {
                    Console.WriteLine("Checkmate! White Wins!");
                }
            }
            else if (!playerHasMoves && !kingIsAttacked)
            {
                Console.WriteLine("Stalemate!");
            }
            //Console.WriteLine("Checkmate was checked for");

        }

    }
}
