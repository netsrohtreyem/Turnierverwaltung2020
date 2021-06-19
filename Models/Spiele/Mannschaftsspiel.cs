using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020
{
    public class Mannschaftsspiel : Spiel
    {
        #region Eigenschaften
        private int _Spieltag;
        private Mannschaft _man1;
        private Mannschaft _man2;
        private int _ergebnis1;
        private int _ergebnis2;
        #endregion

        #region Accessoren/Modifier
        public Mannschaft Man1 { get => _man1; set => _man1 = value; }
        public Mannschaft Man2 { get => _man2; set => _man2 = value; }
        public int Ergebnis1 { get => _ergebnis1; set => _ergebnis1 = value; }
        public int Ergebnis2 { get => _ergebnis2; set => _ergebnis2 = value; }
        public int Spieltag { get => _Spieltag; set => _Spieltag = value; }

        #endregion

        #region Konstruktoren
        public Mannschaftsspiel() : base()
        {
            Man1 = null;
            Man2 = null;
            Ergebnis1 = -1;
            Ergebnis2 = -1;
            Spieltag = 0;
        }
        public Mannschaftsspiel(Turnier turn, Mannschaft man1, Mannschaft man2, int spieltag) : base(turn)
        {
            Man1 = man1;
            Man2 = man2;
            Ergebnis1 = -1;
            Ergebnis2 = -1;
            Spieltag = spieltag;
        }
        public Mannschaftsspiel(Mannschaftsspiel value) : base(value)
        {
            Man1 = value.Man1;
            Man2 = value.Man2;
            Ergebnis1 = value.Ergebnis1;
            Ergebnis2 = value.Ergebnis2;
            Spieltag = value.Spieltag;
        }

        #endregion

        #region Worker
        public override string getSpieltag()
        {
            return this.Spieltag.ToString();
        }

        public override string getMannschaftName1()
        {
            return Man1.Name;
        }

        public override string getMannschaftName2()
        {
            return Man2.Name;
        }

        public override string getErgebniswert1()
        {
            return Ergebnis1.ToString();
        }

        public override string getErgebniswert2()
        {
            return Ergebnis2.ToString();
        }

        public override int Get_Spieltag()
        {
            return Spieltag;
        }

        public override int getGruppe()
        {
            return -1;
        }

        public override Teilnehmer getTeilnehmer1()
        {
            return this.Man1;
        }

        public override Teilnehmer getTeilnehmer2()
        {
            return this.Man2;
        }

        public override void setErgebniswert1(string ergebnis1)
        {
            if (ergebnis1 != "")
            {
                this.Ergebnis1 = Convert.ToInt32(ergebnis1);
            }
            else
            { }
        }

        public override void setErgebniswert2(string ergebnis2)
        {
            if (ergebnis2 != "")
            {
                this.Ergebnis2 = Convert.ToInt32(ergebnis2);
            }
            else
            { }
        }

        public override bool TeilnehmerVorhanden(List<Teilnehmer> value)
        {
            bool gefunden = false;
           foreach(Teilnehmer tln in value)
            {
                if(tln.Name.Equals(this.Man1.Name) || tln.Name.Equals(this.Man2.Name))
                {
                    gefunden = true; 
                }
                else
                { }
            }
            return gefunden;
        }

        public override void SetTeilnehmer1(Teilnehmer man1)
        {
            this.Man1 = (Mannschaft)man1;
        }

        public override void SetTeilnehmer2(Teilnehmer man2)
        {
            this.Man2 = (Mannschaft)man2;
        }
        #endregion
    }
}
