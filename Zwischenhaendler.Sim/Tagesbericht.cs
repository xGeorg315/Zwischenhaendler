using System;
using Zwischenhaendler.Sim;
public class Tagesbericht
{
    public double TagesAusgaben;
    double TagesEinnahmen;
    double Kontostand;
    double Lagerkosten;
    double KreditRueckzahlung;

    /// <summary>
    /// Addiere einen Verkaufswert auf die Tageseinnahmen
    /// </summary>
    public void AddiereEinnahmen (double VerkaufsWert)
    {
        TagesEinnahmen += VerkaufsWert;
    }

    /// <summary>
    /// Addiere einen Einkaufswert auf die Tageseinnahmen
    /// </summary>
    public void AddiereAusgaben (double EinkaufsWert, double KreditRueckzahlung = 0)
    {
        TagesAusgaben += EinkaufsWert;
        this.KreditRueckzahlung = KreditRueckzahlung;
    }

    /// <summary>
    /// Speichert den Kontostand auf den der Bericht basieren soll
    /// </summary>
    public void SpeichereKontostand(double Kontostand)
    {
        this.Kontostand = Kontostand;
    }

    /// <summary>
    /// Speichert die Lagerkosten die für den Tag angefallen sind
    /// </summary>
    public void AnfallendeLagerkosten (double Lagerkosten)
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
        Console.WriteLine(string.Format(Ausgabe, Math.Round(Kontostand,2)));

        Ausgabe = "Ausgaben:                - {0}";
        Console.WriteLine(string.Format(Ausgabe, Math.Round(TagesAusgaben,2)));

        Ausgabe = "Lagerkosten:             - {0}";
        Console.WriteLine(string.Format(Ausgabe, Math.Round(Lagerkosten,2)));

        Ausgabe = "Kredit Rückzahlung:      - {0}";
        Console.WriteLine(string.Format(Ausgabe, Math.Round(KreditRueckzahlung,2)));

        Ausgabe = "Einnahmen:               + {0}";
        Console.WriteLine(string.Format(Ausgabe, Math.Round(TagesEinnahmen,2)));

        Ausgabe = "Neuer Kontostand:        = {0}\n";
        Console.WriteLine(string.Format(Ausgabe,  Math.Round(Händler.Kontostand,2)));
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