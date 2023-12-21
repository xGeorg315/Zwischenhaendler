using Zwischenhaendler.Sim;

namespace KreditSim
{
    public class Kredit
    {
        public int KreditAufgenommenTag = 1;
        public bool KreditAufgenommen = false;
        public double KreditBetrag = 0;
        public double ZinssatzProzent = 0;
        public int RückzahlTag = 7;

        public bool CheckeKreditwürdigkeit(Zwischenhändler Haendler)
        {   
            if (!KreditAufgenommen) return true;
            return false; 
        }
        public bool ZahleKreditZurueck(Zwischenhändler Haendler)
        {
            if(!KreditAufgenommen) return false;

            if(KreditAufgenommenTag == RückzahlTag)
            {
                double Betrag = KreditBetrag + (KreditBetrag * (1/ZinssatzProzent)); 
                Haendler.Kontostand -= Betrag;
                KreditBezahlt();
                Haendler.Tagesbericht.AddiereAusgaben(0, Betrag);
                return true;     
            }

            KreditAufgenommenTag++;
            return false;
        }
        public void KreditBezahlt()
        {
            KreditAufgenommen = false;
            KreditAufgenommenTag = 1;
        }
        public bool KreditAufnehmen(Zwischenhändler Haendler, double Betrag, double ZinssatzProzent)
        {
            if(!CheckeKreditwürdigkeit(Haendler))
            {
              return false;   
            }

            KreditBetrag = Betrag;
            this.ZinssatzProzent = ZinssatzProzent;
            KreditAufgenommen = true;
            return true;
        }
    } 
}
