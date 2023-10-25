using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
class DateiLesen
{
    string GesuchteDatei = "Produkte.yaml";
    string PfadGesuchterDatei = "";
    string [] DateiInhalt;

    public void LeseProdukte()
    {   
        FindeDatei(GesuchteDatei);
        LeseDatei();
        FilterProdukte();
    }

    public void FindeDatei(string GesuchteDatei)
    {
        string? AktuellerPfad = Directory.GetCurrentDirectory();
        string [] DateiSuche;
        bool Gefunden = false;

        while(!Gefunden)
        {
             if(AktuellerPfad == null)
            {
                Console.WriteLine("Die Produkt Datei konnte nicht gefunden werden");
                break;
            }

            DateiSuche = Directory.GetFiles(AktuellerPfad);
           
            foreach(var Datei in DateiSuche)
            {
                if(Datei.Contains(GesuchteDatei))
                {
                    Gefunden = true;
                    PfadGesuchterDatei = Datei;
                }
            }
            AktuellerPfad = Path.GetDirectoryName(AktuellerPfad);
        }
    }

    public void LeseDatei()
    {
        if(!File.Exists(PfadGesuchterDatei)){
            Console.WriteLine("Beim Lesen ist etwas schief gelaufen"); 
        }
        
        DateiInhalt = File.ReadAllLines(PfadGesuchterDatei);
    }    

    public void FilterProdukte()
    {
        Produkte Produkt = new Produkte(); 

        foreach (var Zeile in DateiInhalt)
        {
            if(Zeile.Contains("- Name:"))
            {
              string name = Zeile.Replace("- Name: ", "");
              Produkt.ProduktName = name;
            }
            else if (Zeile.Contains("Haltbarkeit:"))
            {
              int IntHaltbarkeit;
              string Haltbarkeit = Zeile.Replace("Haltbarkeit: ", "");
              if (Int32.TryParse(Haltbarkeit, out IntHaltbarkeit)){
                Produkt.Haltbarkeit = IntHaltbarkeit;
              }
            }
            else if (Zeile.Contains("Basispreis:"))
            {
              int IntBasispreis;
              string Haltbarkeit = Zeile.Replace("Basispreis: ", "");
              if (Int32.TryParse(Haltbarkeit, out IntBasispreis)){
                Produkt.BasisPreis = IntBasispreis;
              }
                Globals.VerfügbareProdukte.Add(Produkt);
                Produkt = new Produkte();
            }
            else{
                Console.WriteLine("Die Formatierung der YAML Datei scheint Fehlerhaft zu sein");
            }
        }
    }
}