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
        private Point[] _wayAsArray;
        private bool _init = true;

        private const int _skal = 2;

        public ArrayList Way
        {
            get { return _way; }
            set { _way = value; }
        } 

        public void AddPointToWay(Point pnt)
        {
            _way.Add(new Point(pnt.X/_skal, pnt.Y/_skal));
            _wayAsArray = (Point[])_way.ToArray(typeof(Point));
            this.Refresh();
        }

        public void ShowCurrentWay(ArrayList points)
        {
            _wayAsArray = new Point[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                _wayAsArray[i] = new Point(((Point)points[i]).X / _skal, ((Point)points[i]).Y / _skal);
            }
            this.Refresh();
        }

        public FDrawForm(LoadTSP load)
        {
            InitializeComponent();

            _tspFile = load;
            Thread t = new Thread(new ThreadStart(ShowDrawForm));
            t.Start();
        }

        private void ShowDrawForm()
        {
            this.ShowDialog();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (_tspFile == null)
                return;

            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Color.Red);

            foreach (Point pt in _tspFile.Koords.Values)
            {
                g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(pt.X / _skal, pt.Y / _skal, 4, 4));
            }

            if((_wayAsArray != null) && (_wayAsArray.Length > 1))
            {                
                g.DrawLines(pen, _wayAsArray);
            }            
        }             
    }
}
