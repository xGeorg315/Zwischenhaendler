using Zwischenhaendler.Sim;

namespace VerkaufsSim
{
    public class Verkaufen
    {    
        /// <summary>
        /// Passt alle Parameter an die sich durch den Kauf ändern und Speichert alle neuen Werte
        /// </summary>
        public void WickleVerkaufAb(Zwischenhändler Händler, int AusgewaehltesProdukt, int VerkaufAnzahl)
        {
            double Verkaufspreis;
                //Berechne Verkaufspreis und Buche auf den Kontostand 
                Verkaufspreis = Händler.GekaufteProdukte[AusgewaehltesProdukt - 1].EinkaufsPreis * 0.8 * VerkaufAnzahl; 
                Händler.Kontostand += Verkaufspreis;
                //Berechne die übrige Menge
                Händler.GekaufteProdukte[AusgewaehltesProdukt - 1].Menge -= VerkaufAnzahl;
                //Füge die Einnahmen hinzu
                Händler.Tagesbericht.AddiereEinnahmen(Verkaufspreis);
                Händler.Lager.SubtrahiereBestand(VerkaufAnzahl);
                //Wenn die Menge auf Null fällt, lösche das Produkt
                if(Händler.GekaufteProdukte[AusgewaehltesProdukt - 1].Menge == 0)
                {
                    Händler.GekaufteProdukte.RemoveAt(AusgewaehltesProdukt - 1);
                }
        }

        /// <summary>
        /// Beginnt nach der Überprüfung der Verkaufsprozess
        /// </summary>
        public bool BeginneVerkaufProzess(Zwischenhändler Händler, int AusgewaehltesProdukt, int VerkaufAnzahl)
        {
            if(!ÜberprüfeVerkaufsRegeln(Händler,AusgewaehltesProdukt,VerkaufAnzahl)) return false;
            WickleVerkaufAb(Händler, AusgewaehltesProdukt, VerkaufAnzahl);
            return true;
        }

        public bool ÜberprüfeVerkaufsRegeln(Zwischenhändler Händler, int AusgewaehltesProdukt, int VerkaufAnzahl)
        {   
            if (VerkaufAnzahl < 1)
            {
                throw new ArgumentException("Gültige Verkaufsmenge auswählen");
            }
            if(AusgewaehltesProdukt > Händler.GekaufteProdukte.Count())
            {
                throw new ArgumentException("Ungültige Produktnummer");
            }
            if(VerkaufAnzahl > Händler.GekaufteProdukte[AusgewaehltesProdukt-1].Menge)
            {
                throw new ArgumentException("Nicht genügend Produkte");
            } 
            return true;
        }

    }
}