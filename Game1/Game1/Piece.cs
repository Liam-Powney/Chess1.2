﻿using Microsoft.Xna.Framework;
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
        public bool hasMoved;
        public bool pawnLastDoulbeMove = false;

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
            this.hasMoved = false;
        }

        public virtual List<Vector2> controlledSquares(List<Piece> pieceList)
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

        public virtual void DrawPiece(SpriteBatch sb)
        {
        }

        // creates a new copy of an instance of a piece
        public virtual Piece copyPiece()
        {
            Piece copiedPiece = new Piece(this.x, this.y, this.isWhite);
            copiedPiece.hasMoved = this.hasMoved;
            copiedPiece.pawnLastDoulbeMove = this.pawnLastDoulbeMove;
            return copiedPiece;
        }

    }
}
