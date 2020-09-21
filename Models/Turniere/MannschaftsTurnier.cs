using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020
{
    public class MannschaftsTurnier : Turnier
    {
        #region Eigenschaften
        private List<Teilnehmer> _Teilnehmer;
        private int _anzahlTeilnehmer;
        private int _maxSpieltag;
        private Ranking _Tabelle;
        #endregion

        #region Accessoren/Modifier
        public List<Teilnehmer> Teilnehmer { get => _Teilnehmer; set => _Teilnehmer = value; }
        public int AnzahlTeilnehmer { get => _anzahlTeilnehmer; set => _anzahlTeilnehmer = value; }
        public int MaxSpieltag { get => _maxSpieltag; set => _maxSpieltag = value; }
        public Ranking Tabelle { get => _Tabelle; set => _Tabelle = value; }
        #endregion

        #region Konstruktoren
        public MannschaftsTurnier() : base()
        {
            this.Teilnehmer = new List<Teilnehmer>();
            this.MaxSpieltag = 0;
            this.AnzahlTeilnehmer = 0;
        }
        public MannschaftsTurnier(string bez, sportart sport, List<Mannschaft> Teiln) : base(bez, sport)
        {
            this.Teilnehmer = new List<Teilnehmer>(Teiln);
            this.AnzahlTeilnehmer = this.Teilnehmer.Count;
            this.MaxSpieltag = 0;
        }
        public MannschaftsTurnier(string bez, sportart sport) : base(bez, sport)
        {
            this.Teilnehmer = new List<Teilnehmer>();
            this.AnzahlTeilnehmer = this.Teilnehmer.Count;
            this.MaxSpieltag = 0;
        }
        public MannschaftsTurnier(MannschaftsTurnier value) : base(value)
        {
            this.Teilnehmer = new List<Teilnehmer>(value.Teilnehmer);
            this.AnzahlTeilnehmer = value.AnzahlTeilnehmer;
            this.MaxSpieltag = 0;
        }

        #endregion

        #region Worker
        public override bool AddToDatabase()
        {
            bool ergebnis = false;
            MySqlConnection Conn = new MySqlConnection();
            string MyConnectionString = "server=127.0.0.1;database=turnierverwaltung;uid=user;password=user";
            int sportartenid = -1;

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
            string SqlString = "select id from sportarten where Bezeichnung = '" + this.Sportart + "' ;";
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

            SqlString = "insert into turnier (ID,Bezeichnung,Sportart,Typ) " +
            "VALUES (null,'" + this.Bezeichnung + "', " + sportartenid + " , 0);";

            command = new MySqlCommand(SqlString, Conn);
            int anzahl = command.ExecuteNonQuery();

            if (anzahl > 0)
            {
                int turnierid = (int)command.LastInsertedId;
                this.ID = turnierid;
                foreach (Mannschaft man in this.Teilnehmer)
                {
                    SqlString = "insert into turnierteilnehmer (ID,Mannschaft,Gruppe,Turnier) " +
                    "VALUES (null,'" + man.ID + "', null, '" + turnierid + "');";
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
                        ergebnis = true;
                    }
                }
            }
            else
            {
                ergebnis = false;
            }

            Conn.Close();
            return ergebnis;
        }

        public override bool DeleteFromDB()
        {
            bool ergebnis = false;
            MySqlConnection Conn = new MySqlConnection();
            string MyConnectionString = "server=127.0.0.1;database=turnierverwaltung;uid=user;password=user";

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

            string SqlString = "delete from turnierteilnehmer where turnier = '" + this.ID + "' ;";
            MySqlCommand command = new MySqlCommand(SqlString, Conn);

            int anzahl = command.ExecuteNonQuery();

            if (anzahl < 0)
            {
                return false;
            }
            else
            { }

            SqlString = "delete from turnier where ID = '" + this.ID + "' ;";
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

            return ergebnis;
        }

        public override bool ChangeInDB()
        {
            bool ergebnis = false;
            MySqlConnection Conn = new MySqlConnection();
            string MyConnectionString = "server=127.0.0.1;database=turnierverwaltung;uid=user;password=user";

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

            string SqlString = "update turnier " +
                               "set Bezeichnung = '" + this.Bezeichnung + "' , " +
                               "Sportart = (select id from sportarten where bezeichnung = '" + this.Sportart + "') , " +
                               "Typ = '0' " +
                               "where ID = '" + this.ID + "';";
            MySqlCommand command = new MySqlCommand(SqlString, Conn);

            int anzahl = command.ExecuteNonQuery();

            if (anzahl < 0)
            {
                return false;
            }
            else
            {
                ergebnis = true;
            }

            SqlString = "delete from turnierteilnehmer where turnier = '" + this.ID + "' ;";
            command = new MySqlCommand(SqlString, Conn);
            anzahl = command.ExecuteNonQuery();

            if (anzahl < 0)
            {
                return false;
            }
            else
            {
                ergebnis = true;
            }

            foreach (Mannschaft man in this.Teilnehmer)
            {
                SqlString = "insert into turnierteilnehmer " +
                            "(ID,Mannschaft,Gruppe,Turnier) " +
                            "values(null,'" + man.ID + "' , null , '" + this.ID + "');";
                command = new MySqlCommand(SqlString, Conn);
                anzahl = command.ExecuteNonQuery();
            }

            return ergebnis;
        }

        public override int getAnzahlTeilnehmer()
        {
            return this.Teilnehmer.Count;
        }

        public override List<Teilnehmer> getTeilnemer()
        {
            return this.Teilnehmer;

        }

        public override void clearTeilnehmer()
        {
            this.Teilnehmer.Clear();
        }

        public override void addTeilnehmer(object value)
        {
            this.Teilnehmer.Add((Mannschaft)value);
            this.AnzahlTeilnehmer++;
        }

        public override string GetTypus()
        {
            return "Mannschaften";
        }

        public override void addSpiel(int spieltag, object mannschaft1, object mannschaft2)
        {
            Spiel neu = new Mannschaftsspiel(this, ((Mannschaft)mannschaft1), ((Mannschaft)mannschaft1), spieltag);
            if (neu.AddToDatabase())
            {
                if (neu.ID == -1)
                {
                    neu.ID = this.Spiele.Count + 1;
                }
                else
                {
                }
                this.Spiele.Add(neu);
                if (MaxSpieltag < spieltag)
                {
                    MaxSpieltag = spieltag;
                }
                else
                { }
            }
            else
            { }
        }
        public override void addSpiel(Spiel neu)
        {
            if (neu.AddToDatabase())
            {
                if (neu.ID == -1)
                {
                    neu.ID = this.Spiele.Count + 1;
                }
                else
                {
                }
                this.Spiele.Add(neu);
                if (MaxSpieltag < ((Mannschaftsspiel)neu).Spieltag)
                {
                    MaxSpieltag = ((Mannschaftsspiel)neu).Spieltag;
                }
                else
                { }
            }
            else
            { }
        }
        public override string getSpieleBezeichnung()
        {
            return "Spieltag";
        }

        public override string getTeilnehmerbezeichnung()
        {
            return "Mannschaft";
        }

        public override Teilnehmer getGruppe(int v)
        {
            return null;
        }

        public override int Get_MaxRunden()
        {
            return MaxSpieltag;
        }


        public override bool isSpielVorhanden(Spiel search)
        {
            foreach (Spiel sp in this.Spiele)
            {
                if (search.getMannschaftName1().Equals(sp.getMannschaftName1()) &&
                    search.getMannschaftName2().Equals(sp.getMannschaftName2()) &&
                    search.Get_Spieltag() == sp.Get_Spieltag())
                {
                    return true;
                }
                else
                { }
            }
            return false;
        }

        public override int getSelectedGruppe()
        {
            return -1;
        }

        public override void ClearSpiele(int value)
        {
            this.Spiele.Clear();
            this.MaxSpieltag = 0;
        }

        public override void setSelectedGruppe(int v)
        {
        }

        public override void SetMaxSpieltag(int v)
        {
            this.MaxSpieltag = v;
        }
        //Bestimmt die Anzahl der Personen in einer Mannschaft (value) oder alle value = -1
        public override int getAnzahlPersonenteilnehmer(int value)
        {
            int ergebnis = 0;

            if (value == -1)
            {
                foreach (Mannschaft man in this.Teilnehmer)
                {
                    ergebnis += man.Mitglieder.Count;
                }
            }
            else
            {
                ergebnis = ((Mannschaft)Teilnehmer[value - 1]).Mitglieder.Count;
            }
            return ergebnis;
        }

        public override void ChangeSpiel(int id, string name1, string name2, string ergebnis1, string ergebnis2)
        {
            foreach (Spiel sp in this.Spiele)
            {
                if (sp.ID == id && sp.Turnier.ID == this.ID)
                {
                    sp.setErgebniswert1(ergebnis1);
                    sp.setErgebniswert2(ergebnis2);
                }
                else
                { }
            }
        }

        public void MakeRanking()
        {
            //Tabelle erstellen
            this.Tabelle = new Ranking(this.Sportart, new List<string>(), new List<TableRow>());

            this.Sportart.setTabelle(this.Tabelle, this.Teilnehmer, this.Spiele);
        }


        public override Ranking GetRanking(int value)
        {
            MakeRanking();
            return this.Tabelle;
        }
        #endregion
    }
}
