using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Knight : Piece
    {
        const int TILE_SIZE = 128;

        public Knight(int x, int y, bool isWhite)
            : base(x, y, isWhite)
        {
        }

        public override void DrawPiece(SpriteBatch sb)
        {
            Rectangle r = new Rectangle( x*TILE_SIZE + TILE_SIZE*1/9 , (-y+7)*TILE_SIZE + TILE_SIZE*1/9, TILE_SIZE*7/9, TILE_SIZE*7/9);
            if (this.isWhite)
            {
                sb.Draw(whiteKnight, r, Color.White);
            }
            else
            {
                sb.Draw(blackKnight, r, Color.White);
            }
        }

        public override Piece copyPiece()
        {
            Knight copiedPiece = new Knight(this.x, this.y, this.isWhite);
            copiedPiece.hasMoved = this.hasMoved;
            copiedPiece.pawnLastDoulbeMove = this.pawnLastDoulbeMove;
            return copiedPiece;
        }

        //returns available moves for a selected peice
        public override List<Vector2> controlledSquares(List<Piece> pieceList)
        {
            List<Vector2> potentialMoves = new List<Vector2>();
            potentialMoves.Add(new Vector2(this.x + 1, this.y + 2));
            potentialMoves.Add(new Vector2(this.x - 1, this.y + 2));
            potentialMoves.Add(new Vector2(this.x + 1, this.y - 2));
            potentialMoves.Add(new Vector2(this.x - 1, this.y - 2));
            potentialMoves.Add(new Vector2(this.x + 2, this.y + 1));
            potentialMoves.Add(new Vector2(this.x + 2, this.y - 1));
            potentialMoves.Add(new Vector2(this.x - 2, this.y + 1));
            potentialMoves.Add(new Vector2(this.x - 2, this.y - 1));

            potentialMoves.RemoveAll(move => move.X > 7 || move.X < 0 || move.Y > 7 || move.Y < 0);

            List<Vector2> controlledSquares = new List<Vector2>();
            foreach(Vector2 move in potentialMoves)
            {
                controlledSquares.Add(move);
            }

            foreach(Vector2 move in potentialMoves)
            {
                foreach(Piece p in pieceList) if (p.x == (int)move.X && p.y == (int)move.Y && p.isWhite == this.isWhite)
                {
                    controlledSquares.Remove(move);
                }
            }

            return controlledSquares;
        }
    }
}
