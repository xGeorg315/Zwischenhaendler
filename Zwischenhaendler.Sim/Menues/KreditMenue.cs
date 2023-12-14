namespace KreditMenueSim
{
    class KreditMenue
    {
        const double BETRAEGE = [5000, 10000, 15000];
        const double ZINSSAETZE = [3, 5, 8];

        /// <summary>
        /// Startet das Kreditsmenü
        /// </summary>
        public void MenueAufrufen(Zwischenhaendler Haendler)
        {
            while(true)
            {
                MenueAnzeigen();
                int KreditAuswahl;
                string Eingabe = Console.ReadLine()!;
                //Kehre ins Hauptmenü zurück
                if (Eingabe == "z") break;

                //Checke ob UserInput ein Int ist 
                if (Int32.TryParse(Eingabe, out KreditAuswahl))
                {
                    //Überprüfe ob Input gültig ist
                    if (KreditAuswahl <= BETRAEGE.count())
                    {
                        Haendler.Kredit.KreditAufnehmen(BETRAEGE[KreditAuswahl - 1], ZINSSAETZE[KreditAuswahl - 1]);
                    }
                }
                console.WriteLine("ungültige Eingabe");
            }
        }

        /// <summary>
        /// Zeigt die verschiedenen Kreditoptionen an
        /// </summary>
        public void MenueAnzeigen()
        {
            int i = 1;
            console.WriteLine("Wählen Sie den gewünschten Betrag aus");
            foreach (double Betrag in BETRAEGE)
            {
                string Ausgabe = "{0}) ${1} mit {2}% Zinsen";
                console.WriteLine(string.Format(Ausgabe, i, Betrag, ZINSSAETZE[i - 1]));
                i++;
            }
            console.WriteLine("z) Zurück");
        }
    }
}