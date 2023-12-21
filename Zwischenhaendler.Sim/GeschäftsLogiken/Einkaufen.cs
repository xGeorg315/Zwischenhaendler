using Zwischenhaendler.Sim;
using GlobalsSim;
using ProdukteSim;

namespace Einkaufen.Sim
{
    public class EinkaufenKlasse
    {
        
        /// <summary>
        /// Überprügt ob der Kauf allen Regeln entspricht
        /// </summary>
        public bool ValidiereKauf (int KaufAnzahl, Zwischenhändler Händler, Produkte AusgewaehltesProdukt, double NeuerKontostand)
        {
            //Checke ob genug Platz in Lager
            if(KaufAnzahl > Händler.Lager.BerechneFreienPlatz())
            {
                throw new ArgumentException("Fehler beim Kauf, nicht genügend Platz im Lager\n");
            }
            //Checke ob genug Produkte Verfügbar sind
            if(KaufAnzahl > AusgewaehltesProdukt.Menge)
            { 
                string Ausgabe = "Fehler beim Kauf, nicht genügend {0} Verfügbar\n";
                throw new ArgumentException(string.Format(Ausgabe, AusgewaehltesProdukt.ProduktName));
            }
            //Checke ob Genug Geld vorhanden
            if (NeuerKontostand < 0)
            {
                throw new ArgumentException("Fehler beim Kauf, nicht genug Geld\n");
            }
                return true;
        }

        /// <summary>
        /// Passt alle Parameter an die sich durch den Kauf ändern und Speichert alle neuen Werte
        /// </summary>
        public void WickleKaufAb (Zwischenhändler Händler, double NeuerKontostand, Produkte AusgewaehltesProdukt, int KaufAnzahl, int ProduktNummer)
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
        }

        /// <summary>
        /// Leitet den Kaufprozess nach der Eingabe des Händlers ein
        /// </summary>
        public bool BeginneKaufProzess(Zwischenhändler Händler, Produkte AusgewaehltesProdukt, int KaufAnzahl, int ProduktNummer)
        {
            //Berechne den Einkaufspreis und beziehe dabei mögliche Rabatte mit ein
            double Einkaufspreis = AusgewaehltesProdukt.EinkaufsPreis  - (AusgewaehltesProdukt.EinkaufsPreis * (Händler.EinkaufsRabatte[ProduktNummer - 1]/100));
            //Berechne Vorraussichtlichen Kontostand
            double NeuerKontostand = Händler.Kontostand - (Math.Round(Einkaufspreis, 2) * KaufAnzahl);
            //Checke ob der Kauf den Regeln und Limits entspricht
            if(!ValidiereKauf(KaufAnzahl, Händler, AusgewaehltesProdukt, NeuerKontostand)) return false;
            //Wickle den Kauf ab
            WickleKaufAb(Händler, NeuerKontostand, AusgewaehltesProdukt, KaufAnzahl, ProduktNummer);
            //Füge die Ausgaben zu den Tagesausgaben hinzu
            Händler.Tagesbericht.AddiereAusgaben(Einkaufspreis * KaufAnzahl);
            return true;
        }
    }
}