using System;
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
    public partial class FInput : Form
    {
        #region private Fields


        private FDrawForm _drawForm;
        private Color _errCol = Color.Yellow;
        private Color _okCol = Color.White;

        private Algorithm _algorithm;
        private LoadTSP _tspFile;

        private bool _antCountFehlerfrei = false;
        private bool _cityCountFehlerfrei = false;
        private bool _iterCountFehlerfrei = false;
        private bool _alphaFehlerfrei = true; // predefined in GUI
        private bool _betaFehlerfrei = true; // predefined in GUI
        private bool _rhoFehlerfrei = false;
        private bool _tauFehlerfrei = false;
        private bool _qFehlerfrei = false;

        private DateTime _startTime;
        private System.Windows.Forms.Timer _timer;

        #endregion

        #region constructors

        public FInput()
        {
            InitializeComponent();
        }

        #endregion        

        #region checkfied-Methods

        private bool CheckIfInt(string p)
        {
            int num;
            if (Int32.TryParse(p, out num) && (num > 0))
                return true;

            return false;
        }

        private bool CheckIfDouble(string p)
        {
            double num;
            if (!p.Contains(".") && Double.TryParse(p, out num) && num > .0)
                return true;
            return false;
        }

        /// <summary>
        /// bigger a smaler or equals b
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private bool CheckIfBetween(double num, double a, double b, bool biggerA)
        {
            if (!biggerA && ((num >= a && num <= b)))
                return true;

            if (num > a && num <= b)
                return true;

            return false;
        }

        private bool CheckIfValid 
        {
            get
            {
                return _antCountFehlerfrei
                          && _cityCountFehlerfrei
                          && _iterCountFehlerfrei
                          && _alphaFehlerfrei
                          && _betaFehlerfrei
                          && _rhoFehlerfrei
                          && _tauFehlerfrei
                          && _qFehlerfrei;
            }     
        }

        #endregion

        #region Events

        private void _smiTSPLadenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _tspFile = new LoadTSP();
            _drawForm = new FDrawForm(_tspFile);
        }

        private void tODOErgsSpeichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Speichern ...";
            sfd.Filter = sfd.Filter = "xml files (*.xml)|*.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
            }
        }

        private void TextChanged(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            string ctrlName = ctrl.Name;

            switch (ctrlName)
            {
                case "_tbAntCount":
                    _antCountFehlerfrei = CheckIfInt(_tbAntCount.Text);
                    _tbAntCount.BackColor = (_antCountFehlerfrei) ? _okCol : _errCol;
                    break;

                case "_tbCityCount":
                    _cityCountFehlerfrei = CheckIfInt(_tbCityCount.Text);
                    _tbCityCount.BackColor = (_cityCountFehlerfrei) ? _okCol : _errCol;
                    break;

                case "_tbIterationCount":
                    _iterCountFehlerfrei = CheckIfInt(_tbIterationCount.Text);
                    _tbIterationCount.BackColor = (_iterCountFehlerfrei) ? _okCol : _errCol;
                    break;
                case "_tbAlpha":
                    double num;
                    _alphaFehlerfrei = Double.TryParse(_tbAlpha.Text, out num) && CheckIfBetween(num, 0, 1, false);
                    _tbAlpha.BackColor = (_alphaFehlerfrei) ? _okCol : _errCol;
                    break;
                case "_tbBeta":
                    _betaFehlerfrei = CheckIfDouble(_tbBeta.Text);
                    _tbBeta.BackColor = (_betaFehlerfrei) ? _okCol : _errCol;
                    break;
                case "_tbRho":
                    _rhoFehlerfrei = CheckIfDouble(_tbRho.Text) && CheckIfBetween(double.Parse(_tbRho.Text), 0, 1, true);
                    _tbRho.BackColor = (_rhoFehlerfrei) ? _okCol : _errCol;
                    break;
                case "_tbTau":
                    _tauFehlerfrei = CheckIfDouble(_tbTau.Text);
                    _tbTau.BackColor = (_tauFehlerfrei) ? _okCol : _errCol;
                    break;
                case "_tbQ":
                    _qFehlerfrei = CheckIfDouble(_tbQ.Text);
                    _tbQ.BackColor = (_qFehlerfrei) ? _okCol : _errCol;
                    break;

            }
        }

        private void _btnStart_Click(object sender, EventArgs e)
        {
            if (!CheckIfValid)
            {
                MessageBox.Show(this, "Die eingegebenen Daten sind unvollständig oder fehlerhaft!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_tspFile == null)
            {
                MessageBox.Show(this, "Es muss zuerst eine *.tsp-Datei geladen werden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _startTime = DateTime.Now;
            _timer = new System.Windows.Forms.Timer();
            _timer.Tick += new EventHandler(SetTimer);
            _timer.Interval = 100;
            _timer.Start();

            _algorithm = new Algorithm(this, _tspFile, _drawForm, double.Parse(_tbTau.Text),
                double.Parse(_tbQ.Text),
                double.Parse(_tbRho.Text),
                double.Parse(_tbAlpha.Text),
                double.Parse(_tbBeta.Text),
                int.Parse(_tbIterationCount.Text),
                int.Parse(_tbAntCount.Text));
        }

        #endregion

        public void ShowDrawForm()
        {
            this.ShowDialog();
        }

        internal void SetLBLTimeText(string p)
        {
            _lblTime.Text = p;
        }
        
        private void SetTimer(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now - _startTime;
            _lblTime.Text = "Zeit: " + span.Minutes + ":" + span.Seconds + ":" + span.Milliseconds;
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        internal void SetBestTourGlobal(String global)
        {
            _tbBestGlob.Text = global;
        }

        internal void SetBestTourIter(String iter)
        {
            _tbBestIter.Text = iter;
        }

        internal void SetAVRGlobal(String global)
        {
            _tbAVRGlob.Text = global;
        }

        internal void SetAVRIter(String iter)
        {
            _tbAVRIter.Text = iter;
        }

        
    }
}