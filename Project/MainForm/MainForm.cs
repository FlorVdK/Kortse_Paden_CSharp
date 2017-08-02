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
        Logic lo;

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
        }

        private void Start_Click(object sender, EventArgs e)
        {

        }
    }
}
