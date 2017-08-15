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
        public int x;
        public int y;
        public Node nextnode;

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void DrawLine(Graphics g, Color color)
        {
            g.DrawLine(new Pen(color), nextnode.x, nextnode.y, x, y);
        }

        internal void BerekenAfstand(List<Node> nodes)
        {
            double min = 100000000;
            foreach (var node in nodes)
            {
                double dX = node.x - x;
                double dY = node.y - y;
                double dist = dX * dX + dY * dY;
                if (dist < min)
                {
                    min = dist;
                    nextnode = node;
                }
            }
        }
    }
}
