class VerkaufsMenue:Menue
{
    /// <summary>
    /// Starte das Verkaufsmenü
    /// </summary>
    public void MenueAufrufen(Zwischenhändler Händler)
    {
        int AusgewaehltesProdukt;
        //Checke ob Produkte zum Verkauf sind 
        if(Händler.GekaufteProdukte.Count() == 0)
        {
            Console.WriteLine("Keine Produkte gekauft");
            return;
        }

        //Warte auf gültigen Input
        while(true)
        {
            MenueAnzeigen(Händler);
            string UserInput = Console.ReadLine()!;
            //Checke ob User Input ein Int ist
            if (Int32.TryParse(UserInput, out AusgewaehltesProdukt))
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
                    //Schließe Kauf ab
                    MenueLogik(Händler, AusgewaehltesProdukt);
                }   
            }
            
            if(UserInput == "z")
            {
                Console.WriteLine("Verkauf abgebrochen");
                return;
            }
        }
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
                Produkt.BasisPreis * 0.8));
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
            double Verkaufspreis;

            //Checke ob UserInput ein Int ist
            if (Int32.TryParse(UserInput, out VerkaufAnzahl))
            {
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
                    Console.WriteLine("Verkauf erfolgreich");
                    return;
                }
            }
            //Breche Kauf ab
            if(UserInput == "z")
            {
                Console.WriteLine("Verkauf abgebrochen");
                return;
            }
        }
    }
}