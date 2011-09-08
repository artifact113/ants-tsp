﻿using System;
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

        private bool _antCountFehlerfrei = false;
        private bool _cityCountFehlerfrei = false;
        private bool _iterCountFehlerfrei = false;
        private bool _alphaFehlerfrei = false;
        private bool _betaFehlerfrei = false;
        private bool _rhoFehlerfrei = false;
        private bool _tauFehlerfrei = false;
        private bool _qFehlerfrei = false;

        #endregion

        public FInput()
        {
            InitializeComponent();

            MaximumSize = Size;
            MinimumSize = Size;
        }

        private void _smiTSPLadenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadTSP tspFile = new LoadTSP();
            _drawForm = new FDrawForm(tspFile);
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
                    _alphaFehlerfrei = CheckIfDouble(_tbAlpha.Text);
                    _tbAlpha.BackColor = (_alphaFehlerfrei) ? _okCol : _errCol;
                break;
                case "_tbBeta":
                _betaFehlerfrei = CheckIfDouble(_tbBeta.Text);
                _tbBeta.BackColor = (_betaFehlerfrei) ? _okCol : _errCol;
                break;
                case "_tbRho":
                _rhoFehlerfrei = CheckIfDouble(_tbRho.Text) && CheckIfBetween(double.Parse(_tbRho.Text), 0, 1);
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
        private bool CheckIfBetween(double num, double a, double b)
        {
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

        private void _btnStart_Click(object sender, EventArgs e)
        {
            if (!CheckIfValid) 
            {
                MessageBox.Show(this, "Du DÖDEL!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}