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

        private const int _skalAbsolut = 2;
        private double skalX = .0;
        private double skalY = .0;
        private const double maxHeight = 1500;
        private const double maxWidth = 2000;

        public ArrayList Way
        {
            get { return _way; }
            set { _way = value; }
        } 

        //public void AddPointToWay(Point pnt)
        //{
        //    _way.Add(new Point(pnt.X/skal, pnt.Y/skal));
        //    _wayAsArray = (Point[])_way.ToArray(typeof(Point));
        //    this.Refresh();
        //}

        public void ShowCurrentWay(ArrayList points)
        {
            _wayAsArray = new Point[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                int pointX = (int)(((Point)(points[i])).X * skalX);
                int pointY = (int)(((Point)(points[i])).Y * skalY);

                _wayAsArray[i] = new Point(pointX, pointY);
                //_wayAsArray[i] = new Point(((Point)points[i]).X / skal, ((Point)points[i]).Y / skal);
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
            
            double windowWidth = this.Width;
            double windowHeight = this.Height;

            skalX = windowWidth / maxWidth;
            skalY = windowHeight / maxHeight;
            

            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Color.Red);

            foreach (Point pt in _tspFile.Koords.Values)
            {
                int pointX = (int)(pt.X * skalX);
                int pointY = (int)(pt.Y * skalY);

                g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(pointX, pointY, 4, 4));
                //g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(pt.X / skal, pt.Y / skal, 4, 4));
            }

            if((_wayAsArray != null) && (_wayAsArray.Length > 1))
            {                
                g.DrawPolygon(pen, _wayAsArray);
            }            
        }             
    }
}
