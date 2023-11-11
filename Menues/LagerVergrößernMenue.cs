class LagerVergrößernMenue
{
    /// <summary>
    /// Ruft das Menue auf
    /// </summary>
    public void MenueAufrufen(Zwischenhändler Händler)
    {
        while(true)
        {
            MenueAnzeigen();
            string UserInput = Console.ReadLine()!;

            if (UserInput == "v")
            {
                Console.WriteLine("Um wie viele Einheiten soll Ihr Lager vergrößert werden?");
                MenueLogik(Händler);
            }
            else if (UserInput == "z")
            {
                break;
            }

        }
    }
    /// <summary>
    /// Printet das Menue
    /// </summary>
    public void MenueAnzeigen()
    {
        Console.WriteLine("Sie können ihr Lager für 50$ pro Einheit vergrößern");
        Console.WriteLine("v) vergrößern");
        Console.WriteLine("z) zurück");
    }

    /// <summary>
    /// Handled die Logik des Menues
    /// </summary>
    public void MenueLogik(Zwischenhändler Händler)
    {
        //Warte auf gültigen Input 
        while(true)
        {
            string UserInput = Console.ReadLine()!;
            int KaufAnzahl;
            //Checke ob UserInput ein Int ist 
            if (Int32.TryParse(UserInput, out KaufAnzahl))
            {
                if(WickleKaufAb(Händler, KaufAnzahl)) break;
            }
            //Breche Kauf ab 
            if(UserInput == "z")
            {
                Console.WriteLine("Kauf abgebrochen");
                return;
            }
            Console.WriteLine("Bitte geben Sie eine Zahl an oder brechen Sie den Kauf mit \"z\" ab");
        }
    }

    /// <summary>
    /// Passt alle Parameter an die sich durch den Kauf ändern und Speichert alle neuen Werte
    /// </summary>
    public bool WickleKaufAb (Zwischenhändler Händler, int KaufAnzahl)
    {
        int NeuerKontostand = Händler.Kontostand - KaufAnzahl * 50;
        if(NeuerKontostand < 0)
        {
            Console.WriteLine("Kauf fehlgeschlagen, nicht genügend Geld");
            return false;
        }
        Händler.Kontostand = NeuerKontostand;
        Händler.Lager.MaxKapazität += KaufAnzahl;
        Console.WriteLine("Kauf erfolgreich");
        return true;
    }
}