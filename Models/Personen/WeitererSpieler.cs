using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turnierverwaltung2020
{
    public class WeitererSpieler : Person
    {
        #region Eigenschaften

        #endregion

        #region Accessoren/Modifier

        #endregion

        #region Konstruktoren
        public WeitererSpieler() : base()
        {
            GewonneneSpiele = 0;
        }
        public WeitererSpieler(string name, string vornam, DateTime geb, sportart sport, int anz, int gew) : base(name, vornam, geb, sport, anz)
        {
            GewonneneSpiele = gew;
        }
        public WeitererSpieler(WeitererSpieler value) : base(value)
        {
            GewonneneSpiele = value.GewonneneSpiele;
        }
        #endregion

        #region Worker

        #region Vergleichsmethoden
        public override int CompareByName(Teilnehmer value)
        {
            int rueck = Name.CompareTo(value.Name);
            return rueck;
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
        public override int CompareByGewonneneSpiele(Teilnehmer value)
        {
            if (value is Tennisspieler)
            {
                if (GewonneneSpiele > ((Tennisspieler)value).GewonneneSpiele)
                {
                    return 1;
                }
                else if (GewonneneSpiele < ((Tennisspieler)value).GewonneneSpiele)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else if (value is WeitererSpieler)
            {
                if (GewonneneSpiele > ((WeitererSpieler)value).GewonneneSpiele)
                {
                    return 1;
                }
                else if (GewonneneSpiele < ((WeitererSpieler)value).GewonneneSpiele)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
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
        public override int CompareByEinsatz(Teilnehmer value)
        {
            if (value is Fussballspieler)
            {
                return "Spieler".CompareTo(((Fussballspieler)value).Position);
            }
            else if (value is Handballspieler)
            {
                return "Spieler".CompareTo(((Handballspieler)value).Einsatzbereich);
            }
            else if (value is AndereAufgaben)
            {
                return "Spieler".CompareTo(((AndereAufgaben)value).Einsatz);
            }
            else if (value is Physiotherapeut)
            {
                return "Spieler".CompareTo("Physio");
            }
            else if (value is Trainer)
            {
                return "Spieler".CompareTo("Trainer");
            }
            else
            {
                return -1;
            }
        }
        #endregion

        public override string GetListData()
        {
            return (ID + ", " + Name + ", " + Vorname + ", " + Geburtsdatum.ToShortDateString() + ", Spieler, " + Sportart.name);
        }
        public override void ChangeValues(Person edit)
        {
            this.ID = edit.ID;
            this.Name = edit.Name;
            this.Vorname = edit.Vorname;
            this.Geburtsdatum = edit.Geburtsdatum;
            this.Sportart = edit.Sportart;
            this.Anzahlspiele = ((WeitererSpieler)edit).Anzahlspiele;
            this.GewonneneSpiele = ((WeitererSpieler)edit).GewonneneSpiele;
        }
        public override int CompareByAnzahlVereine(Teilnehmer value)
        {
            return -1;
        }

        public override int CompareByAnzahlJahre(Teilnehmer value)
        {
            return -1; ;
        }
        #endregion
    }
}