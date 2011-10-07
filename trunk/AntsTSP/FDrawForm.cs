using System;
using System.Collections;
using System.Threading;
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
        private ArrayList _way = new ArrayList();

        private const int _skal = 2;

        public ArrayList Way
        {
            get { return _way; }
            set { _way = value; }
        } 

        public void AddPointToWay(Point pnt)
        {
            _way.Add(pnt);
        }

        public FDrawForm(LoadTSP load)
        {
            InitializeComponent();

            _tspFile = load;
            Thread t = new Thread(new ThreadStart(Show));
            t.Start();
            Visible = true;
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
                g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(pt.X/_skal, pt.Y/_skal, 4, 4));
            }

            if(_way.Count > 1)
            {
                Point[] pnts = new Point[_way.Count];
                for (int i = 0; i < pnts.Length; i++ )
                {
                    pnts[i] = new Point(((Point)Way[i]).X / _skal, ((Point)Way[i]).Y / _skal);
                }
                g.DrawLines(pen, pnts);
            }            
        }             
    }
}
