using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Turnierverwaltung2020
{
    public class Fussballspieler : Person
    {
        #region Eigenschaften
        private int _geschossenentore;
        private string _position;
        #endregion

        #region Accessoren/Modifier
        public int Geschossenentore { get => _geschossenentore; set => _geschossenentore = value; }
        public string Position { get => _position; set => _position = value; }
        #endregion

        #region Konstruktoren
        public Fussballspieler() : base()
        {
            this.Geschossenentore = 0;
            this.Position = "Stürmer";
            this.Sportart = null;
        }
        public Fussballspieler(string name, string vornam, DateTime geb, int anz, string pos, int gesch, sportart sport) : base(name, vornam, geb, sport, anz)
        {
            this.Geschossenentore = gesch;
            this.Position = pos;
            if (sport.name != "Fussball")
            {
                throw (new Exception("Eine Fussballspieler muss als Sportart Fussball haben"));
            }
            else
            { }
        }
        public Fussballspieler(Fussballspieler value) : base(value)
        {
            this.Geschossenentore = value.Geschossenentore;
            this.Position = value.Position;
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
                if (Geschossenentore > ((Fussballspieler)value).Geschossenentore)
                {
                    return 1;
                }
                else if (Geschossenentore < ((Fussballspieler)value).Geschossenentore)
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
                if (Geschossenentore > ((Handballspieler)value).Geworfenetore)
                {
                    return 1;
                }
                else if (Geschossenentore < ((Handballspieler)value).Geworfenetore)
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
                return Position.CompareTo(((Fussballspieler)value).Position);
            }
            else if (value is Handballspieler)
            {
                return Position.CompareTo(((Handballspieler)value).Einsatzbereich);
            }
            else if (value is AndereAufgaben)
            {
                return Position.CompareTo(((AndereAufgaben)value).Einsatz);
            }
            else if (value is WeitererSpieler)
            {
                return Position.CompareTo("Spieler");
            }
            else if (value is Tennisspieler)
            {
                return Position.CompareTo("Spieler");
            }
            else if (value is Trainer)
            {
                return Position.CompareTo("Trainer");
            }
            else if (value is Physiotherapeut)
            {
                return Position.CompareTo("Physio");
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
            return -1; ;
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
            this.Anzahlspiele = ((Fussballspieler)edit).Anzahlspiele;
            this.Geschossenentore = ((Fussballspieler)edit).Geschossenentore;
            this.Position = ((Fussballspieler)edit).Position;
        }

        #endregion
    }
}
