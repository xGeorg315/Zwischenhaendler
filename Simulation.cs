using System;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Data;
using System.Net.Http.Headers;

class Simulation
{   
    int AnzahlZwischenhändler = 0;

    /// <summary>
    /// Abfrage wie viele Händler teilnehmen
    /// </summary>
    public void AnzahlHändler()
    {
        Console.WriteLine("Wieviele Zwischenhändler nehmen teil?");
        //Wartet bis eine Zahl eingegeben wurde   
        while(true)                                                                                     
        {                                                                        
            string UserInput = Console.ReadLine()!;
            //Check ob UserInput eine Zahl ist 
            if (Int32.TryParse(UserInput, out AnzahlZwischenhändler)) break;                            
            Console.WriteLine("Sieht so aus als wäre das keine Nummer gewesen");
            Console.WriteLine("Erneut Versuchen:");
        }
    }

    /// <summary>
    /// Erstelle die Händler Objekte
    /// </summary>
    public void ErstelleHändler()
    {
        //Erstelle x Zwischenhändler
        for(int i = 1; i <= AnzahlZwischenhändler; i++)                                                 
        {                                   
            Zwischenhändler Händler = new Zwischenhändler();

            while(true)
            {
                //Frage Name des Händlers ab
                string Ausgabe = "Name von Zwischenhändler " + i;                                       
                Console.WriteLine(Ausgabe);
                string NameHändler = Console.ReadLine()!;

                //Frage Firma des Händlers ab
                Ausgabe = "Firma von " + NameHändler;                                                   
                Console.WriteLine(Ausgabe);
                string NameFirma = Console.ReadLine()!;   
                
                if(!string.IsNullOrWhiteSpace(NameFirma) && !string.IsNullOrWhiteSpace(NameHändler))
                {
                    //Speichere Name in das angelegte Händler Objekt
                    Händler.Name = NameHändler;            
                    //Speichere Firma in das angelegte Händler Objekt                                    
                    Händler.Firma = NameFirma;                                               
                    break;
                }
                Console.WriteLine("Einer der beiden Namen wurde nicht korrekt ausgefüllt");
                Console.WriteLine("Versuche es nochmal");
            }
            //Speichere die Händler in eine Globale var
            Globals.Händler.Add(Händler);                                                       
            AuswahlSchwierigkeit(Händler);
        }
    }

    /// <summary>
    /// Satrte die Händler Simulation
    /// </summary>
    public void StarteSimulation()
    {
        int AktuellerTag = 1;
        HauptMenue HauptMenue = new HauptMenue();
        InitiereProdukte();
        //Endlosschleife für die Simulation 
        while (true)
        {                                                                                                            
            foreach (Zwischenhändler Händler in Globals.Händler)                                       
            {    
                HauptMenue.MenueAurufen(Händler, AktuellerTag);                                                 
            }
            AktuellerTag++;
            VerschiebeHändlerAnordnung(1);
        }
    }

    void InitiereProdukte()
    {
        foreach(Produkte Produkt in Globals.VerfügbareProdukte)
        {
            Produkt.BerechneMaxMenge();
            Produkt.BerechneMenge();
        }
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

    /// <summary>
    /// Lässt den Händler eine von 3 verschiedenen Schwierigkeiten Auswählen
    /// </summary>
    void AuswahlSchwierigkeit (Zwischenhändler Händler)
    {
        //Geldbeträge für die verschiedenen Schwierigkeiten
        const int EINFACH = 15000;
        const int MITTEL = 10000;
        const int SCHWER = 7000;
        
        Console.WriteLine("Bitte wählen sie die Schwierigkeit vom Händler:");
        Console.WriteLine("a) Einfach - 15000Euro\nb) Mittel 10000Euro\nc) Schwer: 7000Euro");

        //Warte bis Händler einen Kontostand zugewiesen wird
        while(Händler.Kontostand == 0)
        {
            //Weise je nach Input geldbetrag zu 
            string input = Console.ReadLine()!;
            switch(input)
            {
                case "a":
                    Händler.Kontostand = EINFACH;
                    break;
                case "b":
                    Händler.Kontostand = MITTEL;
                    break;    
                case "c":
                    Händler.Kontostand = SCHWER;
                    break;
                default:
                    Console.WriteLine("Ungültige Eingabe :-(");
                    Console.WriteLine("Probiere es nochmal:");
                    break;
            }
        }
    }

}
