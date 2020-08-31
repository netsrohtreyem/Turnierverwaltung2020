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

        #region DB Functions
        public override bool AddToDatabase(List<int> Mitgliederliste)
        {
            MySqlConnection Conn = new MySqlConnection();
            string MyConnectionString = "server=127.0.0.1;database=turnierverwaltung;uid=user;password=user";
            bool ergebnis = false;
            int sportartenid = -1;
            int typid = -1;

            try
            {
                Conn = new MySqlConnection();
                Conn.ConnectionString = MyConnectionString;
                Conn.Open();
            }
            catch (MySqlException)
            {
                return true;//Datenbank nicht verfügbar true damit Objekt im Controller gespeichert wird
            }

            string SqlString = "select id from sportarten where Bezeichnung = 'Handball';";
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

            SqlString = "select id from personentypen where Bezeichnung = 'Handballspieler';";
            command = new MySqlCommand(SqlString, Conn);
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
            typid = rdr.GetInt32(0);
            rdr.Close();

            SqlString = "insert into personen (ID,Name,Vorname,Geburtsdatum,Sportart,Typ,Details) " +
            "VALUES (null,'" + this.Name + "','" + this.Vorname + "','" + this.Geburtsdatum.ToShortDateString() + "', '" + sportartenid + "', '" + typid + "', null);";

            command = new MySqlCommand(SqlString, Conn);
            int anzahl = command.ExecuteNonQuery();

            if (anzahl > 0)
            {
                int perid = (int)command.LastInsertedId;
                this.ID = perid;
                SqlString = "insert into handballspieler (ID,anzahlspiele,geworfenetore,Einsatzbereich,person) " +
                "VALUES (null,'" + this.Anzahlspiele + "', '" + this.Geworfenetore + "', '" + this.Einsatzbereich + "', '" + perid + "');";
                command = new MySqlCommand(SqlString, Conn);
                try
                {
                    anzahl = command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Conn.Close();
                    return false;
                }
                if (anzahl > 0)
                {
                    int handballid = (int)command.LastInsertedId;
                    SqlString = "update personen set Details='" + handballid + "' where id='" + perid + "';";
                    command = new MySqlCommand(SqlString, Conn);
                    try
                    {
                        anzahl = command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        Conn.Close();
                        return false;
                    }
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
            int persid = -1;
            if (ID > -1)
            {
                try
                {
                    Conn = new MySqlConnection();
                    Conn.ConnectionString = MyConnectionString;
                    Conn.Open();
                }
                catch (MySqlException)
                {
                    return true;//Datenbank nicht verfügbar true damit Objekt im Controller gespeichert wird
                }

                string SqlString = "select details from personen where id = " + ID + ";";
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
                persid = rdr.GetInt32(0);
                rdr.Close();

                SqlString = "delete from handballspieler where id = " + persid + ";";
                command = new MySqlCommand(SqlString, Conn);
                int anzahl = -1;
                try
                {
                    anzahl = command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Conn.Close();
                    return false;
                }

                if (anzahl > 0)
                {
                    SqlString = "delete from personen where id = " + ID + ";";
                    command = new MySqlCommand(SqlString, Conn);
                    try
                    {
                        anzahl = command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        Conn.Close();
                        return false;
                    }
                    if (anzahl > 0)
                    {
                        Conn.Close();
                        return true;
                    }
                    else
                    {
                        Conn.Close();
                        return false;
                    }
                }
                else
                {
                    Conn.Close();
                    return false;
                }
            }
            else
            {
                Conn.Close();
                return true;
            }
        }
        public override bool ChangeInDatabase(string name, sportart spart, List<int> Mitgliederliste)
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
                return true;//Datenbank nicht verfügbar true damit Objekt im Controller gespeichert wird
            }


            string SqlString = "select details from personen where id = " + ID + ";";
            MySqlCommand command = new MySqlCommand(SqlString, Conn);
            MySqlDataReader rdr = null;

            try
            {
                rdr = command.ExecuteReader();
            }
            catch (Exception)
            {
                Conn.Close();
                return false;
            }

            if (rdr.HasRows)
            {
                rdr.Read();
            }
            else
            {
                Conn.Close();
                return false;
            }
            int persid = rdr.GetInt32(0);
            rdr.Close();

            SqlString = "update handballspieler set " +
                        "anzahlspiele = '" +
                        Anzahlspiele + "' , " +
                        "geworfenetore = '" +
                        Geworfenetore + "' , " +
                        "einsatzbereich = '" +
                        Einsatzbereich + "' " +
                        " where id = '" + persid + "';";
            command = new MySqlCommand(SqlString, Conn);
            int anzahl = -1;
            try
            {
                anzahl = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Conn.Close();
                return false;
            }

            if (anzahl > 0)
            {
                SqlString = "update personen set " +
                            "Name = '" + Name + "' , " +
                            "Vorname = '" + Vorname + "' , " +
                            "Geburtsdatum = '" + Geburtsdatum.ToShortDateString() + "' " +
                            "where id = " + ID + ";";
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

        public override Ranking getRanking(List<Spiel> value)
        {
            return null;
        }

        public override bool isInDatabase()
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
