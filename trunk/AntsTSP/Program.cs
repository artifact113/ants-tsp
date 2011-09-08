using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AntsTSP
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FInput());


            // Tests zu Listen
            //List<List<City>> field = new List<List<City>>();

            //City ci = field[0][0];

            //foreach (List<City> cityRow in field)
            //{
            //    foreach (City city in cityRow)
            //    {

            //    }
            //}
        }
    }
}
