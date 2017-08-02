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
        public void InitGame()
        {
            string configvalue1 = ConfigurationManager.AppSettings["logfilelocation"];
            Console.WriteLine("Game : Speed = " + configvalue1);
        }

        public void InitTimer()
        {
            Console.WriteLine("Timer");
        }

        public void InitRenderer()
        {
            Console.WriteLine("Render");
        }
    }
}
