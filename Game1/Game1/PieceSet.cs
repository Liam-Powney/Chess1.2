using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class PieceSet
    {
        public List<Piece> pieceList = new List<Piece>();
        public bool isWhite;

        public PieceSet(bool isWhite)
        {
            this.isWhite = isWhite;
        }
    }
}
