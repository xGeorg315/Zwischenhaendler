using Zwischenhaendler.Sim;
using GlobalsSim;
class RabattMenue
{
    /// <summary>
    /// Gibt den Rabatt aus wenn einer Vorhanden ist
    /// </summary>
    public string GebeRabattAus (Zwischenhändler Händler, int ProduktNummer)
    {
        double EinkaufsRabatt = Händler.EinkaufsRabatte[ProduktNummer];
        double EinkaufsPreis = Globals.VerfügbareProdukte[ProduktNummer].EinkaufsPreis;
        string Ausgabe = "Ihr Neuer Preis: {0}$ (Rabatt: {1}%)";
        double NeuerPreis = EinkaufsPreis - (EinkaufsPreis * (EinkaufsRabatt / 100));
        if(EinkaufsRabatt == 0) return "";
        return string.Format(Ausgabe, Math.Round(NeuerPreis,2),EinkaufsRabatt);
    }
}