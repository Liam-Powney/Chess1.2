using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Rook : Piece
    {

        const int TILE_SIZE = 128;

        public Rook(int x, int y, bool isWhite)
            : base(x, y, isWhite)
        {
        }

        public override void DrawPiece(SpriteBatch sb)
        {
            Rectangle r = new Rectangle( x*TILE_SIZE + TILE_SIZE*1/9 , (-y+7)*TILE_SIZE + TILE_SIZE*1/9, TILE_SIZE*7/9, TILE_SIZE*7/9);
            if (this.isWhite)
            {
                sb.Draw(whiteRook, r, Color.White);
            }
            else
            {
                sb.Draw(blackRook, r, Color.White);
            }
        }

        //returns available moves for a selected peice
        public override List<Vector2> availableMoves(List<Piece> pieceList)
        {
            List<Vector2> availableMoves = new List<Vector2>();
            bool timeToStop = false;

            //check squares left of rook
            for (int i = x + 1; i < 8; i++)
            {
                foreach (Piece p in pieceList)
                {
                    if (p.x == i && p.y == y)
                    {
                        if (p.isWhite != isWhite)
                        {
                            availableMoves.Add(new Vector2(i, y));
                            timeToStop = true;
                            break;
                        }
                        timeToStop = true;
                        break;
                    }
                }
                if (timeToStop)
                {
                    break;
                }
                else
                {
                    availableMoves.Add(new Vector2(i, y));
                }
            }
            timeToStop = false;

            //check squares right of rook
            for (int i = x - 1; i >= 0; i = i - 1)
            {
                foreach (Piece p in pieceList)
                {
                    if (p.x == i && p.y == y)
                    {
                        if (p.isWhite != isWhite)
                        {
                            availableMoves.Add(new Vector2(i, y));
                            timeToStop = true;
                            break;
                        }
                        timeToStop = true;
                        break;
                    }
                }
                if (timeToStop)
                {
                    break;
                }
                else
                {
                    availableMoves.Add(new Vector2(i, y));
                }
            }
            timeToStop = false;

            //check squares above rook
            for (int i = y + 1; i < 8; i++)
            {
                foreach (Piece p in pieceList)
                {
                    if (p.x == x && p.y == i)
                    {
                        if (p.isWhite != isWhite)
                        {
                            availableMoves.Add(new Vector2(x, i));
                            timeToStop = true;
                            break;
                        }
                        timeToStop = true;
                        break;
                    }
                }
                if (timeToStop)
                {
                    break;
                }
                else
                {
                    availableMoves.Add(new Vector2(x, i));
                }
            }
            timeToStop = false;

            //check squares under rook
            for (int i = y - 1; i >= 0; i = i - 1)
            {
                foreach (Piece p in pieceList)
                {
                    if (p.x == x && p.y == i)
                    {
                        if (p.isWhite != isWhite)
                        {
                            availableMoves.Add(new Vector2(x, i));
                            timeToStop = true;
                            break;
                        }
                        timeToStop = true;
                        break;
                    }
                }
                if (timeToStop)
                {
                    break;
                }
                else
                {
                    availableMoves.Add(new Vector2(x, i));
                }
            }

            return availableMoves;
        }
    }
}

