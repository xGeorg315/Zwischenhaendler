using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using DateiLesen.Sim;

namespace Zwischenhändler_1
{
    public class Program
    {  
        static void Main(string[] args)
        {
           DateiLesenKlasse DateiLesen = new DateiLesenKlasse();
           DateiLesen.LeseProdukte();
           
           Voreinstellungen Voreinstellungen = new Voreinstellungen();
           Voreinstellungen.StelleSimulationEin();
           Simulation Simulation = new Simulation(Voreinstellungen.LetzterTag, Voreinstellungen.AnzahlZwischenhändler);
           Simulation.InitiereSimulation();

        }
    }
}
