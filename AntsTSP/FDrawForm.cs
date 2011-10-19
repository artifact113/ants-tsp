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
        private delegate void CityNumberInvoker(int numberOfCities);

        private LoadTSP _tspFile;
        private ArrayList _way = new ArrayList();
        private Point[] _wayAsArray;
        private Point[] _bestWayAsArray;
        private Point[] _wayAsSortedListWithSkal;
        private Point[] _bestWayAsSortedListWithSkal;
        private bool _algoIsRunning = false;
        private FInput _owner;

        private const int _skalAbsolut = 2;
        private double _skalX = .0;
        private double _skalY = .0;
        private const double _maxHeight = 1500;
        private const double _maxWidth = 2000;

        public ArrayList Way
        {
            get { return _way; }
            set { _way = value; }
        }

        public void IsAlgoRunning(bool enabled)
        {
            _algoIsRunning = enabled;
        }

        public void ShowCurrentWay(ArrayList currentWay, ArrayList bestWay)
        {
            _wayAsArray = new Point[currentWay.Count];
            //if (!_bestWayAsArray.Equals(bestWay))
            //{
                _bestWayAsArray = new Point[bestWay.Count];
            //}

            for (int i = 0; i < currentWay.Count; i++)
            {
                int pointX = ((Point)(currentWay[i])).X;
                int pointY = ((Point)(currentWay[i])).Y;

                //int pointX = (int)(((Point)(points[i])).X * _skalX);
                //int pointY = (int)(((Point)(points[i])).Y * _skalY);

                _wayAsArray[i] = new Point(pointX, pointY);

                pointX = ((Point)(bestWay[i])).X;
                pointY = ((Point)(bestWay[i])).Y;

                _bestWayAsArray[i] = new Point(pointX, pointY);
            }
            this.Refresh();
        }

        private void UpdateCurrentWay()
        {
            _wayAsSortedListWithSkal = new Point[_wayAsArray.Length];
            _bestWayAsSortedListWithSkal = new Point[_bestWayAsArray.Length];

            for (int i = 0; i < _wayAsArray.Length; i++)
            {
                //das geht beim erneuten Zeichnen krachen, die Werte in _wayAsArray auch verändert werden
                //weil Point nur ein Struct ist und das byRef übergeben wird
                int x = (int)(_wayAsArray[i].X * _skalX);
                int y = (int)(_wayAsArray[i].Y * _skalY);
                _wayAsSortedListWithSkal[i] = new Point(x, y);
                //_wayAsSortedListWithSkal[i].Y = (int)(_wayAsArray[i].Y * _skalY);

                x = (int)(_bestWayAsArray[i].X * _skalX);
                y = (int)(_bestWayAsArray[i].Y * _skalY);
                _bestWayAsSortedListWithSkal[i] = new Point (x,y);

            }
        }

        private void UpdateCurrentWayNotWorking()
        {
            _wayAsSortedListWithSkal = _wayAsArray;

            for (int i = 0; i < _wayAsArray.Length; i++)
            {
                //_wayAsArray[i].X = (int)(_wayAsArray[i].X * skalForWayX);
                //_wayAsArray[i].Y = (int)(_wayAsArray[i].Y * skalForWayY);

                //Point point = new Point((int)(wayAsArrayAsReference[i].X * _skalX),(int)(wayAsArrayAsReference[i].Y * _skalY));

                //_wayAsSortedListWithSkal.Add(i,point);
                _wayAsSortedListWithSkal[i].X = (int)(_wayAsArray[i].X * _skalX);
                _wayAsSortedListWithSkal[i].Y = (int)(_wayAsArray[i].Y * _skalY);
            }
        }

        public FDrawForm(LoadTSP load, FInput owner)
        {
            InitializeComponent();

            _tspFile = load;
            
            Thread t = new Thread(new ThreadStart(ShowDrawForm));
            t.Start();
            _owner = owner;
            _owner.Invoke(new CityNumberInvoker(_owner.SetNumberOfCities), _tspFile.Koords.Count);
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

            _skalX = windowWidth / _maxWidth;
            _skalY = windowHeight / _maxHeight;
            

            Graphics g = this.CreateGraphics();
            Pen redPen = new Pen(Color.Red);
            Pen grayPen = new Pen(Color.Gray);
            Pen thickPen = new Pen(Color.Red, 2);

            foreach (Point pt in _tspFile.Koords.Values)
            {
                int pointX = (int)(pt.X * _skalX);
                int pointY = (int)(pt.Y * _skalY);

                g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(pointX, pointY, 4, 4));
            }

            if((_wayAsArray != null) && (_wayAsArray.Length > 1))
            {
                UpdateCurrentWay();
                g.DrawPolygon(grayPen, _wayAsSortedListWithSkal);
                g.DrawPolygon(thickPen, _bestWayAsSortedListWithSkal);
                //g.DrawPolygon(pen, _wayAsArray);
            }            
        }

        private void FDrawForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (!_algoIsRunning)
            {
                double x = e.X / _skalX;
                double y = e.Y / _skalY;
                Point newPoint = new Point((int)x, (int)y);
                if (!_tspFile.Koords.ContainsValue(newPoint))
                {
                    int length = _tspFile.Koords.Count;

                    _tspFile.Koords.Add(length + 1, newPoint);
                    this.Refresh();
                    _owner.Invoke(new CityNumberInvoker(_owner.SetNumberOfCities), _tspFile.Koords.Count);
                }
            }
        }             
    }
}
