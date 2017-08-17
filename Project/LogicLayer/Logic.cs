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
        public Node randomnode;
        int mapheigth;
        int mapwidth;
        public Robot robot;
        public int motorforce;
        public int friction;
        public int robotweigth;
        public double robotmaxspeed;

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
            motorforce = da.motorforce;
            friction = da.friction;
            robotweigth = da.robotweigth;
            robotmaxspeed = da.robotmaxspeed;
            robot = new Robot(startPt.X, startPt.Y, 30, new Vector(-1,0), motorforce, robotweigth, robotmaxspeed);
        }

        public void DoMovement(bool motor)
        {
            if(robot.x == nodes.First().X && robot.y == nodes.First().Y)
            {
                nodes.Remove(nodes.First());
            }
            Node next = nodes.First();
            if (robot.direction != new Vector(robot.x - next.X, robot.y - next.Y))
            {
                Console.WriteLine("not right direction");
            }
        }

        /**private void CheckCollision()
        {
            foreach (var wall in walls)
            {
                if (Math.Sqrt(Math.Pow((robot.x - wall.X), 2) + Math.Pow((robot.y - wall.Y), 2)) < robot.straal)
                {
                    DoCollision(robot1, robot2);
                }
            }
        }

        private void DoCollision(Robot robot1, Robot robot2)
        {
            Console.WriteLine("col");
            double temp = robot1.speed;
            robot1.speed = -robot2.speed;
            robot2.speed = -temp;
            if (robot1.hasBall || robot2.hasBall)
            {
                Random rd = new Random();
                if (rd.Next(5) > 3)
                {
                    if (robot1.hasBall)
                    {
                        robot1.hasBall = false;
                        robot2.hasBall = true;
                        team1Bal = false;
                        team2Bal = true;
                    }
                    else
                    {
                        robot1.hasBall = true;
                        robot2.hasBall = false;
                        team1Bal = true;
                        team2Bal = false;
                    }
                }
            }
        }**/

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
