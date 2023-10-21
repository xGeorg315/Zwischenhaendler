using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters;
using System.Threading;

namespace Zwischenhändler_1
{
    class Program
    {  
        static void Main(string[] args)
        {
           Simulation start = new Simulation();
           start.AnzahlHändler();
           start.HändlerErstellen();
           start.StarteSimulation();
        }
    }
}
