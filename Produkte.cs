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
}
