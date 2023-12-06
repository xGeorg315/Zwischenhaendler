using Microsoft.VisualStudio.TestTools.UnitTesting;
using Einkaufen.Sim;
using Zwischenhaendler.Sim;
using DateiLesen.Sim;
using GlobalsSim;
using ProdukteSim;
using System.Runtime.InteropServices;

namespace Einkaufen.Test
{
    [TestClass]
    public class EinkaufsTests
    {
        private readonly EinkaufenKlasse einkaufen;
        private readonly Zwischenhändler zwischenhaendler;
        private readonly Produkte produkt;
        private Produkte GekauftesProdukt;
        private int StartKontostand = 100;

        public EinkaufsTests()
        {
            einkaufen = new EinkaufenKlasse();
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
            Globals.VerfügbareProdukte.Add(produkt);
            GekauftesProdukt = (Produkte)produkt.Clone();
            
            //Erstelle Händler
            zwischenhaendler.EinkaufsRabatte.Add(0);
            zwischenhaendler.Kontostand = StartKontostand;
            this.StartKontostand = StartKontostand;
        }

        /// <summary>
        /// Testet ob der Kontostand nach einem erfolgreichen Kauf richtig berechnet wird
        /// </summary>
        [TestMethod]
        [DataRow(2,1,0)]
        [DataRow(4,1,0)]
        [DataRow(6,1,20)]
        public void BeginneKaufProzess_ErfolgreicherKauf_KontostandAnpassung(int KaufAnzahl, int ProdukNummer, int Rabatt)
        {
            zwischenhaendler.EinkaufsRabatte[0] = Rabatt;
            bool Ergebniss = einkaufen.BeginneKaufProzess(zwischenhaendler, GekauftesProdukt, KaufAnzahl, ProdukNummer);
            Assert.IsTrue(Ergebniss, "Fehler beim Kauf");

            //Checke Kontostand
            double ErwarteterKontostand = StartKontostand - ((produkt.EinkaufsPreis - (produkt.EinkaufsPreis * Rabatt/100)) * KaufAnzahl);
            Assert.AreEqual(ErwarteterKontostand,zwischenhaendler.Kontostand, message: "Kontostand falsch berechnet");
        }

        /// <summary>
        /// Testet ob der Lagerbestand nach einem erfolgreichen Kauf richtig angepasst wird
        /// </summary>
        [TestMethod]
        [DataRow(2,1)]
        [DataRow(4,1)]
        [DataRow(5,1)]
        public void BeginneKaufProzess_ErfolgreicherKauf_LagerAnpassung(int KaufAnzahl, int ProdukNummer)
        {
            bool Ergebniss = einkaufen.BeginneKaufProzess(zwischenhaendler, GekauftesProdukt, KaufAnzahl, ProdukNummer);
            Assert.IsTrue(Ergebniss, "Fehler beim Kauf");

            //Checke Produkte im Lager
            Assert.AreEqual(KaufAnzahl,zwischenhaendler.GekaufteProdukte[0].Menge, message: "Menge nicht korrekt");
            Assert.AreEqual(produkt.ProduktName,zwischenhaendler.GekaufteProdukte[0].ProduktName, message: "Falsches Produkt hinzugefügt");
            Assert.AreEqual(produkt.EinkaufsPreis,zwischenhaendler.GekaufteProdukte[0].EinkaufsPreis, message: "Falscher Einkaufspreis");
        }

        /// <summary>
        /// Testet ob die Marktverfügbarkeit eines Produktes nach einem erfolgreichen Kauf richtig angepasst wird
        /// </summary>
        [TestMethod]
        [DataRow(2,1)]
        [DataRow(4,1)]
        [DataRow(5,1)]
        public void BeginneKaufProzess_ErfolgreicherKauf_MarkverfügbarkeitAnpassung(int KaufAnzahl, int ProdukNummer)
        {
            int erwarteteVerfügbarkeit = Globals.VerfügbareProdukte[0].Menge - KaufAnzahl;
            bool Ergebniss = einkaufen.BeginneKaufProzess(zwischenhaendler, GekauftesProdukt, KaufAnzahl, ProdukNummer);
            Assert.IsTrue(Ergebniss, "Fehler beim Kauf");

            //Checke ob Verfügbarkeit richtig angepassr wurde
            Assert.AreEqual(erwarteteVerfügbarkeit, Globals.VerfügbareProdukte[0].Menge);
        }

