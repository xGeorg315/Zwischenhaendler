using System;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Data;
using System.Net.Http.Headers;

class Simulation
{   
    int AnzahlZwischenhändler = 0;

    public void AnzahlHändler()
    {
        Console.WriteLine("Wieviele Zwischenhändler nehmen teil?");   
        while(true)                                                                                     //Wartet bis eine Zahl eingegeben wurde
        {                                                                        
            string UserInput = Console.ReadLine()!;
            if (Int32.TryParse(UserInput, out AnzahlZwischenhändler)) break;                            //Check ob UserInput eine Zahl ist 
            Console.WriteLine("Sieht so aus als wäre das keine Nummer gewesen");
            Console.WriteLine("Erneut Versuchen:");
        }
    }

    public void ErstelleHändler()
    {
        for(int i = 1; i <= AnzahlZwischenhändler; i++)                                                 //Erstelle x Zwischenhändler
        {                                   
            Zwischenhändler Händler = new Zwischenhändler();

            while(true)
            {
                string Ausgabe = "Name von Zwischenhändler " + i;                                       //Frage Name des Händlers ab
                Console.WriteLine(Ausgabe);
                string NameHändler = Console.ReadLine()!;

                Ausgabe = "Firma von " + NameHändler;                                                   //Frage Firma des Händlers ab
                Console.WriteLine(Ausgabe);
                string NameFirma = Console.ReadLine()!;   
                
                if(!string.IsNullOrWhiteSpace(NameFirma) && !string.IsNullOrWhiteSpace(NameHändler))
                {
                    Händler.Name = NameHändler;                                                 //Speichere Name in den Händler
                    Händler.Firma = NameFirma;                                                  //Speichere Firma in den Händler
                    break;
                }
                Console.WriteLine("Einer der beiden Namen wurde nicht korrekt ausgefüllt");
                Console.WriteLine("Versuche es nochmal");
            }
            Globals.Händler.Add(Händler);                                                       //Speichere die Händler in eine Globale var
            AuswahlSchwierigkeit(Händler);
        }
    }

    public void StarteSimulation()
    {
        int AktuellerTag = 1;
        while (true)
        {                                                                                                            
            foreach (Zwischenhändler Händler in Globals.Händler)                                       //Endlosschleife für die Simulation 
            {    
                Hauptmenue(Händler, AktuellerTag);                                                 //Checke die Möglichkeiten des Händlers
            }
            AktuellerTag++;
            VerschiebeHändlerAnordnung(1);
        }
    }

