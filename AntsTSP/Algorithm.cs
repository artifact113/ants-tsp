using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

//TODOs:
//Wenn der OpenFile-Dialog abgebrochen wird, geht trotzdem ein Fenster auf
//Wenn bereits ein TSP-File geladen ist und ein 2. Mal auf Laden geklickt wird,
//wirft er nach dem Start eine Exception, weil sich das Prog. auf den falschen Thread bezieht

namespace AntsTSP
{
    public class Algorithm
    {
        #region private Fields

        private SortedList<int, SortedList<int, City>> _cityList = new SortedList<int, SortedList<int, City>>();
        private List<Ant> _antList = new List<Ant>();
        private SortedList<int, City> _cityListForAnt = new SortedList<int,City>();

        private LoadTSP _tsp;
        private OutputData _outputData;

        private double _tau;
        private double _q;
        private double _rho;
        private double _alpha;
        private double _beta;
        private int _antCount;
        private int _iterCount;
        private ArrayList _bestWay= new ArrayList();
        private double _minTourLength;
        private double _optTourLength = double.NegativeInfinity;

        private DateTime _startTime;
        private FInput _owner;
        private FDrawForm _drawForm;

        private double _bestTourGloabl;
        private double _bestTourIter;
        private double _avrTourGloabl;
        private double _avrTourIter;

        private delegate void UpdateWayByStepInvoker(Point pnt);
        private delegate void UpdateWayByAntInvoker(ArrayList currentWayAsArray, ArrayList bestWay);
        private delegate void OutputWindowInvoker(String str);
        private delegate void StopTimerInvoker();
        private delegate void FormDisablementInvoker(bool enabled);
        private delegate void IterCountInvoker(int iter);
        
        #endregion

        #region Constructors

        public Algorithm(
            FInput fInput, LoadTSP tsp,
            FDrawForm drawForm, double tau,
            double q, double rho, 
            double alpha, double beta, 
            int iter, int ants,
            double minTour, double optTour)
        {
            _tsp = tsp;
            _owner = fInput;

            _tau = tau;
            _q = q;
            _rho = rho;
            _alpha = alpha;
            _beta = beta;
            _antCount = ants;
            _iterCount = iter;
            _drawForm = drawForm;
            _minTourLength = minTour;
            _optTourLength = optTour;

            // init
            InitCityList();
            InitCityListForAnt();
            InitAnts();
            _startTime = DateTime.Now;

            _bestTourGloabl = double.PositiveInfinity;
            _bestTourIter = double.PositiveInfinity;

            Thread solveTSPThread = new Thread(new ThreadStart(TryToSolveTSP));
            solveTSPThread.Start();
        }
        #endregion

        #region Init

        private void InitAntsAfterIter()
        {
            int cityNum = 1;

            for (int i = 0; i < _antCount; i++)
            {
                if (cityNum > _cityList.Count)
                    cityNum = 1;

                cityNum = RandomNumber(1, _cityListForAnt.Count);
                Ant currentAnt = new Ant();
                currentAnt.walkedDistance = .0;
                currentAnt.firstCity = cityNum;
                currentAnt.city = cityNum;
                SortedList<int, City> lst = new SortedList<int, City>(_cityListForAnt);
                currentAnt.cityList = lst;
                currentAnt.DeleteCityFromList(currentAnt.city);

                _antList[i] = currentAnt;
            }
        }

        private void InitAnts()
        {            
            int cityNum = 1;

            for (int i = 1; i <= _antCount; i++)
            {

                if (cityNum > _cityListForAnt.Count)
                    cityNum = 1;

                cityNum = RandomNumber(1, _cityListForAnt.Count);
                Ant currentAnt = new Ant();
                currentAnt.walkedDistance = .0;
                currentAnt.firstCity = cityNum;
                currentAnt.city = cityNum;
                SortedList<int, City> lst = new SortedList<int,City>(_cityListForAnt);
                currentAnt.cityList = lst;
                currentAnt.DeleteCityFromList(currentAnt.city);

                _antList.Add(currentAnt);
            }
        } 

