using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020
{
    public abstract class Turnier
    {
        #region Eigenschaften
        private int _id;
        private string _Bezeichnung;
        private sportart _sportart;
        private List<Spiel> _Spiele;
        #endregion

        #region Accessoren/Modifier
        public int ID { get => _id; set => _id = value; }
        public string Bezeichnung { get => _Bezeichnung; set => _Bezeichnung = value; }
        public sportart Sportart { get => _sportart; set => _sportart = value; }
        public List<Spiel> Spiele { get => _Spiele; set => _Spiele = value; }
        #endregion

        #region Konstruktoren
        public Turnier()
        {
            this.ID = -1;
            this.Bezeichnung = "noname";
            this.Sportart = new sportart("Fussball", true, false, 3, 0, 1);
            this.Spiele = new List<Spiel>();
        }
        public Turnier(string bez, sportart sport)
        {
            this.ID = -1;
            Bezeichnung = bez;
            Sportart = sport;
            this.Spiele = new List<Spiel>();
        }
        public Turnier(Turnier value)
        {
            this.ID = value.ID;
            Bezeichnung = value.Bezeichnung;
            Sportart = value.Sportart;
            this.Spiele = new List<Spiel>(value.Spiele);
        }
        #endregion

        #region Worker
        public string getSportart()
        {
            return Sportart.name;
        }
        public int CompareByBezeichnung(Turnier value)
        {
            return Bezeichnung.CompareTo(value.Bezeichnung);
        }
        public int CompareBySportart(Person value)
        {
            return Sportart.name.CompareTo(value.Sportart.name);
        }
        public abstract string getSpieleBezeichnung();
        public abstract bool AddToDatabase();
        public abstract bool DeleteFromDB();
        public abstract bool ChangeInDB();
        public abstract int getAnzahlTeilnehmer();
        public abstract List<Teilnehmer> getTeilnemer();
        public abstract void clearTeilnehmer();
        public abstract void addTeilnehmer(object items);
        public abstract string GetTypus();
        public abstract void addSpiel(int number, object Teilnehmer1, object Teilnehmer2);
        public abstract void addSpiel(Spiel value);
        public abstract string getTeilnehmerbezeichnung();
        public abstract Teilnehmer getGruppe(int v);
        public abstract int Get_MaxRunden();
        public void RemoveSpiel(int id)
        {
            foreach (Spiel sp in this.Spiele)
            {
                if (sp.ID == id)
                {
                    if (sp.DeleteFromDB())
                    {
                        this.Spiele.Remove(sp);
                        break;
                    }
                    else
                    { }
                }
                else
                { }
            }
        }
        public abstract bool isSpielVorhanden(Spiel search);
        public List<Spiel> Get_Spiele()
        {
            return this.Spiele;
        }
        public abstract int getSelectedGruppe();
        public abstract void ClearSpiele(int gruppe);
        public abstract void setSelectedGruppe(int v);
        public Spiel getSpiel(int spielid)
        {
            Spiel ergebnis = null;
            foreach (Spiel sp in this.Spiele)
            {
                if (sp.ID == spielid)
                {
                    ergebnis = sp;
                    break;
                }
                else
                { }
            }
            return ergebnis;
        }
        public abstract Spiel getSpiel(int spielid, int spieltag);
        
        public abstract void SetMaxSpieltag(int v);
        public abstract int getAnzahlPersonenteilnehmer(int value);
        public abstract void ChangeSpiel(int id, string name1, string name2, string ergebnis1, string ergebnis2);
        public abstract bool SindMannschaftenAmSpieltagVorhanden(int teilnehmer1, int teilnehmer2, List<Mannschaft> liste, int spieltag);
        #endregion
    }
}
