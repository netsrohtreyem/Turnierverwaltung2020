//Autor:            Meyer
//Datum:            3/2019
//Datei:            Station.cs
//Klasse:           AI118
//Beschreibung:     Klasse Fahrradstation
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020
{
    public class Gruppe : Teilnehmer
    {
        #region Eigenschaften
        private List<Teilnehmer> _mitglieder;
        #endregion

        #region Accessoren/Modifier
        public List<Teilnehmer> Mitglieder { get => _mitglieder; set => _mitglieder = value; }
        #endregion

        #region Konstruktoren
        public Gruppe()
        {
            Mitglieder = new List<Teilnehmer>();
        }
        public Gruppe(string nam, sportart sport) : base(nam, sport, 0)
        {
            Mitglieder = new List<Teilnehmer>();
        }
        public Gruppe(Gruppe value) : base(value)
        {
            Mitglieder = new List<Teilnehmer>(value.Mitglieder);
        }
        #endregion

        #region Worker
        public void Add(Teilnehmer value)
        {
            this.Mitglieder.Add(value);
        }
        public void Delete(Teilnehmer value)
        {
            if (this.Mitglieder.Contains(value))
            {
                this.Mitglieder.Remove(value);
            }
            else
            {
            }
        }
        //ToDo
        public override int CompareByName(Teilnehmer value)
        {
            return -1;
        }
        public override int CompareByAnzahlspiele(Teilnehmer value)
        {
            return -1; ;
        }
        public void QuickSort(int richtung, int kriterium)
        {
            throw new NotImplementedException();
        }
        public void SelectionSort(int richtung, int kriterium)
        {
            throw new NotImplementedException();
        }
        public void BubbleSort(int richtung, int kriterium)
        {
            List<Teilnehmer> Listenp = new List<Teilnehmer>(this.Mitglieder);
            List<Teilnehmer> Listes = new List<Teilnehmer>();
            foreach (Teilnehmer p in this.Mitglieder)
            {
                Listenp.Remove(p);
                Listes.Add(p);
            }


            for (int index1 = 0; index1 < this.Mitglieder.Count - 1; index1++)
            {
                for (int index2 = 0; index2 < this.Mitglieder.Count - 1; index2++)
                {
                    if (richtung == 0)//aufwärts
                    {
                        switch (kriterium)
                        {
                            case 0: //nach Name
                                if (this.Mitglieder[index2].CompareByName(this.Mitglieder[index2 + 1]) > 0)
                                {
                                    TauscheElement(index2, index2 + 1);
                                }
                                else if (this.Mitglieder[index2].CompareByName(this.Mitglieder[index2 + 1]) == 0) //identisch dann nach Vorname
                                {
                                    if (((Person)this.Mitglieder[index2]).CompareByVorname(((Person)this.Mitglieder[index2 + 1])) > 0)
                                    {
                                        TauscheElement(index2, index2 + 1);
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                { }
                                break;
                        }
                    }
                    else //abwärts
                    {
                        switch (kriterium)
                        {
                            case 0: //nach Name
                                if (this.Mitglieder[index2].CompareByName(this.Mitglieder[index2 + 1]) < 0)
                                {
                                    TauscheElement(index2, index2 + 1);
                                }
                                else if (this.Mitglieder[index2].CompareByName(this.Mitglieder[index2 + 1]) == 0) //identisch dann nach Vorname
                                {
                                    if (((Person)this.Mitglieder[index2]).CompareByVorname(((Person)this.Mitglieder[index2 + 1])) < 0)
                                    {
                                        TauscheElement(index2, index2 + 1);
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                { }
                                break;
                        }
                    }
                }
            }
        }
        private void TauscheElement(int index1, int index2)
        {
            Teilnehmer temp = this.Mitglieder[index1];
            this.Mitglieder[index1] = this.Mitglieder[index2];
            this.Mitglieder[index2] = temp;
        }
        public bool contains(Person person)
        {
            foreach (Person pers in this.Mitglieder)
            {
                if (pers.ID == person.ID && pers.Name == person.Name && pers.Vorname == person.Vorname && pers.Geburtsdatum == person.Geburtsdatum)
                {
                    return true;
                }
                else
                { }
            }
            return false;
        }

        public override int CompareByEinsatz(Teilnehmer value)
        {
            return -1;
        }

        public override int CompareByAnzahlVereine(Teilnehmer value)
        {
            return -1;
        }

        public override int CompareByAnzahlJahre(Teilnehmer value)
        {
            return -1; ;
        }

        public override int CompareByGewonneneSpiele(Teilnehmer value)
        {
            return -1;
        }

        public override int CompareByErzielteTore(Teilnehmer value)
        {
            return -1; ;
        }

        public override int CompareByVorname(Teilnehmer person)
        {
            return -1; ;
        }

        public override int CompareByGeb(Teilnehmer value)
        {
            return -1; ;
        }

        public override int CompareBySportart(Teilnehmer value)
        {
            return -1; ;
        }
        #endregion
    }
}
