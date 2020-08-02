using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020
{
    public class sportart
    {
        #region Eigenschaftten
        private int _id;
        private string _name;
        private bool _mannschaft;
        private bool _einzel;
        private int _pluspunkteproSpiel;
        private int _minupunkteproSpiel;
        private int _unentschiedenpunkteproSpiel;
        #endregion

        #region Accessoren/Modifier
        public int id { get => _id; set => _id = value; }
        public string name { get => _name; set => _name = value; }
        public bool Mannschaft { get => _mannschaft; set => _mannschaft = value; }
        public bool Einzel { get => _einzel; set => _einzel = value; }
        public int PluspunkteproSpiel { get => _pluspunkteproSpiel; set => _pluspunkteproSpiel = value; }
        public int MinupunkteproSpiel { get => _minupunkteproSpiel; set => _minupunkteproSpiel = value; }
        public int UnentschiedenpunkteproSpiel { get => _unentschiedenpunkteproSpiel; set => _unentschiedenpunkteproSpiel = value; }
        #endregion

        #region Konstruktoren
        public sportart()
        {
            this.id = -1;
            this.name = "default";
            this.Mannschaft = false;
            this.Einzel = false;
            this.PluspunkteproSpiel = 0;
            this.MinupunkteproSpiel = 0;
            this.UnentschiedenpunkteproSpiel = 0;
        }
        public sportart(string bezeichnung, bool man, bool grp, int plus, int minus, int unent)
        {
            this.id = -1;
            this.name = bezeichnung;
            this.Mannschaft = man;
            this.Einzel = grp;
            this.PluspunkteproSpiel = plus;
            this.MinupunkteproSpiel = minus;
            this.UnentschiedenpunkteproSpiel = unent;
        }
        public sportart(sportart value)
        {
            this.id = value.id;
            this.name = value.name;
            this.Mannschaft = value.Mannschaft;
            this.Einzel = value.Einzel;
            this.PluspunkteproSpiel = value.PluspunkteproSpiel;
            this.MinupunkteproSpiel = value.MinupunkteproSpiel;
            this.UnentschiedenpunkteproSpiel = value.UnentschiedenpunkteproSpiel;
        }
        #endregion

        #region Worker
        public bool IsOK()
        {
            if ((Mannschaft || Einzel) && name != "default")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void setTabelle(Ranking tabelle, List<Teilnehmer> teilnehmer, List<Spiel> spiele)
        {
            //Daten aktualisieren
            DatenAktualisieren(teilnehmer, spiele);

            //Tabelle erstellen
            if (this.name == "Handball")
            {
                //Sortieren
                SortMitglieder(teilnehmer);
                //Zeilen
                int index = 1;
                foreach (Teilnehmer man in teilnehmer)
                {
                    TableRow neu = new TableRow();
                    TableCell neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = index.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    if (man is Mannschaft)
                    {
                        neuecell.Text = man.Name;
                    }
                    else
                    {
                        neuecell.Text = man.Name + ", " + ((Person)man).Vorname;
                    }
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.Anzahlspiele.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.Punkte.ToString() + ":" + (man.VerloreneSpiele * 2 + man.Unentschieden).ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.GewonneneSpiele.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.Unentschieden.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.VerloreneSpiele.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.TorePlus.ToString() + ":" + man.Toreminus.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center; ;
                    int diff = man.TorePlus - man.Toreminus;
                    if (diff >= 0)
                    {
                        neuecell.Text = "+" + diff.ToString();
                    }
                    else
                    {
                        neuecell.Text = diff.ToString();
                    }
                    neu.Cells.Add(neuecell);
                    tabelle.Zeilen.Add(neu);
                    index++;
                }
                tabelle.Titelzeile.Clear();
                tabelle.Titelzeile.Add("Pos.");
                if (teilnehmer[0] is Mannschaft)
                {
                    tabelle.Titelzeile.Add("Verein");
                }
                else
                {
                    tabelle.Titelzeile.Add("Name");
                }
                tabelle.Titelzeile.Add("Spiele");
                tabelle.Titelzeile.Add("Punkte"); // z.B. 32:12
                tabelle.Titelzeile.Add("S");
                tabelle.Titelzeile.Add("U");
                tabelle.Titelzeile.Add("N");
                tabelle.Titelzeile.Add("Tore"); // z.B. 84:23
                tabelle.Titelzeile.Add("Diff"); // z.B.+61
            }
            else if (this.name == "Tennis")
            {
                //Sortieren
                SortMitglieder(teilnehmer);
                //Zeilen
                int index = 1;
                foreach (Teilnehmer man in teilnehmer)
                {
                    TableRow neu = new TableRow();
                    TableCell neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = index.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    if (man is Mannschaft)
                    {
                        neuecell.Text = man.Name;
                    }
                    else
                    {
                        neuecell.Text = man.Name + ", " + ((Person)man).Vorname;
                    }
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.Anzahlspiele.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.GewonneneSpiele.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.Unentschieden.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.VerloreneSpiele.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.TorePlus.ToString() + ":" + man.Toreminus.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center; ;
                    int diff = man.TorePlus - man.Toreminus;
                    if (diff >= 0)
                    {
                        neuecell.Text = "+" + diff.ToString();
                    }
                    else
                    {
                        neuecell.Text = diff.ToString();
                    }
                    neu.Cells.Add(neuecell);
                    tabelle.Zeilen.Add(neu);
                    index++;
                }
                tabelle.Titelzeile.Clear();
                tabelle.Titelzeile.Add("Pos.");
                if (teilnehmer[0] is Mannschaft)
                {
                    tabelle.Titelzeile.Add("Verein");
                }
                else
                {
                    tabelle.Titelzeile.Add("Name");
                }
                tabelle.Titelzeile.Add("Spiele");
                tabelle.Titelzeile.Add("S");
                tabelle.Titelzeile.Add("U");
                tabelle.Titelzeile.Add("N");
                tabelle.Titelzeile.Add("Sätze"); //z.B. 14:12
                tabelle.Titelzeile.Add("Diff"); // z.B.+61
            }
            else if (this.name == "Fussball")
            {
                //Sortieren
                SortMitglieder(teilnehmer);
                //Zeilen
                int index = 1;
                foreach (Teilnehmer man in teilnehmer)
                {
                    TableRow neu = new TableRow();
                    TableCell neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = index.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    if (man is Mannschaft)
                    {
                        neuecell.Text = man.Name;
                    }
                    else
                    {
                        neuecell.Text = man.Name + ", " + ((Person)man).Vorname;
                    }
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.Anzahlspiele.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.Punkte.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.GewonneneSpiele.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.Unentschieden.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.VerloreneSpiele.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.TorePlus.ToString() + ":" + man.Toreminus.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center; ;
                    int diff = man.TorePlus - man.Toreminus;
                    if (diff >= 0)
                    {
                        neuecell.Text = "+" + diff.ToString();
                    }
                    else
                    {
                        neuecell.Text = diff.ToString();
                    }
                    neu.Cells.Add(neuecell);
                    tabelle.Zeilen.Add(neu);
                    index++;
                }
                tabelle.Titelzeile.Clear();
                tabelle.Titelzeile.Add("Pos.");
                if (teilnehmer[0] is Mannschaft)
                {
                    tabelle.Titelzeile.Add("Verein");
                }
                else
                {
                    tabelle.Titelzeile.Add("Name");
                }
                tabelle.Titelzeile.Add("Spiele");
                tabelle.Titelzeile.Add("Punkte"); // z.B. 45
                tabelle.Titelzeile.Add("S");
                tabelle.Titelzeile.Add("U");
                tabelle.Titelzeile.Add("N");
                tabelle.Titelzeile.Add("Tore"); // z.B. 84:23
                tabelle.Titelzeile.Add("Diff"); // z.B.+61
            }
            else
            {
                //Sortieren
                SortMitglieder(teilnehmer);
                //Zeilen
                int index = 1;
                foreach (Teilnehmer man in teilnehmer)
                {
                    TableRow neu = new TableRow();
                    TableCell neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = index.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    if (man is Mannschaft)
                    {
                        neuecell.Text = man.Name;
                    }
                    else
                    {
                        neuecell.Text = man.Name + ", " + ((Person)man).Vorname;
                    }
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.Anzahlspiele.ToString();
                    neu.Cells.Add(neuecell);

                    if (this.PluspunkteproSpiel > 0)
                    {
                        neuecell = new TableCell();
                        neuecell.HorizontalAlign = HorizontalAlign.Center;
                        neuecell.Text = man.Punkte.ToString();
                        neu.Cells.Add(neuecell);
                    }
                    else
                    { }
                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.GewonneneSpiele.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.Unentschieden.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.VerloreneSpiele.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center;
                    neuecell.Text = man.TorePlus.ToString() + ":" + man.Toreminus.ToString();
                    neu.Cells.Add(neuecell);

                    neuecell = new TableCell();
                    neuecell.HorizontalAlign = HorizontalAlign.Center; ;
                    int diff = man.TorePlus - man.Toreminus;
                    if (diff >= 0)
                    {
                        neuecell.Text = "+" + diff.ToString();
                    }
                    else
                    {
                        neuecell.Text = diff.ToString();
                    }
                    neu.Cells.Add(neuecell);
                    tabelle.Zeilen.Add(neu);
                    index++;
                }
                tabelle.Titelzeile.Clear();
                tabelle.Titelzeile.Add("Pos.");
                if (teilnehmer[0] is Mannschaft)
                {
                    tabelle.Titelzeile.Add("Verein");
                }
                else
                {
                    tabelle.Titelzeile.Add("Name");
                }
                tabelle.Titelzeile.Add("Spiele");
                if (this.PluspunkteproSpiel > 0)
                {
                    tabelle.Titelzeile.Add("Punkte");
                }
                else
                { }
                tabelle.Titelzeile.Add("S");
                tabelle.Titelzeile.Add("U");
                tabelle.Titelzeile.Add("N");
                tabelle.Titelzeile.Add("Tore/Punkte/Sätze"); // z.B. 84:23
                tabelle.Titelzeile.Add("Diff"); // z.B.+61
            }
        }
        private void DatenAktualisieren(List<Teilnehmer> teilnehmer, List<Spiel> spiele)
        {
            if (teilnehmer[0] is Mannschaft)
            {
                foreach (Mannschaft man in teilnehmer)
                {
                    man.Punkte = 0;
                    man.Anzahlspiele = 0;
                    man.GewonneneSpiele = 0;
                    man.Toreminus = 0;
                    man.TorePlus = 0;
                    man.Unentschieden = 0;
                    man.VerloreneSpiele = 0;
                }
                foreach (Mannschaftsspiel mansp in spiele)
                {
                    if (mansp.Ergebnis1 > -1 && mansp.Ergebnis2 > -1) //Nur Spiele mit Daten werten
                    {
                        if (mansp.Ergebnis1 > mansp.Ergebnis2)
                        {
                            mansp.Man1.Punkte += this.PluspunkteproSpiel;
                            mansp.Man1.GewonneneSpiele += 1;
                            mansp.Man2.Punkte += this.MinupunkteproSpiel;
                            mansp.Man2.VerloreneSpiele += 1;
                        }
                        else if (mansp.Ergebnis1 == mansp.Ergebnis2)
                        {
                            mansp.Man1.Punkte += this.UnentschiedenpunkteproSpiel;
                            mansp.Man1.Unentschieden += 1;
                            mansp.Man2.Punkte += this.UnentschiedenpunkteproSpiel;
                            mansp.Man2.Unentschieden += 1;
                        }
                        else
                        {
                            mansp.Man2.Punkte += this.PluspunkteproSpiel;
                            mansp.Man2.GewonneneSpiele += 1;
                            mansp.Man2.Punkte += this.MinupunkteproSpiel;
                            mansp.Man1.VerloreneSpiele += 1;
                        }
                        mansp.Man1.Anzahlspiele++;
                        mansp.Man2.Anzahlspiele++;

                        mansp.Man1.TorePlus += mansp.Ergebnis1;
                        mansp.Man1.Toreminus += mansp.Ergebnis2;

                        mansp.Man2.TorePlus += mansp.Ergebnis2;
                        mansp.Man2.Toreminus += mansp.Ergebnis1;
                    }
                    else
                    { }
                }
            }
            else //Gruppen
            {
                foreach (Person pers in teilnehmer)
                {
                    pers.Punkte = 0;
                    pers.Anzahlspiele = 0;
                    pers.GewonneneSpiele = 0;
                    pers.Toreminus = 0;
                    pers.TorePlus = 0;
                    pers.Unentschieden = 0;
                    pers.VerloreneSpiele = 0;
                }
                foreach (Gruppenspiel grpsp in spiele)
                {
                    if (grpsp.Ergebnis1 > -1 && grpsp.Ergebnis2 > -1) //Nur Spiele mit Daten werten
                    {
                        if (grpsp.Ergebnis1 > grpsp.Ergebnis2)
                        {
                            grpsp.Teilnehmer1.Punkte += this.PluspunkteproSpiel;
                            grpsp.Teilnehmer1.GewonneneSpiele += 1;
                            grpsp.Teilnehmer2.Punkte += this.MinupunkteproSpiel;
                            grpsp.Teilnehmer2.VerloreneSpiele += 1;
                        }
                        else if (grpsp.Ergebnis1 == grpsp.Ergebnis2)
                        {
                            grpsp.Teilnehmer1.Punkte += this.UnentschiedenpunkteproSpiel;
                            grpsp.Teilnehmer1.Unentschieden += 1;
                            grpsp.Teilnehmer2.Punkte += this.UnentschiedenpunkteproSpiel;
                            grpsp.Teilnehmer2.Unentschieden += 1;
                        }
                        else
                        {
                            grpsp.Teilnehmer2.Punkte += this.PluspunkteproSpiel;
                            grpsp.Teilnehmer2.GewonneneSpiele += 1;
                            grpsp.Teilnehmer2.Punkte += this.MinupunkteproSpiel;
                            grpsp.Teilnehmer1.VerloreneSpiele += 1;
                        }
                        grpsp.Teilnehmer1.Anzahlspiele++;
                        grpsp.Teilnehmer2.Anzahlspiele++;

                        grpsp.Teilnehmer1.TorePlus += grpsp.Ergebnis1;
                        grpsp.Teilnehmer1.Toreminus += grpsp.Ergebnis2;

                        grpsp.Teilnehmer2.TorePlus += grpsp.Ergebnis2;
                        grpsp.Teilnehmer2.Toreminus += grpsp.Ergebnis1;
                    }
                    else
                    { }
                }
            }
        }
        private void SortMitglieder(List<Teilnehmer> teilnehmer)
        {
            for (int index1 = 0; index1 < teilnehmer.Count - 1; index1++)
            {
                for (int index2 = 0; index2 < teilnehmer.Count - 1; index2++)
                {
                    int pluspunkte1 = teilnehmer[index2].GewonneneSpiele * this.PluspunkteproSpiel +
                        teilnehmer[index2].Unentschieden * this.UnentschiedenpunkteproSpiel;
                    int minuspunkte1 = teilnehmer[index2].VerloreneSpiele * this.MinupunkteproSpiel +
                        teilnehmer[index2].Unentschieden * this.UnentschiedenpunkteproSpiel;
                    int pluspunkte2 = teilnehmer[index2 + 1].GewonneneSpiele * this.PluspunkteproSpiel +
                        teilnehmer[index2 + 1].Unentschieden * this.UnentschiedenpunkteproSpiel;
                    int minuspunkte2 = teilnehmer[index2 + 1].VerloreneSpiele * this.MinupunkteproSpiel +
                        teilnehmer[index2 + 1].Unentschieden * this.UnentschiedenpunkteproSpiel;
                    int diff1 = teilnehmer[index2].TorePlus - teilnehmer[index2].Toreminus;
                    int diff2 = teilnehmer[index2 + 1].TorePlus - teilnehmer[index2 + 1].Toreminus;
                    if (pluspunkte1 < pluspunkte2)
                    {
                        Mitgliedertauschen(index2, index2 + 1, teilnehmer);
                    }
                    else if (pluspunkte1 == pluspunkte2 && minuspunkte1 > minuspunkte2)
                    {
                        Mitgliedertauschen(index2, index2 + 1, teilnehmer);
                    }
                    else if (pluspunkte1 == pluspunkte2 && minuspunkte1 == minuspunkte2 && diff1 < diff2)
                    {
                        Mitgliedertauschen(index2, index2 + 1, teilnehmer);
                    }
                    else
                    {
                        //nicht tauschen
                    }
                }
            }
        }
        private void Mitgliedertauschen(int value1, int value2, List<Teilnehmer> teilnehmer)
        {
            Teilnehmer temp = teilnehmer[value1];
            teilnehmer[value1] = teilnehmer[value2];
            teilnehmer[value2] = temp;
        }
        #endregion
    }
}