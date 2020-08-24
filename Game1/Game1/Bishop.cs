using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Bishop : Piece
    {
        const int TILE_SIZE = 128;

        public Bishop(int x, int y, bool isWhite)
            : base(x, y, isWhite)
        {
        }

        public override void DrawPiece(SpriteBatch sb)
        {
            Rectangle r = new Rectangle( x*TILE_SIZE + TILE_SIZE*1/9 , (-y+7)*TILE_SIZE + TILE_SIZE*1/9, TILE_SIZE*7/9, TILE_SIZE*7/9);
            if (this.isWhite)
            {
                sb.Draw(whiteBishop, r, Color.White);
            }
            else
            {
                sb.Draw(blackBishop, r, Color.White);
            }
        }

        public override Piece copyPiece()
        {
            Bishop copiedPiece = new Bishop(this.x, this.y, this.isWhite);
            copiedPiece.hasMoved = this.hasMoved;
            copiedPiece.pawnLastDoulbeMove = this.pawnLastDoulbeMove;
            return copiedPiece;
        }

        //returns available moves for a selected peice
        public override List<Vector2> controlledSquares(List<Piece> pieceList)
        {
            List<Vector2> controlledSquares = new List<Vector2>();

            int squareToCheckX = this.x;
            int squareToCheckY = this.y;
            
            //checks squares up and right
            while(squareToCheckX < 8)
            {
                squareToCheckX++;
                squareToCheckY++;
                foreach(Piece p in pieceList)
                {
                    if (p.x == squareToCheckX && p.y == squareToCheckY)
                    {
                        if (this.isWhite == p.isWhite)
                        {
                            goto NextIteration;
                        }
                        else
                        {
                            controlledSquares.Add(new Vector2(squareToCheckX, squareToCheckY));
                            goto NextIteration;
                        }
                    }
                }
                controlledSquares.Add(new Vector2(squareToCheckX, squareToCheckY));
            }

            NextIteration : 

            squareToCheckX = this.x;
            squareToCheckY = this.y;

            // checks squares down and right
            while (squareToCheckX < 8)
            {
                squareToCheckX++;
                squareToCheckY--;
                foreach (Piece p in pieceList)
                {
                    if (p.x == squareToCheckX && p.y == squareToCheckY)
                    {
                        if (this.isWhite == p.isWhite)
                        {
                            goto NextIteration1;
                        }
                        else
                        {
                            controlledSquares.Add(new Vector2(squareToCheckX, squareToCheckY));
                            goto NextIteration1;
                        }
                    }
                }
                controlledSquares.Add(new Vector2(squareToCheckX, squareToCheckY));
            }

            NextIteration1 :

            squareToCheckX = this.x;
            squareToCheckY = this.y;

            // checks squares down and left
            while (squareToCheckX >= 0)
            {
                squareToCheckX--;
                squareToCheckY--;
                foreach(Piece p in pieceList)
                {
                    if (p.x == squareToCheckX && p.y == squareToCheckY)
                    {
                        if (this.isWhite == p.isWhite)
                        {
                            goto NextIteration2;
                        }
                        else
                        {
                            controlledSquares.Add(new Vector2(squareToCheckX, squareToCheckY));
                            goto NextIteration2;
                        }
                    }
                }
                controlledSquares.Add(new Vector2(squareToCheckX, squareToCheckY));
            }

            NextIteration2 :

            squareToCheckX = this.x;
            squareToCheckY = this.y;

            // checks squares up and left
            while (squareToCheckX >= 0)
            {
                squareToCheckX--;
                squareToCheckY++;
                foreach(Piece p in pieceList)
                {
                    if (p.x == squareToCheckX && p.y == squareToCheckY)
                    {
                        if (this.isWhite == p.isWhite)
                        {
                            goto End;
                        }
                        else
                        {
                            controlledSquares.Add(new Vector2(squareToCheckX, squareToCheckY));
                            goto End;
                        }
                    }
                }
                controlledSquares.Add(new Vector2(squareToCheckX, squareToCheckY));
            }
        End: return controlledSquares;
        }
    }
}
