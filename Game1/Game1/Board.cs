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
            foreach(Piece p in Pieces)
            {
                if (p.isSelected)
                {
                    foreach(Vector2 vec2 in availableMoves(p))
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

        //returns piece on give board coord
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

        // returns whether a move is valid
        public bool isMoveValid(int x, int y)
        {
            foreach (Vector2 vec2 in availableMoves(selectedPiece()))
            {
                if ((int)vec2.X == x && (int)vec2.Y == y)
                {
                    return true;
                }
            }
            return false;
        }


        //returns available moves for a selected peice
        public List<Vector2> availableMoves(Piece p)
        {
            List<Vector2> availableMoves = new List<Vector2>();

            if (p is Rook)
            {
                //check squares left of rook
                for (int i = p.x+1; i < 8; i++)
                {
                    if (pieceOnCoord(i, p.y) == null)
                    {
                        availableMoves.Add(new Vector2(i, p.y));
                    }
                    else
                    {
                        if (pieceOnCoord(i, p.y).isWhite != p.isWhite)
                        {
                            availableMoves.Add(new Vector2(i, p.y));
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                //check squares right of rook
                for (int i = p.x - 1; i >= 0; i = i-1)
                {
                    if (pieceOnCoord(i, p.y) == null)
                    {
                        availableMoves.Add(new Vector2(i, p.y));
                    }
                    else
                    {
                        if (pieceOnCoord(i, p.y).isWhite != p.isWhite)
                        {
                            availableMoves.Add(new Vector2(i, p.y));
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                //check squares above rook
                for (int i = p.y + 1; i < 8; i++)
                {
                    if (pieceOnCoord(p.x, i) == null)
                    {
                        availableMoves.Add(new Vector2(p.x, i));
                    }
                    else
                    {
                        if (pieceOnCoord(p.x, i).isWhite != p.isWhite)
                        {
                            availableMoves.Add(new Vector2(p.x, i));
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                //check squares under rook
                for (int i = p.y - 1; i >= 0; i = i - 1)
                {
                    if (pieceOnCoord(p.x, i) == null)
                    {
                        availableMoves.Add(new Vector2(p.x, i));
                    }
                    else
                    {
                        if (pieceOnCoord(p.x, i).isWhite != p.isWhite)
                        {
                            availableMoves.Add(new Vector2(p.x, i));
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return availableMoves;
        }

        // moves a piece, and takes the piece if necessary
        public void Move(Piece p, int newX, int newY)
        {

            if (pieceOnCoord(newX, newY) != null)
            {
                Pieces.Remove(pieceOnCoord(newX, newY));
                Console.WriteLine(" a piece was taken!");
            }
            p.x = newX;
            p.y = newY;
        }
    }
}
