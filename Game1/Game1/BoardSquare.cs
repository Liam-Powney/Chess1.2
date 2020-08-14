using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{

    public class BoardSquare
    {
        public Coord squareCoord;

        public BoardSquare(int x, int y)
        {
            this.squareCoord.x = x;
            this.squareCoord.y = y;
        }

        public int SquareCoordX
        {
            get
            {
                return squareCoord.x;
            }
            set
            {
                squareCoord.x = value;
            }
        }

        public int SquareCoordY
        {
            get
            {
                return squareCoord.y;
            }
            set
            {
                squareCoord.y = value;
            }
        }

        public Coord SquareCoord
        {
            get
            {
                return squareCoord;
            }
            set
            {
                squareCoord = value;
            }
        }

    }
}
