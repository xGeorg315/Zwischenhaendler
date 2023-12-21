using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zwischenhaendler.Sim;
using LagerVergrößernSim;

namespace LagerVergrößern.Test
{
    [TestClass]
    public class LagerVergrößernTests
    {
        private Zwischenhändler zwischenhaendler = new Zwischenhändler();
        private LagerVergrößernSim.LagerVergrößern lagerVergrößern = new LagerVergrößernSim.LagerVergrößern();
        private double StartKontostand = 15000;

        [TestInitialize()]
        public void TestInitialize()
        {
            ParameterInitialisieren();
        }

        /// <summary>
        /// Initialisiert die Objekte die nötig sind um die Kauffunktion zu Testen
        /// </summary>
        public void ParameterInitialisieren(double StartKontostand = 15000)
        {
            zwischenhaendler.Kontostand = StartKontostand;
            this.StartKontostand = StartKontostand;
        }

        
        /// <summary>
        /// Testet ob die Parameter nach einer erfolgreichen Vergrößerung des Lagers richtig angepasst werden
        /// </summary>
        [TestMethod]
        [DataRow(50)]
        [DataRow(100)]
        [DataRow(1000)]
        public void WickleKaufAb_Erfolgreich_ParameterAnpassung(int KaufAnzahl)
        {
            int ErwarteteKapazitaet = zwischenhaendler.Lager.MaxKapazität + KaufAnzahl;
            double VergrößerungsKosten = KaufAnzahl * 50; 
            ParameterInitialisieren(VergrößerungsKosten);
            
            bool Ergebniss = lagerVergrößern.WickleKaufAb(zwischenhaendler, KaufAnzahl);

            Assert.IsTrue(Ergebniss, "Lager konnte nicht vergrößert werden");
            Assert.AreEqual(0, zwischenhaendler.Kontostand,message: "Kosten falsch berechnet");
            Assert.AreEqual(ErwarteteKapazitaet, zwischenhaendler.Lager.MaxKapazität,message: "Kapazität wurde nicht richtig hinzugefügt");
        }

        
        /// <summary>
        /// Testet ob die Parameter nach einer fehlerhaften Vergrößerung unverändert bleiben
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(50)]
        [DataRow(100)]
        [DataRow(1000)]
        public void WickleKaufAb_Fehlerhaft_ParameterAnpassung(int KaufAnzahl)
        {
            int ErwarteteKapazitaet = zwischenhaendler.Lager.MaxKapazität;
            double VergrößerungsKosten = KaufAnzahl * 50; 
            ParameterInitialisieren(VergrößerungsKosten - 1);
            
            bool Ergebniss = lagerVergrößern.WickleKaufAb(zwischenhaendler, KaufAnzahl);

            Assert.IsFalse(Ergebniss, "Lager wurde fälschlicherweise vergrößert");
            Assert.AreEqual(StartKontostand, zwischenhaendler.Kontostand, message: "Kontostand wurde verändert");
            Assert.AreEqual(ErwarteteKapazitaet, zwischenhaendler.Lager.MaxKapazität, message: "Kapazität wurde verändert");
        }
    }
}