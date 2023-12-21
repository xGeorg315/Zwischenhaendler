using Zwischenhaendler.Sim;
using LagerVergrößernSim;
public class LagerVergrößernMenue
{
    /// <summary>
    /// Ruft das Menue auf
    /// </summary>
    public void MenueAufrufen(Zwischenhändler Händler)
    {
        while(true)
        {
            MenueAnzeigen();
            string UserInput = Console.ReadLine()!;

            if (UserInput == "v")
            {
                Console.WriteLine("Um wie viele Einheiten soll Ihr Lager vergrößert werden?");
                MenueWeiterleiten(Händler);
            }
            else if (UserInput == "z")
            {
                break;
            }

        }
    }
    /// <summary>
    /// Printet das Menue
    /// </summary>
    public void MenueAnzeigen()
    {
        Console.WriteLine("Sie können ihr Lager für 50$ pro Einheit vergrößern");
        Console.WriteLine("v) vergrößern");
        Console.WriteLine("z) zurück");
    }

    /// <summary>
    /// Handled die Logik des Menues
    /// </summary>
    public void MenueWeiterleiten(Zwischenhändler Händler)
    {
        //Warte auf gültigen Input 
        while(true)
        {
            string UserInput = Console.ReadLine()!;
            int KaufAnzahl;
            //Checke ob UserInput ein Int ist 
            if (Int32.TryParse(UserInput, out KaufAnzahl))
            {
                if(KaufprozessEinleiten(Händler, KaufAnzahl)) break;
            }
            //Breche Kauf ab 
            if(UserInput == "z")
            {
                Console.WriteLine("Kauf abgebrochen");
                return;
            }
            Console.WriteLine("Bitte geben Sie eine Zahl an oder brechen Sie den Kauf mit \"z\" ab");
        }
    }

    /// <summary>
    /// Leite den Kaufprozess ein und gebe auftretende exceptions aus
    /// </summary>
    public bool KaufprozessEinleiten(Zwischenhändler Händler, int KaufAnzahl)
    {
        try
        {
            LagerVergrößern lagerVergrößern = new LagerVergrößern();
            if(!lagerVergrößern.WickleKaufAb(Händler, KaufAnzahl)) return false;
            Console.WriteLine("Vergrößerung erfolgreich\n");
            return true;  
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }  
    }

}