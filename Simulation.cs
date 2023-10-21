using System;

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
            WähleSchwierigkeit(Händler);
        }
    }

    public void StarteSimulation()
    {
        int AktuellerTag = 1;
        while (true)
        {                                                                                                            
            foreach (Zwischenhändler Händler in Globals.Händler)                                       //Endlosschleife für die Simulation 
            {                               
                string Ausgabe = "{0} von {1} | {2} | Tag {3}";                                         //Erstelle Output String
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

    void WähleSchwierigkeit (Zwischenhändler Händler) 
    {
        const int EINFACH = 15000;
        const int MITTEL  = 10000;
        const int SCHWER  = 7000;

        string Ausgabe = "Bitte wählen Sie eine Schwierigkeit für {0} von {1}:";
        Console.WriteLine(string.Format(Ausgabe, Händler.Name, Händler.Firma));
        Console.WriteLine("a) Einfach - 15000€\nb) Mittel - 10000€\nc) Schwer - 7000€");

         while (Händler.Kontostand == 0)
        {                                                                                               //Warte auf gültigen Input
            string input = Console.ReadLine()!;
            
            switch (input)
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