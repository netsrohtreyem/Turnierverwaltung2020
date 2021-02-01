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
    public class Handballspieler : Person
    {
        #region Eigenschaften
        private int _geworfenetore;
        private string _einsatzbereich;
        #endregion

        #region Accessoren/Modifier
        public int Geworfenetore { get => _geworfenetore; set => _geworfenetore = value; }
        public string Einsatzbereich { get => _einsatzbereich; set => _einsatzbereich = value; }
        #endregion

        #region Konstruktoren
        public Handballspieler() : base()
        {
            this.Geworfenetore = 0;
            this.Einsatzbereich = "non";
            this.Sportart = null;
        }

        public Handballspieler(string nam, string vornam, DateTime geb, int anz, string eins, int tore, sportart sport) : base(nam, vornam, geb, sport, anz)
        {
            this.Geworfenetore = tore;
            this.Einsatzbereich = eins;
            if (sport.name != "Handball")
            {
                throw (new Exception("Eine Handballspieler muss als Sportart Handball haben"));
            }
            else
            { }
        }
        public Handballspieler(Handballspieler value) : base(value)
        {
            this.Geworfenetore = value.Geworfenetore;
            this.Einsatzbereich = value.Einsatzbereich;
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
        public override int CompareByErzielteTore(Teilnehmer value)
        {
            if (value is Fussballspieler)
            {
                if (Geworfenetore > ((Fussballspieler)value).Geschossenentore)
                {
                    return 1;
                }
                else if (Geworfenetore < ((Fussballspieler)value).Geschossenentore)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else if (value is Handballspieler)
            {
                if (Geworfenetore > (((Handballspieler)value).Geworfenetore))
                {
                    return 1;
                }
                else if (Geworfenetore < (((Handballspieler)value).Geworfenetore))
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
                return Einsatzbereich.CompareTo(((Fussballspieler)value).Position);
            }
            else if (value is Handballspieler)
            {
                return Einsatzbereich.CompareTo(((Handballspieler)value).Einsatzbereich);
            }
            else if (value is AndereAufgaben)
            {
                return Einsatzbereich.CompareTo(((AndereAufgaben)value).Einsatz);
            }
            else if (value is Tennisspieler)
            {
                return Einsatzbereich.CompareTo("Spieler");
            }
            else if (value is WeitererSpieler)
            {
                return Einsatzbereich.CompareTo("Spieler");
            }
            else if (value is Physiotherapeut)
            {
                return Einsatzbereich.CompareTo("Physio");
            }
            else if (value is Trainer)
            {
                return Einsatzbereich.CompareTo("Trainer");
            }
            else
            {
                return -1;
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
        public override int CompareByAnzahlVereine(Teilnehmer value)
        {
            return -1;
        }
        public override int CompareByAnzahlJahre(Teilnehmer value)
        {
            return -1;
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
            this.Anzahlspiele = ((Handballspieler)edit).Anzahlspiele;
            this.Geworfenetore = ((Handballspieler)edit).Geworfenetore;
            this.Einsatzbereich = ((Handballspieler)edit).Einsatzbereich;
        }

        #endregion
    }
}
