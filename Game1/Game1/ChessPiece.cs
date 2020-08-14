using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Piece
    {
        public Coord pieceCoord;
        public bool isWhite;
        public bool hasMoved;

        public Piece(int x, int y, bool isWhite)
        {
            this.pieceCoord.x = x;
            this.pieceCoord.y = y;
            this.isWhite = isWhite;
            this.hasMoved = false;
        }

        public Coord PieceCoord
        {
            get
            {
                return pieceCoord;
            }
            set
            {
                pieceCoord = value;
            }
        }

        public void SetHasMoved()
        {
            this.hasMoved = true;
        }


        //THIS DOESNT WORK LOL
        public List<BoardSquare> validMoves(Board board)
        {
            List<BoardSquare> validMoves = new List<BoardSquare>();

            if (this is Rook == true)
            {
                for(int i = ; i > 0; i = i - 1)
                {
                    if (board.isPieceOnSquare(i, this.PieceCoord.y) == false)
                    {
                        validMoves.Add(board.boardSquares[i, this.PieceCoord.y]);
                    }
                }





                /*//Check Squares Left of Rook
                for (int i = this.PieceCoord.x; i >= 0; i = i - 1)
                {
                    if (board.isPieceOnSquare(i, this.PieceCoord.y) == false)
                    {
                        validMoves.Add(board.boardSquares[i, this.PieceCoord.y]);
                    }
                    else
                    {
                        if (board.pieceOnSquareIsWhite(i, this.PieceCoord.y) != this.isWhite)
                        {
                            validMoves.Add(board.boardSquares[i, this.PieceCoord.y]);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                //CheckSquare Right of Rook
                for (int i = this.PieceCoord.x; i <= 7; i = i + 1)
                {
                    if (board.isPieceOnSquare(i, this.PieceCoord.y) == false)
                    {
                        validMoves.Add(board.boardSquares[i, this.PieceCoord.y]);
                    }
                    else
                    {
                        if (board.pieceOnSquareIsWhite(i, this.PieceCoord.y) != this.isWhite)
                        {
                            validMoves.Add(board.boardSquares[i, this.PieceCoord.y]);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                //Check Squares Above Rook
                for (int i = this.PieceCoord.y; i >= 0; i = i - 1)
                {
                    if (board.isPieceOnSquare(this.PieceCoord.x, i) == false)
                    {
                        validMoves.Add(board.boardSquares[this.PieceCoord.x, i]);
                    }
                    else
                    {
                        if (board.pieceOnSquareIsWhite(this.PieceCoord.x, i) != this.isWhite)
                        {
                            validMoves.Add(board.boardSquares[this.PieceCoord.x, i]);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                //Check Squares Under Rook
                for (int i = this.PieceCoord.y; i <= 0; i = i + 1)
                {
                    if (board.isPieceOnSquare(this.PieceCoord.x, i) == false)
                    {
                        validMoves.Add(board.boardSquares[this.PieceCoord.x, i]);
                    }
                    else
                    {
                        if (board.pieceOnSquareIsWhite(this.PieceCoord.x, i) != this.isWhite)
                        {
                            validMoves.Add(board.boardSquares[this.PieceCoord.x, i]);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }*/
            }
            return validMoves;
        }
        
    }
}
