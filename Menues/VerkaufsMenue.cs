class VerkaufsMenue
{
    /// <summary>
    /// Starte das Verkaufsmenü
    /// </summary>
    public void MenueAufrufen(Zwischenhändler Händler)
    {
        if(!ÜberprüfeObHändlerProdukteBesitzt(Händler)) return;
        StarteMenue(Händler);
    }

    /// <summary>
    /// Überprüft ob der Händler Produkte Besitzt die er Verkaufen könnte
    /// </summary>
    public bool ÜberprüfeObHändlerProdukteBesitzt(Zwischenhändler Händler)
    {
        //Checke ob Produkte zum Verkauf sind 
        if(Händler.GekaufteProdukte.Count() == 0)
        {
            Console.WriteLine("Keine Produkte gekauft\n");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Startet das Verkaufsmenue
    /// </summary>
    public void StarteMenue(Zwischenhändler Händler)
    {
        int AusgewaehltesProdukt;
        //Warte auf gültigen Input
        while(true)
        {
            MenueAnzeigen(Händler);
            string UserInput = Console.ReadLine()!;
            //Checke ob User Input ein Int ist
            if (Int32.TryParse(UserInput, out AusgewaehltesProdukt))
            {
                BeginneKaufProzess(Händler, AusgewaehltesProdukt);
            }
            
            if(UserInput == "z")
            {
                Console.WriteLine("Verkauf abgebrochen\n");
                return;
            }
        }
    }

    /// <summary>
    /// Beginnt nach der Überprüfung der Verkaufsprozess
    /// </summary>
    public void BeginneKaufProzess(Zwischenhändler Händler, int AusgewaehltesProdukt)
    {
        if(!ÜberprüfeVerkaufsRegeln(Händler, AusgewaehltesProdukt)) return;
        MenueLogik(Händler, AusgewaehltesProdukt);
    }

    /// <summary>
    /// Überprüft ob die Regeln für den Verkauf eingehalten werden
    /// </summary>
    public bool ÜberprüfeVerkaufsRegeln (Zwischenhändler Händler, int AusgewaehltesProdukt)
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
            return true;
        }   
        return false;
    }

    /// <summary>
    /// Printe das Verkaufsmenü
    /// </summary>
    public void MenueAnzeigen(Zwischenhändler Händler)
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
                Convert.ToInt32(Produkt.EinkaufsPreis * 0.8)));
            i++;
        }
        Console.WriteLine("z) Zurück");
    }

    /// <summary>
    /// Funktion um Verkauf abzuschließen
    /// </summary>
    public void MenueLogik(Zwischenhändler Händler, int AusgewaehltesProdukt)
    {
        while(true)
        {
            string UserInput = Console.ReadLine()!;
            int VerkaufAnzahl;
            //Checke ob UserInput ein Int ist
            if (Int32.TryParse(UserInput, out VerkaufAnzahl))
            {
                WickleKaufAb(Händler, AusgewaehltesProdukt, VerkaufAnzahl);
                return;
            }
            
            //Breche Kauf ab
            if(UserInput == "z")
            {
                Console.WriteLine("Verkauf abgebrochen\n");
                return;
            }
        }
    }
    
    /// <summary>
    /// Passt alle Parameter an die sich durch den Kauf ändern und Speichert alle neuen Werte
    /// </summary>
    public void WickleKaufAb(Zwischenhändler Händler, int AusgewaehltesProdukt, int VerkaufAnzahl)
    {
        double Verkaufspreis;
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
            Console.WriteLine("Verkauf erfolgreich\n");
        }
    }
}