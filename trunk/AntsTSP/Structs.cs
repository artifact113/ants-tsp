using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntsTSP
{
    public struct City
    {
        public Point koord;
        public int key;
        public double phero;
        public double distance;
        public double atractivity;
    }

    public struct Ant
    {
        public int firstCity;
        public int city;
        public SortedList<int,City> cityList;
        public double walkedDistance;
        public void DeleteCityFromList(int key)
        {
            if (cityList.ContainsKey(key))
                cityList.Remove(key);
        }
    }
}
