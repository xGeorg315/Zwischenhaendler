using System;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Data;
using System.Net.Http.Headers;

class Simulation
{   
    int AnzahlZwischenhändler = 0;
    int LetzterTag = 0;
    int AktuellerTag = 1;

    public Simulation (int LetzterTag, int AnzahlZwischenhändler)
    {
        this.LetzterTag = LetzterTag;
        this.AnzahlZwischenhändler = AnzahlZwischenhändler;
    }

    /// <summary>
    /// Initiert die Händler Simulation und Startet dieses anschließend
    /// </summary>
    public void InitiereSimulation()
    {
        HauptMenue HauptMenue = new HauptMenue();
        ProduktBerechnungen ProduktBerechnungen = new ProduktBerechnungen();
        ProduktBerechnungen.InitiereProdukte();
        Bankrott Bankrott = new Bankrott();

        StarteSimulation(ProduktBerechnungen, HauptMenue, Bankrott);
    }

    public void StarteSimulation(ProduktBerechnungen ProduktBerechnungen,HauptMenue HauptMenue, Bankrott Bankrott)
    {
        //Endlosschleife für die Simulation 
        while (AktuellerTag <= LetzterTag && Globals.Händler.Count() > 1)
        {  
            ProduktBerechnungen.BerechneMenge();
            ProduktBerechnungen.BerechneEinkaufsPreis();                                                                                                          
            foreach (Zwischenhändler Händler in Globals.Händler)                                       
            {    
                Händler.Lager.VerrechneLagerkosten(Händler);
                if(!Bankrott.ÜberprüfeBankrott(Händler, AktuellerTag))
                {
                    HauptMenue.MenueAufrufen(Händler, AktuellerTag);    
                }                                                
            }
            AktuellerTag++;
            VerschiebeHändlerAnordnung(1);
            Bankrott.LöscheAusgeschiedeneHändler();
        }
        BeeendeSimulation(Bankrott);
    }

    public void BeeendeSimulation (Bankrott Bankrott)
    {
        Rangliste Rangliste = new Rangliste();
        Rangliste.TeilnehmerAnzahl = AnzahlZwischenhändler;
        Rangliste.ErmittleRangfolge(Globals.Händler);
        Rangliste.ZeigeRangliste(Bankrott);
        Console.WriteLine("Danke fürs mitmachen");
    }

    /// <summary>
    ///Verschiebt die Händler um den Mitgegebenen Parameter nach rechts
    ///Letzter Händler kommt dabei an die erste Stelle
    /// </summary>
    void VerschiebeHändlerAnordnung (int Verschiebung)
    {
        for(int i = 0; i < Verschiebung; i++){
            Zwischenhändler ErsterHändler = Globals.Händler[0];
            Globals.Händler.Add(ErsterHändler);
            Globals.Händler.RemoveAt(0);
        }    
    }
}
