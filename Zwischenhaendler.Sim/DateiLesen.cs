using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using GlobalsSim;
using ProdukteSim;

namespace DateiLesen.Sim
{
  public class DateiLesenKlasse
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
      DateiInhaltAuswerten();
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
    public void DateiInhaltAuswerten()
    {
      Produkte NeuesProdukt = new Produkte(); 
      //Gehe alle Zeilen durch
      foreach (var Zeile in DateiInhalt)
      {
        SpeichereWennName(Zeile, NeuesProdukt);
        SpeichereWennMinProduktionsRate(Zeile, NeuesProdukt);
        SpeichereWennMaxProduktionsRate(Zeile, NeuesProdukt);
        SpeichereWennHaltbarkeit(Zeile, NeuesProdukt);
        if (SpeichereWennBasispreis(Zeile, NeuesProdukt))
        {
          //Speichere das Produkt in die Globale var und lege ein neues Produkt an
          Globals.VerfügbareProdukte.Add(NeuesProdukt);
          NeuesProdukt = new Produkte();
        }
      }
    }

    /// <summary>
    /// Überprüft ob die jeweilige Zeile den Namen des Produktes enthält und speichert diesen ggfs
    /// </summary>
    public void SpeichereWennName(string Zeile, Produkte NeuesProdukt)
    {
      //Speichere die einzelenen Attribute in das angelegte Objekt
      if(Zeile.Contains("- Name:"))
      {
        string name = Zeile.Replace("- Name: ", "");
        NeuesProdukt.ProduktName = name;
      }
    }

    /// <summary>
    /// Überprüft ob die jeweilige Zeile den Namen die Haltbarkeit enthält und speichert diesen ggfs
    /// </summary>
    public void SpeichereWennHaltbarkeit(string Zeile, Produkte NeuesProdukt)
    {
      if (Zeile.Contains("Haltbarkeit:"))
      {
        int IntHaltbarkeit;
        string Haltbarkeit = Zeile.Replace("Haltbarkeit: ", "");
        if (Int32.TryParse(Haltbarkeit, out IntHaltbarkeit)){
          NeuesProdukt.Haltbarkeit = IntHaltbarkeit;
        }
      }    
    }
    
    /// <summary>
    /// Überprüft ob die jeweilige Zeile die MinProduktionsRate des Produktes enthält und speichert diesen ggfs
    /// </summary>
    public void SpeichereWennMinProduktionsRate(string Zeile, Produkte NeuesProdukt)
    {
      if (Zeile.Contains("MinProduktionsRate:"))
      {
        int IntProduktionsRate;
        string ProduktionsRate = Zeile.Replace("MinProduktionsRate: ", "");
        if (Int32.TryParse(ProduktionsRate, out IntProduktionsRate)){
          NeuesProdukt.MinProduktionsRate = IntProduktionsRate;
        }
      }    
    }

    /// <summary>
    /// Überprüft ob die jeweilige Zeile die MaxProduktionsRate des Produktes enthält und speichert diesen ggfs
    /// </summary>
    public void SpeichereWennMaxProduktionsRate(string Zeile, Produkte NeuesProdukt)
    {
      if (Zeile.Contains("MaxProduktionsRate:"))
      {
        int IntProduktionsRate;
        string ProduktionsRate = Zeile.Replace("MaxProduktionsRate: ", "");
        if (Int32.TryParse(ProduktionsRate, out IntProduktionsRate)){
          NeuesProdukt.MaxProduktionsRate = IntProduktionsRate;
        }
      }
    }  

    /// <summary>
    /// Überprüft ob die jeweilige Zeile des Basispreises des Produktes enthält und speichert diesen ggfs
    /// </summary>
    public bool SpeichereWennBasispreis(string Zeile, Produkte NeuesProdukt)
    {
      if (Zeile.Contains("Basispreis:"))
      {
        int IntBasispreis;
        string BasisPreis = Zeile.Replace("Basispreis: ", "");
        if (Int32.TryParse(BasisPreis, out IntBasispreis)){
          NeuesProdukt.BasisPreis = IntBasispreis;
        }
        return true;
      }
      return false;
    }  
    
  }
}