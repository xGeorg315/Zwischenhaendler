using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerkaufsSim;
using Zwischenhaendler.Sim;
using DateiLesen.Sim;
using GlobalsSim;
using ProdukteSim;
using System.Runtime.InteropServices;

namespace VerkaufenTests
{
    [TestClass]
    public class VerkaufsTest
    {
        private Verkaufen verkaufen = new Verkaufen();
        private Zwischenhändler zwischenhaendler = new Zwischenhändler();
        private Produkte produkt = new Produkte();
        private Produkte GekauftesProdukt = new Produkte();
        private int StartKontostand = 100;

        [TestInitialize()]
        public void TestInitialize()
        {
            verkaufen = new Verkaufen();
            produkt = new Produkte();
            GekauftesProdukt = new Produkte();
            zwischenhaendler = new Zwischenhändler();
            ParameterInitialisieren();
        }

        /// <summary>
        /// Initialisiert die Objekte die nötig sind um die Kauffunktion zu Testen
        /// </summary>
        public void ParameterInitialisieren(int Menge = 10, double Einkaufspreis = 20, int StartKontostand = 100)
        {
            //Erstelle ein Produkt
            produkt.ProduktName = "TestProdukt";
            produkt.EinkaufsPreis = Einkaufspreis;
            produkt.Menge = Menge;
            Globals.VerfügbareProdukte.Clear();
            Globals.VerfügbareProdukte.Add(produkt);
            GekauftesProdukt = (Produkte)produkt.Clone();
            
            //Erstelle Händler
            zwischenhaendler.GekaufteProdukte.Add(GekauftesProdukt);
            zwischenhaendler.Lager.AddiereBestand(Menge);
            zwischenhaendler.Kontostand = StartKontostand;
            this.StartKontostand = StartKontostand;
        }

        /// <summary>
        /// Testet ob der Kontostand nach einem erfolgreichen Kauf richtig berechnet wird
        /// </summary>
        [TestMethod]
        [DataRow(2,1)]
        [DataRow(4,1)]
        [DataRow(6,1)]
        public void BeginneKaufProzess_ErfolgreicherVerkauf_KontostandAnpassung(int VerkaufAnzahl, int ProduktNummer)
        {
            double ErwarteterKontostand = StartKontostand + Globals.VerfügbareProdukte[0].EinkaufsPreis * 0.8 * VerkaufAnzahl;
            bool Ergebniss = verkaufen.BeginneVerkaufProzess(zwischenhaendler, ProduktNummer, VerkaufAnzahl);

            Assert.IsTrue(Ergebniss, "Fehler beim Verkauf");
            Assert.AreEqual(ErwarteterKontostand,zwischenhaendler.Kontostand, message: "Kontostand falsch berechnet");
        }

        /// <summary>
        /// Testet ob der Kontostand nach einem erfolgreichen Kauf richtig berechnet wird
        /// </summary>
        [TestMethod]
        [DataRow(2,1)]
        [DataRow(4,1)]
        [DataRow(6,1)]
        public void BeginneKaufProzess_ErfolgreicherVerkauf_LagerAnpassung(int VerkaufAnzahl, int ProduktNummer)
        {
            int ErwarteterLagerbestand = zwischenhaendler.Lager.BerechneFreienPlatz() + VerkaufAnzahl;
            bool Ergebniss = verkaufen.BeginneVerkaufProzess(zwischenhaendler, ProduktNummer, VerkaufAnzahl);

            Assert.IsTrue(Ergebniss, "Fehler beim Verkauf");
            Assert.AreEqual(ErwarteterLagerbestand,zwischenhaendler.Lager.BerechneFreienPlatz(), message: "Lager falsch berechnet");
        }

        /// <summary>
        /// Testet ob der Kontostand nach einem erfolgreichen Kauf richtig berechnet wird
        /// </summary>
        [TestMethod]
        [DataRow(2,1)]
        [DataRow(4,1)]
        [DataRow(6,1)]
        public void BeginneKaufProzess_ErfolgreicherVerkauf_PorduktMengeAnpassung(int VerkaufAnzahl, int ProduktNummer)
        {
            int ErwarteteMenge = zwischenhaendler.GekaufteProdukte[0].Menge - VerkaufAnzahl;
            bool Ergebniss = verkaufen.BeginneVerkaufProzess(zwischenhaendler, ProduktNummer, VerkaufAnzahl);

            Assert.IsTrue(Ergebniss, "Fehler beim Verkauf");
            Assert.AreEqual(ErwarteteMenge,zwischenhaendler.GekaufteProdukte[0].Menge, message: "Menge falsch berechnet");
        }

        /// <summary>
        /// Testet ob die Parameter nach einem nicht erfolgreichen Verkauf unverändert bleiben
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(2,2)]
        [DataRow(4,3)]
        [DataRow(50,1)]
        public void BeginneKaufProzess_Fehlschlag_ParameterAnpassungAnpassung(int VerkaufAnzahl, int ProduktNummer)
        {
            int ErwarteteMenge = zwischenhaendler.GekaufteProdukte[0].Menge;
            double ErwarteterKontostand = zwischenhaendler.Kontostand;
            int ErwarteterLagerbestand = zwischenhaendler.Lager.BerechneFreienPlatz();
            bool Ergebniss = verkaufen.BeginneVerkaufProzess(zwischenhaendler, ProduktNummer, VerkaufAnzahl);
            
            Assert.IsFalse(Ergebniss, "Fehler beim Verkauf");
            Assert.AreEqual(ErwarteteMenge,zwischenhaendler.GekaufteProdukte[0].Menge, message: "Menge falsch berechnet");
            Assert.AreEqual(ErwarteterKontostand,zwischenhaendler.Kontostand, message: "Kontostand wurde verändert");
            Assert.AreEqual(ErwarteterLagerbestand,zwischenhaendler.Lager.BerechneFreienPlatz(), message: "Lager wurde verändert");
        }

    }
}