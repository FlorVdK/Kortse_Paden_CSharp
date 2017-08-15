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

        Timer timer;
        int[] Score = new int[2] { 0, 0 };
        bool team1Bal;
        bool team2Bal;
        List<System.Drawing.Point[]> rrtList;

        // Globale variabelen voor GDI+
        Graphics screen;
        Bitmap backBuffer;
        float SchaalX;
        float SchaalY;

        //teams aanmaken
        int numberRobots = 2;
        Robot robot;
        int id;

        // variabelen voor model

        Int32 time;                  // in msec
        const double straal = 30.0d; //van de bol, in m
        const double balstraal = 15.0d; //van de bol, in m

        #endregion Variables

        public MainForm(Logic lo)
        {
            this.lo = lo;
            InitializeComponent();
            InitRenderer();				//aanmaken backbuffer 
            InitGame();
            InitTimer();
        }

        private void InitTimer()
        {
            lo.InitTimer();
        }

        private void InitGame()
        {
            lo.InitGame();
        }

        private void InitRenderer()
        {
            lo.InitRenderer();
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
            //assen
            DrawField();
            //DrawBotsAndBal();

            // toon backbuffer op display
            output.DrawImage(backBuffer, new Rectangle(0, 0, display.Width, display.Height), new Rectangle(0, 0, display.Width, display.Height), GraphicsUnit.Pixel);
            Console.WriteLine(display.Width + " " + display.Height);
            // display textboxes
            //tijdBox.Text = String.Format("{0:F}", time / 1000.0d);
            //label3.Text = "team blauw :" + Score[1];
            //label4.Text = "team rood :" + Score[0];
        }

        private void DrawField()
        {
            screen.Clear(Color.Blue);
            walls = lo.walls;
            foreach (var wall in walls)
            {
                Rectangle rect = new Rectangle(wall.X, wall.Y, wall.width, wall.heigth);
                screen.FillRectangle(new SolidBrush(Color.Red), rect);
            }            
        }

        private void Start_Click(object sender, EventArgs e)
        {

        }
    }
}
