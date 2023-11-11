using System.Security.Cryptography;

class Produkte : ICloneable
{
    public string ProduktName = "";
    public int Haltbarkeit = 0;
    public double BasisPreis = 0;
    public int Menge = 0;
    public int MinProduktionsRate = 0;
    public int MaxProduktionsRate = 0;
    public int MaxMenge = 0;
    public double EinkaufsPreis = 0;
    
    /// <summary>
    /// Klone das Objekt 
    /// </summary>
    public object Clone()
    {
        return this.MemberwiseClone();
    }

    /// <summary>
    /// Berechne die Menge für den Tag anhand der Max und Min Produktionsrate sowie der Aktuellen Menge
    /// </summary>
    public void BerechneMenge()
    {
        Random Random = new Random();
        int ProduktionsRate = Random.Next(MinProduktionsRate,MaxProduktionsRate);
        Menge += ProduktionsRate;

        if(Menge < 0) Menge = 0;
        if(Menge > MaxMenge) Menge = MaxMenge;
    }
    
    /// <summary>
    /// Berechne die Maximale Menge eines Produktes
    /// </summary>
    public void BerechneMaxMenge()
    {
        MaxMenge = Haltbarkeit * MaxProduktionsRate;
    }

    /// <summary>
    /// Entferne einen Bestimmten Betrag aus der Verfügbaren Menge
    /// </summary>
    public void SubtrahiereMenge(int Betrag)
    {
        Menge -= Betrag;
    }
}

class ProduktBerechnungen
{
    /// <summary>
    /// Initiert den Ausgangsstand der Produkte
    /// </summary> 
    public void InitiereProdukte()
    {
        foreach(Produkte Produkt in Globals.VerfügbareProdukte)
        {
            Produkt.BerechneMaxMenge();
            Produkt.BerechneMenge();
            Produkt.EinkaufsPreis = Produkt.BasisPreis;
        }
    }

    /// <summary>
    /// Iteriert durch jedes Produkt und berechnet seine Menge
    /// </summary> 
    public void BerechneMenge ()
    {
        foreach(Produkte Produkt in Globals.VerfügbareProdukte)
        {
            Produkt.BerechneMenge();
        }
    }

    /// <summary>
    /// Berechnet den neuen Einkaufspreis 
    /// </summary> 
    public void BerechneEinkaufsPreis()
    {
        Random Random = new Random();
        foreach(Produkte Produkt in Globals.VerfügbareProdukte)
        {   
            float VerfügbarZuMaxVerfügbarkeit = Produkt.Menge / Produkt.MaxMenge;

            if(VerfügbarZuMaxVerfügbarkeit < 0.25)
            {
                Produkt.EinkaufsPreis = Produkt.EinkaufsPreis * (Random.Next(-10,30) / 100.0) + Produkt.EinkaufsPreis;
            }
            else if(VerfügbarZuMaxVerfügbarkeit > 0.25 && VerfügbarZuMaxVerfügbarkeit < 0.8)
            {
                Produkt.EinkaufsPreis = Produkt.EinkaufsPreis * (Random.Next(-5,5) / 100.0) + Produkt.EinkaufsPreis;
            }
            else
            {
                Produkt.EinkaufsPreis = Produkt.EinkaufsPreis * (Random.Next(-10,6) / 100.0) + Produkt.EinkaufsPreis;
            }

        }
        CheckeMinMaxEinkaufspreis();
    }

    /// <summary>
    /// Checkt ob der Preis im gültigen Bereich liegen 
    /// </summary> 
    public void CheckeMinMaxEinkaufspreis ()
    {
            foreach(Produkte Produkt in Globals.VerfügbareProdukte)
            {
                double MaxPreis = Produkt.BasisPreis * 3;
                double MinPreis = Produkt.BasisPreis * 0.25;

                if(Produkt.EinkaufsPreis > MaxPreis) Produkt.EinkaufsPreis = MaxPreis;
                if(Produkt.EinkaufsPreis < MinPreis) Produkt.EinkaufsPreis = MinPreis;
            }
    }
}