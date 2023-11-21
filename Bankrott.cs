class Bankrott
{
    public static List<Zwischenhändler> AusgeschiedeneHändler = new List<Zwischenhändler>(); 
    public static List<int> ZuLöschendeHändler = new List<int>();            
    public bool ÜberprüfeBankrott(Zwischenhändler Händler, int Tag)
    {
        if (Händler.Kontostand < 0)
        {
            LeiteBankrottEin(Händler, Tag);
            return true;
        }
        return false;
    }

    public void LeiteBankrottEin (Zwischenhändler Händler, int Tag)
    {
        AusgeschiedeneHändler.Insert(0, Händler);
        ZuLöschendeHändler.Add(Händler.ID);
        Händler.TagAusscheidung = Tag;

        //Erstelle Output String
        string Ausgabe = "{0} von {1} | {2} | Lager: {3}/{4} | Tag: {5}"; 
        Console.WriteLine(string.Format(
            Ausgabe, 
            Händler.Name, 
            Händler.Firma, 
            Händler.Kontostand,
            Händler.Lager.Lagerbestand,
            Händler.Lager.MaxKapazität, 
            Tag
            ));
        Console.WriteLine("Ihr Kontostand ist unter 0 gefallen");
        Console.WriteLine("Sie sind Bankrott\n");
    }

    public void LöscheAusgeschiedeneHändler ()
    {
        Globals.Händler.RemoveAll(r => ZuLöschendeHändler.Any(ID => ID == r.ID));
        ZuLöschendeHändler.Clear();
    }
}