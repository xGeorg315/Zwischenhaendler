using Zwischenhaendler.Sim;
namespace LagerVergrößernSim
{
    public class LagerVergrößern
    {
        /// <summary>
        /// Passt alle Parameter an die sich durch den Kauf ändern und Speichert alle neuen Werte
        /// </summary>
        public bool WickleKaufAb (Zwischenhändler Händler, int KaufAnzahl)
        {
            double NeuerKontostand = Händler.Kontostand - KaufAnzahl * 50;
            if (!ValidiereKauf(Händler, KaufAnzahl, NeuerKontostand)) return false;
            Händler.Kontostand = NeuerKontostand;
            Händler.Lager.MaxKapazität += KaufAnzahl;
            return true;
        }

        /// <summary>
        /// Überprüft ob die Regeln des Kaufs eingehalteb werden
        /// </summary>
        public bool ValidiereKauf(Zwischenhändler Händler, int KaufAnzahl, double NeuerKontostand)
        {
            //Überprüfe ob genug Geld im Konto des Händler ist
            if(NeuerKontostand < 0)
            {
                throw new ArgumentException("Nicht genügend Geld");
            }

            return true;
        }
    }
}