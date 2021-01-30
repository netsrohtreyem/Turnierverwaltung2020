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

        #region DB Functions
        public override bool DeleteFromDatabase()
        {
            MySqlConnection Conn = new MySqlConnection();
            string MyConnectionString = Properties.Settings.Default.Connectionstring;
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

                SqlString = "delete from fussballspieler where id = " + persid + ";";
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

            SqlString = "update fussballspieler set anzahlspiele = '" +
                        Anzahlspiele + "' , " +
                        "geschossenetore = '" +
                        Geschossenentore + "' , " +
                        "Position = '" +
                        Position + "' " +
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
                SqlString = "update personen set Name = '" + Name + "' , " +
                            "Vorname = '" + Vorname + "' , " +
                            "Geburtsdatum = '" + Geburtsdatum.ToShortDateString() + "' " +
                            "where id = '" + ID + "';";
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
        public override bool AddToDatabase(List<int> Mitgliederliste)
        {
            MySqlConnection Conn = new MySqlConnection();
            string MyConnectionString = Properties.Settings.Default.Connectionstring;
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
            string SqlString = "select id from sportarten where Bezeichnung = 'Fussball';";
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

            SqlString = "select id from personentypen where Bezeichnung = 'Fussballspieler';";
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
                SqlString = "insert into fussballspieler (ID,anzahlspiele,geschossenetore,Position,person) " +
                "VALUES (null,'" + this.Anzahlspiele + "', '" + this.Geschossenentore + "', '" + this.Position + "', '" + perid + "');";
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
                    int fussballid = (int)command.LastInsertedId;
                    SqlString = "update personen set Details='" + fussballid + "' where id='" + perid + "';";
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

        public override Ranking getRanking(List<Spiel> value)
        {
            return null;
        }
        #endregion
    }
}
