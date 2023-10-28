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
                MenueLogik(Händler, AusgewaehltesProdukt);
             }   
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
                Produkt.BasisPreis,
                Produkt.Menge));
            i++;
        }
    }

    /// <summary>
    /// Funktion um Kauf abzuschließen
    /// </summary>
    public void MenueLogik(Zwischenhändler Händler, int AusgewaehltesProdukt)
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
                Produkte GekauftesProdukt = (Produkte)Globals.VerfügbareProdukte[AusgewaehltesProdukt - 1].Clone();
                //Passe die Menge anhand der Gekauften Menge an
                GekauftesProdukt.Menge = KaufAnzahl;
                //Buche Betrag ab und Füge das Produkt dem jeweiligen Händler hinzu
                Händler.Kontostand -= GekauftesProdukt.BasisPreis * KaufAnzahl; 
                Händler.GekaufteProdukte.Add(GekauftesProdukt);
                //Ziehe Gekaufte Menge von der Verfügbaren Menge ab
                Globals.VerfügbareProdukte[AusgewaehltesProdukt - 1].SubtrahiereMenge(KaufAnzahl);
                
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
}