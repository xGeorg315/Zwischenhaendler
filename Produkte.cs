class Produkte : ICloneable
{
    public string ProduktName = "";
    public int Haltbarkeit = 0;
    public int BasisPreis = 0;
    public int Menge = 0;
    
    /// <summary>
    /// Klone das Objekt 
    /// </summary>
    public object Clone()
    {
        return this.MemberwiseClone();
    }

    /// <summary>
    /// Liste alle Verfügbaren Produkte auf
    /// </summary>
    public void ListeProdukte()
    {
        int i = 1;

        Console.WriteLine("Verfügbare Produkte:");
        foreach(Produkte Produkt in Globals.VerfügbareProdukte)
        {
            String Ausgabe = "{0}) {1} ({2} Tage) ${3}/Stück";
            Console.WriteLine(string.Format(Ausgabe, i, Produkt.ProduktName, Produkt.Haltbarkeit, Produkt.BasisPreis));
            i++;
        }
    }
}