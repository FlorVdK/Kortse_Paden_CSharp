using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Globals;
using DataLayer;
using System.Drawing;

namespace LogicLayer
{
    public class Logic
    {
        public DataAccess da;
        public string numberwalls;
        public string[] coordinates;
        public Wall[] walls;
        int straal = 200;
        public List<Node> nodes;
        public PointPt startPt;
        public PointPt goalPt;
        Node startNd;
        Node goalNd;
        int nrnodes = 0;
        public Node randomnode;
        int mapheigth;
        int mapwidth;
        public Robot robot;

        public bool found { get; private set; }

        public Logic(DataAccess da)
        {
            this.da = da;
        }

        public void InitGame(int x, int y)
        {
            da.InitGame();
            mapheigth = y;
            mapwidth = x;
            found = false;
        }        

        public Node FindNextNode()
        {
            do
            {
                Random r = new Random();
                var startnode = nodes[r.Next(0, nodes.Count)];
                randomnode = new Node(startnode.X + r.Next(-straal,0), startnode.Y + r.Next(-straal, straal));
                randomnode.BerekenAfstand(nodes);
            } while (randomnode.X > mapwidth  /2 || randomnode.X < -mapwidth / 2 || randomnode.Y > mapheigth /2 || randomnode.Y < -mapheigth/2 || randomnode.collide(walls));
            
            nodes.Add(randomnode);
            if (Math.Abs(randomnode.X - goalNd.X) < 30 && Math.Abs(randomnode.Y - goalNd.Y) < 30)
            {
                goalNd.nextnode = randomnode;
                found = true;
            }
            return randomnode;
        }

        public void InitTimer()
        {
            da.InitTimer();

        }

        public void MakeRobot()
        {
            robot = new Robot(startPt.X, startPt.Y, 30, new Vector(0, -1));
        }

        public void DoMovement(bool motor)
        {

        }

        public void DrawPath()
        {
            nodes = new List<Node>();
            Node node = goalNd;
            do
            {
                nodes.Add(node);
                node = node.nextnode;
            } while (node != startNd);
        }

        public void InitRenderer()
        {
            da.InitRenderer();
            startPt = da.start;
            goalPt = da.goal;
            startNd = new Node(startPt.X, startPt.Y);
            startNd.nextnode = startNd;
            goalNd = new Node(goalPt.X, goalPt.Y);
            nodes = new List<Node>();
            nodes.Add(startNd);
            walls = da.walls;
        }
    }
}
