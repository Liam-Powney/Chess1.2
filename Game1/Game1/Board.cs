using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Board
    {
        public BoardSquare[,] boardSquares = new BoardSquare[8,8];
        public PieceSet[] allPieces = new PieceSet[2];

        public Board()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    boardSquares[i, j] = new BoardSquare(i, j);
                }
            }
            allPieces[0] = new PieceSet(true);
            allPieces[1] = new PieceSet(false);
        }

        public bool isPieceOnSquare(int xCoord, int yCoord)
        {
            foreach(PieceSet pieceSet in allPieces)
            {
                foreach(Piece piece in pieceSet.pieceList)
                {
                    if (piece.PieceCoord.x == xCoord && piece.PieceCoord.y == yCoord)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool pieceOnSquareIsWhite(int xCoord, int yCoord)
        {
            foreach (PieceSet pieceSet in allPieces)
            {
                foreach (Piece piece in pieceSet.pieceList)
                {
                    if (piece.PieceCoord.x == xCoord && piece.PieceCoord.y == yCoord)
                    {
                        return piece.isWhite;
                    }
                }
            }
            return false;
        }

        public Piece pieceOnSquare(int xCoord, int yCoord)
        {
            foreach (PieceSet pieceSet in allPieces)
            {
                foreach (Piece piece in pieceSet.pieceList)
                {
                    if (piece.PieceCoord.x == xCoord && piece.PieceCoord.y == yCoord)
                    {
                        return piece;
                    }
                }
            }
            return null;
        }

        public void setupNewPieces()
        {
            allPieces[0].pieceList.Clear();
            allPieces[1].pieceList.Clear();

            Rook whiteRook1 = new Rook(0, 7, true);
            Rook whiteRook2 = new Rook(7, 7, true);
            Rook blackRook1 = new Rook(0, 0, false);
            Rook blackRook2 = new Rook(7, 0, false);
            allPieces[0].pieceList.Add(whiteRook1);
            allPieces[0].pieceList.Add(whiteRook2);
            allPieces[1].pieceList.Add(blackRook1);
            allPieces[1].pieceList.Add(blackRook2);

        }
    }
}
