using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020
{
    public abstract class Spiel
    {
        #region Eigenschaften
        private int _id;
        private int _Turnierid;
        #endregion

        #region Accessoren/Modifier
        public int ID { get => _id; set => _id = value; }
        public int Turnier { get => _Turnierid; set => _Turnierid = value; }

        #endregion

        #region Konstruktoren
        public Spiel()
        {
            this.ID = -1;
            this.Turnier = -1;
        }
        public Spiel(Turnier turn)
        {
            this.ID = -1;
            this.Turnier = turn.ID;
        }
        public Spiel(Spiel value)
        {
            this.ID = value.ID;
            this.Turnier = value.Turnier;
        }
        #endregion

        #region Worker
        public abstract string getSpieltag();
        public abstract string getMannschaftName1();
        public abstract string getMannschaftName2();
        public abstract string getErgebniswert1();
        public abstract string getErgebniswert2();
        public abstract int Get_Spieltag();
        public abstract int getGruppe();
        public abstract object getTeilnehmer1();
        public abstract object getTeilnehmer2();
        public abstract void setErgebniswert1(string ergebnis1);
        public abstract void setErgebniswert2(string ergebnis2);
        public abstract bool TeilnehmerVorhanden(List<Teilnehmer> value);
        #endregion
    }
}
