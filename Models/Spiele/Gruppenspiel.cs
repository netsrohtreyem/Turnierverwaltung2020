using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020
{
    public class Gruppenspiel : Spiel
    {
        #region Eigenschaften
        private int _GruppenID;
        private Teilnehmer _teilnehmer1;
        private Teilnehmer _teilnehmer2;
        private int _Ergebnis1;
        private int _Ergebnis2;
        #endregion

        #region Accessoren/Modifier
        public int Ergebnis2 { get => _Ergebnis2; set => _Ergebnis2 = value; }
        public int Ergebnis1 { get => _Ergebnis1; set => _Ergebnis1 = value; }
        public Teilnehmer Teilnehmer1 { get => _teilnehmer1; set => _teilnehmer1 = value; }
        public Teilnehmer Teilnehmer2 { get => _teilnehmer2; set => _teilnehmer2 = value; }
        public int GruppenID { get => _GruppenID; set => _GruppenID = value; }

        #endregion

        #region Konstruktoren
        public Gruppenspiel() : base()
        {
            Teilnehmer1 = null;
            Teilnehmer2 = null;
            Ergebnis1 = -1;
            Ergebnis2 = -1;
            GruppenID = -1;
        }
        public Gruppenspiel(Turnier turn, int GrpID, Teilnehmer pers1, Teilnehmer pers2) : base(turn)
        {
            Teilnehmer1 = pers1;
            Teilnehmer2 = pers2;
            Ergebnis1 = -1;
            Ergebnis2 = -1;
            GruppenID = GrpID;
        }
        public Gruppenspiel(Gruppenspiel value) : base(value)
        {
            Teilnehmer1 = value.Teilnehmer1;
            Teilnehmer2 = value.Teilnehmer2;
            Ergebnis1 = value.Ergebnis1;
            Ergebnis2 = value.Ergebnis2;
            GruppenID = value.GruppenID;
        }
        #endregion

        #region Worker

        public override string getSpieltag()
        {
            return "";
        }

        public override string getMannschaftName1()
        {
            return (this.Teilnehmer1.Name + ", " + ((Person)this.Teilnehmer1).Vorname);
        }

        public override string getMannschaftName2()
        {
            return (this.Teilnehmer2.Name + ", " + ((Person)this.Teilnehmer2).Vorname);
        }

        public override string getErgebniswert1()
        {
            return this.Ergebnis1.ToString();
        }

        public override string getErgebniswert2()
        {
            return this.Ergebnis2.ToString();
        }

        public override int Get_Spieltag()
        {
            return -1;
        }

        public override int getGruppe()
        {
            return GruppenID;
        }

        public override Teilnehmer getTeilnehmer1()
        {
            return this.Teilnehmer1;
        }

        public override Teilnehmer getTeilnehmer2()
        {
            return this.Teilnehmer2;
        }

        public override void setErgebniswert1(string ergebnis1)
        {
            this.Ergebnis1 = Convert.ToInt32(ergebnis1);
        }

        public override void setErgebniswert2(string ergebnis2)
        {
            this.Ergebnis2 = Convert.ToInt32(ergebnis2);
        }

        public override bool TeilnehmerVorhanden(List<Teilnehmer> value)
        {
            bool gefunden = false;
            foreach (Teilnehmer tln in value)
            {
                if (tln.Name.Equals(this.Teilnehmer1.Name) || tln.Name.Equals(this.Teilnehmer2.Name))
                {
                    gefunden = true;
                }
                else
                { }
            }
            return gefunden;
        }

        public override void SetTeilnehmer1(Teilnehmer tln1)
        {
            this.Teilnehmer1 = tln1;
        }

        public override void SetTeilnehmer2(Teilnehmer tln2)
        {
            this.Teilnehmer2 = tln2;
        }
        #endregion
    }
}
