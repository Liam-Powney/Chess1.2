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
    public class Piece
    {
        const int TILE_SIZE = 128;

        public int x;
        public int y;
        public bool isWhite;
        public bool isSelected;

        public static Texture2D whiteRook;
        public static Texture2D blackRook;
        public static Texture2D whiteKnight;
        public static Texture2D blackKnight;
        public static Texture2D whiteBishop;
        public static Texture2D blackBishop;
        public static Texture2D whiteQueen;
        public static Texture2D blackQueen;
        public static Texture2D whiteKing;
        public static Texture2D blackKing;
        public static Texture2D whitePawn;
        public static Texture2D blackPawn;

        public Piece(int x, int y, bool isWhite)
        {
            this.x = x;
            this.y = y;
            this.isWhite = isWhite;
            this.isSelected = false;
        }

        public virtual List<Vector2> availableMoves()
        {
            return new List<Vector2>();
        }

        public static void LoadPieceTextures(ContentManager c)
        {
            whiteRook = c.Load<Texture2D>("w_rook");
            blackRook= c.Load<Texture2D>("b_rook");
            whiteKnight = c.Load<Texture2D>("w_knight");
            blackKnight = c.Load<Texture2D>("b_knight");
            whiteBishop = c.Load<Texture2D>("w_bishop");
            blackBishop = c.Load<Texture2D>("b_bishop");
            whiteQueen = c.Load<Texture2D>("w_queen");
            blackQueen = c.Load<Texture2D>("b_queen");
            whiteKing = c.Load<Texture2D>("w_king");
            blackKing = c.Load<Texture2D>("b_king");
            whitePawn = c.Load<Texture2D>("w_pawn");
            blackPawn = c.Load<Texture2D>("b_pawn");
        }

        public void DrawPiece(SpriteBatch sb)
        {
            Rectangle r = new Rectangle( x*TILE_SIZE + TILE_SIZE*1/9 , (-y+7)*TILE_SIZE + TILE_SIZE*1/9, TILE_SIZE*7/9, TILE_SIZE*7/9);
            if (this is Rook == true)
            {
                if (this.isWhite)
                {
                    sb.Draw(whiteRook, r, Color.White);
                }
                else
                {
                    sb.Draw(blackRook, r, Color.White);
                }
                
            }
        }

    }
}
