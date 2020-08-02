using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnierverwaltung2020
{
    public abstract class Person : Teilnehmer
    {
        #region Eigenschaften
        private string _vorname;
        private DateTime _geburtsdatum;
        #endregion

        #region Accessoren/Modifier
        public string Vorname { get => _vorname; set => _vorname = value; }
        public DateTime Geburtsdatum { get => _geburtsdatum; set => _geburtsdatum = value; }
        #endregion

        #region Konstruktoren
        public Person() : base()
        {
            this.Vorname = "noname";
            this.Geburtsdatum = new DateTime();
        }
        public Person(string nam, string vornam, DateTime geb, sportart sport, int anzspiele) : base(nam, sport, anzspiele)
        {
            Vorname = vornam;
            Geburtsdatum = geb;
        }
        public Person(Person value) : base(value)
        {
            Vorname = value.Vorname;
            Geburtsdatum = value.Geburtsdatum;
        }
        #endregion

        #region Worker
        public string getSportart()
        {
            return Sportart.name;
        }
        public override int CompareBySportart(Teilnehmer value)
        {
            return Sportart.name.CompareTo(value.Sportart.name);
        }
        public override int CompareByVorname(Teilnehmer person)
        {
            return this.Vorname.CompareTo(((Person)person).Vorname);
        }
        public override int CompareByGeb(Teilnehmer value)
        {
            return -1;
        }
        public abstract string GetListData();
        public abstract void ChangeValues(Person edit);
        #endregion
    }
}
