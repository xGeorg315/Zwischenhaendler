using System.Runtime.CompilerServices;
using Zwischenhaendler.Sim;
using ProdukteSim;
using GlobalsSim;

public class Rabatt
{
    public int RabattProzent = 0;

    /// <summary>
    /// Berechnet ob dem Händler einen Rabatt zusteht
    /// </summary>
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

    /// <summary>
    /// Zählt wv Produkte der Händler eines bestimmten Produktes gekauft hat
    /// </summary>
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

    /// <summary>
    /// Aktualisiert den Rabatt des Händlers
    /// </summary>
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
}