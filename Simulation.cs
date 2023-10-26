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
        //Endlosschleife für die Simulation 
        while (true)
        {                                                                                                            
            foreach (Zwischenhändler Händler in Globals.Händler)                                       
            {    
                Hauptmenue(Händler, AktuellerTag);                                                 
            }
            AktuellerTag++;
            VerschiebeHändlerAnordnung(1);
        }
    }

    /// <summary>
    /// Starte Hauptmenü
    /// </summary>
    void Hauptmenue(Zwischenhändler Händler, int AktuellerTag)
    {
        //Warte auf gültigen Input
        while (true)
        {    
            //Leite je nach Input Weiter oder beende die Runde
            HauptmenueAnzeigen(Händler,AktuellerTag);                                                                                          
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

    /// <summary>
    /// Starte das Einkaufsmenü
    /// </summary>
    public void EinkaufsMenue(Zwischenhändler Händler)
    {
        //Warte auf gültigen Input
        while(true)
        {
            EinkaufsMenueAnzeigen();
            int AusgewaehltesProdukt;
            string UserInput = Console.ReadLine()!;
            //Kehre ins Hauptmenü zurück
            if (UserInput == "z") break;

            //Checke ob UserInput ein Int ist 
            if (Int32.TryParse(UserInput, out AusgewaehltesProdukt))
            {
             int GesamtAnzahlProdukte = Globals.VerfügbareProdukte.Count();
             //Checke ob UserInput in der Gültigen Range liegt 
             if(AusgewaehltesProdukt <= GesamtAnzahlProdukte && AusgewaehltesProdukt > 0)
             {
                string Ausgabe = "Wie viele vom Produkt ({0}) möchten Sie kaufen";
                Console.WriteLine(string.Format(Ausgabe, Globals.VerfügbareProdukte[AusgewaehltesProdukt - 1].ProduktName));
                EinkaufsLogik(Händler, AusgewaehltesProdukt);
             }   
            }
        }
    }

    /// <summary>
    /// Printe das Einkaufsmenü mit allen Verfügbaren Produkten
    /// </summary>
    public void EinkaufsMenueAnzeigen()
    {
        Produkte EinkaufsMenü = new Produkte();
        EinkaufsMenü.ListeProdukte();
        Console.WriteLine("z) Zurück");
    }

    /// <summary>
    /// Printe das Hauptmenü 
    /// </summary>
    public void HauptmenueAnzeigen(Zwischenhändler Händler, int AktuellerTag)
    {
        //Erstelle Output String
        string Ausgabe = "{0} von {1} | {2} | Tag: {3}";                                       
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

    /// <summary>
    /// Funktion um Kauf abzuschließen
    /// </summary>
    public void EinkaufsLogik(Zwischenhändler Händler, int AusgewaehltesProdukt)
    {
        //Warte auf gültigen Input 
        while(true)
        {
            string UserInput = Console.ReadLine()!;
            int KaufAnzahl;
            //Checke ob UserInput ein Int ist 
            if (Int32.TryParse(UserInput, out KaufAnzahl))
            {
                //Clone das Produkt aus der Globalen Produktliste
                Produkte GekauftesProdukt = (Produkte)Globals.VerfügbareProdukte[AusgewaehltesProdukt - 1].Klone();
                //Passe die Menge anhand der Gekauften Menge an
                GekauftesProdukt.Menge = KaufAnzahl;
                //Buche Betrag ab und Füge das Produkt dem jeweiligen Händler hinzu
                Händler.Kontostand -= GekauftesProdukt.BasisPreis * KaufAnzahl; 
                Händler.GekaufteProdukte.Add(GekauftesProdukt);
                Console.WriteLine("Kauf erfolgreich");
                return;
            }
            //Breche Kauf ab 
            if(UserInput == "z")
            {
                Console.WriteLine("Kauf abgebrochen");
                return;
            }

        }
    }

    /// <summary>
    /// Starte das Verkaufsmenü
    /// </summary>
    public void VerkaufsMenue(Zwischenhändler Händler)
    {
        int AusgewaehltesProdukt;
        //Checke ob Produkte zum Verkauf sind 
        if(Händler.GekaufteProdukte.Count() == 0)
        {
            Console.WriteLine("Keine Produkte gekauft");
            return;
        }

        //Warte auf gültigen Input
        while(true)
        {
            VerkaufsMenueAnzeigen(Händler);
            string UserInput = Console.ReadLine()!;
            //Checke ob User Input ein Int ist
            if (Int32.TryParse(UserInput, out AusgewaehltesProdukt))
            {
                //Checke ob UserInput in der gültigen Range liegt
                int GesamtAnzahlProdukte = Globals.VerfügbareProdukte.Count();
                if(AusgewaehltesProdukt <= GesamtAnzahlProdukte && AusgewaehltesProdukt > 0)
                {
                    string Ausgabe = "Wie viele vom Produkt ({0}) möchten Sie verkaufen (max: {1})";
                    Console.WriteLine(string.Format(
                        Ausgabe, 
                        Händler.GekaufteProdukte[AusgewaehltesProdukt - 1].ProduktName, 
                        Händler.GekaufteProdukte[AusgewaehltesProdukt - 1].Menge));
                    //Schließe Kauf ab
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

    /// <summary>
    /// Printe das Verkaufsmenü
    /// </summary>
    public void VerkaufsMenueAnzeigen(Zwischenhändler Händler)
    {
        int i = 1;

        Console.WriteLine("Produkte im Besitz:");
        //Itteriere durch die Gekauften Produkte und printe sie 
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

    /// <summary>
    /// Funktion um Verkauf abzuschließen
    /// </summary>
    public void VerkaufsLogik(Zwischenhändler Händler, int AusgewaehltesProdukt)
    {
        while(true)
        {
            string UserInput = Console.ReadLine()!;
            int VerkaufAnzahl;
            double Verkaufspreis;

            //Checke ob UserInput ein Int ist
            if (Int32.TryParse(UserInput, out VerkaufAnzahl))
            {
                //Checke ob Verkaufsmenge im gültigen Bereich liegt
                if(VerkaufAnzahl <= Händler.GekaufteProdukte[AusgewaehltesProdukt -1 ].Menge && VerkaufAnzahl > 0)
                {
                    //Berechne Verkaufspreis und Buche auf den Kontostand 
                    Verkaufspreis = Convert.ToDouble(Händler.GekaufteProdukte[AusgewaehltesProdukt - 1].BasisPreis) * 0.8 * VerkaufAnzahl; 
                    Händler.Kontostand += Convert.ToInt32(Verkaufspreis);
                    //Berechne die übrige Menge
                    Händler.GekaufteProdukte[AusgewaehltesProdukt - 1].Menge -= VerkaufAnzahl;

                    //Wenn die Menge auf Null fällt, lösche das Produkt
                    if(Händler.GekaufteProdukte[AusgewaehltesProdukt - 1].Menge == 0)
                    {
                        Händler.GekaufteProdukte.RemoveAt(AusgewaehltesProdukt - 1);
                    }
                    Console.WriteLine("Verkauf erfolgreich");
                    return;
                }
            }
            //Breche Kauf ab
            if(UserInput == "z")
            {
                Console.WriteLine("Verkauf abgebrochen");
                return;
            }
        }
    }
}