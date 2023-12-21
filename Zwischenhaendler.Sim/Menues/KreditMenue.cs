using Zwischenhaendler.Sim;
namespace KreditMenueSim
{
    class KreditMenue
    {
        readonly double[] BETRAEGE = {5000, 10000, 15000};
        readonly double[] ZINSSAETZE = {3, 5, 8};

        /// <summary>
        /// Startet das Kreditsmenü
        /// </summary>
        public void MenueAufrufen(Zwischenhändler Haendler)
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
                    StarteKreditAufnahme(Haendler, KreditAuswahl);
                    return;
                }
                Console.WriteLine("ungültige Eingabe");
            }
        }

        public void StarteKreditAufnahme (Zwischenhändler Haendler, int KreditAuswahl)
        {
            //Überprüfe ob Input gültig ist
            if (KreditAuswahl <= BETRAEGE.Count())
            {
                if(Haendler.Kredit.KreditAufnehmen(Haendler, BETRAEGE[KreditAuswahl - 1], ZINSSAETZE[KreditAuswahl - 1]))
                {           
                    Console.WriteLine("Kredit aufgenommen");
                    return;
                }
                Console.WriteLine("Nicht Kreditwürdig");
                return;
            }
        }

        /// <summary>
        /// Zeigt die verschiedenen Kreditoptionen an
        /// </summary>
        public void MenueAnzeigen()
        {
            int i = 1;
            Console.WriteLine("Wählen Sie den gewünschten Betrag aus");
            foreach (double Betrag in BETRAEGE)
            {
                string Ausgabe = "{0}) ${1} mit {2}% Zinsen";
                Console.WriteLine(string.Format(Ausgabe, i, Betrag, ZINSSAETZE[i - 1]));
                i++;
            }
            Console.WriteLine("z) Zurück");
        }
    }
}