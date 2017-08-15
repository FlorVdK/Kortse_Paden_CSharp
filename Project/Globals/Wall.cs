using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globals
{
    public class Wall
    {
        public int X;
        public int Y;
        public int width;
        public int heigth;

        public Wall(int X, int Y, int width, int heigth)
        {
            this.X = X;
            this.Y = Y;
            this.width = width;
            this.heigth = heigth;
        }

    }
}
