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
    public partial class StartForm : Form
    {
        Logic lo;
        public StartForm(Logic lo)
        {
            this.lo = lo;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm(lo);
            main.Show();
        }
        
    }
}
