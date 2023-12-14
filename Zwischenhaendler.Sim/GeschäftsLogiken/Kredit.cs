
namespace KreditSim
{
    class Kredit
    {
        public int KreditAufgenommenTag = 0;
        public bool KreditAufgenommen = false;
        public double KreditBetrag = 0;
        public double ZinssatzProzent = 0;
        public int RückzahlTag = 7

        public bool CheckeKreditwürdigkeit(Zwischenhaendler Haendler)
        {   
            if (Haendler.KreditAufgenommen) return true;
            return false; 
        }
        public void ZahleKreditZurueck(Zwischenhaendler Haendler)
        {
            if(!KreditAufgenommen) return;

            if(KreditAufgenommenTag == RückzahlTag)
            {
                Haendler.Kontostand -= (KreditBetrag + (KreditBetrag * (1/ZinssatzProzent)); 
                KreditBezahlt();
                return     
            }

            KreditAufgenommenTag++;
        }
        public void KreditBezahlt()
        {
            KreditAufgenommen = false;
            KreditAufgenommenTag = 0;
        }
        public bool KreditAufnehmen(double Betrag, double ZinssatzProzent)
        {
            if(!CheckeKreditwürdigkeit())
            {
              console.WriteLine("Sie sind aktuell nicht Kreditwürdig");
              return false;   
            }

            KreditBetrag = Betrag;
            this.ZinssatzProzent = ZinssatzProzent;
            KreditAufgenommen = true;
            return true;
        }
    } 
}
