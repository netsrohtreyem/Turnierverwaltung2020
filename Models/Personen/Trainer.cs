//Autor:            Meyer
//Datum:            3/2020
//Datei:            Trainer.cs
//Klasse:           AI118
//Beschreibung:     Klasse Trainer
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnierverwaltung2020
{
    public class Trainer : Person
    {
        #region Eigenschaften
        private int _anzahlvereine; //Erfahrung
        #endregion

        #region Accessoren/Modifier
        public int Anzahlvereine { get => _anzahlvereine; set => _anzahlvereine = value; }
        #endregion

        #region Konstruktoren
        public Trainer() : base()
        {
            this.Anzahlvereine = 0;
            this.Sportart = null;
        }

        public Trainer(string nam, string vornam, DateTime geb, int anz, sportart sport) : base(nam, vornam, geb, sport, 0)
        {
            this.Anzahlvereine = anz;
        }

        public Trainer(Trainer value) : base(value)
        {
            this.Anzahlvereine = value.Anzahlvereine;
        }
        #endregion

        #region Worker
        #region Vergleichsmethoden
        public override int CompareByName(Teilnehmer value)
        {
            return Name.CompareTo(value.Name);
        }
        public override int CompareByAnzahlVereine(Teilnehmer value)
        {
            if (value is Trainer)
            {
                if (Anzahlvereine > ((Trainer)value).Anzahlvereine)
                {
                    return 1;
                }
                else if (Anzahlvereine < ((Trainer)value).Anzahlvereine)
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
                return "Trainer".CompareTo(((Fussballspieler)value).Position);
            }
            else if (value is Handballspieler)
            {
                return "Trainer".CompareTo(((Handballspieler)value).Einsatzbereich);
            }
            else if (value is AndereAufgaben)
            {
                return "Trainer".CompareTo(((AndereAufgaben)value).Einsatz);
            }
            else if (value is Tennisspieler || value is WeitererSpieler)
            {
                return "Trainer".CompareTo("Spieler");
            }
            else if (value is Physiotherapeut)
            {
                return "Trainer".CompareTo("Physio");
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
            if (value is Trainer)
            {
                if (Anzahlvereine > ((Trainer)value).Anzahlvereine)
                {
                    return 1;
                }
                else if (Anzahlvereine < ((Trainer)value).Anzahlvereine)
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
                return 0;
            }
        }
        #endregion

        public override string GetListData()
        {
            return (ID + ", " + Name + ", " + Vorname + ", " + Geburtsdatum.ToShortDateString() + ", Trainer, " + Sportart.name);
        }
        public override void ChangeValues(Person edit)
        {
            this.ID = edit.ID;
            this.Name = edit.Name;
            this.Vorname = edit.Vorname;
            this.Geburtsdatum = edit.Geburtsdatum;
            this.Sportart = edit.Sportart;
            this.Anzahlvereine = ((Trainer)edit).Anzahlvereine;
        }

        #endregion
    }
}
