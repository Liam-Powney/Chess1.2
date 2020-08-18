using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class King : Piece
    {

        const int TILE_SIZE = 128;

        public King(int x, int y, bool isWhite)
            : base(x, y, isWhite)
        {
        }

        public override void DrawPiece(SpriteBatch sb)
        {
            Rectangle r = new Rectangle(x * TILE_SIZE + TILE_SIZE * 1 / 9, (-y + 7) * TILE_SIZE + TILE_SIZE * 1 / 9, TILE_SIZE * 7 / 9, TILE_SIZE * 7 / 9);
            if (this.isWhite)
            {
                sb.Draw(whiteKing, r, Color.White);
            }
            else
            {
                sb.Draw(blackKing, r, Color.White);
            }
        }

        public override List<Vector2> availableMoves(List<Piece> pieceList)
        {
            List<Vector2> availableMoves = new List<Vector2>();

            for (int i = -1; i < 2; i++)
			{
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    foreach (Piece p in pieceList)
                    {
                        if (p.x == i && p.y == j)
                        {
                            if (p.isWhite != isWhite)
                            {
                                availableMoves.Add(new Vector2(x + i, y + j));
                                break;
                            }
                            break;
                        }
                    }
                    availableMoves.Add(new Vector2(x + i, y + j));
                }
			}

            return availableMoves;
        }


    }
}
