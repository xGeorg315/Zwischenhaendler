using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GlobalsSim;
using ProdukteSim;

namespace Zwischenhaendler.Sim
{
    public class Zwischenhändler
    {
        public string Name = "";
        public string Firma = "";
        public int ID;                      //Für den Fall dass zwei Händler gleich heißen
        public double Kontostand = 0;
        public List<Produkte> GekaufteProdukte = new List<Produkte>();
        public Lager Lager = new Lager();
        public int TagAusscheidung = 0; 
        public Tagesbericht Tagesbericht = new Tagesbericht();
        public Rabatt Rabatt = new Rabatt();
        public List <double> EinkaufsRabatte= new List<double>(new double[Globals.VerfügbareProdukte.Count()]);

        public Zwischenhändler()
        {
            ID = Globals.Zähler + 1;
            Globals.Zähler++;
        }
    }
}