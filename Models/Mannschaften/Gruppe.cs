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
        private Ranking _Tabelle;
        private List<Teilnehmer> _mitglieder;
        #endregion

        #region Accessoren/Modifier
        public Ranking Tabelle { get => _Tabelle; set => _Tabelle = value; }
        public List<Teilnehmer> Mitglieder { get => _mitglieder; set => _mitglieder = value; }
        #endregion

        #region Konstruktoren
        public Gruppe()
        {
            Mitglieder = new List<Teilnehmer>();
            Tabelle = new Ranking(this.Sportart, new List<string>(), new List<TableRow>());
        }
        public Gruppe(string nam, sportart sport) : base(nam, sport, 0)
        {
            Mitglieder = new List<Teilnehmer>();
            Tabelle = new Ranking(this.Sportart, new List<string>(), new List<TableRow>());
        }
        public Gruppe(Gruppe value) : base(value)
        {
            Mitglieder = new List<Teilnehmer>(value.Mitglieder);
            Tabelle = new Ranking(value.Tabelle);
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

        public override bool AddToDatabase(List<int> Mitgliederliste)
        {
            MySqlConnection Conn = new MySqlConnection();
            string MyConnectionString = "server=127.0.0.1;database=turnierverwaltung;uid=user;password=user";
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
            string SqlString = "select id from sportarten where Bezeichnung = '" + Sportart + "';";
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
            SqlString = "INSERT INTO gruppen " +
                               "(ID, Name, Sportart) " +
                               "VALUES (NULL, '" + Name + "', '" + sportartenid + "');";

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
                    string sqlstring2 = "INSERT INTO gruppenmitglieder (ID, Gruppe, Person) " +
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
            string MyConnectionString = "server=127.0.0.1;database=turnierverwaltung;uid=user;password=user";
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
            string SqlString = "DELETE FROM gruppenmitglieder WHERE gruppe = '" + ID + "';";
            MySqlCommand command = new MySqlCommand(SqlString, Conn);
            int anzahl = command.ExecuteNonQuery();

            if (anzahl >= 0)
            {
                ergebnis = true;
                SqlString = "DELETE FROM gruppen WHERE ID = '" + ID + "';";
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
            string MyConnectionString = "server=127.0.0.1;database=turnierverwaltung;uid=user;password=user";
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
            this.Sportart = spart;
            //TODO
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

            SqlString = "UPDATE gruppen SET Name = '" + name + "', Sportart = '" + sportartenid + "' " +
                        "WHERE gruppen.ID = '" + ID + "';";
            command = new MySqlCommand(SqlString, Conn);
            int anzahl = command.ExecuteNonQuery();
            if (anzahl > 0)
            {
                ergebnis = true;
                SqlString = " DELETE FROM gruppenmitglieder WHERE gruppe = '" + ID + "';";
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
                        string sqlstring2 = "INSERT INTO gruppenmitglieder (ID, gruppe, Person) " +
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

        public void MakeRanking(List<Spiel> spiele)
        {
            //Tabelle erstellen
            this.Tabelle = new Ranking(this.Sportart, new List<string>(), new List<TableRow>());

            this.Sportart.setTabelle(this.Tabelle, this.Mitglieder, spiele);
        }

        public override Ranking getRanking(List<Spiel> value)
        {
            MakeRanking(value);
            return this.Tabelle;
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

        public override bool isInDatabase()
        {
            bool ergebnis = false;
            string MyConnectionString = "server=127.0.0.1;database=turnierverwaltung;uid=user;password=user";
            MySqlConnection Conn;


            try
            {
                Conn = new MySqlConnection(MyConnectionString);
                Conn.Open();
            }
            catch (Exception)
            {
                return false;
            }
            string sqlstring = "select gruppen.Name,sportarten.Bezeichnung from gruppen JOIN sportarten WHERE gruppen.sportart = sportarten.ID;";
            MySqlCommand command = new MySqlCommand(sqlstring, Conn);

            MySqlDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                string name = rdr.GetValue(0).ToString();
                string sportart = rdr.GetValue(1).ToString();
                if (name.Equals(this.Name) && this.Sportart.name.Equals(sportart))
                {
                    ergebnis = true;
                    break;
                }
                else
                { }
            }

            rdr.Close();
            Conn.Close();
            return ergebnis;
        }
        #endregion
    }
}
