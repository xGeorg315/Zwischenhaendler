using System;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Data;

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
                string Ausgabe = "{0} von {1} | {2} | Tag: {3}";                                       //Erstelle Output String
                Console.WriteLine(string.Format(
                    Ausgabe, 
                    Händler.Name, 
                    Händler.Firma, 
                    Händler.Kontostand, 
                    AktuellerTag
                    ));
                CheckChoices();                                                                        //Checke die Möglichkeiten des Händlers
            }
            AktuellerTag++;
            VerschiebeHändlerAnordnung(1);
        }
    }

    void CheckChoices()
    {
        Console.WriteLine("b) Runde beenden");   
        while (true)
        {                                                                                               //Warte auf gültigen Input
            string input = Console.ReadLine()!;
            if (input == "b") break;
            Console.WriteLine("Ungültige Eingabe :-(");
            Console.WriteLine("Probiere es nochmal:");
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

}