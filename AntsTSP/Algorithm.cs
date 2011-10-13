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
//Wenn bereits ein TSP-File geladen ist und ein 2. Mal auf Laden geklickt wird, wirft er nach dem Start eine Exception, weil sich das Prog. auf den falschen Thread bezieht
//Algo-Schummelei anpassen (mit den optim. Parametern)
//Global total = iter Total ???
//Benamsung der Delegate anpassen

namespace AntsTSP
{
    public class Algorithm
    {
        #region private Fields

        private SortedList<int, SortedList<int, City>> _cityList = new SortedList<int, SortedList<int, City>>();
        private List<Ant> _antList = new List<Ant>();

        private LoadTSP _tsp;
        private double _tau;
        private double _q;
        private double _rho;
        private double _alpha;
        private double _beta;
        private int _antCount;
        private int _iterCount;

        private DateTime _startTime;
        private FInput _owner;
        private FDrawForm _drawForm;

        private double _bestTourGloabl;
        private double _bestTourIter;
        private double _avrTourGloabl;
        private double _avrTourIter;

        private delegate void UpdateWayByStepInvoker(Point pnt);
        private delegate void UpdateWayByAntInvoker(ArrayList points);
        private delegate void GlobalWayTotalInvoker(String global);
        private delegate void GlobalWayIterInvoker(String global);
        private delegate void AVRWayTotalInvoker(String iter);
        private delegate void AVRWayIterInvoker(String iter);
        private delegate void StopTimerInvoker();
        
                        

        #endregion

        #region Constructors

        public Algorithm(FInput fInput, LoadTSP tsp, FDrawForm drawForm, double tau, double q, double rho, double alpha, double beta, int iter, int ants)
        {
            _tsp = tsp;

            _tau = tau;
            _q = q;
            _rho = rho;
            _alpha = alpha;
            _beta = beta;
            _antCount = ants;
            _iterCount = iter;
            _owner = fInput;
            _drawForm = drawForm;

            // init
            InitCityList();
            InitAnts();
            _startTime = DateTime.Now;

            _bestTourGloabl = double.PositiveInfinity;
            _bestTourIter = double.PositiveInfinity;

            Thread solveTSPThread = new Thread(new ThreadStart(TryToSolveTSP));
            solveTSPThread.Start();

            //TryToSolveTSP();
            //UpdateTime();
        }


        

        #endregion

        #region Init

