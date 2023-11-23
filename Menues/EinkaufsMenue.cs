using System.Runtime.CompilerServices;

class EinkaufsMenue
{
    /// <summary>
    /// Starte das Einkaufsmenü
    /// </summary>
    public void MenueAufrufen(Zwischenhändler Händler)
    {
        //Warte auf gültigen Input
        while(true)
        {
            MenueAnzeigen();
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
    public void MenueAnzeigen()
    {
        ListeProdukte();
        Console.WriteLine("z) Zurück");
    }

    /// <summary>
    /// Liste alle Verfügbaren Produkte auf
    /// </summary>
    public void ListeProdukte()
    {
        int i = 1;

        Console.WriteLine("Verfügbare Produkte:");
        foreach(Produkte Produkt in Globals.VerfügbareProdukte)
        {
            String Ausgabe = "{0}) {1} ({2} Tage) ${3}/Stück - {4} Verfügbar";
            Console.WriteLine(string.Format(
                Ausgabe, 
                i, 
                Produkt.ProduktName, 
                Produkt.Haltbarkeit, 
                (int)Produkt.EinkaufsPreis,
                Produkt.Menge));
            i++;
        }
    }

    /// <summary>
    /// Funktion um die Menue Logik zu handeln
    /// </summary>
    public void MenueLogik(Zwischenhändler Händler, Produkte AusgewaehltesProdukt, int ProduktNummer)
    {
        //Warte auf gültigen Input 
        while(true)
        {
            string UserInput = Console.ReadLine()!;
            int KaufAnzahl;
            //Checke ob UserInput ein Int ist 
            if (Int32.TryParse(UserInput, out KaufAnzahl))
            {
                Einkaufen Einkauf = new Einkaufen();
                if(Einkauf.BeginneKaufProzess(Händler, AusgewaehltesProdukt, KaufAnzahl, ProduktNummer)) return;
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
            MenueLogik(Händler, AusgewähltesProdukt, ProduktNummer);
            return;
        }
        Ausgabe = "Es gibt kein Produkt mit der Nummer {0}";
        Console.WriteLine (string.Format(Ausgabe, ProduktNummer));   
    }
}