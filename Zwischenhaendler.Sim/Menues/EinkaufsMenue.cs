using System.Runtime.CompilerServices;
using Zwischenhaendler.Sim;
using Einkaufen.Sim;
using ProdukteSim;
using GlobalsSim;
public class EinkaufsMenue
{
    private RabattMenue rabattMenue = new RabattMenue();
    /// <summary>
    /// Starte das Einkaufsmenü
    /// </summary>
    public void MenueAufrufen(Zwischenhändler Händler)
    {
        //Warte auf gültigen Input
        while(true)
        {
            Händler.Rabatt.BerechneRabatt(Händler);
            MenueAnzeigen(Händler);
            int ProduktNummer;
            string UserInput = Console.ReadLine()!;
            //Kehre ins Hauptmenü zurück
            if (UserInput == "z") break;

            //Checke ob UserInput ein Int ist 
            if (Int32.TryParse(UserInput, out ProduktNummer))
            {
                FrageKaufAnzahlAb(Händler, ProduktNummer);
            }
        }
    }

    /// <summary>
    /// Printe das Einkaufsmenü mit allen Verfügbaren Produkten
    /// </summary>
    public void MenueAnzeigen(Zwischenhändler Händler)
    {
        ListeProdukte(Händler);
        Console.WriteLine("z) Zurück");
    }

    /// <summary>
    /// Liste alle Verfügbaren Produkte auf
    /// </summary>
    public void ListeProdukte(Zwischenhändler Händler)
    {
        int i = 1;

        Console.WriteLine("Verfügbare Produkte:");
        foreach(Produkte Produkt in Globals.VerfügbareProdukte)
        {
            String Ausgabe = "{0}) {1} ({2} Tage) {3}$/Stück - {4} Verfügbar | {5}";
            Console.WriteLine(string.Format(
                Ausgabe, 
                i, 
                Produkt.ProduktName, 
                Produkt.Haltbarkeit, 
                Math.Round(Produkt.EinkaufsPreis,2),
                Produkt.Menge,
                rabattMenue.GebeRabattAus(Händler,i - 1)));
            i++;
        }
    }

    /// <summary>
    /// Funktion um die Menue Logik zu handeln
    /// </summary>
    public void UntermenueWeiterleiten(Zwischenhändler Händler, Produkte AusgewaehltesProdukt, int ProduktNummer)
    {
        //Warte auf gültigen Input 
        while(true)
        {
            string UserInput = Console.ReadLine()!;
            int KaufAnzahl;
            //Checke ob UserInput ein Int ist 
            if (Int32.TryParse(UserInput, out KaufAnzahl))
            {
               if(KaufprozessEinleiten(Händler, AusgewaehltesProdukt, KaufAnzahl, ProduktNummer)) return;
            }
            //Breche Kauf ab 
            if(UserInput == "z")
            {
                Console.WriteLine("Kauf abgebrochen\n");
                return;
            }
            Console.WriteLine("Keine gültige eingabe, probieren Sie es erneut oder brechen sie mit \"Z\" ab\n");
        }
    }

    /// <summary>
    /// Überprüft ob das Ausgewaehlte Produkt existiert und leitet dementsprechend den Kaufvorgang ein
    /// </summary>
    public void FrageKaufAnzahlAb (Zwischenhändler Händler, int ProduktNummer)
    {
        String Ausgabe;
        int GesamtAnzahlProdukte = Globals.VerfügbareProdukte.Count();
        //Checke ob UserInput in der Gültigen Range liegt 
        if(ProduktNummer <= GesamtAnzahlProdukte && ProduktNummer > 0)
        {
            //Clone zu kaufendes Produkt
            Produkte AusgewähltesProdukt = (Produkte)Globals.VerfügbareProdukte[ProduktNummer - 1].Clone();
            Ausgabe = "Wie viele vom Produkt ({0}) möchten Sie kaufen";
            Console.WriteLine(string.Format(Ausgabe, AusgewähltesProdukt.ProduktName));
            UntermenueWeiterleiten(Händler, AusgewähltesProdukt, ProduktNummer);
            return;
        }
        Ausgabe = "Es gibt kein Produkt mit der Nummer {0}";
        Console.WriteLine (string.Format(Ausgabe, ProduktNummer));   
    }

    /// <summary>
    /// Leite den Einkaufssprozess ein und gebe auftretende exceptions aus
    /// </summary>
    public bool KaufprozessEinleiten(Zwischenhändler Händler, Produkte AusgewaehltesProdukt, int KaufAnzahl, int ProduktNummer)
    {
        try
        {
            EinkaufenKlasse Einkauf = new EinkaufenKlasse();
            if(!Einkauf.BeginneKaufProzess(Händler, AusgewaehltesProdukt, KaufAnzahl, ProduktNummer)) return false;
            Console.WriteLine("Kauf erfolgreich\n");
            return true;  
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }  
    }
}