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
    public partial class FDrawForm : Form
    {

        private LoadTSP _tspFile;
        

        public FDrawForm(LoadTSP load)
        {
            InitializeComponent();

            _tspFile = load;

            Show();
        }

        

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (_tspFile == null)
                return;

            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Color.Red);
            foreach (Point pt in _tspFile.Koords.Values)
            {
                // Skalierung ist nen test
                //g.DrawRectangle(pen, pt.X/2, pt.Y/2, 4, 4);
                g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(pt.X/2, pt.Y/2, 4, 4));
            }
        }

             
    }
}
