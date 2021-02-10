using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020
{
    public class Ranking
    {
        #region Eigenschaften
        private List<string> _Titelzeile;
        private List<TableRow> _Zeilen;
        private sportart _Sportart;
        #endregion

        #region Accessoren/Modifier
        public List<string> Titelzeile { get => _Titelzeile; set => _Titelzeile = value; }
        public List<TableRow> Zeilen { get => _Zeilen; set => _Zeilen = value; }
        public sportart Sportart { get => _Sportart; set => _Sportart = value; }
        #endregion

        #region Konstruktoren
        public Ranking()
        {
            this.Sportart = new sportart();
            this.Titelzeile = null;
            this.Zeilen = null;
        }
        public Ranking(sportart value, List<string> titel, List<TableRow> rows)
        {
            this.Sportart = value;
            this.Titelzeile = titel;
            this.Zeilen = rows;
        }

        public Ranking(Ranking value)
        {
            this.Sportart = value.Sportart;
            this.Titelzeile = value.Titelzeile;
            this.Zeilen = value.Zeilen;
        }
        #endregion

        #region Worker
        public void setTitel(List<string> zeile)
        {
            this.Titelzeile = zeile;
        }
        public void addZeile(TableRow row)
        {
            this.Zeilen.Add(row);
        }

        public void clearZeilen()
        {
            this.Zeilen.Clear();
        }

        public void makeRanking(List<Spiel> spiele,Turnier turn, bool mannschaft)
        {        
            //Alle Teilnehmer reset
            foreach(Teilnehmer tln in turn.getTeilnehmer())
            {
                if (tln is Mannschaft)
                {
                    tln.Anzahlspiele = 0;
                    tln.GewonneneSpiele = 0;
                    tln.Unentschieden = 0;
                    tln.VerloreneSpiele = 0;
                    tln.Toreminus = 0;
                    tln.TorePlus = 0;
                }
                else
                {
                    foreach(Person pers in ((Gruppe)tln).Mitglieder)
                    {
                        pers.Anzahlspiele = 0;
                        pers.GewonneneSpiele = 0;
                        pers.Unentschieden = 0;
                        pers.VerloreneSpiele = 0;
                        pers.TorePlus = 0;
                        pers.Toreminus = 0;
                    }
                }
            }
            //Punkte berechnen
            foreach (Spiel sp in spiele)
            {
                //Sieg Tln 1
                if ((Convert.ToInt32(sp.getErgebniswert1()) > Convert.ToInt32(sp.getErgebniswert2()))&&
                    ((Convert.ToInt32(sp.getErgebniswert1()) >= 0 && Convert.ToInt32(sp.getErgebniswert2())>= 0)))
                {
                    if (mannschaft)
                    {
                        Teilnehmer tln1 = turn.getTeilnehmer(sp.getTeilnehmer1(),turn.getSelectedGruppe());
                        Teilnehmer tln2 = turn.getTeilnehmer(sp.getTeilnehmer2(),turn.getSelectedGruppe());
                        Mannschaft man1 = (Mannschaft)tln1;
                        Mannschaft man2 = (Mannschaft)tln2;
                        man1.Anzahlspiele++;
                        man1.GewonneneSpiele++;
                        man1.Punkte = man1.GewonneneSpiele * this.Sportart.PluspunkteproSpiel +
                                        man1.Unentschieden * this.Sportart.UnentschiedenpunkteproSpiel -
                                        man1.VerloreneSpiele * this.Sportart.MinupunkteproSpiel;
                        man1.TorePlus += Convert.ToInt32(sp.getErgebniswert1());
                        man1.Toreminus += Convert.ToInt32(sp.getErgebniswert2());

                        man2.Anzahlspiele++;
                        man2.VerloreneSpiele++;
                        man2.Punkte = man2.GewonneneSpiele * this.Sportart.PluspunkteproSpiel +
                                        man2.Unentschieden * this.Sportart.UnentschiedenpunkteproSpiel -
                                        man2.VerloreneSpiele * this.Sportart.MinupunkteproSpiel;
                        man2.TorePlus += Convert.ToInt32(sp.getErgebniswert2());
                        man2.Toreminus += Convert.ToInt32(sp.getErgebniswert1());
                        sp.SetTeilnehmer1(man1);
                        sp.SetTeilnehmer2(man2);
                    }
                    else
                    {
                        if (sp.getGruppe() == turn.getSelectedGruppe())
                        {
                            Teilnehmer tln1 = turn.getTeilnehmer(sp.getTeilnehmer1(), turn.getSelectedGruppe());
                            Teilnehmer tln2 = turn.getTeilnehmer(sp.getTeilnehmer2(), turn.getSelectedGruppe());
                            tln1.Anzahlspiele++;
                            tln1.GewonneneSpiele++;
                            tln1.Punkte = tln1.GewonneneSpiele * this.Sportart.PluspunkteproSpiel +
                                            tln1.Unentschieden * this.Sportart.UnentschiedenpunkteproSpiel -
                                            tln1.VerloreneSpiele * this.Sportart.MinupunkteproSpiel;
                            tln1.TorePlus += Convert.ToInt32(sp.getErgebniswert1());
                            tln1.Toreminus += Convert.ToInt32(sp.getErgebniswert2());

                            tln2.Anzahlspiele++;
                            tln2.VerloreneSpiele++;
                            tln2.Punkte = tln2.GewonneneSpiele * this.Sportart.PluspunkteproSpiel +
                                            tln2.Unentschieden * this.Sportart.UnentschiedenpunkteproSpiel -
                                            tln2.VerloreneSpiele * this.Sportart.MinupunkteproSpiel;
                            tln2.TorePlus += Convert.ToInt32(sp.getErgebniswert2());
                            tln2.Toreminus += Convert.ToInt32(sp.getErgebniswert1());
                            sp.SetTeilnehmer1(tln1);
                            sp.SetTeilnehmer2(tln2);
                        }
                        else
                        { }
                    }
                }
                //Sieg Tln2
                else if((Convert.ToInt32(sp.getErgebniswert2()) > Convert.ToInt32(sp.getErgebniswert1())) &&
                    ((Convert.ToInt32(sp.getErgebniswert1()) >= 0 && Convert.ToInt32(sp.getErgebniswert2()) >= 0)))
                {
                    if (mannschaft)
                    {
                        Teilnehmer tln1 = turn.getTeilnehmer(sp.getTeilnehmer1(), turn.getSelectedGruppe());
                        Teilnehmer tln2 = turn.getTeilnehmer(sp.getTeilnehmer2(), turn.getSelectedGruppe());
                        Mannschaft man1 = (Mannschaft)tln1;
                        Mannschaft man2 = (Mannschaft)tln2;
                        man2.Anzahlspiele++;
                        man2.GewonneneSpiele++;
                        man2.Punkte = man2.GewonneneSpiele * this.Sportart.PluspunkteproSpiel +
                                        man2.Unentschieden * this.Sportart.UnentschiedenpunkteproSpiel -
                                        man2.VerloreneSpiele * this.Sportart.MinupunkteproSpiel;
                        man2.TorePlus += Convert.ToInt32(sp.getErgebniswert1());
                        man2.Toreminus += Convert.ToInt32(sp.getErgebniswert2());

                        man1.Anzahlspiele++;
                        man1.VerloreneSpiele++;
                        man1.Punkte = man1.GewonneneSpiele * this.Sportart.PluspunkteproSpiel +
                                        man1.Unentschieden * this.Sportart.UnentschiedenpunkteproSpiel -
                                        man1.VerloreneSpiele * this.Sportart.MinupunkteproSpiel;
                        man1.TorePlus += Convert.ToInt32(sp.getErgebniswert2());
                        man1.Toreminus += Convert.ToInt32(sp.getErgebniswert1());
                        sp.SetTeilnehmer1(man1);
                        sp.SetTeilnehmer2(man2);
                    }
                    else
                    {
                        if (sp.getGruppe() == turn.getSelectedGruppe())
                        {
                            Teilnehmer tln1 = turn.getTeilnehmer(sp.getTeilnehmer1(), turn.getSelectedGruppe());
                            Teilnehmer tln2 = turn.getTeilnehmer(sp.getTeilnehmer2(), turn.getSelectedGruppe());
                            tln2.Anzahlspiele++;
                            tln2.GewonneneSpiele++;
                            tln2.Punkte = tln2.GewonneneSpiele * this.Sportart.PluspunkteproSpiel +
                                            tln2.Unentschieden * this.Sportart.UnentschiedenpunkteproSpiel -
                                            tln2.VerloreneSpiele * this.Sportart.MinupunkteproSpiel;
                            tln2.TorePlus += Convert.ToInt32(sp.getErgebniswert1());
                            tln2.Toreminus += Convert.ToInt32(sp.getErgebniswert2());

                            tln1.Anzahlspiele++;
                            tln1.VerloreneSpiele++;
                            tln1.Punkte = tln1.GewonneneSpiele * this.Sportart.PluspunkteproSpiel +
                                            tln1.Unentschieden * this.Sportart.UnentschiedenpunkteproSpiel -
                                            tln1.VerloreneSpiele * this.Sportart.MinupunkteproSpiel;
                            tln1.TorePlus += Convert.ToInt32(sp.getErgebniswert2());
                            tln1.Toreminus += Convert.ToInt32(sp.getErgebniswert1());
                            sp.SetTeilnehmer1(tln1);
                            sp.SetTeilnehmer2(tln2);
                        }
                        else
                        { }
                    }
                }
                //unentschieden
                else if ((Convert.ToInt32(sp.getErgebniswert2()) == Convert.ToInt32(sp.getErgebniswert1())) &&
                    ((Convert.ToInt32(sp.getErgebniswert1()) >= 0 && Convert.ToInt32(sp.getErgebniswert2()) >= 0)))
                {
                    if (mannschaft)
                    {
                        Teilnehmer tln1 = turn.getTeilnehmer(sp.getTeilnehmer1(), turn.getSelectedGruppe());
                        Teilnehmer tln2 = turn.getTeilnehmer(sp.getTeilnehmer2(), turn.getSelectedGruppe());
                        Mannschaft man1 = (Mannschaft)tln1;
                        Mannschaft man2 = (Mannschaft)tln2;
                        man1.Anzahlspiele++;
                        man1.Unentschieden++;
                        man1.Punkte = man1.GewonneneSpiele * this.Sportart.PluspunkteproSpiel +
                                        man1.Unentschieden * this.Sportart.UnentschiedenpunkteproSpiel -
                                        man1.VerloreneSpiele * this.Sportart.MinupunkteproSpiel;
                        man1.TorePlus += Convert.ToInt32(sp.getErgebniswert1());
                        man1.Toreminus += Convert.ToInt32(sp.getErgebniswert2());

                        man2.Anzahlspiele++;
                        man2.Unentschieden++;
                        man2.Punkte = man2.GewonneneSpiele * this.Sportart.PluspunkteproSpiel +
                                        man2.Unentschieden * this.Sportart.UnentschiedenpunkteproSpiel -
                                        man2.VerloreneSpiele * this.Sportart.MinupunkteproSpiel;
                        man2.TorePlus += Convert.ToInt32(sp.getErgebniswert2());
                        man2.Toreminus += Convert.ToInt32(sp.getErgebniswert1());
                        sp.SetTeilnehmer1(man1);
                        sp.SetTeilnehmer2(man2);
                    }
                    else
                    {
                        if (sp.getGruppe() == turn.getSelectedGruppe())
                        {
                            Teilnehmer tln1 = turn.getTeilnehmer(sp.getTeilnehmer1(), turn.getSelectedGruppe());
                            Teilnehmer tln2 = turn.getTeilnehmer(sp.getTeilnehmer2(), turn.getSelectedGruppe());
                            tln1.Anzahlspiele++;
                            tln1.Unentschieden++;
                            tln1.Punkte = tln1.GewonneneSpiele * this.Sportart.PluspunkteproSpiel +
                                            tln1.Unentschieden * this.Sportart.UnentschiedenpunkteproSpiel -
                                            tln1.VerloreneSpiele * this.Sportart.MinupunkteproSpiel;
                            tln1.TorePlus += Convert.ToInt32(sp.getErgebniswert1());
                            tln1.Toreminus += Convert.ToInt32(sp.getErgebniswert2());

                            tln2.Anzahlspiele++;
                            tln2.Unentschieden++;
                            tln2.Punkte = tln2.GewonneneSpiele * this.Sportart.PluspunkteproSpiel +
                                            tln2.Unentschieden * this.Sportart.UnentschiedenpunkteproSpiel -
                                            tln2.VerloreneSpiele * this.Sportart.MinupunkteproSpiel;
                            tln2.TorePlus += Convert.ToInt32(sp.getErgebniswert2());
                            tln2.Toreminus += Convert.ToInt32(sp.getErgebniswert1());
                            sp.SetTeilnehmer1(tln1);
                            sp.SetTeilnehmer2(tln2);
                        }
                        else
                        { }
                    }
                }
                else
                { }
            }

            Comparison<Teilnehmer> Vergleich = new Comparison<Teilnehmer>(VergleichPunkte);
            //Rows erzeugen
            this.Zeilen = new List<TableRow>();

            
            int rank = 1;
            if (mannschaft)
            {
                turn.getTeilnehmer().Sort(Vergleich);
                foreach (Teilnehmer tln in turn.getTeilnehmer())
                {
                    TableRow neu = new TableRow();
                    TableCell neucell = new TableCell();
                    neucell.Text = rank.ToString();
                    neucell.HorizontalAlign = HorizontalAlign.Center;
                    rank++;
                    neu.Cells.Add(neucell);

                    neucell = new TableCell();
                    neucell.Text = tln.Name;
                    neu.Cells.Add(neucell);

                    neucell = new TableCell();
                    neucell.Text = tln.Anzahlspiele.ToString();
                    neucell.HorizontalAlign = HorizontalAlign.Center;
                    neu.Cells.Add(neucell);

                    neucell = new TableCell();
                    neucell.Text = tln.Punkte.ToString();
                    neucell.HorizontalAlign = HorizontalAlign.Center;
                    neu.Cells.Add(neucell);

                    neucell = new TableCell();
                    neucell.Text = tln.GewonneneSpiele.ToString();
                    neucell.HorizontalAlign = HorizontalAlign.Center;
                    neu.Cells.Add(neucell);

                    neucell = new TableCell();
                    neucell.Text = tln.Unentschieden.ToString();
                    neucell.HorizontalAlign = HorizontalAlign.Center;
                    neu.Cells.Add(neucell);

                    neucell = new TableCell();
                    neucell.Text = tln.VerloreneSpiele.ToString();
                    neucell.HorizontalAlign = HorizontalAlign.Center;
                    neu.Cells.Add(neucell);

                    neucell = new TableCell();
                    neucell.Text = tln.TorePlus.ToString() + ":" + tln.Toreminus.ToString();
                    neucell.HorizontalAlign = HorizontalAlign.Center;
                    neu.Cells.Add(neucell);

                    neucell = new TableCell();
                    int diff = (tln.TorePlus - tln.Toreminus);
                    if (diff > 0)
                    {
                        neucell.Text = "+" + diff.ToString();
                    }
                    else if (diff < 0)
                    {
                        neucell.Text = diff.ToString();
                    }
                    else
                    {
                        neucell.Text = "0";
                    }
                    neucell.HorizontalAlign = HorizontalAlign.Center;
                    neu.Cells.Add(neucell);

                    this.Zeilen.Add(neu);
                }
            }
            else
            {
                if (turn.getSelectedGruppe() > 0)
                {
                    ((Gruppe)turn.getTeilnehmer()[turn.getSelectedGruppe() - 1]).Mitglieder.Sort(Vergleich);
                    foreach (Teilnehmer tln in ((Gruppe)turn.getTeilnehmer()[turn.getSelectedGruppe() - 1]).Mitglieder)
                    {
                        TableRow neu = new TableRow();
                        TableCell neucell = new TableCell();
                        neucell.Text = rank.ToString();
                        neucell.HorizontalAlign = HorizontalAlign.Center;
                        rank++;
                        neu.Cells.Add(neucell);

                        neucell = new TableCell();
                        neucell.Text = tln.Name;
                        neu.Cells.Add(neucell);

                        neucell = new TableCell();
                        neucell.Text = tln.Anzahlspiele.ToString();
                        neucell.HorizontalAlign = HorizontalAlign.Center;
                        neu.Cells.Add(neucell);

                        neucell = new TableCell();
                        neucell.Text = tln.Punkte.ToString();
                        neucell.HorizontalAlign = HorizontalAlign.Center;
                        neu.Cells.Add(neucell);

                        neucell = new TableCell();
                        neucell.Text = tln.GewonneneSpiele.ToString();
                        neucell.HorizontalAlign = HorizontalAlign.Center;
                        neu.Cells.Add(neucell);

                        neucell = new TableCell();
                        neucell.Text = tln.Unentschieden.ToString();
                        neucell.HorizontalAlign = HorizontalAlign.Center;
                        neu.Cells.Add(neucell);

                        neucell = new TableCell();
                        neucell.Text = tln.VerloreneSpiele.ToString();
                        neucell.HorizontalAlign = HorizontalAlign.Center;
                        neu.Cells.Add(neucell);

                        neucell = new TableCell();
                        neucell.Text = tln.TorePlus.ToString() + ":" + tln.Toreminus.ToString();
                        neucell.HorizontalAlign = HorizontalAlign.Center;
                        neu.Cells.Add(neucell);

                        neucell = new TableCell();
                        int diff = (tln.TorePlus - tln.Toreminus);
                        if (diff > 0)
                        {
                            neucell.Text = "+" + diff.ToString();
                        }
                        else if (diff < 0)
                        {
                            neucell.Text = diff.ToString();
                        }
                        else
                        {
                            neucell.Text = "0";
                        }
                        neucell.HorizontalAlign = HorizontalAlign.Center;
                        neu.Cells.Add(neucell);

                        this.Zeilen.Add(neu);
                    }
                }
                else
                { }
            }
        }

        private int VergleichPunkte(Teilnehmer x, Teilnehmer y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (x.Punkte > y.Punkte)
                {
                    return -1;
                }
                else if(x.Punkte < y.Punkte)
                {
                    return 1;
                }
                else
                {
                    if((x.TorePlus - x.Toreminus) > (y.TorePlus - y.Toreminus))
                    {
                        return -1;
                    }
                    else if((x.TorePlus - x.Toreminus) < (y.TorePlus - y.Toreminus))
                    {
                        return 1;
                    }
                    else
                    {
                        if(x.TorePlus > y.TorePlus)
                        {
                            return -1;
                        }
                        else if(x.TorePlus < y.TorePlus)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }
        #endregion
    }
}