    void Hauptmenue(Zwischenhändler Händler, int AktuellerTag)
    {
        while (true)
        {    
            HauptmenueAnzeigen(Händler,AktuellerTag);                                                                                          //Warte auf gültigen Input
            string input = Console.ReadLine()!;
            if (input == "b")
            {
                break;
            } 
            else if (input == "e") 
            {
                EinkaufsMenue(Händler);
            }
            else if(input == "v")
            {
                VerkaufsMenue(Händler);
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe :-(");
                Console.WriteLine("Probiere es nochmal:");
            } 
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
        const int EINFACH = 15000;
        const int MITTEL = 10000;
        const int SCHWER = 7000;
        
        Console.WriteLine("Bitte wählen sie die Schwierigkeit vom Händler:");
        Console.WriteLine("a) Einfach - 15000Euro\nb) Mittel 10000Euro\nc) Schwer: 7000Euro");
        while(Händler.Kontostand == 0)
        {
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

    public void EinkaufsMenue(Zwischenhändler Händler)
    {
        while(true)
        {
            EinkaufsMenueAnzeigen();
            int AusgewaehltesProdukt;
            string UserInput = Console.ReadLine()!;
            if (UserInput == "z") break;

            if (Int32.TryParse(UserInput, out AusgewaehltesProdukt))
            {

             int GesamtAnzahlProdukte = Globals.VerfügbareProdukte.Count();
             if(AusgewaehltesProdukt <= GesamtAnzahlProdukte && AusgewaehltesProdukt > 0)
             {
                string Ausgabe = "Wie viele vom Produkt ({0}) möchten Sie kaufen";
                Console.WriteLine(string.Format(Ausgabe, Globals.VerfügbareProdukte[AusgewaehltesProdukt - 1].ProduktName));
                EinkaufsLogik(Händler, AusgewaehltesProdukt);
             }   

            }
        }
    }

    public void EinkaufsMenueAnzeigen()
    {
        Produkte EinkaufsMenü = new Produkte();
        EinkaufsMenü.ListeProdukte();
        Console.WriteLine("z) Zurück");
    }

    public void HauptmenueAnzeigen(Zwischenhändler Händler, int AktuellerTag)
    {
        string Ausgabe = "{0} von {1} | {2} | Tag: {3}";                                       //Erstelle Output String
        Console.WriteLine(string.Format(
            Ausgabe, 
            Händler.Name, 
            Händler.Firma, 
            Händler.Kontostand, 
            AktuellerTag
            ));
        Console.WriteLine("e) Einkaufen");  
        Console.WriteLine("v) Verkaufen"); 
        Console.WriteLine("b) Runde beenden"); 
    }

    public void EinkaufsLogik(Zwischenhändler Händler, int AusgewaehltesProdukt)
    {
        while(true)
        {
            string UserInput = Console.ReadLine()!;
            int KaufAnzahl;

            if (Int32.TryParse(UserInput, out KaufAnzahl))
            {
                Produkte GekauftesProdukt = (Produkte)Globals.VerfügbareProdukte[AusgewaehltesProdukt - 1].Clone();
                GekauftesProdukt.Menge = KaufAnzahl;
                Händler.Kontostand -= GekauftesProdukt.BasisPreis * KaufAnzahl; 
                Händler.GekaufteProdukte.Add(GekauftesProdukt);
                Console.WriteLine("Kauf erfolgreich");
                return;
            }
            if(UserInput == "z")
            {
                Console.WriteLine("Kauf abgebrochen");
                return;
            }

        }
    }
    public void VerkaufsMenue(Zwischenhändler Händler)
    {
        int AusgewaehltesProdukt;

        if(Händler.GekaufteProdukte.Count() == 0)
        {
            Console.WriteLine("Keine Produkte gekauft");
            return;
        }

        while(true)
        {
            VerkaufsMenueAnzeigen(Händler);
            string UserInput = Console.ReadLine()!;
            if (Int32.TryParse(UserInput, out AusgewaehltesProdukt))
            {

                int GesamtAnzahlProdukte = Globals.VerfügbareProdukte.Count();
                if(AusgewaehltesProdukt <= GesamtAnzahlProdukte && AusgewaehltesProdukt > 0)
                {
                    string Ausgabe = "Wie viele vom Produkt ({0}) möchten Sie verkaufen (max: {1})";
                    Console.WriteLine(string.Format(
                        Ausgabe, 
                        Händler.GekaufteProdukte[AusgewaehltesProdukt - 1].ProduktName, 
                        Händler.GekaufteProdukte[AusgewaehltesProdukt - 1].Menge));
                    VerkaufsLogik(Händler, AusgewaehltesProdukt);
                }   
            }
            
            if(UserInput == "z")
            {
                Console.WriteLine("Verkauf abgebrochen");
                return;
            }
        }
    }
    public void VerkaufsMenueAnzeigen(Zwischenhändler Händler)
    {
        int i = 1;

        Console.WriteLine("Produkte im Besitz:");
        foreach(Produkte Produkt in Händler.GekaufteProdukte)
        {
            String Ausgabe = "{0}) {1} ({2} Tage) ${3}/Stück";
            Console.WriteLine(string.Format(
                Ausgabe, 
                i, 
                Produkt.ProduktName,
                Produkt.Haltbarkeit, 
                Produkt.BasisPreis * 0.8));
            i++;
        }
        Console.WriteLine("z) Zurück");
    }

    public void VerkaufsLogik(Zwischenhändler Händler, int AusgewaehltesProdukt)
    {
        while(true)
        {
            string UserInput = Console.ReadLine()!;
            int VerkaufAnzahl;
            double Verkaufspreis;

            if (Int32.TryParse(UserInput, out VerkaufAnzahl))
            {
                if(VerkaufAnzahl <= Händler.GekaufteProdukte[AusgewaehltesProdukt -1 ].Menge && VerkaufAnzahl > 0)
                {
                    Verkaufspreis = Convert.ToDouble(Händler.GekaufteProdukte[AusgewaehltesProdukt - 1].BasisPreis) * 0.8 * VerkaufAnzahl; 
                    Händler.Kontostand += Convert.ToInt32(Verkaufspreis);
                    Händler.GekaufteProdukte[AusgewaehltesProdukt - 1].Menge -= VerkaufAnzahl;

                    if(Händler.GekaufteProdukte[AusgewaehltesProdukt - 1].Menge == 0)
                    {
                        Händler.GekaufteProdukte.RemoveAt(AusgewaehltesProdukt - 1);
                    }
                    Console.WriteLine("Verkauf erfolgreich");
                    return;
                }
            }
            if(UserInput == "z")
            {
                Console.WriteLine("Verkauf abgebrochen");
                return;
            }
        }
    }
}