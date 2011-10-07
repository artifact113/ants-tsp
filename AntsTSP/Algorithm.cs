using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        #endregion

        #region Constructors

        public Algorithm(FInput fInput, LoadTSP tsp, FDrawForm _drawForm, double tau, double q, double rho, double alpha, double beta, int iter, int ants)
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

            // init
            
            InitCityList();
            InitAnts();
            _startTime = DateTime.Now;

            TryToSolveAntsTSP();
        }

        

        #endregion

        #region Init

        private void InitAnts()
        {            
            int cityNum = 1;

            for (int i = 1; i <= _antCount; i++)
            {
                if (cityNum > _cityList.Count)
                    cityNum = 1;

                Ant currentAnt = new Ant();
                currentAnt.walkedDistance = .0;
                currentAnt.city = cityNum;
                SortedList<int, City> lst = new SortedList<int,City>(_cityList.ElementAt(0).Value);
                currentAnt.cityList = lst;
                currentAnt.DeleteCityFromList(currentAnt.city);

                _antList.Add(currentAnt);

                cityNum += 1;
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

        private void TryToSolveAntsTSP()
        {            
                        

            for (int iter = 0; iter < _iterCount; iter++ )
            {
                if (iter == 51)
                {

                }
                for (int antPos = 0; antPos < _antList.Count; antPos++ )
                {
                    double highestLikeliness = .0;
                    int keyToHighestLikeliness = -1;

                    foreach (City city in _antList[antPos].cityList.Values)
                    {
                        // Nenner der Formel
                        double sum = .0;
                        int smallForNenner = -1;
                        int bigForNenner = -1;

                        foreach (City c in _antList[antPos].cityList.Values)
                        {                            
                            smallForNenner = c.key;
                            bigForNenner = _antList[antPos].city;
                            CheckIndices(ref smallForNenner, ref bigForNenner);
                            
                            sum += Math.Pow(_cityList[smallForNenner][bigForNenner].phero, _alpha) * Math.Pow(_cityList[smallForNenner][bigForNenner].atractivity, _beta);                            
                        }
                        
                        int small = city.key;
                        int big = _antList[antPos].city;
                        CheckIndices(ref small, ref big);

                        double likeliness = (Math.Pow(_cityList[small][big].phero, _alpha)
                            * Math.Pow(_cityList[small][big].atractivity, _beta))
                            / (sum);
                        

                        if (highestLikeliness <= likeliness)
                        {
                            highestLikeliness = likeliness;
                            keyToHighestLikeliness = city.key;
                        }

                        
                    }

                    // Update walked distance
                    int keyFrom = _antList[antPos].city;
                    int keyTo = keyToHighestLikeliness;                    
                    CheckIndices(ref keyFrom, ref keyTo);
                    double dist = _cityList[keyFrom][keyTo].distance;

                    // Ameise in neue Stadt setzen und neue Stadt aus Ameisenstädte löschen
                    Ant ant = _antList[antPos];
                        // distance addieren
                    ant.walkedDistance += dist; 
                    ant.city = keyToHighestLikeliness;
                    ant.DeleteCityFromList(keyToHighestLikeliness);
                    _antList[antPos] = ant;

                    // TODO Pheromonupdate ausführen
                    UpdatePheromon(keyFrom, keyTo, ant.walkedDistance);
                }


            } 
        }

        private void UpdatePheromon(int currRow, int currCol, double walkedDist)
        {
            for (int row = 1; row <= _cityList.Count; row++)
            {
                int col = row;

                for (; col <= _cityList[row].Count; col++)
                {
                    int x = row;
                    int y = col;
                    CheckIndices(ref x, ref y);

                    double deltaTau = .0;
                    if ((row == currRow) && (col == currCol))
                        deltaTau = _q/walkedDist;

                    City city = _cityList[x][y];
                    city.phero = (1 - _rho) * _cityList[x][y].phero + deltaTau;                    
                    _cityList[x][y] = city;
                }
            }
        }

        private void UpdateTime()
        {
            TimeSpan span = DateTime.Now - _startTime;
            _owner.SetLBLTimeText("Zeit: "+Convert.ToString(span));

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

        #endregion

    }
}
