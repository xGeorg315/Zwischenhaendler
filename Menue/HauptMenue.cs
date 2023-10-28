class HauptMenue
{
    EinkaufsMenue EinkaufsMenue = new EinkaufsMenue();
    VerkaufsMenue VerkaufsMenue = new VerkaufsMenue();

    /// <summary>
    /// Starte Hauptmenü
    /// </summary>
    public void MenueAurufen(Zwischenhändler Händler, int AktuellerTag)
    {
        //Warte auf gültigen Input
        while (true)
        {    
            //Leite je nach Input Weiter oder beende die Runde
            MenueAnzeigen(Händler,AktuellerTag);                                                                                          
            string input = Console.ReadLine()!;
            if (input == "b")
            {
                break;
            } 
            else if (input == "e") 
            {
                EinkaufsMenue.MenueAufrufen(Händler);
            }
            else if(input == "v")
            {
                VerkaufsMenue.MenueAufrufen(Händler);
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe :-(");
                Console.WriteLine("Probiere es nochmal:");
            } 
        }
    }
    
    /// <summary>
    /// Printe das Hauptmenü 
    /// </summary>
    public void MenueAnzeigen(Zwischenhändler Händler, int AktuellerTag)
    {
        //Erstelle Output String
        string Ausgabe = "{0} von {1} | {2} | Lager: {3}/{4} | Tag: {5}";                                       
        Console.WriteLine(string.Format(
            Ausgabe, 
            Händler.Name, 
            Händler.Firma, 
            Händler.Kontostand,
            Händler.Lager.Lagerbestand,
            Händler.Lager.MaxKapazität, 
            AktuellerTag
            ));
        Console.WriteLine("e) Einkaufen");  
        Console.WriteLine("v) Verkaufen"); 
        Console.WriteLine("b) Runde beenden"); 
    }
}