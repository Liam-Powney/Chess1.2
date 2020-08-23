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

        public bool whitesTurn = false;
        public List<Vector2> availableMoves = new List<Vector2>();

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
            if (selectedPiece() != null)
            {
                if (availableMoves.Any())
                {
                    foreach (Vector2 vec2 in availableMoves)
                    {
                        sb.Draw(selectCircle, new Rectangle((int)vec2.X * TILE_SIZE + TILE_SIZE * 1 / 5, (-(int)vec2.Y + 7) * TILE_SIZE + TILE_SIZE * 1 / 5, TILE_SIZE * 3 / 5, TILE_SIZE * 3 / 5), Color.Red * 0.65f);
                    }
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
            //Pieces.Add(new Knight(6, 2, true));
            //Pieces.Add(new Bishop(6, 6, true));
            //Pieces.Add(new Queen(4, 4, true));
            //Pieces.Add(new Pawn(1, 1, true));
            //Pieces.Add(new Pawn(0, 6, false));

        }

        //returns selected piece if there is one
        public Piece selectedPiece()
        {
            foreach(Piece p in Pieces)
            {
                if (p.isSelected)
                {
                    return p;
                }
            }
            return null;
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

        // returns whether mouse click is on a valid move square for the current selected piece
        public bool clickIsValidMove(int x, int y)
        {
            foreach (Vector2 vec2 in availableMoves)
            {
                if ((int)vec2.X == x && (int)vec2.Y == y)
                {
                    return true;
                }
            }
            return false;
        }

        // moves a piece, takes the piece if necessary, and changes who's turn it is
        public void Move(Piece p, int newX, int newY)
        {
            // en passant 
            if (p is Pawn && p.x != newX && pieceOnCoord(newX, newY) == null)
            {
                if (p.isWhite)
                {
                    Pieces.Remove(pieceOnCoord(newX, newY - 1));
                    Console.WriteLine("White took en passant!");
                }
                else
                {
                    Pieces.Remove(pieceOnCoord(newX, newY + 1));
                    Console.WriteLine("Black took en passant!");
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
            else if (pieceOnCoord(newX, newY) != null)
            {
                Pieces.Remove(pieceOnCoord(newX, newY));
                //Console.WriteLine(" a piece was taken!");
            }

            // track if a pawn double moved for en passant
            if (p is Pawn && Math.Abs(newY - p.y) == 2)
            {
                p.pawnLastDoulbeMove = true;
            }
            else if (p is Pawn && !(Math.Abs(newY - p.y) == 2))
            {
                p.pawnLastDoulbeMove = false;
            }

            // move piece to new location
            p.x = newX;
            p.y = newY;
            p.hasMoved = true;

            // makes it the other players turn and checks for checkmate and stalemate
            whitesTurn = !whitesTurn;
            checkmate();
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

        //returns all pieces of a single colour
        public List<Piece> colouredPieces(bool whitePieces)
        {
            List<Piece> colouredPieces = new List<Piece>();
            foreach(Piece p in Pieces)
            {
                if (p.isWhite == whitePieces)
                {
                    colouredPieces.Add(p);
                }
            }
            return colouredPieces;
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

        // check for checkmate
        public void checkmate()
        {
            List<Vector2> allMoves = new List<Vector2>();
            foreach(Piece p in Pieces)
            {
                if (p.isWhite == whitesTurn)
                {
                    foreach (Vector2 move in p.availableMoves(Pieces))
                    {
                        allMoves.Add(move);
                    }
                }
            }

            if(!allMoves.Any())
            {
                if (whitesTurn)
                {
                    //Console.WriteLine("Checkmate! Black Wins!");
                }
                else
                {
                    //Console.WriteLine("Checkmate! White Wins!");
                }
            }
            //Console.WriteLine("Checkmate was checked for");

        }

        //creates a copy of the current board instance
        public Board copiedBoard()
        {
            Board copiedBoard = new Board();
            copiedBoard.whitesTurn = whitesTurn;
            foreach(Piece p in this.Pieces)
            {
                copiedBoard.Pieces.Add(p.copyPiece());
                //Console.WriteLine("Copied a " + p.GetType().ToString());
            }
            //Console.WriteLine("Board was copied successfully!");
            return copiedBoard;
        }

        // copies a list of vec2s

        public List<Vector2> copyList(List<Vector2> list)
        {
            List<Vector2> copiedList = new List<Vector2>();
            foreach (Vector2 vec2 in list)
            {
                copiedList.Add(vec2);
            }
            return copiedList;
        }


        // vets the list of available moves to remove moves that would leave the player in check
        public List<Vector2> checklessAvailableMoves(List<Vector2> availableMoves)
        {
            int cnt = 1;
            List<Vector2> checklessAvailableMoves = copyList(availableMoves);
            // for each available move
            foreach (Vector2 move in availableMoves)
            {
                cnt++;
                // create a copy of the board
                Board copiedBoard = this.copiedBoard();
                // perform the move on the copied board
                copiedBoard.Move(copiedBoard.selectedPiece(), (int)move.X, (int)move.Y);
                // for all the pieces on the copied board that are the opposite colour to the piece just moved
                foreach (Piece p in copiedBoard.colouredPieces(!this.selectedPiece().isWhite))
                {
                    // create a list of new moves for that piece on the new board
                    List<Vector2> newMoves = p.availableMoves(copiedBoard.Pieces);
                    //for each of those moves:
                    foreach (Vector2 newMove in newMoves)
                    {
                        // if the move coords are the same as the kings coords
                        if ((int)newMove.X == copiedBoard.king(this.selectedPiece().isWhite).x && (int)newMove.Y == copiedBoard.king(this.selectedPiece().isWhite).y)
                        {
                            // remove the move from the original list as it would lead to check, and repeat for all the other moves
                            checklessAvailableMoves.Remove(move);
                            goto MoveRemoved;
                        }
                    }
                }

                goto MoveIsFine;

            MoveRemoved: //Console.WriteLine("Move was removed as it left the king in check!");
                //Console.WriteLine(cnt.ToString());
                continue;

            MoveIsFine: //Console.WriteLine("Move is Legal!");
                //Console.WriteLine(cnt.ToString());
                continue;



            }
            return checklessAvailableMoves;
        }

    }
}
