using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020
{
    public class GruppenTurnier : Turnier
    {
        #region Eigenschaften
        private List<Teilnehmer> _Gruppen;
        private int _anzahlGruppen;
        private int _selectedGruppe;
        #endregion

        #region Accessoren/Modifier
        public List<Teilnehmer> Gruppen { get => _Gruppen; set => _Gruppen = value; }
        public int AnzahlTeilnehmer { get => AnzahlGruppen; set => AnzahlGruppen = value; }
        public int AnzahlGruppen { get => _anzahlGruppen; set => _anzahlGruppen = value; }
        public int SelectedGruppe { get => _selectedGruppe; set => _selectedGruppe = value; }
        public ListItemCollection Items { get; }
        #endregion

        #region Konstruktoren
        public GruppenTurnier() : base()
        {
            this.Gruppen = new List<Teilnehmer>();
            this.AnzahlTeilnehmer = 0;
        }
        public GruppenTurnier(string bez, sportart sport, List<Gruppe> Teiln) : base(bez, sport)
        {
            this.Gruppen = new List<Teilnehmer>(Teiln);
            this.AnzahlTeilnehmer = this.Gruppen.Count;
        }
        public GruppenTurnier(string bez, sportart sport) : base(bez, sport)
        {
            this.Gruppen = new List<Teilnehmer>();
            this.AnzahlTeilnehmer = this.Gruppen.Count;
        }
        public GruppenTurnier(GruppenTurnier value) : base(value)
        {
            this.Gruppen = new List<Teilnehmer>(value.Gruppen);
            this.AnzahlTeilnehmer = value.AnzahlTeilnehmer;
        }

        public GruppenTurnier(string bez, sportart sport, ListItemCollection items) : this(bez, sport)
        {
            Items = items;
        }

        #endregion

        #region Worker
        public override int getAnzahlTeilnehmer()
        {
            this.AnzahlGruppen = this.Gruppen.Count;
            return this.Gruppen.Count;
        }
        public override int getAnzahlPersonenteilnehmer(int value)
        {
            int ergebnis = 0;

            ergebnis = ((Gruppe)this.Gruppen[value - 1]).Mitglieder.Count;

            return ergebnis;
        }

        public override List<Teilnehmer> getTeilnehmer()
        {
            return this.Gruppen;
        }

        public override void addTeilnehmer(object value)
        {
            this.Gruppen.Add((Gruppe)value);
            this.AnzahlTeilnehmer++;
            this.AnzahlGruppen++;
        }

        public override void clearTeilnehmer()
        {
            this.Gruppen.Clear();
            this.AnzahlGruppen = 0;
        }

        public override string GetTypus()
        {
            return "Gruppen";
        }

        public override void addSpiel(int grpid, object teilnehmer1, object teilnehmer2)
        {
            Person pers1 = ((Person)teilnehmer1);
            Person pers2 = ((Person)teilnehmer2);
            Spiel neu = new Gruppenspiel(this, grpid, pers1, pers2);
                if (neu.ID == -1)
                {
                    neu.ID = this.Spiele.Count + 1;
                }
                else
                { }
                this.Spiele.Add(neu);
        }
        public override void addSpiel(Spiel neu)
        {
                if (neu.ID == -1)
                {
                    neu.ID = this.Spiele.Count + 1;
                }
                else
                { }
                this.Spiele.Add(neu);
        }
        public override string getSpieleBezeichnung()
        {
            return "SpielNr.";
        }

        public override string getTeilnehmerbezeichnung()
        {
            return "Teilnehmer";
        }

        public override Teilnehmer getGruppe(int index)
        {
            return Gruppen[index];
        }
        public override bool isSpielVorhanden(Spiel search)
        {
            foreach (Spiel sp in Spiele)
            {
                if (search.Turnier == sp.Turnier &&
                    search.getMannschaftName1().Equals(sp.getMannschaftName1()) &&
                    search.getMannschaftName2().Equals(sp.getMannschaftName2()))
                {
                    return true;
                }
                else
                { }
            }
            return false;
        }
        public override int Get_MaxRunden()
        {
            return AnzahlGruppen;
        }

        public override int getSelectedGruppe()
        {
            return SelectedGruppe;
        }

        public override void ClearSpiele(int gruppe)
        {
            for (int index = 0; index < this.Spiele.Count; index++)
            {
                if (this.Spiele[index].getGruppe() == gruppe)
                {
                    this.Spiele.RemoveAt(index);
                    index = -1;
                }
                else
                {
                }
            }
        }

        public override void setSelectedGruppe(int v)
        {
            SelectedGruppe = v;
        }

        public override void SetMaxSpieltag(int v)
        {
            //dummy für GruppenTurnier
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
            throw new NotImplementedException();
        }

        public override Spiel getSpiel(int spielid, int selectedgruppe)
        {
            Spiel ergebnis = null;
            foreach (Spiel sp in this.Spiele)
            {
                if (sp.getGruppe() == selectedgruppe)
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
            neu.makeRanking(this.Spiele,this, false);

            return neu;
        }

        public override Teilnehmer getTeilnehmer(Teilnehmer teilnehmer,int selectedgruppe)
        {
            Teilnehmer such = teilnehmer;
            Teilnehmer ergebnis = null;



            foreach (Teilnehmer teiln in ((Gruppe)this.Gruppen[selectedgruppe+1]).Mitglieder)
            {
                if (teiln.Name.Equals(teilnehmer.Name) &&
                    teiln.ID == teilnehmer.ID)
                {
                    ergebnis = teiln;
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
