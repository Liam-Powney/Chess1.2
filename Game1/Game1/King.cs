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

        public override Piece copyPiece()
        {
            King copiedPiece = new King(this.x, this.y, this.isWhite);
            copiedPiece.isSelected = this.isSelected;
            copiedPiece.hasMoved = this.hasMoved;
            copiedPiece.pawnLastDoulbeMove = this.pawnLastDoulbeMove;
            return copiedPiece;
        }

        public override List<Vector2> availableMoves(List<Piece> pieceList)
        {
            //Console.WriteLine("Finding King Moves");
            List<Vector2> availableMoves = new List<Vector2>();

            for (int i = -1; i < 2; i++)
			{
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    availableMoves.Add(new Vector2(x + i, y + j));

                    foreach (Piece p in pieceList)
                    {
                        if (p.x == (x + i) && p.y == (y + j))
                        {
                            if (p.isWhite != isWhite)
                            {
                                break;
                            }
                            else
                            {
                                availableMoves.RemoveAt(availableMoves.Count - 1);
                                break;
                            }
                        }
                    }
                }
			}

            // castlingOptions

            // King Side Castling

            List<Vector2> castlingSquares = new List<Vector2>();
            castlingSquares.Add(new Vector2(this.x + 1, this.y));
            castlingSquares.Add(new Vector2(this.x + 2, this.y));

            
            foreach(Vector2 square in castlingSquares)
            {
                // is there a piece blocking castling?
                foreach(Piece p in pieceList)
                {
                    if (p.x == (int)square.X && p.y == (int)square.Y)
                    {
                        goto QueenSideCastle;
                    }
                }
                // removes castling through check
                foreach(Piece p in pieceList) if (p.isWhite != this.isWhite && !(p is King))
                {
                    foreach(Vector2 move in p.availableMoves(pieceList))
                    {
                        if (move.X == square.X && move.Y == square.Y)
                        {
                            goto QueenSideCastle;
                        }
                    }
                }
            }

            // checks that the rook satisfies castling requirements 
            castlingSquares.Add(new Vector2(this.x + 3, this.y));
            List<Piece> RookList = new List<Piece>();
            foreach(Piece p in pieceList) if (p is Rook && p.isWhite == this.isWhite)
            {
                RookList.Add(p);
            }

            foreach (Piece rook in RookList) if (rook.x == (int)castlingSquares[2].X && rook.y == (int)castlingSquares[2].Y && !rook.hasMoved)
            {
                availableMoves.Add(new Vector2(this.x + 2, this.y));
            }

            // Queen Side Castling

        QueenSideCastle: ;

            castlingSquares.Clear();

            castlingSquares.Add(new Vector2(this.x - 1, this.y));
            castlingSquares.Add(new Vector2(this.x - 2, this.y));
            castlingSquares.Add(new Vector2(this.x - 3, this.y));

            foreach (Vector2 square in castlingSquares)
            {
                foreach (Piece p in pieceList)
                {
                    if (p.x == (int)square.X && p.y == (int)square.Y)
                    {
                        goto End;
                    }
                }
                foreach (Piece p in pieceList) if (p.isWhite != this.isWhite && !(p is King))
                    {
                        foreach (Vector2 move in p.availableMoves(pieceList))
                        {
                            if (move.X == square.X && move.Y == square.Y)
                            {
                                goto End;
                            }
                        }
                    }
            }

            castlingSquares.Add(new Vector2(this.x - 4, this.y));
            // if there is a rook that hasn't moved on the correct square for castling
            List<Piece> RookList1 = new List<Piece>();
            foreach (Piece p in pieceList) if (p is Rook && p.isWhite == this.isWhite)
                {
                    RookList1.Add(p);
                }

            foreach (Piece rook in RookList1) if (rook.x == (int)castlingSquares[3].X && rook.y == (int)castlingSquares[3].Y && !rook.hasMoved)
                {
                    availableMoves.Add(new Vector2(this.x - 2, this.y));
                }

            End : 

            return availableMoves;
        }


    }
}
