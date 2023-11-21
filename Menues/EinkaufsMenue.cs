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
    /// Überprüft ob das Ausgewaehlte Produkt existiert und leitet dementsprechend den Kaufvorgang weiter
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
                if(BeginneKaufProzess(Händler, AusgewaehltesProdukt, KaufAnzahl, ProduktNummer)) return;
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
    /// Überprügt ob der Kauf allen Regeln entspricht
    /// </summary>
    public bool ValidiereKauf (int KaufAnzahl, Zwischenhändler Händler, Produkte AusgewaehltesProdukt, int NeuerKontostand)
    {
        //Checke ob genug Platz in Lager
        if(KaufAnzahl > Händler.Lager.getFreierPlatz())
        {
            Console.WriteLine("Fehler beim Kauf, nicht genügend Platz im Lager\n");
            return false;
        }
        //Checke ob genug Produkte Verfügbar sind
        if(KaufAnzahl > AusgewaehltesProdukt.Menge)
        { 
            string Ausgabe = "Fehler beim Kauf, nicht genügend {0} Verfügbar\n";
            Console.WriteLine(string.Format(Ausgabe, AusgewaehltesProdukt.ProduktName));
            return false;
        }
        //Checke ob Genug Geld vorhanden
        if (NeuerKontostand < 0)
        {
            Console.WriteLine("Fehler beim Kauf, nicht genug Geld\n");
            return false;
        }
            return true;
    }

    /// <summary>
    /// Passt alle Parameter an die sich durch den Kauf ändern und Speichert alle neuen Werte
    /// </summary>
    public void WickleKaufAb (Zwischenhändler Händler, int NeuerKontostand, Produkte AusgewaehltesProdukt, int KaufAnzahl, int ProduktNummer)
    {
        //Buche den neuen Kontostand
        Händler.Kontostand = NeuerKontostand;
        //Passe die Menge anhand der Gekauften Menge an
        AusgewaehltesProdukt.Menge = KaufAnzahl;
        //Füge das Produkt dem jeweiligen Händler hinzu
        Händler.GekaufteProdukte.Add(AusgewaehltesProdukt);
        //Ziehe Gekaufte Menge von der Verfügbaren Menge ab
        Globals.VerfügbareProdukte[ProduktNummer - 1].SubtrahiereMenge(KaufAnzahl);
        //Addiere Menge in den Bestand 
        Händler.Lager.Lagerbestand += AusgewaehltesProdukt.Menge;
        Console.WriteLine("Kauf erfolgreich\n");
    }

    /// <summary>
    /// Leitet den Kaufprozess nach der Eingabe des Händlers ein
    /// </summary>
    public bool BeginneKaufProzess(Zwischenhändler Händler, Produkte AusgewaehltesProdukt, int KaufAnzahl, int ProduktNummer)
    {
        //Berechne Vorraussichtlichen Kontostand
        int NeuerKontostand = Händler.Kontostand - (int)AusgewaehltesProdukt.BasisPreis * KaufAnzahl; 
        //Checke ob der Kauf den Regeln und Limits entspricht
        if(!ValidiereKauf(KaufAnzahl, Händler, AusgewaehltesProdukt, NeuerKontostand)) return false;
        //Wickle den Kauf ab
        WickleKaufAb(Händler, NeuerKontostand, AusgewaehltesProdukt, KaufAnzahl, ProduktNummer);
        return true;
    }
}