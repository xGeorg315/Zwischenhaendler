using System;

class Simulation
{   
    int AnzahlZwischenhändler = 0;

    public void AnzahlHändler()
    {
        Console.WriteLine("Wieviele Zwischenhändler nehmen teil?");   
        while(true)                                                                         //Wartet bis eine Zahl eingegeben wurde
        {                                                                        
            string UserInput = Console.ReadLine();       
            if (Int32.TryParse(UserInput, out AnzahlZwischenhändler)) break;                //Check ob UserInput eine Zahl ist 
            Console.WriteLine("Sieht so aus als wäre das keine Nummer gewesen");
            Console.WriteLine("Erneut Versuchen:");
        }
    }

    public void HändlerErstellen()
    {
        for(int i = 1; i <= AnzahlZwischenhändler; i++)                                      //Erstelle x Zwischenhändler
        {                                   
            Zwischenhändler Zwischenhändler = new Zwischenhändler();

            string Ausgabe = "Name von Zwischenhändler " + i;                                //Frage Name des Händlers ab
            Console.WriteLine(Ausgabe);
            string Name = Console.ReadLine();
            Zwischenhändler.Name = Name;                                                               //Speichere Name in den Händler

            Ausgabe = "Firma von " + Name;                                                   //Frage Firma des Händlers ab
            Console.WriteLine(Ausgabe);
            Name = Console.ReadLine();   
            Zwischenhändler.Firma = Name;                                                              //Speichere Firma in den Händler

            Globals.Händler.Add(Zwischenhändler);                                                      //Speichere die Händler in eine Globale var
        }
    }

    public void StarteSimulation()
    {
        int Tag = 1;
        while (true)
        {                                                                                                            
            foreach (Zwischenhändler Händler in Globals.Händler)                            //Endlosschleife für die Simulation 
            {                               
                string Output = Händler.Name + " von " + Händler.Firma + " | Tag " + Tag;   //Erstelle Output String
                Console.WriteLine(Output);
                CheckChoices();                                                             //Checke die Möglichkeiten des Händlers
            }
            Tag++;
        }
    }

    void CheckChoices()
    {
        Console.WriteLine("b) Runde beenden");   
        while (true)
        {                                                                       //Warte auf gültigen Input
            string input = Console.ReadLine();
            if (input == "b") break;
            Console.WriteLine("Ungültige Eingabe :-(");
            Console.WriteLine("Probiere es nochmal:");
        }
    }

}