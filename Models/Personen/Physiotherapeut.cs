//Autor:            Meyer
//Datum:            3/2020
//Datei:            Physiotherapeut.cs
//Klasse:           AI118
//Beschreibung:     Klasse Physiotherapeut
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnierverwaltung2020
{
    public class Physiotherapeut : Person
    {
        #region Eigenschaften
        private int _anzahljahre; //Erfahrung
        #endregion

        #region Accessoren/Modifier
        public int Anzahljahre { get => _anzahljahre; set => _anzahljahre = value; }
        #endregion

        #region Konstruktoren
        public Physiotherapeut() : base()
        {
            this.Anzahljahre = 0;
        }

        public Physiotherapeut(string nam, string vornam, DateTime geb, int anz, sportart sport) : base(nam, vornam, geb, sport, 0)
        {
            this.Anzahljahre = anz;
        }

        public Physiotherapeut(Physiotherapeut value) : base(value)
        {
            this.Anzahljahre = value.Anzahljahre;
        }
        #endregion

        #region Worker
        #region Vergleichsmethoden
        public override int CompareByName(Teilnehmer value)
        {
            return Name.CompareTo(value.Name);
        }
        public override int CompareByEinsatz(Teilnehmer value)
        {
            if (value is Fussballspieler)
            {
                return "Physio".CompareTo(((Fussballspieler)value).Position);
            }
            else if (value is Handballspieler)
            {
                return "Physio".CompareTo(((Handballspieler)value).Einsatzbereich);
            }
            else if (value is Tennisspieler || value is WeitererSpieler)
            {
                return "Physio".CompareTo("Spieler");
            }
            else if (value is Trainer)
            {
                return "Physio".CompareTo("Trainer");
            }
            else if (value is AndereAufgaben)
            {
                return "Physio".CompareTo(((AndereAufgaben)value).Einsatz);
            }
            else
            {
                return 0;
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
                if (Anzahljahre > ((Physiotherapeut)value).Anzahljahre)
                {
                    return 1;
                }
                else if (Anzahljahre < ((Physiotherapeut)value).Anzahljahre)
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
        #endregion

        public override string GetListData()
        {
            return (ID + ", " + Name + ", " + Vorname + ", " + Geburtsdatum.ToShortDateString() + ", Physiotherapeut, " + Sportart.name);
        }
        public override void ChangeValues(Person edit)
        {
            this.ID = edit.ID;
            this.Name = edit.Name;
            this.Vorname = edit.Vorname;
            this.Geburtsdatum = edit.Geburtsdatum;
            this.Sportart = edit.Sportart;
            this.Anzahljahre = ((Physiotherapeut)edit).Anzahljahre;
        }

        #endregion
    }
}