        private void InitCityList()
        {
            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;

            for (int row = 0; row < _tsp.Koords.Values.Count; row++)
            {
                _cityList.Add(_tsp.Koords.Keys[row], new SortedList<int, City>()); // add row

                Point current = _tsp.Koords.Values[row];
                x1 = current.X;
                y1 = current.Y;


                for (int col = row + 1; col < _tsp.Koords.Values.Count; col++)
                {
                    x2 = _tsp.Koords.Values[col].X;
                    y2 = _tsp.Koords.Values[col].Y;

                    double phero;
                    double atractivity;
                    double dist;

                    //if (col == row)
                    //{
                    //    dist = 0;
                    //    phero = 0;
                    //    atractivity = 0;
                    //}
                    //else 
                    //{
                        dist = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                        phero = _tau;
                        atractivity = 1 / dist;
                    //}
                    City currCity = new City();
                    currCity.key = _tsp.Koords.Keys[col];
                    currCity.atractivity = atractivity;
                    currCity.distance = dist;
                    currCity.phero = phero;
                    currCity.koord = new Point(x2, y2);
                    
                    _cityList[_tsp.Koords.Keys[row]].Add(_tsp.Koords.Keys[col],currCity);
                }
            }
        }

        private void InitCityListForAnt()
        {
            int x = 0;
            int y = 0;

            for (int row = 0; row < _tsp.Koords.Values.Count; row++)
            {

                Point current = _tsp.Koords.Values[row];
                x = current.X;
                y = current.Y;
                
                City currCity = new City();
                currCity.key = _tsp.Koords.Keys[row];
                currCity.koord = new Point(x, y);

                _cityListForAnt.Add(_tsp.Koords.Keys[row], currCity);
            }
        }

        #endregion

        #region algorithm

        private void TryToSolveTSP()
        {
            //Zeichenform akzeptiert keine Mausklicks mehr
            _drawForm.Invoke(new FormDisablementInvoker(_drawForm.IsAlgoRunning), true);

            for (int iter = 0; iter < _iterCount; iter++)
            {
                //Überprüfen der Abbruchkriterien
                if (_bestTourGloabl <= _optTourLength)
                {
                    _owner.Invoke(new StopTimerInvoker(_owner.StopTimer));
                    MessageBox.Show("Optimale Tour gefunden");
                    return;
                }
                if (_bestTourGloabl <= _minTourLength)
                {
                    _owner.Invoke(new StopTimerInvoker(_owner.StopTimer));
                    MessageBox.Show("Gewüsnchte Tour gefunden");
                    return;
                }

                //Werte der GUI und Bestwerte der Iteration aktualisieren
                _owner.Invoke(new IterCountInvoker(_owner.SetNumberOfIters), iter + 1);
                _owner.Invoke(new OutputWindowInvoker(_owner.SetBestTourIter), "0");
                _owner.Invoke(new OutputWindowInvoker(_owner.SetAVRIter), "0");
                _bestTourIter = double.PositiveInfinity;
                _avrTourIter = 0;

                List<City> currentWay = new List<City>();
                //für jede Ameise
                for (int iterAnt = 0; iterAnt < _antList.Count; iterAnt++)
                {
                    Ant ant = _antList[iterAnt];      
                    
                    ArrayList currentWayAsArray = new ArrayList();

                    while (_antList[iterAnt].cityList.Count > 0)
                    {
                        //enthält den Wert der attraktivsten Strecke
                        double highestLikeliness = .0;
                        //enthält den Key zu der Stadt, die auf der attraktivsten Strecke liegt
                        int keyToHighestLikeliness = -1;

                        //für jede noch nicht besuchte Stadt der Ameise die likeliness berechnen
                        foreach (City cityToGo in _antList[iterAnt].cityList.Values)
                        {
                            // Nenner der Formel
                            double sum = .0;
                            int smallForNenner = -1;
                            int bigForNenner = -1;

                            //Berechnung des Nenners
                            foreach (City c in _antList[iterAnt].cityList.Values)
                            {
                                smallForNenner = c.key;
                                bigForNenner = _antList[iterAnt].city;
                                CheckIndices(ref smallForNenner, ref bigForNenner);

                                sum += Math.Pow(_cityList[smallForNenner][bigForNenner].phero, _alpha)
                                    * Math.Pow(_cityList[smallForNenner][bigForNenner].atractivity, _beta);
                            }
                            int small = cityToGo.key;
                            int big = _antList[iterAnt].city;
                            CheckIndices(ref small, ref big);

                            double likeliness = (Math.Pow(_cityList[small][big].phero, _alpha)
                                * Math.Pow(_cityList[small][big].atractivity, _beta))
                                / (sum);

                            if (!((Double)likeliness).Equals(Double.NaN))
                            {
                                if (highestLikeliness <= likeliness)
                                {
                                    highestLikeliness = likeliness;
                                    keyToHighestLikeliness = cityToGo.key;
                                }
                            }
                            else
                            {
                                keyToHighestLikeliness = _antList[iterAnt].cityList.Keys[0];
                            }
                        }//Ende der Auswahl der nächsten Stadt

                        // Update walked distance
                        if (keyToHighestLikeliness >= 0)
                        {
                            int keyFrom = _antList[iterAnt].city;
                            int keyTo = keyToHighestLikeliness;
                            CheckIndices(ref keyFrom, ref keyTo);
                            double dist = _cityList[keyFrom][keyTo].distance;

                            // Ameise in neue Stadt setzen und neue Stadt aus Ameisenstädte löschen
                            // distance addieren
                            ant.walkedDistance += dist;
                            ant.city = keyToHighestLikeliness;
                            currentWay.Add(_cityList[keyFrom][keyTo]);
                            ant.DeleteCityFromList(keyToHighestLikeliness);
                            _antList[iterAnt] = ant;

                            int x = _tsp.Koords[keyToHighestLikeliness].X;
                            int y = _tsp.Koords[keyToHighestLikeliness].Y;
                            currentWayAsArray.Add(new Point(x,y));
                        }

                    }//Ameise ist alle Städte einmal durchgelaufen
                    int smallCity = ant.city;
                    int bigCity = ant.firstCity;
                    CheckIndices(ref smallCity, ref bigCity);
                    double newDist = _cityList[smallCity][bigCity].distance;
                    ant.walkedDistance += newDist;
                    ant.city = ant.firstCity;
                    int x2 = _tsp.Koords[ant.city].X;
                    int y2 = _tsp.Koords[ant.city].Y;
                    currentWayAsArray.Add(new Point(x2, y2));
                    
                    _antList[iterAnt] = ant; ;

                    // GUI aktualisieren
                    if (_antList[iterAnt].walkedDistance < _bestTourGloabl)
                    {
                        _bestWay = currentWayAsArray;
                    }

                    UpdateLength(iterAnt, iter);
                    _drawForm.Invoke(new UpdateWayByAntInvoker(_drawForm.ShowCurrentWay), currentWayAsArray, _bestWay);

                    //Pheromonupdate ausführen
                    for (int row = 1; row <= _cityList.Count; row++)
                    {
                        for (int col = row + 1; col < _cityList[row].Values.Count; col++)
                        {
                            City city = _cityList[row][col];

                            double deltaTau = .0;
                            if (currentWay.Contains(city))
                            {
                                deltaTau = _q / ant.walkedDistance;
                            }

                            city.phero = (1 - _rho) *
                                city.phero + deltaTau;

                            _cityList[row][col] = city;
                        }
                    }

                }//Alle Ameisen sind für diese Iteration durchgelaufen

                InitAntsAfterIter();
                
            }//Alle Iterationen sind beendet

            _owner.Invoke(new StopTimerInvoker(_owner.StopTimer));
            MessageBox.Show("Alle Iterationen durchlaufen");
            _drawForm.Invoke(new FormDisablementInvoker(_drawForm.IsAlgoRunning), false);
            _outputData = new OutputData(_tau, _q, _rho, _alpha, _beta, _antCount, _iterCount,
                _bestWay, _bestTourGloabl, _avrTourGloabl, _owner);
        }

