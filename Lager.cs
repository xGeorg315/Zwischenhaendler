using System.Security.Cryptography.X509Certificates;

class Lager
{
    public int MaxKapazität;
    public int Lagerbestand = 0;

    public Lager(int MaxKapazität = 100)
    {
        this.MaxKapazität = MaxKapazität;
    }

    /// <summary>
    /// Addiert auf den aktuellen Bestand, den mitgegebenen Wert
    /// </summary>
    public bool AddiereBestand (int Lagerbestand)
    {
        if(this.Lagerbestand + Lagerbestand <= MaxKapazität)
        {
            this.Lagerbestand += Lagerbestand;
            return true;
        }
        Console.WriteLine("Der Kauf würde die maximale Lagerkapazizät überschreiten");
        return false;
    }

    /// <summary>
    /// Subtrahiert den aktuellen Bestand mit den mitgegebenen Wert
    /// </summary>
    public void SubtrahiereBestand (int Lagerbestand)
    {
        this.Lagerbestand -= Lagerbestand;
    }
}