        private void InitAnts()
        {            
            int cityNum = 1;

            for (int i = 1; i <= _antCount; i++)
            {
                //if (cityNum > _cityList.Count)
                //    cityNum = 1;

                cityNum = RandomNumber(1, _cityList.ElementAt(0).Value.Count);
                Ant currentAnt = new Ant();
                currentAnt.walkedDistance = .0;
                currentAnt.firstCity = cityNum;
                currentAnt.city = cityNum;
                SortedList<int, City> lst = new SortedList<int,City>(_cityList.ElementAt(0).Value);
                currentAnt.cityList = lst;
                currentAnt.DeleteCityFromList(currentAnt.city);

                _antList.Add(currentAnt);

                //cityNum += 1;
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

                int col = row;
                for (; col < _tsp.Koords.Values.Count; col++)
                {
                    x2 = _tsp.Koords.Values[col].X;
                    y2 = _tsp.Koords.Values[col].Y;

                    double phero;
                    double atractivity;
                    double dist;

                    if (col == row)
                    {
                        dist = 0;
                        phero = 0;
                        atractivity = 0;
                    }
                    else 
                    {
                        dist = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                        phero = _tau;
                        atractivity = 1 / dist;                        
                    }
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

        #endregion

        #region algorithm

        private void TryToSolveTSP()
        {
            for (int iter = 0; iter < _iterCount; iter++)
            {
                _owner.Invoke(new GlobalWayIterInvoker(_owner.SetBestTourIter), "0");
                _owner.Invoke(new AVRWayIterInvoker(_owner.SetAVRIter), "0");
                _bestTourIter = double.PositiveInfinity;
                _avrTourIter = 0;

                List<City> currentWay = new List<City>();
                //für jede Ameise
                for (int iterAnt = 0; iterAnt < _antList.Count; iterAnt++)
                {
                    Ant ant = _antList[iterAnt];      
                    
                    ArrayList points = new ArrayList();

                    while (_antList[iterAnt].cityList.Count > 0)
                    {
                        double highestLikeliness = .0;
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
                            points.Add(new Point(x,y));

                            // GUI aktualisieren --> Test
                            //_drawForm.Invoke(new UpdateWayByStepInvoker(_drawForm.AddPointToWay), new Point(x, y));
                        }

                    }//Ameise ist alle Städte einmal durchgelaufen

                    // TODO hier nochmal überdenken
                    // die Ameise wird nicht zurückgeschrieben
                    // muss das???
                    int smallCity = ant.city;
                    int bigCity = ant.firstCity;
                    CheckIndices(ref smallCity, ref bigCity);
                    double newDist = _cityList[smallCity][bigCity].distance;
                    ant.walkedDistance += newDist;
                    ant.city = ant.firstCity;
                    int x2 = _tsp.Koords[ant.city].X;
                    int y2 = _tsp.Koords[ant.city].Y;
                    points.Add(new Point(x2, y2));

                    // GUI aktualisieren
                    UpdateLength(iterAnt); //geht nicht, weil das Ausgabefenster nicht im eigenen Thread läuft
                    _drawForm.Invoke(new UpdateWayByAntInvoker(_drawForm.ShowCurrentWay), points);
                    

                    //Pheromonupdate ausführen
                    for (int row = 1; row <= _cityList.Count; row++)
                    {
                        for (int col = row; col < _cityList[row].Values.Count; col++)
                        {
                            int small = row;
                            int big = col;
                            CheckIndices(ref small, ref big);
                            City city = _cityList[small][big];

                            double deltaTau = .0;
                            if (currentWay.Contains(city))
                            {
                                deltaTau = _q / ant.walkedDistance;
                            }

                            city.phero = (1 - _rho) *
                                city.phero + deltaTau;

                            _cityList[small][big] = city;
                        }
                    }

                }//Alle Ameisen sind für diese Iteration durchgelaufen
                InitAnts();
            }//Alle Iterationen sind beendet
            //UpdateTime();
            _owner.Invoke(new StopTimerInvoker(_owner.StopTimer));
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

        private void UpdateLength(int iterAnt)
        {
            if (_antList[iterAnt].walkedDistance < _bestTourGloabl)
            {
                _bestTourGloabl = _antList[iterAnt].walkedDistance;
                _owner.Invoke(new GlobalWayTotalInvoker(_owner.SetBestTourGlobal), _bestTourGloabl.ToString());
            }
            if (_antList[iterAnt].walkedDistance < _bestTourIter)
            {
                _bestTourIter = _antList[iterAnt].walkedDistance;
                _owner.Invoke(new GlobalWayIterInvoker(_owner.SetBestTourIter), _bestTourIter.ToString());
            }
            _avrTourGloabl = _avrTourGloabl * (_iterCount - 1) / _iterCount + _antList[iterAnt].walkedDistance / _iterCount;
            _avrTourIter = _avrTourIter * (_antCount - 1) / _antCount + _antList[iterAnt].walkedDistance / _antCount;

            //_drawForm.Invoke(new UpdateWayByAntInvoker(_drawForm.ShowCurrentWay), points);

            _owner.Invoke(new AVRWayTotalInvoker(_owner.SetAVRGlobal), _avrTourGloabl.ToString());
            _owner.Invoke(new AVRWayIterInvoker(_owner.SetAVRIter), _avrTourIter.ToString());


        }        

        #endregion
    }
}
