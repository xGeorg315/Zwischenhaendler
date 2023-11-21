class Einkaufen
{
    
    /// <summary>
    /// Überprügt ob der Kauf allen Regeln entspricht
    /// </summary>
    public bool ValidiereKauf (int KaufAnzahl, Zwischenhändler Händler, Produkte AusgewaehltesProdukt, int NeuerKontostand)
    {
        //Checke ob genug Platz in Lager
        if(KaufAnzahl > Händler.Lager.FreierPlatz())
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
        Globals.VerfügbareProdukte[ProduktNummer - 1].Menge -= KaufAnzahl;
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