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
                    foreach(Vector2 vec2 in p.availableMoves(Pieces))
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




        // returns whether mouse click is on a valid move square for the current selected piece
        public bool isMoveValid(int x, int y)
        {
            foreach (Vector2 vec2 in selectedPiece().availableMoves(Pieces))
            {
                if ((int)vec2.X == x && (int)vec2.Y == y)
                {
                    return true;
                }
            }
            return false;
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
            p.hasMoved = true;
            whitesTurn = !whitesTurn;
        }

        // returns the king piece for the current players turn
        public Piece king()
        {
            foreach (Piece p in Pieces)
            {
                if (p is King && p.isWhite == whitesTurn)
                {
                    return p;
                }
            }
            return null;
        }

    }
}
