using Microsoft.VisualStudio.TestTools.UnitTesting;
using KreditSim;
using Zwischenhaendler.Sim;


namespace KreditAufnehmen.Test
{
    [TestClass]
    public class KreditAufnehmenTests
    {
        private Zwischenhändler zwischenhaendler = new Zwischenhändler();
        private int StartKontostand = 15000;

        [TestInitialize()]
        public void TestInitialize()
        {
            zwischenhaendler = new Zwischenhändler();
            ParameterInitialisieren();
        }

        /// <summary>
        /// Initialisiert die Objekte die nötig sind um die Kauffunktion zu Testen
        /// </summary>
        public void ParameterInitialisieren(int StartKontostand = 15000)
        {
            //Erstelle Händler
            zwischenhaendler.EinkaufsRabatte.Add(0);
            zwischenhaendler.Kontostand = StartKontostand;
            this.StartKontostand = StartKontostand;
        }

        [TestMethod]
        [DataRow(5000,3)]
        [DataRow(10000,5)]
        [DataRow(15000,10)]
        public void KreditAufnehmen_Erfolgreich_ParameterAnpassung(double Betrag, double ZinssatzProzent)
        {
            bool Ergebniss = zwischenhaendler.Kredit.KreditAufnehmen(zwischenhaendler, Betrag, ZinssatzProzent);

            Assert.IsTrue(Ergebniss, message: "Kredit konnte nicht aufgenommen werden");

            Assert.AreEqual(Betrag, zwischenhaendler.Kredit.KreditBetrag, message: "Betrag nicht korrekt übertragen");

            Assert.AreEqual(ZinssatzProzent, zwischenhaendler.Kredit.ZinssatzProzent, message: "Zinssatz nicht korrekt übertragen");
        }

        [TestMethod]
        [DataRow(5000,3)]
        [DataRow(10000,5)]
        [DataRow(15000,10)]
        public void KreditAufnehmen_Fehlschlag_DoppelterKredit(double Betrag, double ZinssatzProzent)
        {
            bool Ergebniss = zwischenhaendler.Kredit.KreditAufnehmen(zwischenhaendler, Betrag, ZinssatzProzent);

            Assert.IsTrue(Ergebniss, message: "Kredit konnte nicht aufgenommen werden");

            Ergebniss = zwischenhaendler.Kredit.KreditAufnehmen(zwischenhaendler, Betrag, ZinssatzProzent);

            Assert.IsFalse(Ergebniss, message: "Kredit wurde ein zweites mal aufgenommen");
        }

        [TestMethod]
        [DataRow(5000,3)]
        [DataRow(10000,5)]
        [DataRow(15000,10)]
        public void KreditAufnehmen_Fehlschlag_ParameterUnverändert(double Betrag, double ZinssatzProzent)
        {
            zwischenhaendler.Kredit.KreditAufnehmen(zwischenhaendler, Betrag, ZinssatzProzent);

            bool Ergebniss = zwischenhaendler.Kredit.KreditAufnehmen(zwischenhaendler, Betrag, ZinssatzProzent);

            Assert.IsFalse(Ergebniss, message: "Kredit wurde ein zweites mal aufgenommen");

            Assert.AreEqual(Betrag, zwischenhaendler.Kredit.KreditBetrag, message: "Betrag nicht korrekt übertragen");

            Assert.AreEqual(ZinssatzProzent, zwischenhaendler.Kredit.ZinssatzProzent, message: "Zinssatz nicht korrekt übertragen");
        }

        [TestMethod]
        [DataRow(5000,3)]
        [DataRow(10000,5)]
        [DataRow(15000,10)]
        public void ZahleKreditZurueck_Erfolgreich_KreditwürdigkeitAngepasst(double Betrag, double ZinssatzProzent)
        {
            zwischenhaendler.Kredit.KreditAufnehmen(zwischenhaendler, Betrag, ZinssatzProzent);
            zwischenhaendler.Kredit.KreditAufgenommenTag = 7;
            zwischenhaendler.Kredit.ZahleKreditZurueck(zwischenhaendler);
            bool Ergebniss = zwischenhaendler.Kredit.KreditAufnehmen(zwischenhaendler, Betrag, ZinssatzProzent);
            Assert.IsTrue(Ergebniss, message: "Händler kann keinen Kredit wieder aufnehmen");
        }

        [TestMethod]
        [DataRow(5000,3)]
        [DataRow(10000,5)]
        [DataRow(13000,10)]
        public void ZahleKreditZurueck_Erfolgreich_KontostandAngepasst(double Betrag, double ZinssatzProzent)
        {
            double RückzahlBetrag = Betrag + (Betrag * (1/ZinssatzProzent));
            double ErwarteterKontostand = zwischenhaendler.Kontostand - RückzahlBetrag;

            zwischenhaendler.Kredit.KreditAufnehmen(zwischenhaendler, Betrag, ZinssatzProzent);
            zwischenhaendler.Kredit.KreditAufgenommenTag = 7;
            zwischenhaendler.Kredit.ZahleKreditZurueck(zwischenhaendler);

            Assert.AreEqual(ErwarteterKontostand, zwischenhaendler.Kontostand, message: "Kontostand falsch berechnet");
        }        

        [TestMethod]
        [DataRow(5000,3,2)]
        [DataRow(10000,5,5)]
        [DataRow(13000,10,6)]
        public void ZahleKreditZurueck_Fehlschlag_KontostandAnpassung(double Betrag, double ZinssatzProzent, int VergangegeneTage)
        {
            zwischenhaendler.Kredit.KreditAufnehmen(zwischenhaendler, Betrag, ZinssatzProzent);
            zwischenhaendler.Kredit.KreditAufgenommenTag = VergangegeneTage;
            bool Ergebniss = zwischenhaendler.Kredit.ZahleKreditZurueck(zwischenhaendler);

            Assert.IsFalse(Ergebniss, message: "Kredit wurde rückgezahlt");
            Assert.AreEqual(StartKontostand, zwischenhaendler.Kontostand, message: "Kontostand fälschlicherweise angepasst");
        }  

        [TestMethod]
        [DataRow(5000,3)]
        [DataRow(10000,5)]
        [DataRow(13000,10)]
        public void ZahleKreditZurueck_Erfolgreich_ParameterRücksetung(double Betrag, double ZinssatzProzent)
        {
            zwischenhaendler.Kredit.KreditAufnehmen(zwischenhaendler, Betrag, ZinssatzProzent);
            zwischenhaendler.Kredit.KreditAufgenommenTag = 7;
            bool Ergebniss = zwischenhaendler.Kredit.ZahleKreditZurueck(zwischenhaendler);

            Assert.IsTrue(Ergebniss, message: "Kredit rückzahlung fehlgeschlagen");
            Assert.IsFalse(zwischenhaendler.Kredit.KreditAufgenommen, message: "Kreditwürdigkeit wurde nicht wiederhergestellt");
            Assert.AreEqual(1, zwischenhaendler.Kredit.KreditAufgenommenTag, message: "Tage wurden nicht rückgesetzt");
        }  
    }
}