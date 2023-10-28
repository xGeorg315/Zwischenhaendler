using System.Security.Cryptography;

class Produkte : ICloneable
{
    public string ProduktName = "";
    public int Haltbarkeit = 0;
    public int BasisPreis = 0;
    public int Menge = 0;
    public int MinProduktionsRate = 0;
    public int MaxProduktionsRate = 0;
    public int MaxMenge = 0;
    
    /// <summary>
    /// Klone das Objekt 
    /// </summary>
    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public void BerechneMenge()
    {
        Random Random = new Random();
        int ProduktionsRate = Random.Next(MinProduktionsRate,MaxProduktionsRate);
        Menge += ProduktionsRate;

        if(Menge < 0) Menge = 0;
        if(Menge > MaxMenge) Menge = MaxMenge;
    }
    public void BerechneMaxMenge()
    {
        MaxMenge = Haltbarkeit * MaxProduktionsRate;
    }

    public void SubtrahiereMenge(int Betrag)
    {
        Menge -= Betrag;
    }
}

class ProduktBerechnungen
{ 
    public void InitiereProdukte()
    {
        foreach(Produkte Produkt in Globals.VerfügbareProdukte)
        {
            Produkt.BerechneMaxMenge();
            Produkt.BerechneMenge();
        }
    }

    public void BerechneMenge ()
    {
        foreach(Produkte Produkt in Globals.VerfügbareProdukte)
        {
            Produkt.BerechneMenge();
        }
    }
}