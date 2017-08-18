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
        public double motorforce;
        public double friction;
        public double robotweigth;
        public double robotmaxspeed;
        public double force;

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
            CheckCollision();
            if (Math.Sqrt(Math.Pow((robot.x - nodes.First().X), 2) + Math.Pow((robot.y - nodes.First().Y), 2)) < robot.straal)
            {
                nodes.Remove(nodes.Last());
            }
            Node next = nodes.Last();
            if (robot.direction != new Vector(next.X - robot.x , next.Y -robot.y ))
            {
                robot.direction = new Vector(next.X - robot.x, next.Y - robot.y);
            }
            AdjustSpeed(motor);
            robot.x += (int) Math.Round(robot.speed * robot.direction.X);
            robot.y += (int)Math.Round(robot.speed * robot.direction.Y);
        }

        private void AdjustSpeed(bool motor)
        {
            force = (motor) ? (robot.motorforce - friction)/robot.weigth : -friction / robot.weigth;
            if (robot.speed > 0)
            {
                robot.speed += force;
            }
            else if (robot.speed == 0)
            {
                if (force > 0)
                {
                    robot.speed += force;
                }
            }
            if (robot.speed > robot.maxspeed)
            {
                robot.speed = robot.maxspeed;
            }
            if (robot.speed < 0)
            {
                robot.speed = 0;
            }
        }

        private void CheckCollision()
        {
            foreach (var wall in walls)
            {
                if (LineToPointDistance2D(new PointPt(wall.X, wall.Y), new PointPt(wall.X+wall.width, wall.Y), new PointPt(robot.x, robot.y) ,true) <= robot.straal ||
                    LineToPointDistance2D(new PointPt(wall.X, wall.Y), new PointPt(wall.X , wall.Y + wall.heigth), new PointPt(robot.x, robot.y), true) <= robot.straal ||
                    LineToPointDistance2D(new PointPt(wall.X + wall.width, wall.Y), new PointPt(wall.X + wall.width, wall.Y + wall.heigth), new PointPt(robot.x, robot.y), true) <= robot.straal ||
                    LineToPointDistance2D(new PointPt(wall.X, wall.Y + wall.heigth), new PointPt(wall.X + wall.width, wall.Y + wall.heigth), new PointPt(robot.x, robot.y), true) <= robot.straal )
                {
                    DoCollision(robot, wall);
                }
            }
        }
        private void DoCollision(Robot robot1, Wall wall)
        {
            robot1.direction = new Vector(robot1.direction.X, -robot1.direction.Y);
        }



        #region distance point->line
        //https://stackoverflow.com/questions/4438244/how-to-calculate-shortest-2d-distance-between-a-point-and-a-line-segment-in-all
        private double DotProduct(PointPt pointA, PointPt pointB, PointPt pointC)
        {
            PointPt AB = new PointPt(pointB.X - pointA.X, pointB.Y - pointA.Y);
            PointPt BC = new PointPt(pointC.X - pointB.X, pointC.Y - pointB.Y);
            double dot = AB.X * BC.X + AB.Y * BC.Y;
            return dot;
        }

        private double CrossProduct(PointPt pointA, PointPt pointB, PointPt pointC)
        {
            PointPt AB = new PointPt(pointB.X - pointA.X, pointB.Y - pointA.Y);
            PointPt AC = new PointPt(pointC.X - pointA.X, pointC.Y - pointA.Y);
            double cross = AB.X * AC.Y - AB.Y * AC.X;
            return cross;
        }

        private double Distance(PointPt pointA, PointPt pointB)
        {
            double d1 = pointA.X - pointB.X;
            double d2 = pointA.Y - pointB.Y;

            return Math.Sqrt(d1 * d1 + d2 * d2);
        }

        private double LineToPointDistance2D(PointPt pointA, PointPt pointB, PointPt pointC, bool isSegment)
        {
            double dist = CrossProduct(pointA, pointB, pointC) / Distance(pointA, pointB);
            if (isSegment)
            {
                double dot1 = DotProduct(pointA, pointB, pointC);
                if (dot1 > 0) return Distance(pointB, pointC);
                double dot2 = DotProduct(pointB, pointA, pointC);
                if (dot2 > 0) return Distance(pointA, pointC);
            }
            return Math.Abs(dist);
        } 
        #endregion

        

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
