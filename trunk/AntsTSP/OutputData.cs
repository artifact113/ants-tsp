using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntsTSP
{
    [Serializable]
    public class OutputData
    {
        // TODO
        // -Werte der Parameter
        // -beste Tour(Koordinaten) + Länge + Zeit
        // -durchschnittl. Tour + Länge

        private double _tau;
        private double _q;
        private double _rho;
        private double _alpha;
        private double _beta;
        private int _antCount;
        private int _iterCount;
        private int _cityCount;

        private ArrayList _bestTour = new ArrayList();
        private double _bestLength;
        private String _bestTimeAsString;
        private ArrayList _avrTour = new ArrayList();
        private double _avrLength;

        public OutputData(double tau, double q, double rho, double alpha, double beta,
            int antCount, int iterCount, int cityCount, ArrayList best, double bestL,
            String bestT, ArrayList avr, double avrL)
        {
            _tau = tau;
            _q = q;
            _rho = rho;
            _alpha = alpha;
            _beta = beta;
            _antCount = antCount;
            _iterCount = iterCount;
            _cityCount = cityCount;
            _bestTour = best;
            _bestLength = bestL;
            _bestTimeAsString = bestT;
            _avrTour = avr;
            _avrLength = avrL;
        }        

    }
}
