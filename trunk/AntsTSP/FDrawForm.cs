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
        #region private Fields
        private delegate void CityNumberInvoker(int numberOfCities);
        public bool _isClosed;

        private LoadTSP _tspFile;
        private ArrayList _way = new ArrayList();
        private Point[] _wayAsArray;
        private Point[] _bestWayAsArray;
        private Point[] _wayAsArrayWithSkal;
        private Point[] _bestWayAsArrayWithSkal;
        private bool _algoIsRunning = false;
        private FInput _owner;

        private double _skalX = .0;
        private double _skalY = .0;
        private const double _maxHeight = 1500; //Y-Höchstwert der TSP-Koords. inkl. Puffer
        private const double _maxWidth = 2000;//X-Höchstwert der TSP-Koords. inkl. Puffer

        public ArrayList Way
        {
            get { return _way; }
            set { _way = value; }
        }

        #endregion

        #region Constructors

        public FDrawForm(LoadTSP load, FInput owner)
        {
            InitializeComponent();
            _isClosed = false;

            _tspFile = load;

            Thread t = new Thread(new ThreadStart(ShowDrawForm));
            t.Start();
            _owner = owner;
            _owner.Invoke(new CityNumberInvoker(_owner.SetNumberOfCities), _tspFile.Koords.Count);
        }

        #endregion

        #region Methods

        public void IsAlgoRunning(bool enabled)
        {
            _algoIsRunning = enabled;
        }

        private void ShowDrawForm()
        {
            this.ShowDialog();
        }

        public void ShowCurrentWay(ArrayList currentWay, ArrayList bestWay)
        {
            _wayAsArray = new Point[currentWay.Count];
                _bestWayAsArray = new Point[bestWay.Count];

            for (int i = 0; i < currentWay.Count; i++)
            {
                int pointX = ((Point)(currentWay[i])).X;
                int pointY = ((Point)(currentWay[i])).Y;

                _wayAsArray[i] = new Point(pointX, pointY);

                pointX = ((Point)(bestWay[i])).X;
                pointY = ((Point)(bestWay[i])).Y;

                _bestWayAsArray[i] = new Point(pointX, pointY);
            }
            this.Refresh();
        }

        private void UpdateCurrentWay()
        {
            _wayAsArrayWithSkal = new Point[_wayAsArray.Length];
            _bestWayAsArrayWithSkal = new Point[_bestWayAsArray.Length];

            for (int i = 0; i < _wayAsArray.Length; i++)
            {
                int x = (int)(_wayAsArray[i].X * _skalX);
                int y = (int)(_wayAsArray[i].Y * _skalY);
                _wayAsArrayWithSkal[i] = new Point(x, y);

                x = (int)(_bestWayAsArray[i].X * _skalX);
                y = (int)(_bestWayAsArray[i].Y * _skalY);
                _bestWayAsArrayWithSkal[i] = new Point(x, y);
            }
        }

        #endregion

        #region Paint

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (_tspFile == null)
                return;
            
            double windowWidth = this.Width;
            double windowHeight = this.Height;

            _skalX = windowWidth / _maxWidth;
            _skalY = windowHeight / _maxHeight;
            

            Graphics g = this.CreateGraphics();
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
                g.DrawPolygon(grayPen, _wayAsArrayWithSkal);
                g.DrawPolygon(thickPen, _bestWayAsArrayWithSkal);
            }            
        }

        #endregion

        #region MouseEvents

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
        #endregion

        private void FDrawForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _isClosed = true;
        }
    }
}
