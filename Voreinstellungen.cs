class Voreinstellungen
{
    public int AnzahlZwischenhändler = 0;
    public int LetzterTag = 0;

    /// <summary>
    /// Erstelle die Händler Objekte
    /// </summary>
    public void StelleSimulationEin()
    {
        Schwierigkeit AuswahlSchwierigkeit = new Schwierigkeit();
        ErstelleHändler ErstelleHändler = new ErstelleHändler();
        AbfrageAnzahlHändler();
        //Erstelle x Zwischenhändler
        for(int i = 1; i <= AnzahlZwischenhändler; i++)                                                 
        {                                   
            Zwischenhändler Händler = new Zwischenhändler();
            ErstelleHändler.ErstelleZwischenhändler(i, Händler);                                 
            AuswahlSchwierigkeit.AbfrageSchwierigkeit(Händler);
        }
        FrageSimulationDauerAb();
    }   

    /// <summary>
    /// Abfrage wie viele Händler teilnehmen
    /// </summary>
    public void AbfrageAnzahlHändler()
    {
        Console.WriteLine("Wieviele Zwischenhändler nehmen teil?");
        //Wartet bis eine Zahl eingegeben wurde   
        while(true)                                                                                     
        {                                                                        
            string Eingabe = Console.ReadLine()!;
            //Check ob UserInput eine Zahl ist 
            if (Int32.TryParse(Eingabe, out AnzahlZwischenhändler)) break;                            
            Console.WriteLine("Sieht so aus als wäre das keine Nummer gewesen");
            Console.WriteLine("Erneut Versuchen:");
        }
    }

    /// <summary>
    /// Abfrage wie viele Tage die Simulation dauern soll
    /// </summary>
    public void FrageSimulationDauerAb () 
    {
        //Wartet bis eine Zahl eingegeben wurde   
        while(true)                                                                                     
        {       
            Console.WriteLine("Wie lange soll die Simulation laufen?:");                                                                 
            string Eingabe = Console.ReadLine()!;
            //Check ob UserInput eine Zahl ist 
            if (Int32.TryParse(Eingabe, out LetzterTag)) break;                            
            Console.WriteLine("Sieht so aus als wäre das keine Nummer gewesen");
            Console.WriteLine("Erneut Versuchen:");
        }
    }
}

class Schwierigkeit
{
    //Geldbeträge für die verschiedenen Schwierigkeiten
    const int EINFACH = 15000;
    const int MITTEL = 10000;
    const int SCHWER = 7000;

    /// <summary>
    /// Lässt den Händler eine von 3 verschiedenen Schwierigkeiten Auswählen
    /// </summary>
    public void AbfrageSchwierigkeit (Zwischenhändler Händler)
    {
        //Warte bis Händler einen Kontostand zugewiesen wird
        while(Händler.Kontostand == 0)
        {
            ZeigeSchwierigkeiten();
            //Weise je nach Input geldbetrag zu 
            WeiseHändlerSchwierigkeitZu(Händler); 
        }
    }

    /// <summary>
    /// Printe die unterschiedlichen Schwierigkeiten
    /// </summary>
    public void ZeigeSchwierigkeiten ()
    {
        Console.WriteLine("Bitte wählen sie die Schwierigkeit vom Händler:");
        Console.WriteLine("a) Einfach - 15000Euro\nb) Mittel - 10000Euro\nc) Schwer - 7000Euro");
    }

    /// <summary>
    /// Weise die ausgewähle Schwierigkeit den Händler zu
    /// </summary>
    public void WeiseHändlerSchwierigkeitZu (Zwischenhändler Händler) 
    {
        string Eingabe = Console.ReadLine()!;
            switch(Eingabe)
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

class ErstelleHändler 
{
    /// <summary>
    /// Frage den Namen des Händler ab 
    /// </summary>
    
    public void ErstelleZwischenhändler (int Nummer, Zwischenhändler Händler)
    {
        while(true)
        {     
            string NameHändler = FrageNameAb(Nummer);
            string FirmaHändler = FrageFirmaAb(NameHändler);
            if(SpeichereEingaben(Händler, NameHändler, FirmaHändler)) break;
            Console.WriteLine("Einer der beiden Namen wurde nicht korrekt ausgefüllt");
            Console.WriteLine("Versuche es nochmal");
        }
        //Speichere die Händler in eine Globale var
        Globals.Händler.Add(Händler); 
    }

    public string FrageNameAb (int HändlerNummer)
    {
        //Frage Name des Händlers ab
        string Ausgabe = "Name von Zwischenhändler " + HändlerNummer;                                       
        Console.WriteLine(Ausgabe);
        string NameHändler = Console.ReadLine()!;
        return NameHändler;
    }

    /// <summary>
    /// Frage die Firma des Händler ab 
    /// </summary>
    public string FrageFirmaAb(string HändlerName)
    {
        //Frage Firma des Händlers ab
        string Ausgabe = "Firma von " + HändlerName;                                                   
        Console.WriteLine(Ausgabe);
        string NameFirma = Console.ReadLine()!; 
        return NameFirma;
    }

    /// <summary>
    /// Checke ob die Eingaben valide sind und speichere diese ggfs
    /// </summary>
    public bool SpeichereEingaben(Zwischenhändler Händler, string NameFirma, string NameHändler)
    {
        if(!string.IsNullOrWhiteSpace(NameFirma) && !string.IsNullOrWhiteSpace(NameHändler))
        {
            //Speichere Name in das angelegte Händler Objekt
            Händler.Name = NameHändler;            
            //Speichere Firma in das angelegte Händler Objekt                                    
            Händler.Firma = NameFirma;                                               
            return true;
        }
        return false;
    }
}