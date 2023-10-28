using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

class Zwischenhändler
{
    public string Name = "";
    public string Firma = "";
    public int ID;                      //Für den Fall dass zwei Händler gleich heißen
    public int Kontostand = 0;
    public List<Produkte> GekaufteProdukte = new List<Produkte>();
    public Lager Lager = new Lager();

    public Zwischenhändler()
    {
        ID = Globals.counter + 1;
        Globals.counter++;
    }
}