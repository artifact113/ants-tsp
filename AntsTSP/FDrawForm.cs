using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AntsTSP
{
    public partial class MainForm : Form
    {

        private LoadTSP _tspFile;

        public MainForm()
        {
            InitializeComponent();
        }

        

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (_tspFile == null)
                return;

            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Color.Red);
            foreach (Point pt in _tspFile.Koords.Values)
            {
                g.DrawRectangle(pen, pt.X, pt.Y, 1, 1);
            }
        }

        private void _smiTSPLadenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _tspFile = new LoadTSP();
        }
    }
}