        /// <summary>
        /// Testet ob die Tagesausgaben richtig berechnet werden
        /// </summary>
        [TestMethod]
        [DataRow(5,1,0)]
        [DataRow(15,1,0)]
        [DataRow(30,1,20)]
        public void BeginneKaufProzess_ErfolgreicherKauf_TagesreportAnpassung(int KaufAnzahl, int ProdukNummer, int Rabatt)
        {
            ParameterInitialisieren(100, 22.5, 15000);
            double TagesAusgaben = 0;
            zwischenhaendler.EinkaufsRabatte[0] = Rabatt;
            for(int i = 0; i < 3; i++)
            {
                bool Ergebniss = einkaufen.BeginneKaufProzess(zwischenhaendler, GekauftesProdukt, KaufAnzahl, ProdukNummer);
                TagesAusgaben += (produkt.EinkaufsPreis - (produkt.EinkaufsPreis * Rabatt/100)) * KaufAnzahl;
                Assert.IsTrue(Ergebniss, "Fehler beim Kauf");
            }

            //Checke berechnung Tagesausgaben
            Assert.AreEqual(TagesAusgaben,zwischenhaendler.Tagesbericht.TagesAusgaben, message: "Tagesausgaben Falsch berechnet");
        }

        /// <summary>
        /// Testet ob der Kontostand nach einem nicht erfolgreichen Kauf unberührt bleibt
        /// </summary>
        [TestMethod]
        [DataRow(10,1)]
        [DataRow(15,1)]
        [DataRow(20,1)]
        public void BeginneKaufProzess_FehlerhafterKauf_KorrekterKontostand(int KaufAnzahl, int ProdukNummer)
        {
            produkt.Menge = 20;
            bool Ergebniss = einkaufen.BeginneKaufProzess(zwischenhaendler, GekauftesProdukt, KaufAnzahl, ProdukNummer);
            //Bei der rückgabe von False wird kein Kauf durchgeführt, die Funktion Beginne Kaufprozess ändert keine Parameter
            Assert.IsFalse(Ergebniss, "Kauf trotz zu niedrigen Kontostand durchgeführt");
            //Routine checks
            //Kontostand bleibt unberührt
            Assert.AreEqual(StartKontostand, zwischenhaendler.Kontostand, message: "Kontostand wurde verändert");
            //Dem Händler werden keine Produkte hinzugefügt
            Assert.AreEqual(0, zwischenhaendler.GekaufteProdukte.Count(), message: "Dem Händler wurde das Produkte hinzugefügt");
        }

        /// <summary>
        /// Testet ob die Marktverfügbarkeit nach einem nicht erfolgreichen Kauf unberührt bleibt
        /// </summary>
        [TestMethod]
        [DataRow(11,1)]
        [DataRow(15,1)]
        [DataRow(20,1)]
        public void BeginneKaufProzess_FehlerhafterKauf_GeringeMarktverfügbarkeit(int KaufAnzahl, int ProdukNummer)
        {
            bool Ergebniss = einkaufen.BeginneKaufProzess(zwischenhaendler, GekauftesProdukt, KaufAnzahl, ProdukNummer);
            //Bei der rückgabe von False wird kein Kauf durchgeführt, die Funktion BeginneKaufprozess ändert keine Parameter
            Assert.IsFalse(Ergebniss, "Kauf trotz zu geringer Marktverfügbarkeit durchgeführt");
            //Routine checks
            //Marktverfügbarkeit bleibt unberührt
            Assert.AreEqual(produkt.Menge, Globals.VerfügbareProdukte[0].Menge, message: "Verfügbare Menge wurde verändert");
            //Dem Händler werden keine Produkte hinzugefügt
            Assert.AreEqual(0, zwischenhaendler.GekaufteProdukte.Count(), message: "Dem Händler wurde das Produkte hinzugefügt");
        }
    }
}