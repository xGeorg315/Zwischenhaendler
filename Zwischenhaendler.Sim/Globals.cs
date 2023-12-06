using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Zwischenhaendler.Sim;
using ProdukteSim;

namespace GlobalsSim
{
        static public class Globals                                                                    //Klasse für Globale Variablen
        {
                //Erzeugung einer eindeutigen ID für jeden Händler        
                public static int Zähler;                                                       
                //Datentyp indem die Händler gespeichert werden                                          
                public static List<Zwischenhändler> Händler = new List<Zwischenhändler>();       
                //Alle Produkte die aktuell zum Kauf Verfügbar sind
                public static List<Produkte> VerfügbareProdukte = new List<Produkte>();
                
        }
}
