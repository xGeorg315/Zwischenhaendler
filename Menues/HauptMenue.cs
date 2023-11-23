class HauptMenue
{
    EinkaufsMenue EinkaufsMenue = new EinkaufsMenue();
    VerkaufsMenue VerkaufsMenue = new VerkaufsMenue();
    LagerVergrößernMenue LagerVergrößernMenue = new LagerVergrößernMenue();


    /// <summary>
    /// Starte Hauptmenü
    /// </summary>
    public void MenueAufrufen(Zwischenhändler Händler, int AktuellerTag)
    {
        //Warte auf gültigen Input
        while (true)
        {    
            MenueAnzeigen(Händler,AktuellerTag);                                                                                          
            string Eingabe = Console.ReadLine()!;
            int MenueStatus = MenueLogik(Eingabe, Händler);
            if(MenueStatus == 0)
            {
                break;
            }
            else if (MenueStatus == 2) 
            {
                Console.WriteLine("Ungültige Eingabe :-(");
                Console.WriteLine("Probiere es nochmal:\n");
            } 
        }
    }

    /// <summary>
    /// Leite je nach Input in die verschiedenen Menues
    /// </summary>
    public int MenueLogik(string Eingabe, Zwischenhändler Händler)
    {
        if (Eingabe == "b")
        {
            return 0;
        } 
        else if (Eingabe == "e") 
        {
            EinkaufsMenue.MenueAufrufen(Händler);
            return 1;
        }
        else if(Eingabe == "v")
        {
            VerkaufsMenue.MenueAufrufen(Händler);
            return 1;
        }
        else if(Eingabe == "l")
        {
            LagerVergrößernMenue.MenueAufrufen(Händler);
            return 1;
        }
        return 2;
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
        Console.WriteLine("l) Lager Vergrößern"); 
        Console.WriteLine("b) Runde beenden"); 
    }
}