using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Rook : Piece
    {
        
        public Rook(int x, int y, bool isWhite) : base(x, y, isWhite)
        {
        }

        public override List<Vector2> availableMoves()
        {
            List<Vector2> availableMoves = new List<Vector2>();

            return availableMoves;
        }

    }
}
