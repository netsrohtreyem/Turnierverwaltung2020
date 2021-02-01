using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turnierverwaltung2020
{
    public class AndereAufgaben : Person
    {
        #region Eigenschaften
        private string _Einsatz;
        #endregion

        #region Accessoren/Modifier
        public string Einsatz { get => _Einsatz; set => _Einsatz = value; }
        #endregion

        #region Konstruktoren
        public AndereAufgaben() : base()
        {
            Einsatz = "nichts besonderes";
        }
        public AndereAufgaben(string name, string vornam, DateTime geb, sportart sport, string eins) : base(name, vornam, geb, sport, 0)
        {
            Einsatz = eins;
        }
        public AndereAufgaben(AndereAufgaben value) : base(value)
        {
            Einsatz = value.Einsatz;
        }
        #endregion

        #region Worker
        #region Vergleichsmethoden
        public override int CompareByName(Teilnehmer value)
        {
            int rueck = Name.CompareTo(value.Name);
            return rueck;
        }
        public override int CompareByEinsatz(Teilnehmer value)
        {
            if (value is Fussballspieler)
            {
                return Einsatz.CompareTo(((Fussballspieler)value).Position);
            }
            else if (value is Handballspieler)
            {
                return Einsatz.CompareTo(((Handballspieler)value).Einsatzbereich);
            }
            else if (value is AndereAufgaben)
            {
                return Einsatz.CompareTo(((AndereAufgaben)value).Einsatz);
            }
            else if (value is Tennisspieler || value is WeitererSpieler)
            {
                return Einsatz.CompareTo("Spieler");
            }
            else if (value is Physiotherapeut)
            {
                return Einsatz.CompareTo("Physio");
            }
            else if (value is Trainer)
            {
                return Einsatz.CompareTo("Trainer");
            }
            else
            {
                return -1;
            }
        }
        public override int CompareByAnzahlspiele(Teilnehmer value)
        {
            if (Anzahlspiele > value.Anzahlspiele)
            {
                return 1;
            }
            else if (Anzahlspiele < value.Anzahlspiele)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        public override int CompareByAnzahlVereine(Teilnehmer value)
        {
            if (value is Trainer)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public override int CompareByErzielteTore(Teilnehmer value)
        {
            if (value is Fussballspieler || value is Handballspieler)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public override int CompareByGewonneneSpiele(Teilnehmer value)
        {
            if (value is WeitererSpieler || value is Tennisspieler)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        public override int CompareByAnzahlJahre(Teilnehmer value)
        {
            if (value is Physiotherapeut)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        public override void ChangeValues(Person edit)
        {
            this.ID = edit.ID;
            this.Name = edit.Name;
            this.Vorname = edit.Vorname;
            this.Geburtsdatum = edit.Geburtsdatum;
            this.Sportart = edit.Sportart;
            this.Einsatz = ((AndereAufgaben)edit).Einsatz;
        }
        public override string GetListData()
        {
            return (ID + ", " + Name + ", " + Vorname + ", " + Geburtsdatum.ToShortDateString() + ", AndereAufgaben, " + Sportart.name);
        }

        #endregion
    }
}