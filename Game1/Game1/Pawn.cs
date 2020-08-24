using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Pawn : Piece
    {
        const int TILE_SIZE = 128;

        public Pawn(int x, int y, bool isWhite) : base(x, y, isWhite)
        {
        }

        public override void DrawPiece(SpriteBatch sb)
        {
            Rectangle r = new Rectangle( x*TILE_SIZE + TILE_SIZE*1/9 , (-y+7)*TILE_SIZE + TILE_SIZE*1/9, TILE_SIZE*7/9, TILE_SIZE*7/9);
            if (this.isWhite)
            {
                sb.Draw(whitePawn, r, Color.White);
            }
            else
            {
                sb.Draw(blackPawn, r, Color.White);
            }
        }

        public override Piece copyPiece()
        {
            Pawn copiedPiece = new Pawn(this.x, this.y, this.isWhite);
            return copiedPiece;
        }

        //returns available moves for a selected peice
        public override List<Vector2> controlledSquares(List<Piece> pieceList)
        {
            List<Vector2> controlledSquares = new List<Vector2>();

            if (this.isWhite)
            {
                // check move forward one square
                foreach(Piece p in pieceList)
                {
                    if (p.x == this.x && p.y == this.y + 1)
                    {
                        goto WhitePawnAttack;
                    }
                }
                controlledSquares.Add(new Vector2(this.x, this.y + 1));

                //check attacking squares
                WhitePawnAttack : 

                foreach(Piece p in pieceList)
                {
                    if (p.x == this.x - 1 && p.y == this.y + 1 && p.isWhite != this.isWhite)
                    {
                        controlledSquares.Add(new Vector2(this.x - 1, this.y + 1));
                    }
                    else if (p.x == this.x + 1 && p.y == this.y + 1 && p.isWhite != this.isWhite)
                    {
                        controlledSquares.Add(new Vector2(this.x + 1, this.y + 1));
                    }
                }

                //check double move square
                if (!this.hasMoved)
                {
                    foreach(Piece p in pieceList)
                    {
                        if (p.x == this.x && p.y == this.y + 2 || p.x == this.x && p.y == this.y + 1)
                        {
                            goto End;
                        }
                    }
                    controlledSquares.Add(new Vector2(this.x, this.y + 2));
                }

                // en passant
                foreach(Piece p in pieceList)
                {
                    if (p is Pawn && this.y == p.y && this.x + 1 == p.x && p.pawnLastDoulbeMove)
                    {
                        controlledSquares.Add(new Vector2(this.x + 1, this.y + 1));
                    }
                    else if (p is Pawn && this.y == p.y && this.x -1 == p.x && p.pawnLastDoulbeMove)
                    {
                        controlledSquares.Add(new Vector2(this.x - 1, this.y + 1));
                    }
                }

            End: return controlledSquares;
            }
            else
            {
                // check move forward one square
                foreach (Piece p in pieceList)
                {
                    if (p.x == this.x && p.y == this.y - 1)
                    {
                        goto BlackPawnAttack;
                    }
                }
                controlledSquares.Add(new Vector2(this.x, this.y - 1));

                //check attacking squares
                BlackPawnAttack:

                foreach (Piece p in pieceList)
                {
                    if (p.x == this.x - 1 && p.y == this.y - 1 && p.isWhite != this.isWhite)
                    {
                        controlledSquares.Add(new Vector2(this.x - 1, this.y - 1));
                    }
                    else if (p.x == this.x + 1 && p.y == this.y - 1 && p.isWhite != this.isWhite)
                    {
                        controlledSquares.Add(new Vector2(this.x + 1, this.y - 1));
                    }
                }

                //check double move square
                if (!this.hasMoved)
                {
                    foreach (Piece p in pieceList)
                    {
                        if (p.x == this.x && p.y == this.y - 2 || p.x == this.x && p.y == this.y - 1)
                        {
                            goto End;
                        }
                    }
                    controlledSquares.Add(new Vector2(this.x, this.y - 2));
                }

                // en passant
                foreach (Piece p in pieceList)
                {
                    if (p is Pawn && this.y == p.y && this.x + 1 == p.x && p.pawnLastDoulbeMove)
                    {
                        controlledSquares.Add(new Vector2(this.x + 1, this.y - 1));
                    }
                    else if (p is Pawn && this.y == p.y && this.x -1 == p.x && p.pawnLastDoulbeMove)
                    {
                        controlledSquares.Add(new Vector2(this.x - 1, this.y - 1));
                    }
                }

            End: return controlledSquares;
            }
        }
    }
}
