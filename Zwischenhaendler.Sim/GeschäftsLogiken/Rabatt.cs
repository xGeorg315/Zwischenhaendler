using System.Runtime.CompilerServices;
using Zwischenhaendler.Sim;
using ProdukteSim;
using GlobalsSim;

public class Rabatt
{
    public int RabattProzent = 0;
    public void BerechneRabatt(Zwischenhändler Händler)
    {
        int Menge = 0;
        int index = 0;
        foreach(Produkte Produkt in Globals.VerfügbareProdukte)
        {
            Menge = ZähleProdukt(Händler, Produkt);
            RabattAktualisieren(Menge);
            Händler.EinkaufsRabatte[index] = RabattProzent;
            index++;
        }
    }

    public int ZähleProdukt(Zwischenhändler Händler, Produkte ZuZählendesProdukt)
    {
        int Menge = 0;
        foreach (Produkte Produkt in Händler.GekaufteProdukte)
        {
            if(Produkt.ProduktName == ZuZählendesProdukt.ProduktName)
            {
                Menge += Produkt.Menge;
            }
        }
        return Menge;
    }

    public void RabattAktualisieren (int Menge)
    {
        switch (Menge)
        {
            case <= 24: 
                RabattProzent = 0;
                break;
            case <= 50: 
                RabattProzent = 2;
                break;
            case <= 74: 
                RabattProzent = 5;
                break;
            case > 75: 
                RabattProzent = 10;
                break;
            default:
                break;
        }
    }

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