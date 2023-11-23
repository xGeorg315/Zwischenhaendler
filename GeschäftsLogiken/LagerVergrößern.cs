class LagerVergrößern
{
    /// <summary>
    /// Passt alle Parameter an die sich durch den Kauf ändern und Speichert alle neuen Werte
    /// </summary>
    public bool WickleKaufAb (Zwischenhändler Händler, int KaufAnzahl)
    {
        int NeuerKontostand = Händler.Kontostand - KaufAnzahl * 50;
        if (!ValidiereKauf()) return false;
        Händler.Kontostand = NeuerKontostand;
        Händler.Lager.MaxKapazität += KaufAnzahl;
        Console.WriteLine("Kauf erfolgreich");
        return true;
    }

    /// <summary>
    /// Überprüft ob die Regeln des Kaufs eingehalteb werden
    /// </summary>
    public bool ValidiereKauf(Zwischenhändler Händler, int KaufAnzahl, int NeuerKontostand)
    {
        //Überprüfe ob genug Geld im Konto des Händler ist
        if(NeuerKontostand < 0)
        {
            Console.WriteLine("Kauf fehlgeschlagen, nicht genügend Geld");
            return false;
        }

        return true;
    }
}