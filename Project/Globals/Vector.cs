using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globals
{
    public class Vector
    {
        public double length;
        public double X;
        public double Y;

        public Vector(double X, double Y)
        {
            this.X = X/Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            this.Y = Y / Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        }
    }
}
