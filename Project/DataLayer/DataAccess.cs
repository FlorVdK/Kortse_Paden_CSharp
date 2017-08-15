using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using Globals;

namespace DataLayer
{
    public class DataAccess
    {

        public string numberwalls;
        public string[] coordinates;
        public Wall[] walls;
        public string[] startstring;
        public string[] goalstring;
        public PointPt start;
        public PointPt goal;

        public void InitGame()
        {
            string configvalue1 = ConfigurationManager.AppSettings["logfilelocation"];
        }

        public void InitTimer()
        {
            Console.WriteLine("Timer");
        }

        public void InitRenderer()
        {
            numberwalls = ConfigurationManager.AppSettings["numberwalls"];
            coordinates = ConfigurationManager.AppSettings["coordinates"].Split(',');
            walls = new Wall[int.Parse(numberwalls)];
            for (int i = 0; i < int.Parse(numberwalls); i++)
            {
                walls[i] = new Wall(int.Parse(coordinates[4 * i]), int.Parse(coordinates[4 * i + 1]), Math.Abs( int.Parse(coordinates[4 * i + 2]) - int.Parse(coordinates[4 * i])), Math.Abs( int.Parse(coordinates[4 * i + 3]) - int.Parse(coordinates[4 * i + 1])));
            }
            startstring = ConfigurationManager.AppSettings["start"].Split(',');
            goalstring = ConfigurationManager.AppSettings["goal"].Split(',');
            start = new PointPt(int.Parse(startstring[0]), int.Parse(startstring[1]));
            goal = new PointPt(int.Parse(goalstring[0]), int.Parse(goalstring[1]));
        }
    }
}
