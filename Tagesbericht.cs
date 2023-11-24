using System;
class Tagesbericht
{
    int TagesAusgaben;
    int TagesEinnahmen;
    int Kontostand;
    int Lagerkosten;

    /// <summary>
    /// Addiere einen Verkaufswert auf die Tageseinnahmen
    /// </summary>
    public void AddiereEinnahmen (int VerkaufsWert)
    {
        TagesEinnahmen += VerkaufsWert;
    }

    /// <summary>
    /// Addiere einen Einkaufswert auf die Tageseinnahmen
    /// </summary>
    public void AddiereAusgaben (int EinkaufsWert)
    {
        TagesAusgaben += EinkaufsWert;
    }

    /// <summary>
    /// Speichert den Kontostand auf den der Bericht basieren soll
    /// </summary>
    public void SpeichereKontostand(int Kontostand)
    {
        this.Kontostand = Kontostand;
    }

    /// <summary>
    /// Speichert die Lagerkosten die für den Tag angefallen sind
    /// </summary>
    public void AnfallendeLagerkosten (int Lagerkosten)
    {
        this.Lagerkosten = Lagerkosten;
    }

    /// <summary>
    /// Zeigt den Tagesbereicht an
    /// </summary>
    public void ErstelleBericht(Zwischenhändler Händler, int AktuellerTag)
    {
        if(AktuellerTag == 1){
            InitiereBericht(Händler);
            return;
        } 

        ZeigeTagesberichtAn(Händler);
        Console.WriteLine("Bestätigen Sie mit Enter\n");
        while(true)
        {
            if(Console.ReadKey().Key == ConsoleKey.Enter)
            {
                InitiereBericht(Händler);
                break;
            }
        }
    }
    
    /// <summary>
    /// Erstellt den Bericht und gibt ihn auf der Konsole aus
    /// </summary>
    public void ZeigeTagesberichtAn(Zwischenhändler Händler)
    {
        string Ausgabe = "Bericht für {0} von {1}:\n";
        Console.WriteLine(string.Format(Ausgabe, Händler.Name, Händler.Firma));

        Ausgabe = "Kontostand:              - {0}";
        Console.WriteLine(string.Format(Ausgabe, Kontostand));

        Ausgabe = "Ausgaben:                - {0}";
        Console.WriteLine(string.Format(Ausgabe, TagesAusgaben));

        Ausgabe = "Lagerkosten:             - {0}";
        Console.WriteLine(string.Format(Ausgabe, Lagerkosten));

        Ausgabe = "Einnahmen:               + {0}";
        Console.WriteLine(string.Format(Ausgabe, TagesEinnahmen));

        Ausgabe = "Neuer Kontostand:        = {0}\n";
        Console.WriteLine(string.Format(Ausgabe, Händler.Kontostand));
    }

    /// <summary>
    /// Setzt die Daten zurück und holt sich den aktuellen Kontostand für den nächsten Bericht
    /// </summary>
    public void InitiereBericht(Zwischenhändler Händler)
    {
        TagesAusgaben = 0;
        TagesEinnahmen = 0;
        Lagerkosten = 0;
        Kontostand = Händler.Kontostand;
    }
}