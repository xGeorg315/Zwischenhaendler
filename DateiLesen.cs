using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
class DateiLesen
{
    string GesuchteDatei = "Produkte.yaml";
    string PfadGesuchterDatei = "";
    string [] DateiInhalt;

    /// <summary>
    /// Starte Lese Zyklus
    /// </summary>
    public void LeseProdukte()
    {   
        FindeDatei(GesuchteDatei);
        LeseDatei();
        FilterProdukte();
    }

    /// <summary>
    /// Suche die Datei
    /// Funktion such im Strang alles ab, geht jedoch nicht andere Ordner/Stränge rein
    /// </summary>
    public void FindeDatei(string GesuchteDatei)
    {
        string? AktuellerPfad = Directory.GetCurrentDirectory();
        string [] DateiSuche;
        bool Gefunden = false;

        //Warte bis die Datei gefunden wurde
        while(!Gefunden)
        {   
            //Wenn der Pfad zuende ist breche ab
             if(AktuellerPfad == null)
            {
                Console.WriteLine("Die Produkt Datei konnte nicht gefunden werden");
                break;
            }
            //Liste die Dateien im aktuellen Pfad
            DateiSuche = Directory.GetFiles(AktuellerPfad);
            // Checke ob gefundene Datei darunter ist
            foreach(var Datei in DateiSuche)
            {
                //Speichere Pfad der Gesuchten datei wenn Datei vorhanden
                if(Datei.Contains(GesuchteDatei))
                {
                    Gefunden = true;
                    PfadGesuchterDatei = Datei;
                }
            }
            //Gehe einen Ordner zurück
            AktuellerPfad = Path.GetDirectoryName(AktuellerPfad);
        }
    }
    /// <summary>
    /// Lese die Datei aus
    /// </summary>
    public void LeseDatei()
    {
        //Checke ob Datei noch existiert
        if(!File.Exists(PfadGesuchterDatei)){
            Console.WriteLine("Beim Lesen ist etwas schief gelaufen"); 
            return;
        }
        //Lese Datei Zeile für Zeile aus
        DateiInhalt = File.ReadAllLines(PfadGesuchterDatei);
    } 
       
    /// <summary>
    /// Speichere die einzelnen Produkte in getrennte Objekte
    /// Eine saubere und einheitliche Struktur sowie Formatierung der Yaml Datei ist dafür notwendig
    /// </summary>
    public void FilterProdukte()
    {
        Produkte Produkt = new Produkte(); 
        //Gehe alle Zeilen durch
        foreach (var Zeile in DateiInhalt)
        {
            //Speichere die einzelenen Attribute in das angelegte Objekt
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
                //Speichere das Produkt in die Globale var und lege ein neues Produkt an
                Globals.VerfügbareProdukte.Add(Produkt);
                Produkt = new Produkte();
            }
            else{
                Console.WriteLine("Die Formatierung der YAML Datei scheint Fehlerhaft zu sein");
            }
        }
    }
}