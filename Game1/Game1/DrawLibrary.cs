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
    public class DrawLibrary
    {

        public const int TILE_SIZE = 128;


        public static void DrawBoard(SpriteBatch s, ContentManager c, Board board, Texture2D[] textureArray, bool theme, List<BoardSquare> potentialMoves)
        {
            for(int i = 0; i < 8; i++)
                for(int j = 0; j < 8; j++)
                {
                    if (theme == true)
                    {
                        if ( (i + j) % 2 == 0)
                        {
                            s.Draw(textureArray[0], new Rectangle(board.boardSquares[i, j].SquareCoord.x * TILE_SIZE, board.boardSquares[i, j].SquareCoord.y * TILE_SIZE, TILE_SIZE, TILE_SIZE), Color.White);
                        }
                        else
                        {
                            s.Draw(textureArray[1], new Rectangle(board.boardSquares[i, j].SquareCoord.x * TILE_SIZE, board.boardSquares[i, j].SquareCoord.y * TILE_SIZE, TILE_SIZE, TILE_SIZE), Color.White);
                        }
                    }
                    else
                    {
                        if ( (i + j) % 2 == 0)
                        {
                            s.Draw(textureArray[2], new Rectangle(board.boardSquares[i, j].SquareCoord.x * TILE_SIZE, board.boardSquares[i, j].SquareCoord.y * TILE_SIZE, TILE_SIZE, TILE_SIZE), Color.White);
                        }
                        else
                        {
                            s.Draw(textureArray[3], new Rectangle(board.boardSquares[i, j].SquareCoord.x * TILE_SIZE, board.boardSquares[i, j].SquareCoord.y * TILE_SIZE, TILE_SIZE, TILE_SIZE), Color.White);
                        }
                    }
                }
            foreach(PieceSet set in board.allPieces)
            {
                if (set.isWhite == true)
                {
                    //draw white pieces
                    foreach(Piece piece in set.pieceList)
                    {
                        if (piece is Rook == true)
                        {
                            s.Draw(textureArray[4], new Rectangle(piece.PieceCoord.x*TILE_SIZE, piece.PieceCoord.y*TILE_SIZE, TILE_SIZE, TILE_SIZE), Color.White);
                        }
                    }
                }
                else
                {
                    //draw black pieces
                    foreach (Piece piece in set.pieceList)
                    {
                        if (piece is Rook == true)
                        {
                            s.Draw(textureArray[5], new Rectangle(piece.PieceCoord.x * TILE_SIZE, piece.PieceCoord.y * TILE_SIZE, TILE_SIZE, TILE_SIZE), Color.White);
                        }
                    }

                }
            }
            if (potentialMoves.Any() == true)
            {
                foreach(BoardSquare square in potentialMoves)
                {
                    s.Draw(textureArray[16], new Rectangle(square.SquareCoordX * TILE_SIZE + TILE_SIZE / 5, square.SquareCoordY * TILE_SIZE + TILE_SIZE / 5, TILE_SIZE * 3 / 5, TILE_SIZE * 3 / 5), Color.Red*0.6f);
                }
            }
        }
    }
}

