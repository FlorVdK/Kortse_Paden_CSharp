using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Globals;
using DataLayer;

namespace LogicLayer
{
    public class Logic
    {
        public DataAccess da;
        public string numberwalls;
        public string[] coordinates;
        public Wall[] walls;

        public Logic(DataAccess da)
        {
            this.da = da;
        }

        public void InitGame()
        {
            da.InitGame();
        }        

        public void InitTimer()
        {
            da.InitTimer();
        }

        public void InitRenderer()
        {
            da.InitRenderer();
            walls = da.walls;
        }
    }
}
