class Verkaufen
{    
    /// <summary>
    /// Passt alle Parameter an die sich durch den Kauf ändern und Speichert alle neuen Werte
    /// </summary>
    public void WickleVerkaufAb(Zwischenhändler Händler, int AusgewaehltesProdukt, int VerkaufAnzahl)
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

    /// <summary>
    /// Beginnt nach der Überprüfung der Verkaufsprozess
    /// </summary>
    public void BeginneVerkaufProzess(Zwischenhändler Händler, int AusgewaehltesProdukt, int VerkaufAnzahl)
    {
        if(!ÜberprüfeVerkaufsRegeln(Händler,AusgewaehltesProdukt,VerkaufAnzahl)) return;
        WickleVerkaufAb(Händler, AusgewaehltesProdukt, VerkaufAnzahl);
    }

    public bool ÜberprüfeVerkaufsRegeln(Zwischenhändler Händler, int AusgewaehltesProdukt, int VerkaufAnzahl)
    {   
        if(VerkaufAnzahl > Händler.GekaufteProdukte[AusgewaehltesProdukt-1].Menge)
        {
            Console.WriteLine("Nicht genug Produkte zum Verkauf");
            return false;
        } 

        return true;
    }

}