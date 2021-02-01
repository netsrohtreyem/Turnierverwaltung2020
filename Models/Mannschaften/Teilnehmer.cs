using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Turnierverwaltung2020
{
    public abstract class Teilnehmer
    {
        #region Eigenschaften
        private int _id;
        private string _Name;
        private sportart _sportart;
        private int _punkte;
        private int _toreplus;
        private int _toreminus;
        private int _anzahlspiele;
        private int _gewonneneSpiele;
        private int _verloreneSpiele;
        private int _unentschieden;
        #endregion

        #region Accessoren/Modifier
        public int ID { get => _id; set => _id = value; }
        public string Name { get => _Name; set => _Name = value; }
        public sportart Sportart { get => _sportart; set => _sportart = value; }
        public int Punkte { get => _punkte; set => _punkte = value; }
        public int TorePlus { get => _toreplus; set => _toreplus = value; }
        public int Toreminus { get => _toreminus; set => _toreminus = value; }
        public int Anzahlspiele { get => _anzahlspiele; set => _anzahlspiele = value; }
        public int GewonneneSpiele { get => _gewonneneSpiele; set => _gewonneneSpiele = value; }
        public int VerloreneSpiele { get => _verloreneSpiele; set => _verloreneSpiele = value; }
        public int Unentschieden { get => _unentschieden; set => _unentschieden = value; }
        #endregion

        #region Konstruktoren
        public Teilnehmer()
        {
            ID = -1;
            Name = "noname";
            Sportart = new sportart("Fussball", true, false, 3, 0, 1);
        }

        public Teilnehmer(string nam, sportart sport, int anzspiele)
        {
            ID = -1;
            Name = nam;
            Sportart = sport;
            Anzahlspiele = anzspiele;
        }

        public Teilnehmer(Teilnehmer value)
        {
            ID = value.ID;
            Name = value.Name;
            Sportart = value.Sportart;
            Anzahlspiele = value.Anzahlspiele;
        }
        #endregion

        #region Worker
        public int CompareByID(Teilnehmer value)
        {
            if (ID > value.ID)
            {
                return 1;
            }
            else if (ID < value.ID)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        public abstract int CompareByName(Teilnehmer value);
        public abstract int CompareByVorname(Teilnehmer person);
        public abstract int CompareByGeb(Teilnehmer value);
        public abstract int CompareByEinsatz(Teilnehmer value);
        public abstract int CompareByAnzahlVereine(Teilnehmer value);
        public abstract int CompareByGewonneneSpiele(Teilnehmer value);
        public abstract int CompareByErzielteTore(Teilnehmer value);
        public abstract int CompareByAnzahlJahre(Teilnehmer value);
        public abstract int CompareByAnzahlspiele(Teilnehmer value);
        public abstract int CompareBySportart(Teilnehmer value);
        #endregion
    }
}