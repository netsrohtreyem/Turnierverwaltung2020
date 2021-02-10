using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020
{
    public class MannschaftsTurnier : Turnier
    {
        #region Eigenschaften
        private List<Teilnehmer> _Teilnehmer;
        private int _anzahlTeilnehmer;
        private int _maxSpieltag;
        #endregion

        #region Accessoren/Modifier
        public List<Teilnehmer> Teilnehmer { get => _Teilnehmer; set => _Teilnehmer = value; }
        public int AnzahlTeilnehmer { get => _anzahlTeilnehmer; set => _anzahlTeilnehmer = value; }
        public int MaxSpieltag { get => _maxSpieltag; set => _maxSpieltag = value; }
        public ListItemCollection Items { get; }
        #endregion

        #region Konstruktoren
        public MannschaftsTurnier() : base()
        {
            this.Teilnehmer = new List<Teilnehmer>();
            this.MaxSpieltag = 0;
            this.AnzahlTeilnehmer = 0;
        }
        public MannschaftsTurnier(string bez, sportart sport, List<Mannschaft> Teiln) : base(bez, sport)
        {
            this.Teilnehmer = new List<Teilnehmer>(Teiln);
            this.AnzahlTeilnehmer = this.Teilnehmer.Count;
            this.MaxSpieltag = 0;
        }
        public MannschaftsTurnier(string bez, sportart sport) : base(bez, sport)
        {
            this.Teilnehmer = new List<Teilnehmer>();
            this.AnzahlTeilnehmer = this.Teilnehmer.Count;
            this.MaxSpieltag = 0;
        }
        public MannschaftsTurnier(MannschaftsTurnier value) : base(value)
        {
            this.Teilnehmer = new List<Teilnehmer>(value.Teilnehmer);
            this.AnzahlTeilnehmer = value.AnzahlTeilnehmer;
            this.MaxSpieltag = 0;
        }

        #endregion

        #region Worker
        public override int getAnzahlTeilnehmer()
        {
            return this.Teilnehmer.Count;
        }
        public override List<Teilnehmer> getTeilnehmer()
        {
            return this.Teilnehmer;

        }
        public override void clearTeilnehmer()
        {
            this.Teilnehmer.Clear();
        }
        public override void addTeilnehmer(object value)
        {
            this.Teilnehmer.Add((Mannschaft)value);
            this.AnzahlTeilnehmer++;
        }
        public override string GetTypus()
        {
            return "Mannschaften";
        }
        public override void addSpiel(int spieltag, object mannschaft1, object mannschaft2)
        {
            Spiel neu = new Mannschaftsspiel(this, ((Mannschaft)mannschaft1), ((Mannschaft)mannschaft1), spieltag);

                if (neu.ID == -1)
                {
                    neu.ID = this.Spiele.Count + 1;
                }
                else
                {
                }
                this.Spiele.Add(neu);
                if (MaxSpieltag < spieltag)
                {
                    MaxSpieltag = spieltag;
                }
                else
                { }
        }
        public override void addSpiel(Spiel neu)
        {

                if (neu.ID == -1)
                {
                    neu.ID = this.Spiele.Count + 1;
                }
                else
                {
                }
                this.Spiele.Add(neu);
                if (MaxSpieltag < ((Mannschaftsspiel)neu).Spieltag)
                {
                    MaxSpieltag = ((Mannschaftsspiel)neu).Spieltag;
                }
                else
                { }
        }
        public override string getSpieleBezeichnung()
        {
            return "Spieltag";
        }
        public override string getTeilnehmerbezeichnung()
        {
            return "Mannschaft";
        }
        public override Teilnehmer getGruppe(int v)
        {
            return null;
        }
        public override int Get_MaxRunden()
        {
            return MaxSpieltag;
        }
        public override bool isSpielVorhanden(Spiel search)
        {
            foreach (Spiel sp in this.Spiele)
            {
                if (search.getMannschaftName1().Equals(sp.getMannschaftName1()) &&
                    search.getMannschaftName2().Equals(sp.getMannschaftName2()))
                {
                    return true;
                }
                else
                { }
            }
            return false;
        }
        public override int getSelectedGruppe()
        {
            return -1;
        }
        public override void ClearSpiele(int value)
        {
            this.Spiele.Clear();
            this.MaxSpieltag = 0;
        }
        public override void setSelectedGruppe(int v)
        {
        }
        public override void SetMaxSpieltag(int v)
        {
            this.MaxSpieltag = v;
        }
        //Bestimmt die Anzahl der Personen in einer Mannschaft (value) oder alle value = -1
        public override int getAnzahlPersonenteilnehmer(int value)
        {
            int ergebnis = 0;

            if (value == -1)
            {
                foreach (Mannschaft man in this.Teilnehmer)
                {
                    ergebnis += man.Mitglieder.Count;
                }
            }
            else
            {
                ergebnis = ((Mannschaft)Teilnehmer[value - 1]).Mitglieder.Count;
            }
            return ergebnis;
        }
        public override void ChangeSpiel(int id, string name1, string name2, string ergebnis1, string ergebnis2)
        {
            foreach (Spiel sp in this.Spiele)
            {
                if (sp.ID == id && sp.Turnier == this.ID)
                {
                    sp.setErgebniswert1(ergebnis1);
                    sp.setErgebniswert2(ergebnis2);
                }
                else
                { }
            }
        }
        public override bool SindMannschaftenAmSpieltagVorhanden(int teilnehmer1, int teilnehmer2, List<Mannschaft> liste, int spieltag)
        {
            bool ergebnis = false;
            foreach(Spiel sp in this.Spiele)
            {
                if(sp.Get_Spieltag() == spieltag && teilnehmer2 != 0)
                {
                    if(liste[teilnehmer1-1].Name == sp.getMannschaftName1() ||
                       liste[teilnehmer1-1].Name == sp.getMannschaftName2() ||
                       liste[teilnehmer2-1].Name == sp.getMannschaftName1() ||
                       liste[teilnehmer2-1].Name == sp.getMannschaftName2())
                    {
                        ergebnis = true;
                        break;
                    }
                    else
                    { }
                }
                else if(sp.Get_Spieltag() == spieltag)
                {
                    if (liste[teilnehmer1 - 1].Name == sp.getMannschaftName1() ||
                        liste[teilnehmer1 - 1].Name == sp.getMannschaftName2())
                    {
                        ergebnis = true;
                        break;
                    }
                    else
                    { }
                }
                else
                { }
            }

            return ergebnis;
        }

        public override Spiel getSpiel(int spielid, int spieltag)
        {
            Spiel ergebnis = null;
            foreach (Spiel sp in this.Spiele)
            {
                if (sp.Get_Spieltag() == spieltag)
                {
                    spielid--;
                    if (spielid <= 0)
                    {
                        ergebnis = sp;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                { }
            }
            return ergebnis;
        }

        public override Ranking GetRanking(int selectedTurnierGruppe)
        {
            Ranking neu = new Ranking();
            neu.Sportart = this.Sportart;
            neu.Titelzeile = new List<string>();
            neu.Titelzeile.Add("Rang");
            neu.Titelzeile.Add("Verein");
            neu.Titelzeile.Add("Spiele");
            neu.Titelzeile.Add("Punkte");
            neu.Titelzeile.Add("Siege");
            neu.Titelzeile.Add("Unentschd.");
            neu.Titelzeile.Add("Verloren");
            neu.Titelzeile.Add("Tore");
            neu.Titelzeile.Add("Diff");
            //Tablerows generieren
            neu.makeRanking(this.Spiele,this,true);

            return neu;
        }

        public override Teilnehmer getTeilnehmer(Teilnehmer teilnehmer,int grp)
        {
            Mannschaft such = (Mannschaft)teilnehmer;
            Mannschaft ergebnis = null;

            foreach(Mannschaft man in this.Teilnehmer)
            {
                if(man.Name.Equals(teilnehmer.Name))
                {
                    ergebnis = man;
                    break;
                }
                else
                { }
            }

            return ergebnis;
        }

        #endregion
    }
}
