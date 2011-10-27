using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;


namespace AntsTSP
{
    public class LoadTSP
    {
        SortedList<int, Point> koords = new SortedList<int, Point>();
        public bool _doNotOpenTSPForm;

        public SortedList<int, Point> Koords
        {
            get { return koords; }
            set { koords = value; }
        }

        #region constructors

        public LoadTSP(bool emptyTSP)
        {
            _doNotOpenTSPForm = false;
        }

        public LoadTSP()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "tsp files (*.tsp)|*.tsp";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                _doNotOpenTSPForm = true;
            }
            else
            {
                _doNotOpenTSPForm = false;
            }

            if (File.Exists(openFileDialog1.FileName))
            {
                StreamReader myFile = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.Default);
                String line = String.Empty;
                bool areKoords = false;
                try
                {
                    while ((line = myFile.ReadLine()) != null)
                    {
                        if (line.Contains("EOF"))
                            areKoords = false;
                        if (line.ElementAt(0) == '1')
                        {
                            areKoords = true;
                        }

                        if (areKoords)
                        {
                            String[] vals = line.Split(' ');
                            koords.Add(Convert.ToInt32(vals[0]), new Point((int)Convert.ToDouble(vals[1].Trim()) / 10, (int)Convert.ToDouble(vals[2].Trim()) / 10));
                        }
                    }
                }
                catch (FormatException e)
                {
                    MessageBox.Show("Die Datei ist keine gültige TSP-Datei.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _doNotOpenTSPForm = true;
                }
                catch (IndexOutOfRangeException e)
                {
                    MessageBox.Show("Die Datei ist keine gültige TSP-Datei.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _doNotOpenTSPForm = true;
                }
                myFile.Close();
            }
        }
        #endregion
    }
}
