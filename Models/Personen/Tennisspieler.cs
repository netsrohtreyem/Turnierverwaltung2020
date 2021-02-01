//Autor:            Meyer
//Datum:            3/2019
//Datei:            Handballspieler.cs
//Klasse:           AI118
//Beschreibung:     Klasse Handballspieler
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnierverwaltung2020
{
    public class Tennisspieler : Person
    {
        #region Eigenschaften

        #endregion

        #region Accessoren/Modifier

        #endregion

        #region Konstruktoren
        public Tennisspieler() : base()
        {
            this.GewonneneSpiele = 0;
            this.Sportart = null;
        }

        public Tennisspieler(string nam, string vornam, DateTime geb, int anz, int gewonnen, sportart sport) : base(nam, vornam, geb, sport, anz)
        {
            this.GewonneneSpiele = gewonnen;
            if (sport.name != "Tennis")
            {
                throw (new Exception("Eine Tennisspieler muss als Sportart Tennis haben"));
            }
            else
            { }
        }
        public Tennisspieler(Tennisspieler value) : base(value)
        {
            this.GewonneneSpiele = value.GewonneneSpiele;
        }
        #endregion

        #region Worker
        #region Vergleichsmethoden
        public override int CompareByName(Teilnehmer value)
        {
            return Name.CompareTo(value.Name);
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
            this.Anzahlspiele = ((Tennisspieler)edit).Anzahlspiele;
            this.GewonneneSpiele = ((Tennisspieler)edit).GewonneneSpiele;
        }

        public override int CompareByAnzahlVereine(Teilnehmer value)
        {
            return -1;
        }

        public override int CompareByAnzahlJahre(Teilnehmer value)
        {
            return -1;
        }
        #endregion
    }
}
