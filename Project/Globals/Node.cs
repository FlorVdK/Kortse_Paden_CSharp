using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globals
{
    public class Node
    {
        public int X;
        public int Y;
        public Node nextnode;

        public Node(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void DrawLine(Graphics g, Color color)
        {
            g.DrawLine(new Pen(color), nextnode.X, nextnode.Y, X, Y);
        }

        public void BerekenAfstand(List<Node> nodes)
        {
            double min = 100000000;
            foreach (var node in nodes)
            {
                double dX = node.X - X;
                double dY = node.Y - Y;
                double dist = dX * dX + dY * dY;
                if (dist < min)
                {
                    min = dist;
                    nextnode = node;
                }
            }
        }

        public bool collide(Wall[] w)
        {
            bool collide = false;
            foreach (var wall in w)
            {
                if ((wall.X <= X+30 && wall.X + wall.width >= X-30 && wall.Y <= Y +30 && wall.Y + wall.heigth >=Y-30) ||
                    LineIntersectsLine(new Point(X, Y), new Point(nextnode.X, nextnode.Y), new Point(wall.X, wall.Y), new Point(wall.X + wall.width, wall.Y)) ||
                    LineIntersectsLine(new Point(X, Y), new Point(nextnode.X, nextnode.Y), new Point(wall.X + wall.width, wall.Y), new Point(wall.X + wall.width, wall.Y + wall.heigth)) ||
                    LineIntersectsLine(new Point(X, Y), new Point(nextnode.X, nextnode.Y), new Point(wall.X + wall.width, wall.Y + wall.heigth), new Point(wall.X , wall.Y + wall.heigth)) ||
                    LineIntersectsLine(new Point(X, Y), new Point(nextnode.X, nextnode.Y), new Point(wall.X, wall.Y + wall.heigth), new Point(wall.X , wall.Y))
                    )
                {
                    collide = true;
                }       
                         
            }
            return collide;
        }


        //https://stackoverflow.com/questions/5514366/how-to-know-if-a-line-intersects-a-rectangle
        private bool LineIntersectsLine(Point l1p1, Point l1p2, Point l2p1, Point l2p2)
        {
            float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }
    }
}
