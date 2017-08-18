using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicLayer;
using Globals;

namespace MainForm
{
    public partial class MainForm : Form
    {


        #region Variables
        Logic lo;

        public string numberwalls;
        public string[] coordinates;
        public Wall[] walls;
        PointPt goalPt;

        Robot robot;
        bool motor;

        Timer timer;
        List<Node> nodes;

        // Globale variabelen voor GDI+
        Graphics screen;
        Bitmap backBuffer;
        float SchaalX;
        float SchaalY;
        
        // variabelen voor model
        
        const double straal = 30.0d; //van de bol, in m
        const double balstraal = 15.0d; //van de bol, in m
        private bool found;

        Color pathcolor = Color.Black;

        #endregion Variables

        public MainForm(Logic lo)
        {
            this.lo = lo;
            InitializeComponent();
            InitRenderer();	
            InitTimer();
            EngineButton.Enabled = false;
        }

        private void InitTimer()
        {
            lo.InitTimer();
            timer = new Timer();
            timer.Interval = 10; // msec, f = 100 Hz;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            DoMovement();
            display.Invalidate(); // force redraw (& paint event);
        }

        private void InitGame()
        {
            lo.InitGame(display.Width, display.Height);
            found = false;
            DoRRT();
            lo.MakeRobot();
            display.Invalidate(); // force redraw (& paint event);
            display.Update();
            timer.Enabled = true;
            EngineButton.Enabled = true;
            motor = false;
        }

        private void DoMovement()
        {
            lo.DoMovement(motor);
        }

        private void DoRRT()
        {

            while (!found)
            {
                if (lo.found)
                {
                    found = true;
                    Console.WriteLine("found");
                    Start.Enabled = false;
                    pathcolor = Color.Green;
                }
                else
                {
                    Node n = lo.FindNextNode();

                }
                display.Invalidate(); // force redraw (& paint event);
                display.Update();
            }
            lo.DrawPath();
            nodes = lo.nodes;
            display.Invalidate(); // force redraw (& paint event);
            display.Update();
        }

        private void InitRenderer()
        {
            lo.InitRenderer();
            nodes = lo.nodes;
            numberwalls = lo.numberwalls;
            coordinates = lo.coordinates;
            backBuffer = new Bitmap(display.Width, display.Height);
            screen = Graphics.FromImage(backBuffer);
            // transformatie voor display met oorsprong in midden, breedte en hoogte van 1000m, rechtshandig assenstelsel
            SchaalX = display.Width / 1000.0f;
            SchaalY = display.Height / 640.0f;
            screen.ResetTransform();
            screen.ScaleTransform(SchaalX, -SchaalY); //schaling
            screen.TranslateTransform(display.Width / (SchaalX * 2f), -display.Height / (SchaalY * 2f)); // oorsprong in centrum
            // trigger Render voor elke refresh van display;
            display.Paint += new PaintEventHandler(PaintDisplay);
        }

        private void PaintDisplay(object sender, PaintEventArgs e)
        {
            // On_Paint event handler voor display
            Render(e.Graphics);
        }

        private void Render(Graphics output)
        {
            DrawField();

            // toon backbuffer op display
            output.DrawImage(backBuffer, new Rectangle(0, 0, display.Width, display.Height), new Rectangle(0, 0, display.Width, display.Height), GraphicsUnit.Pixel);            
        }

        private void DrawField()
        {
            screen.Clear(Color.White);
            DrawWalls();
            robot = lo.robot;
            if (robot != null)
            {
                Rectangle robotRect = new Rectangle((int)(robot.x - robot.straal / 2), (int)(robot.y - robot.straal / 2), (int)robot.straal, (int)robot.straal);
                screen.DrawEllipse(new Pen(Color.Blue), robotRect);
                screen.DrawLine(new Pen(Color.Black), new Point((int)(robot.x), (int)(robot.y)), new Point((int)(robot.x + robot.direction.X * straal / 2), (int)(robot.y + robot.direction.Y * straal / 2)));
            }

        }

        private void DrawWalls()
        {
            walls = lo.walls;
            foreach (var wall in walls)
            {
                Rectangle rect = new Rectangle(wall.X, wall.Y, wall.width, wall.heigth);
                screen.FillRectangle(new SolidBrush(Color.Red), rect);
                screen.DrawRectangle(new Pen(Color.Black), rect);
            }
            goalPt = lo.goalPt;
            Rectangle goalRect = new Rectangle(goalPt.X, goalPt.Y, 35, 35);
            screen.FillEllipse(new SolidBrush(Color.Yellow), goalRect);
            screen.DrawEllipse(new Pen(Color.Black), goalRect);
            foreach (var n in nodes)
            {
                screen.FillEllipse(new SolidBrush(pathcolor), n.X, n.Y, 5, 5);
                if (nodes.Count > 1)
                {
                    n.DrawLine(screen, pathcolor);
                }
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            //aanmaken backbuffer 
            InitGame();
        }

        private void EngineButton_Click(object sender, EventArgs e)
        {
            motor = !motor;
            if (motor)
            {
                EngineButton.Text = "start engine";
            }
            else
            {
                EngineButton.Text = "stop engine";
            }
        }
    }
}
