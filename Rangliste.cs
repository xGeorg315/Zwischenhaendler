class Rangliste
{
    public int TeilnehmerAnzahl = 0;

    /// <summary>
    /// Bubble Sort um Rangfolge zu ermitteln
    /// </summary>
    public void ErmittleRangfolge(List<Zwischenhändler> Händler)
    {
        Zwischenhändler Zwischenspeicher = new Zwischenhändler();

        for (int i = 0; i < Händler.Count(); i++)
        {
            for (int j = 0; j < Händler.Count() - 1; j++)
            {
                if (Händler[j].Kontostand < Händler[j + 1].Kontostand)
                {
                    Zwischenspeicher = Händler[j + 1];
                    Händler[j + 1] = Händler[j];
                    Händler[j] = Zwischenspeicher;
                }
            }
        }
    }

    /// <summary>
    /// Printe die Rangliste
    /// Priorität: 1.Kontostand 2. Tag der Ausscheidung
    /// </summary>
    public void ZeigeRangliste(Bankrott Bankrott)
    {
        int Platzierung = 1;
        string Ausgabe;
        foreach(Zwischenhändler Händler in Globals.Händler)
        {
            Ausgabe = "{0}) | {1} von {2} mit  {3}$";
            Console.WriteLine(string.Format(Ausgabe, Platzierung, Händler.Name, Händler.Firma, Händler.Kontostand));
            Platzierung++;
        }

        foreach(Zwischenhändler Händler in Bankrott.AusgeschiedeneHändler)
        {
            Ausgabe = "{0}) | {1} von {2} mit  {3}$ | Bankrott am {4} Tag";
            Console.WriteLine(string.Format(Ausgabe, Platzierung, Händler.Name, Händler.Firma, Händler.Kontostand, Händler.TagAusscheidung));
            Platzierung++;
        }
    }
}