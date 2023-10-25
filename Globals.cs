using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

static class Globals                                                                    //Klasse für Globale Variablen
{
        public static int counter;                                                      //Erzeugung einer eindeutigen ID für jeden Händler                                            
        public static List<Zwischenhändler> Händler = new List<Zwischenhändler>();      //Datentyp indem die Händler gespeichert werden        
        public static List<Produkte> VerfügbareProdukte = new List<Produkte>();//Alle Produkte die aktuell zum Kauf Verfügbar sind
        
}
