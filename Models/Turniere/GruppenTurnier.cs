using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020
{
    public class GruppenTurnier : Turnier
    {
        #region Eigenschaften
        private List<Teilnehmer> _Gruppen;
        private int _anzahlGruppen;
        private int _selectedGruppe;
        #endregion

        #region Accessoren/Modifier
        public List<Teilnehmer> Gruppen { get => _Gruppen; set => _Gruppen = value; }
        public int AnzahlTeilnehmer { get => AnzahlGruppen; set => AnzahlGruppen = value; }
        public int AnzahlGruppen { get => _anzahlGruppen; set => _anzahlGruppen = value; }
        public int SelectedGruppe { get => _selectedGruppe; set => _selectedGruppe = value; }
        #endregion

        #region Konstruktoren
        public GruppenTurnier() : base()
        {
            this.Gruppen = new List<Teilnehmer>();
            this.AnzahlTeilnehmer = 0;
        }
        public GruppenTurnier(string bez, sportart sport, List<Gruppe> Teiln) : base(bez, sport)
        {
            this.Gruppen = new List<Teilnehmer>(Teiln);
            this.AnzahlTeilnehmer = this.Gruppen.Count;
        }
        public GruppenTurnier(string bez, sportart sport) : base(bez, sport)
        {
            this.Gruppen = new List<Teilnehmer>();
            this.AnzahlTeilnehmer = this.Gruppen.Count;
        }
        public GruppenTurnier(GruppenTurnier value) : base(value)
        {
            this.Gruppen = new List<Teilnehmer>(value.Gruppen);
            this.AnzahlTeilnehmer = value.AnzahlTeilnehmer;
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
            "VALUES (null,'" + this.Bezeichnung + "', " + sportartenid + " , 1 );";

            command = new MySqlCommand(SqlString, Conn);
            int anzahl = command.ExecuteNonQuery();

            if (anzahl > 0)
            {
                int turnierid = (int)command.LastInsertedId;
                this.ID = turnierid;
                foreach (Gruppe grp in this.Gruppen)
                {
                    SqlString = "insert into turnierteilnehmer (ID,Mannschaft,Gruppe,Turnier) " +
                    "VALUES (null, null, '" + grp.ID + "', '" + turnierid + "');";
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
                               "Typ = '1' " +
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

            foreach (Gruppe grp in this.Gruppen)
            {
                SqlString = "insert into turnierteilnehmer " +
                            "(ID,Mannschaft,Gruppe,Turnier) " +
                            "values(null, null , '" + grp.ID + "' , '" + this.ID + "');";
                command = new MySqlCommand(SqlString, Conn);
                anzahl = command.ExecuteNonQuery();
            }
            return ergebnis;
        }

        public override int getAnzahlTeilnehmer()
        {
            this.AnzahlGruppen = this.Gruppen.Count;
            return this.Gruppen.Count;
        }
        public override int getAnzahlPersonenteilnehmer(int value)
        {
            int ergebnis = 0;

            ergebnis = ((Gruppe)this.Gruppen[value - 1]).Mitglieder.Count;

            return ergebnis;
        }

        public override List<Teilnehmer> getTeilnemer()
        {
            return this.Gruppen;
        }

        public override void addTeilnehmer(object value)
        {
            this.Gruppen.Add((Gruppe)value);
            this.AnzahlTeilnehmer++;
            this.AnzahlGruppen++;
        }

        public override void clearTeilnehmer()
        {
            this.Gruppen.Clear();
            this.AnzahlGruppen = 0;
        }

        public override string GetTypus()
        {
            return "Gruppen";
        }

        public override void addSpiel(int grpid, object teilnehmer1, object teilnehmer2)
        {
            Person pers1 = ((Person)teilnehmer1);
            Person pers2 = ((Person)teilnehmer2);
            Spiel neu = new Gruppenspiel(this, grpid, pers1, pers2);
            if (neu.AddToDatabase())
            {
                if (neu.ID == -1)
                {
                    neu.ID = this.Spiele.Count + 1;
                }
                else
                { }
                this.Spiele.Add(neu);
            }
            else
            {

            }
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
                { }
                this.Spiele.Add(neu);
            }
            else
            {

            }
        }
        public override string getSpieleBezeichnung()
        {
            return "SpielNr.";
        }

        public override string getTeilnehmerbezeichnung()
        {
            return "Teilnehmer";
        }

        public override Teilnehmer getGruppe(int index)
        {
            return Gruppen[index];
        }
        public override bool isSpielVorhanden(Spiel search)
        {
            foreach (Spiel sp in Spiele)
            {
                if (search.Turnier.ID == sp.Turnier.ID &&
                    search.getMannschaftName1().Equals(sp.getMannschaftName1()) &&
                    search.getMannschaftName2().Equals(sp.getMannschaftName2()))
                {
                    return true;
                }
                else
                { }
            }
            return false;
        }
        public override int Get_MaxRunden()
        {
            return AnzahlGruppen;
        }

        public override int getSelectedGruppe()
        {
            return SelectedGruppe;
        }

        public override void ClearSpiele(int gruppe)
        {
            for (int index = 0; index < this.Spiele.Count; index++)
            {
                if (this.Spiele[index].getGruppe() == gruppe)
                {
                    this.Spiele.RemoveAt(index);
                    index = -1;
                }
                else
                {
                }
            }
        }

        public override void setSelectedGruppe(int v)
        {
            SelectedGruppe = v;
        }

        public override void SetMaxSpieltag(int v)
        {
            //dummy für GruppenTurnier
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

        public override Ranking GetRanking(int gruppe)
        {
            return this.Gruppen[gruppe - 1].getRanking(this.Spiele);
        }

        public override bool SindMannschaftenAmSpieltagVorhanden(int teilnehmer1, int teilnehmer2, List<Mannschaft> liste, int spieltag)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
