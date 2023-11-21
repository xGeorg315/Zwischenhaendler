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
           DateiLesen DateiLesen = new DateiLesen();
           DateiLesen.LeseProdukte();
           
           Voreinstellungen Voreinstellungen = new Voreinstellungen();
           Voreinstellungen.StelleSimulationEin();
           Simulation Simulation = new Simulation(Voreinstellungen.LetzterTag, Voreinstellungen.AnzahlZwischenhändler);
           Simulation.InitiereSimulation();

        }
    }
}
