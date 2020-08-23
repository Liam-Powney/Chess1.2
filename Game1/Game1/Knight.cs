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
            copiedPiece.isSelected = this.isSelected;
            copiedPiece.hasMoved = this.hasMoved;
            copiedPiece.pawnLastDoulbeMove = this.pawnLastDoulbeMove;
            return copiedPiece;
        }

        //returns available moves for a selected peice
        public override List<Vector2> availableMoves(List<Piece> pieceList)
        {
            List<Vector2> availableMoves = new List<Vector2>();
            availableMoves.Add(new Vector2(this.x + 1, this.y + 2));
            availableMoves.Add(new Vector2(this.x - 1, this.y + 2));
            availableMoves.Add(new Vector2(this.x + 1, this.y - 2));
            availableMoves.Add(new Vector2(this.x - 1, this.y - 2));
            availableMoves.Add(new Vector2(this.x + 2, this.y + 1));
            availableMoves.Add(new Vector2(this.x + 2, this.y - 1));
            availableMoves.Add(new Vector2(this.x - 2, this.y + 1));
            availableMoves.Add(new Vector2(this.x - 2, this.y - 1));

            availableMoves.RemoveAll(move => move.X > 7 || move.X < 0 || move.Y > 7 || move.Y < 0);

            return availableMoves;
        }
    }
}
