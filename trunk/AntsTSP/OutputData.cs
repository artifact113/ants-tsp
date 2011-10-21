using System;
using System.Xml.Serialization;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntsTSP
{
    [XmlInclude(typeof(Point))]
    public class OutputData
    {
        // TODO
        // -Werte der Parameter
        // -beste Tour(Koordinaten) + Länge + Zeit
        // -durchschnittl. Tour + Länge

        public double _tau;
        public double _q;
        public double _rho;
        public double _alpha;
        public double _beta;
        public int _antCount;
        public int _iterCount;
        public int _cityCount;

        
        public ArrayList _bestTour = new ArrayList();

        public double _bestLength;
        public String _bestTimeAsString;
        //public ArrayList _avrTour = new ArrayList(); //ham wa nich
        public double _avrLength;

        private FInput _owner;

        public OutputData(double tau, double q, double rho, double alpha, double beta,
            int antCount, int iterCount, ArrayList best, double bestL, double avrL, FInput owner)
        {
            _tau = tau;
            _q = q;
            _rho = rho;
            _alpha = alpha;
            _beta = beta;
            _antCount = antCount;
            _iterCount = iterCount;
            //_cityCount = cityCount;
            _bestTour = best;
            _bestLength = bestL;
            //_bestTimeAsString = bestT;
            _avrLength = avrL;
            _owner = owner;

            _cityCount = _owner.GetNumberOfCities();
            TimeSpan span = _owner.GetTime();
            _bestTimeAsString = "" + span.Minutes+":" + span.Seconds+":" + span.Milliseconds;


        }

        public OutputData()
        {
        }

    }
}
