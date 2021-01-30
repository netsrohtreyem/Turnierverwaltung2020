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
    public class Mannschaft : Teilnehmer
    {
        #region Eigenschaften
        private List<Person> _mitglieder;
        #endregion

        #region Accessoren/Modifier
        public List<Person> Mitglieder { get => _mitglieder; set => _mitglieder = value; }
        #endregion

        #region Konstruktoren
        public Mannschaft() : base()
        {
            Mitglieder = new List<Person>();
            Punkte = 0;
            TorePlus = 0;
            Toreminus = 0;
            Anzahlspiele = 0;
            GewonneneSpiele = 0;
            VerloreneSpiele = 0;
            Unentschieden = 0;
        }
        public Mannschaft(string nam, sportart sport) : base(nam, sport, 0)
        {
            Mitglieder = new List<Person>();
            Punkte = 0;
            TorePlus = 0;
            Toreminus = 0;
            Anzahlspiele = 0;
            GewonneneSpiele = 0;
            VerloreneSpiele = 0;
            Unentschieden = 0;
        }
        public Mannschaft(Mannschaft value) : base(value)
        {
            Mitglieder = new List<Person>(value.Mitglieder);
            Punkte = value.Punkte;
            TorePlus = value.TorePlus;
            Toreminus = value.Toreminus;
            Anzahlspiele = value.Anzahlspiele;
            GewonneneSpiele = value.GewonneneSpiele;
            VerloreneSpiele = value.VerloreneSpiele;
            Unentschieden = value.Unentschieden;
        }
        #endregion

        #region Worker
        public void Add(Person value)
        {
            this.Mitglieder.Add(value);
        }
        public void Delete(Person value)
        {
            if (this.Mitglieder.Contains(value))
            {
                this.Mitglieder.Remove(value);
            }
            else
            {
            }
        }
        public bool contains(Person person)
        {
            foreach (Person pers in this.Mitglieder)
            {
                if (pers.ID == person.ID)
                {
                    return true;
                }
                else
                { }
            }
            return false;
        }
        public override Ranking getRanking(List<Spiel> value)
        {
            return null;
        }

        #region Sortieren
        //ToDo
        public override int CompareByEinsatz(Teilnehmer value)
        {
            return -1;
        }
        public override int CompareByAnzahlspiele(Teilnehmer value)
        {
            return -1;
        }
        public override int CompareByName(Teilnehmer value)
        {
            return -1;
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
            List<Person> Listenp = new List<Person>(this.Mitglieder);
            List<Person> Listes = new List<Person>();
            foreach (Person p in this.Mitglieder)
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
                                    if (this.Mitglieder[index2].CompareByVorname(this.Mitglieder[index2 + 1]) > 0)
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
                                    if (this.Mitglieder[index2].CompareByVorname(this.Mitglieder[index2 + 1]) < 0)
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
            Person temp = this.Mitglieder[index1];
            this.Mitglieder[index1] = this.Mitglieder[index2];
            this.Mitglieder[index2] = temp;
        }
        #endregion

        #region Database Functions
        public override bool AddToDatabase(List<int> Mitgliederliste)
        {
            MySqlConnection Conn = new MySqlConnection();
            string MyConnectionString = Properties.Settings.Default.Connectionstring;
            bool ergebnis = false;
            int sportartenid = -1;
            try
            {
                Conn = new MySqlConnection();
                Conn.ConnectionString = MyConnectionString;
                Conn.Open();
            }
            catch (MySqlException)
            {
                return true;
            }

            string SqlString = "select id from sportarten where Bezeichnung = '" + Sportart.name + "';";
            MySqlCommand command = new MySqlCommand(SqlString, Conn);
            MySqlDataReader rdr;
            try
            {
                rdr = command.ExecuteReader();
            }
            catch (Exception)
            {
                Conn.Close();
                return false;
            }
            rdr.Read();
            sportartenid = rdr.GetInt32(0);
            rdr.Close();

            try
            {
                Conn = new MySqlConnection();
                Conn.ConnectionString = MyConnectionString;
                Conn.Open();
            }
            catch (MySqlException)
            {
                return false;
            }

            SqlString = "INSERT INTO mannschaften " +
                        "(ID, Name, Sportart, Punkte, toreplus, toreminus, gewonnenespiele, verlorenespiele, unentschieden) " +
                        "VALUES " +
                        "(NULL, " + "'" + Name + "', '" + sportartenid + "', '" + Punkte + "', '" +
                         TorePlus + "', " + "'" + Toreminus + "', '" +
                         GewonneneSpiele + "', '" + VerloreneSpiele + "', '" + Unentschieden + "');";

            command = new MySqlCommand(SqlString, Conn);
            int anzahl = command.ExecuteNonQuery();

            if (anzahl > 0)
            {
                MySqlConnection Conn2;
                try
                {
                    Conn2 = new MySqlConnection();
                    Conn2.ConnectionString = MyConnectionString;
                    Conn2.Open();
                }
                catch (MySqlException)
                {
                    return false;
                }
                this.ID = (int)command.LastInsertedId;

                foreach (int index in Mitgliederliste)
                {
                    string sqlstring2 = "INSERT INTO mannschaftsmitglieder " +
                                        "(ID, Mannschaft, Person) " +
                                        "VALUES(NULL, '" + command.LastInsertedId + "', '" + index + "');";

                    MySqlCommand command2 = new MySqlCommand(sqlstring2, Conn2);
                    anzahl = command2.ExecuteNonQuery();
                    if (anzahl > 0)
                    {
                        ergebnis = true;
                    }
                    else
                    {
                        ergebnis = false;
                        break;
                    }
                }
                if (Mitgliederliste.Count <= 0)
                {
                    ergebnis = true;
                }
                else
                { }

                Conn2.Close();
            }
            else
            {
                ergebnis = false;
            }

            Conn.Close();
            return ergebnis;
        }
        public override bool DeleteFromDatabase()
        {
            MySqlConnection Conn = new MySqlConnection();
            string MyConnectionString = Properties.Settings.Default.Connectionstring;
            bool ergebnis = false;
            try
            {
                Conn = new MySqlConnection();
                Conn.ConnectionString = MyConnectionString;
                Conn.Open();
            }
            catch (MySqlException)
            {
                return true;
            }
            string SqlString = "DELETE FROM mannschaftsmitglieder WHERE mannschaft = '" + ID + "';";
            MySqlCommand command = new MySqlCommand(SqlString, Conn);
            int anzahl = command.ExecuteNonQuery();

            if (anzahl >= 0)
            {
                ergebnis = true;
                SqlString = "DELETE FROM mannschaften WHERE ID = '" + ID + "';";
                command = new MySqlCommand(SqlString, Conn);
                anzahl = command.ExecuteNonQuery();
                if (anzahl > 0)
                {
                    ergebnis = true;
                }
                else
                {
                    ergebnis = false;
                }
            }
            else
            {
                ergebnis = false;
            }
            Conn.Close();
            return ergebnis;
        }
        public override bool ChangeInDatabase(string name, sportart spart, List<int> Mitgliederliste)
        {
            MySqlConnection Conn = new MySqlConnection();
            string MyConnectionString = Properties.Settings.Default.Connectionstring;
            bool ergebnis = false;
            int sportartenid = -1;
            try
            {
                Conn = new MySqlConnection();
                Conn.ConnectionString = MyConnectionString;
                Conn.Open();
            }
            catch (MySqlException)
            {
                return true;
            }
            string SqlString = "select id from sportarten where Bezeichnung = '" + spart.name + "';";
            MySqlCommand command = new MySqlCommand(SqlString, Conn);
            MySqlDataReader rdr;
            try
            {
                rdr = command.ExecuteReader();
            }
            catch (Exception)
            {
                Conn.Close();
                return false;
            }
            rdr.Read();
            sportartenid = rdr.GetInt32(0);
            rdr.Close();
            this.Sportart.name = spart.name;

            try
            {
                Conn = new MySqlConnection();
                Conn.ConnectionString = MyConnectionString;
                Conn.Open();
            }
            catch (MySqlException)
            {
                return false;
            }

            SqlString = "UPDATE mannschaften SET Name = '" + name + "', Sportart = '" + sportartenid + "' " +
                        "WHERE mannschaften.ID = '" + ID + "';";

            command = new MySqlCommand(SqlString, Conn);
            int anzahl = command.ExecuteNonQuery();
            if (anzahl > 0)
            {
                ergebnis = true;
                SqlString = " DELETE FROM mannschaftsmitglieder WHERE Mannschaft = '" + ID + "';";
                command = new MySqlCommand(SqlString, Conn);
                anzahl = command.ExecuteNonQuery();
                if (anzahl >= 0)
                {
                    MySqlConnection Conn2;
                    try
                    {
                        Conn2 = new MySqlConnection();
                        Conn2.ConnectionString = MyConnectionString;
                        Conn2.Open();
                    }
                    catch (MySqlException)
                    {
                        return false;
                    }

                    foreach (int index in Mitgliederliste)
                    {
                        string sqlstring2 = "INSERT INTO mannschaftsmitglieder " +
                                            "(ID, Mannschaft, Person) " +
                                            "VALUES(NULL, '" + ID + "', '" + index + "');";

                        MySqlCommand command2 = new MySqlCommand(sqlstring2, Conn2);
                        anzahl = command2.ExecuteNonQuery();
                        if (anzahl > 0)
                        {
                            this.Name = name;
                            this.Sportart.name = spart.name;
                            ergebnis = true;
                        }
                        else
                        {
                            ergebnis = false;
                            break;
                        }
                    }
                    Conn2.Close();
                }
                else
                {
                    ergebnis = false;
                }
            }
            else
            {
                ergebnis = false;
            }
            Conn.Close();
            return ergebnis;
        }

        public override int CompareByAnzahlVereine(Teilnehmer value)
        {
            return -1;
        }

        public override int CompareByAnzahlJahre(Teilnehmer value)
        {
            return -1;
        }

        public override int CompareByGewonneneSpiele(Teilnehmer value)
        {
            return -1;
        }

        public override int CompareByErzielteTore(Teilnehmer value)
        {
            return -1;
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

        #endregion
    }
}