        private void CheckIndices(ref int small, ref int big)
        {
            int swap = 0;
            if (small > big)
            {
                swap = small;
                small = big;
                big = swap;
            }
        }

        private int RandomNumber(int min, int max)
        {
            return min + (new Random()).Next(max - min);
        }

        #endregion

        #region updateMethods

        public OutputData GetOutputData()
        {
            if (_outputData == null)
            {
                return null;
            }
            return _outputData;
        }

        private void UpdateLength(int iterAnt, int iter)
        {
            if (_antList[iterAnt].walkedDistance < _bestTourGloabl)
            {
                _bestTourGloabl = _antList[iterAnt].walkedDistance;

                _owner.Invoke(new OutputWindowInvoker(_owner.SetBestTourGlobal), _bestTourGloabl.ToString());
            }
            if (_antList[iterAnt].walkedDistance < _bestTourIter)
            {
                _bestTourIter = _antList[iterAnt].walkedDistance;

                _owner.Invoke(new OutputWindowInvoker(_owner.SetBestTourIter), _bestTourIter.ToString());
            }
            _avrTourIter = _avrTourIter * iterAnt / (iterAnt + 1) + _antList[iterAnt].walkedDistance / (iterAnt + 1);
            _avrTourGloabl = _avrTourGloabl * iter / (iter + 1)  + _avrTourIter / (iter + 1);

            _owner.Invoke(new OutputWindowInvoker(_owner.SetAVRGlobal), _avrTourGloabl.ToString());
            _owner.Invoke(new OutputWindowInvoker(_owner.SetAVRIter), _avrTourIter.ToString());
        }        
        #endregion
    }
}
