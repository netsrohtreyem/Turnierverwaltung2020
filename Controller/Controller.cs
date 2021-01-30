//dateikopf
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace Turnierverwaltung2020
{
    public class Controller
    {
        #region Eigenschaften
        private List<Turnier> _Turniere;
        private List<Mannschaft> _Mannschaften;
        private List<Gruppe> _gruppen;
        private List<Teilnehmer> _personen;
        private List<sportart> _sportarten;
        private Person _neuesMitglied;
        private bool _EditPerson;
        private int _EditPersonIndex;
        private int _EditPersonID;
        private bool _MannschaftOderGruppe;
        private bool _MannschaftsAnzeige;
        private int _editgruppe;
        private int _editmannschaft;
        private string _sorter;
        private bool _sortdirection;
        private string _myConnectionString;
        private MySqlConnection _conn;
        private string _sqlString;
        private bool _DB_Status;
        private string _database;
        private bool _dbwarnung;
        private int _indexEditTurnier;
        private int _idEditTurnier;
        private Turnier _selectedTurnier;
        private int _selectedTurnierIndex;
        private int _selectedTurnierSpieltag;
        private int _selectedTurnierGruppe;
        private int _maxPersonen;
        private int _maxMannschaften;
        private int _maxGruppen;
        private int _maxTurniere;
        private bool _hinundrueck;
        private bool _editSpiel;
        private int _editSpielID;
        private string _selectedSportart;
        #endregion

        #region Accessoren/Modifier
        public List<Mannschaft> Mannschaften { get => _Mannschaften; set => _Mannschaften = value; }
        public Person NeuesMitglied { get => _neuesMitglied; set => _neuesMitglied = value; }
        public List<Teilnehmer> Personen { get => _personen; set => _personen = value; }
        public List<sportart> Sportarten { get => _sportarten; set => _sportarten = value; }
        public bool EditPerson { get => _EditPerson; set => _EditPerson = value; }
        public int EditPersonIndex { get => _EditPersonIndex; set => _EditPersonIndex = value; }
        public List<Gruppe> Gruppen { get => _gruppen; set => _gruppen = value; }
        public bool MannschaftOderGruppe { get => _MannschaftOderGruppe; set => _MannschaftOderGruppe = value; }
        public bool MannschaftsAnzeige { get => _MannschaftsAnzeige; set => _MannschaftsAnzeige = value; }
        public int EditGruppe { get => _editgruppe; set => _editgruppe = value; }
        public int EditMannschaft { get => _editmannschaft; set => _editmannschaft = value; }
        public string Sorter { get => _sorter; set => _sorter = value; }
        public bool Sortdirection { get => _sortdirection; set => _sortdirection = value; }
        public string MyConnectionString { get => _myConnectionString; set => _myConnectionString = value; }
        public MySqlConnection Conn { get => _conn; set => _conn = value; }
        public string SqlString { get => _sqlString; set => _sqlString = value; }
        public int EditPersonID { get => _EditPersonID; set => _EditPersonID = value; }
        public List<Turnier> Turniere { get => _Turniere; set => _Turniere = value; }
        public bool DB_Status { get => _DB_Status; set => _DB_Status = value; }
        public string Database { get => _database; set => _database = value; }
        public bool Dbwarnung { get => _dbwarnung; set => _dbwarnung = value; }
        public int IndexEditTurnier { get => _indexEditTurnier; set => _indexEditTurnier = value; }
        public int IdEditTurnier { get => _idEditTurnier; set => _idEditTurnier = value; }
        public Turnier SelectedTurnier { get => _selectedTurnier; set => _selectedTurnier = value; }
        public int SelectedTurnierIndex { get => _selectedTurnierIndex; set => _selectedTurnierIndex = value; }
        public int SelectedTurnierSpieltag { get => _selectedTurnierSpieltag; set => _selectedTurnierSpieltag = value; }
        public int MaxPersonen { get => _maxPersonen; set => _maxPersonen = value; }
        public int MaxMannschaften { get => _maxMannschaften; set => _maxMannschaften = value; }
        public int MaxGruppen { get => _maxGruppen; set => _maxGruppen = value; }
        public int MaxTurniere { get => _maxTurniere; set => _maxTurniere = value; }
        public int SelectedTurnierGruppe { get => _selectedTurnierGruppe; set => _selectedTurnierGruppe = value; }
        public bool hinundrueck { get => _hinundrueck; set => _hinundrueck = value; }
        public bool EditSpiel { get => _editSpiel; set => _editSpiel = value; }
        public int EditSpielID { get => _editSpielID; set => _editSpielID = value; }
        public string SelectedSportart { get => _selectedSportart; set => _selectedSportart = value; }
        #endregion

        #region Konstruktoren
        public Controller()
        {
            this.Turniere = new List<Turnier>();
            this.Personen = new List<Teilnehmer>();
            this.Mannschaften = new List<Mannschaft>();
            this.Gruppen = new List<Gruppe>();
            this.Sportarten = new List<sportart>();
            NeuesMitglied = null;
            EditPerson = false;
            MannschaftOderGruppe = true;
            MannschaftsAnzeige = true;
            Sortdirection = true;
            //MyConnectionString = "server=127.0.0.1;database=turnierverwaltung;uid=user;password=user";
            //MyConnectionString = "server=sql7.freemysqlhosting.net;database=sql7389547;uid=sql7389547;password=xG2ri62Vjn";
            MyConnectionString = Properties.Settings.Default.Connectionstring;
            //Database = "turnierverwaltung";
            Database = "sql7389547";
            DB_Status = false;
            Dbwarnung = false;
            SelectedTurnier = null;
            MaxGruppen = 0;
            MaxMannschaften = 0;
            MaxPersonen = 0;
            MaxTurniere = 0;
            hinundrueck = true;
            EditSpiel = false;
            EditSpielID = -1;
            SelectedTurnierIndex = -1;
            SelectedTurnierGruppe = -1;
            SelectedTurnierSpieltag = -1;
        }
        #endregion

        #region Worker
        #region Sportart
        public void AddSportArt(sportart value)
        {
            addSportArtDB(value);


            this.Sportarten.Add(value);
        }

        public bool DeleteSportart(string name)
        {
            int id = -1;
            bool ergebnis = false;
            sportart loesch = null;
            foreach (sportart art in this.Sportarten)
            {
                if (name == art.name)
                {
                    loesch = art;
                    id = loesch.id;
                    break;
                }
                else
                { }
            }
            if (loesch != null)
            {
                foreach (Person pers in this.Personen)
                {
                    if (pers.Sportart == loesch)
                    {
                        ergebnis = false;
                        return ergebnis;
                    }
                    else
                    { }
                }
                foreach (Mannschaft man in this.Mannschaften)
                {
                    if (man.Sportart == loesch)
                    {
                        ergebnis = false;
                        return ergebnis;
                    }
                    else
                    { }
                }
                foreach (Gruppe grp in this.Gruppen)
                {
                    if (grp.Sportart == loesch)
                    {
                        ergebnis = false;
                        return ergebnis;
                    }
                    else
                    { }
                }
                foreach (Turnier tun in this.Turniere)
                {
                    if (tun.Sportart == loesch)
                    {
                        ergebnis = false;
                        return ergebnis;
                    }
                    else
                    { }
                }
                if (deleteSportartDB(id))
                {
                    ergebnis = (this.Sportarten.Remove(loesch));
                    return ergebnis;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                ergebnis = false;
                return ergebnis;
            }
        }

        public void SportartAktualisieren(string bez, string lost, string sieg, string unent, bool typ)
        {
            foreach(sportart sp in this.Sportarten)
            {
                if(sp.name.Equals(bez))
                {
                    sp.MinupunkteproSpiel = Convert.ToInt32(lost);
                    sp.PluspunkteproSpiel = Convert.ToInt32(sieg);
                    sp.UnentschiedenpunkteproSpiel = Convert.ToInt32(unent);
                    sp.Mannschaft = typ;
                    sp.Einzel = !typ;
                    ChangeSportartDB(sp);
                }
            }
        }
        private void addSportArtDB(sportart value)
        {
            if (value.IsOK())
            {
                try
                {
                    Conn = new MySqlConnection();
                    Conn.ConnectionString = this.MyConnectionString;
                    Conn.Open();
                }
                catch (MySqlException)
                {
                    return;
                }
                string mannschaftsart = "0";
                if (value.Mannschaft)
                {
                    mannschaftsart = "1";
                }
                else
                { }
                SqlString = "INSERT INTO `sportarten` (`ID`, `Bezeichnung`, `Mannschaft`, `Siegpunkte`, `Unentschieden`, `Verlorenpunkte`) VALUES (NULL, '" + value.name + "', '" + mannschaftsart + "', '" + value.PluspunkteproSpiel.ToString() + "', '" + value.UnentschiedenpunkteproSpiel.ToString() + "', '" + value.MinupunkteproSpiel.ToString() + "')";
                MySqlCommand command = new MySqlCommand(SqlString, Conn);

                int anzahl = command.ExecuteNonQuery();

                if (anzahl > 0)
                {
                    value.id = (int)command.LastInsertedId;
                }
                else
                {
                }

                Conn.Close();
            }
            else
            { }
        }
        private bool deleteSportartDB(int id)
        {
            if (id > -1)
            {
                try
                {
                    Conn = new MySqlConnection();
                    Conn.ConnectionString = this.MyConnectionString;
                    Conn.Open();
                }
                catch (MySqlException)
                {
                    return true;
                }

                SqlString = "delete from sportarten where id = " + id;
                MySqlCommand command = new MySqlCommand(SqlString, Conn);

                int anz = -1;

                try
                {
                    anz = command.ExecuteNonQuery();
                    if (anz > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        private void ChangeSportartDB(sportart value)
        {
            if (value.IsOK())
            {
                try
                {
                    Conn = new MySqlConnection();
                    Conn.ConnectionString = this.MyConnectionString;
                    Conn.Open();
                }
                catch (MySqlException)
                {
                    return;
                }
                string mannschaftart = "0";
                if (value.Mannschaft)
                {
                    mannschaftart = "1";
                }
                else
                { }
                SqlString = "UPDATE `sportarten` SET `Bezeichnung` = '" + value.name + "', `Mannschaft` = '" + mannschaftart + "', `Siegpunkte` = '" + value.PluspunkteproSpiel.ToString() + "', `Unentschieden` = '" + value.UnentschiedenpunkteproSpiel.ToString() + "', `Verlorenpunkte` = '" + value.MinupunkteproSpiel.ToString() + "' WHERE `sportarten`.`ID` = '" + value.id + "';";
                MySqlCommand command = new MySqlCommand(SqlString, Conn);

                int anzahl = command.ExecuteNonQuery();

                Conn.Close();
            }
            else
            { }
        }
        #endregion

        #region Person
        public bool AddPerson(Person value)
        {
            bool ergebnis = false;

            ergebnis = value.AddToDatabase(null);

            if (ergebnis)
            {
                if (value.ID != -1)
                {
                    this.Personen.Add(value);
                    this.MaxPersonen++;
                }
                else
                {
                    value.ID = this.MaxPersonen + 1;
                    this.Personen.Add(value);
                    this.MaxPersonen++;
                }
            }
            else
            { }
            return ergebnis;
        }
        public bool DeletePerson(int nummer)
        {
            if (nummer > 0 && nummer <= this.Personen.Count)
            {
                if ((this.Personen[nummer - 1]).DeleteFromDatabase())
                {
                    this.Personen.RemoveAt(nummer - 1);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool ChangePerson(Person edit)
        {
            if (IsPersonVorhanden(EditPersonID) ||
                (EditPersonID == -1 && IsPersonVorhanden(this.Personen[EditPersonIndex - 1].ID)))
            {
                if (edit.ChangeInDatabase(null, null, null))
                {
                    ((Person)this.Personen[EditPersonIndex - 1]).ChangeValues(edit);
                    this.EditPerson = false;
                    this.EditPersonIndex = -1;
                    this.EditPersonID = -1;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Teilnehmer getPerson(int nummer)
        {
            return this.Personen[nummer - 1];
        }
        public Person getPerson(int id, string v)
        {
            if (v == "ID")
            {
                foreach (Person pers in this.Personen)
                {
                    if (pers.ID == id)
                    {
                        return pers;
                    }
                    else
                    {

                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        public void PersonenSortieren(string ID)
        {
            if (ID == Sorter)
            {
                //Richtung ändern
                Sortdirection = !Sortdirection;
            }
            else
            {
                Sorter = ID;
                Sortdirection = true;
            }
            switch (ID)
            {
                case "ID":
                    this.Personen.Sort(CompareByID);
                    break;
                case "VorName":
                    this.Personen.Sort(CompareByVorName);
                    break;
                case "Name":
                    this.Personen.Sort(CompareByName);
                    break;
                case "Geb":
                    this.Personen.Sort(CompareByGeb);
                    break;
                case "Sp":
                    this.Personen.Sort(CompareBySportart);
                    break;
                #region Spezial
                case "AnzSp":
                    this.Personen.Sort(CompareByAnzahlSpiele);
                    //this.SortByAnzahlSpiele();
                    break;
                case "ErzTore":
                    this.Personen.Sort(CompareByErzielteTore);
                    //this.SortByErzielteTore();
                    break;
                case "GewSp":
                    this.Personen.Sort(CompareByGewonneneSpiele);
                    //this.SortByGewonneneSpiele();
                    break;
                case "AnzJahre":
                    this.Personen.Sort(CompareByAnzahlJahre);
                    //this.SortByAnzahlJahre();
                    break;
                case "AnzVereine":
                    this.Personen.Sort(CompareByAnzahlVereine);
                    //this.SortByAnzahlVereine();
                    break;
                case "Einsatz":
                    this.Personen.Sort(CompareByEinsatz);
                    //this.SortByEinsatz();
                    break;
                #endregion
                default:
                    this.Personen.Sort(CompareByName);
                    break;
            }
        }
        #endregion

        #region Mannschaft
        public bool AddMannschaft(Mannschaft value)
        {
            bool ergebnis = false;

            if (this.Mannschaften.Contains(value))
            {
                ergebnis = false;
            }
            else
            {
                ergebnis = true;
            }
            if (value.AddToDatabase(new List<int>()) && ergebnis)
            {
                if (value.ID == -1)
                {
                    value.ID = this.MaxMannschaften + 1;
                    this.Mannschaften.Add(value);
                    this.MaxMannschaften++;
                }
                else
                {
                    this.Mannschaften.Add(value);
                    this.MaxMannschaften++;
                }
                ergebnis = true;
            }
            else
            {
                ergebnis = false;
            }
            return ergebnis;
        }
        public bool AddMannschaft(Mannschaft value, ListItemCollection mitgliedervalue)
        {
            bool ergebnis = false;
            List<int> Mitgliederliste = new List<int>();
            List<Person> Liste = new List<Person>();
            if (mitgliedervalue[0].Text != "bisher keine Mitglieder")
            {
                foreach (ListItem li in mitgliedervalue)
                {
                    int id = Convert.ToInt32(li.Text.Substring(0, li.Text.IndexOf(",")));
                    Mitgliederliste.Add(id);
                }
            }
            else
            { }

            if (value.AddToDatabase(Mitgliederliste))
            {
                if (value.ID == -1)
                {
                    value.ID = this.MaxMannschaften + 1;
                }
                else
                {

                }
                foreach (Person pers in this.Personen)
                {
                    foreach (int persid in Mitgliederliste)
                    {
                        if (pers.ID == persid)
                        {
                            value.Mitglieder.Add(pers);
                        }
                        else
                        { }
                    }
                }
                this.Mannschaften.Add(value);
                this.MaxMannschaften++;
                ergebnis = true;
            }
            else
            {
                ergebnis = false;
            }

            return ergebnis;
        }
        public bool ChangeMannschaft(string name, string sportart, ListItemCollection Mitgliederliste)
        {
            bool ergebnis = false;
            sportart tochange = null;
            List<int> MitgliederIDs = new List<int>();
            foreach (sportart sp in this.Sportarten)
            {
                if (sp.name == sportart)
                {
                    tochange = sp;
                    break;
                }
                else
                { }
            }
            foreach (ListItem Li in Mitgliederliste)
            {
                if (Li.Text != "bisher keine Mitglieder")
                {
                    int id = Convert.ToInt32(Li.Text.Substring(0, Li.Text.IndexOf(",")));
                    MitgliederIDs.Add(id);
                }
                else
                { }
            }

            foreach (Mannschaft man in this.Mannschaften)
            {
                if (man.ID == EditMannschaft)
                {
                    if (man.ChangeInDatabase(name, tochange, MitgliederIDs))
                    {
                        man.Mitglieder.Clear();

                        foreach (int index in MitgliederIDs)
                        {
                            foreach (Person pers in Personen)
                            {
                                if (pers.ID == index)
                                {
                                    man.Mitglieder.Add(pers);
                                }
                                else
                                { }
                            }
                        }
                        man.Name = name;
                        man.Sportart = tochange;

                        ergebnis = true;
                    }
                    else
                    {
                        ergebnis = false;
                    }
                    break;
                }
                else
                { }
            }

            return ergebnis;
        }
        public bool DeleteMannschaft(int index)
        {
            bool ergebnis = false;

            if (Mannschaften[index - 1].DeleteFromDatabase())
            {
                this.Mannschaften.RemoveAt(index - 1);
                ergebnis = true;
            }
            else
            {
                ergebnis = false;
            }
            return ergebnis;
        }
        public bool IsMannschVorhanden(string neu, sportart spart)
        {
            foreach (Mannschaft man in this.Mannschaften)
            {
                if (man.Name.Equals(neu) && man.Sportart.name.Equals(spart))
                {
                    return true;
                }
                else
                { }
            }
            return false;
        }
        #endregion

        #region Gruppe
        public bool AddGruppe(Gruppe value, ListItemCollection mitgliedervalue)
        {
            bool ergebnis = false;
            List<int> Mitgliederliste = new List<int>();
            if(mitgliedervalue == null)
            {
                mitgliedervalue = new ListItemCollection();
                ListItem neuerEintrag = new ListItem("bisher keine Teilnehmer");
                mitgliedervalue.Add(neuerEintrag);
            }
            else
            { }

            if (mitgliedervalue[0].Text != "bisher keine Teilnehmer")
            {
                foreach (ListItem li in mitgliedervalue)
                {
                    int id = Convert.ToInt32(li.Text.Substring(0, li.Text.IndexOf(",")));
                    Mitgliederliste.Add(id);
                }
            }
            else
            { }
            if (value.AddToDatabase(Mitgliederliste))
            {
                if (value.ID == -1)
                {
                    value.ID = this.MaxGruppen + 1;
                }
                else
                { }
                foreach (Person pers in this.Personen)
                {
                    foreach (int persid in Mitgliederliste)
                    {
                        if (pers.ID == persid)
                        {
                            value.Mitglieder.Add(pers);
                        }
                        else
                        { }
                    }
                }
                this.Gruppen.Add(value);
                this.MaxGruppen++;
                ergebnis = true;
            }
            else
            {
                ergebnis = false;
            }

            return ergebnis;
        }

        public bool AddGruppe(Gruppe value)
        {
            bool ergebnis = false;

            if (value.AddToDatabase(null))
            {
                if (value.ID == -1)
                {
                    value.ID = this.MaxGruppen + 1;
                }
                else
                { }

                this.Gruppen.Add(value);
                this.MaxGruppen++;
                ergebnis = true;
            }
            else
            {
                ergebnis = false;
            }
            return ergebnis;
        }
        public bool ChangeGruppe(string name, string sportart, ListItemCollection Mitgliederliste)
        {
            bool ergebnis = false;
            sportart tochange = null;
            List<int> MitgliederIDs = new List<int>();
            foreach (sportart spart in this.Sportarten)
            {
                if (spart.name == sportart)
                {
                    tochange = spart;
                    break;
                }
                else
                { }
            }
            foreach (ListItem Li in Mitgliederliste)
            {
                if (Li.Text != "bisher keine Teilnehmer")
                {
                    int id = Convert.ToInt32(Li.Text.Substring(0, Li.Text.IndexOf(",")));
                    MitgliederIDs.Add(id);
                }
                else
                { }
            }

            foreach (Gruppe grup in this.Gruppen)
            {
                if (grup.ID == EditGruppe)
                {
                    if (grup.ChangeInDatabase(name, tochange, MitgliederIDs))
                    {
                        grup.Mitglieder.Clear();
                        grup.Name = name;
                        grup.Sportart = tochange;
                        foreach (int index in MitgliederIDs)
                        {
                            foreach (Person pers in Personen)
                            {
                                if (pers.ID == index)
                                {
                                    grup.Mitglieder.Add(pers);
                                }
                                else
                                { }
                            }
                        }
                        ergebnis = true;
                    }
                    else
                    {
                        ergebnis = false;
                    }
                    break;
                }
                else
                { }
            }

            return ergebnis;
        }
        public bool DeleteGruppe(int index)
        {
            bool ergebnis = false;

            if (Gruppen[index - 1].DeleteFromDatabase())
            {
                this.Gruppen.RemoveAt(index - 1);
                ergebnis = true;
            }
            else
            {
                ergebnis = false;
            }
            return ergebnis;
        }
        public bool IsGruppeVorhanden(string neu, sportart sportart)
        {
            foreach (Gruppe grup in this.Gruppen)
            {
                if (grup.Name.Equals(neu) && grup.Sportart.name.Equals(sportart.name))
                {
                    return true;
                }
                else
                { }
            }
            return false;
        }
        #endregion

        #region Turnier
        public bool AddTurnier(string Name, string sportart, ListItemCollection items, int typ)
        {
            bool ergebnis = false;
            sportart toadd = null;
            foreach (sportart spart in this.Sportarten)
            {
                if (spart.name == sportart)
                {
                    toadd = spart;
                    break;
                }
                else
                { }
            }
            if (Name == "")
            {
                return ergebnis;
            }
            else
            { }
            if (typ == 0)
            {
                List<Mannschaft> mannschaftsliste = new List<Mannschaft>();
                if (items[0].Text != "bisher keine Mannschaften")
                {
                    foreach (ListItem ls in items)
                    {
                        int mannid = Convert.ToInt32(ls.Text.Substring(0, ls.Text.IndexOf(",")));
                        string mannNameSportArt = ls.Text.Substring(ls.Text.IndexOf(", ") + 2);
                        string mannName = mannNameSportArt.Substring(0, mannNameSportArt.IndexOf(", "));
                        string mannsportart = mannNameSportArt.Substring(mannNameSportArt.IndexOf(", ") + 2);
                        if (mannid < 0)
                        {
                            foreach (Mannschaft man in Mannschaften)
                            {
                                if (man.Name == mannName && man.Sportart.name == mannsportart)
                                {
                                    mannschaftsliste.Add(man);
                                }
                                else
                                { }
                            }
                        }
                        else
                        {
                            foreach (Mannschaft man in Mannschaften)
                            {
                                if (man.ID == mannid)
                                {
                                    mannschaftsliste.Add(man);
                                }
                                else
                                { }
                            }
                        }
                    }
                }
                else
                { }
                //Dummy ohne ID!
                Turnier neu = new MannschaftsTurnier(Name, toadd, mannschaftsliste);

                if (neu.AddToDatabase())
                {
                    if (neu.ID == -1)
                    {
                        neu.ID = this.MaxTurniere + 1;
                    }
                    else
                    { }
                    this.Turniere.Add(neu);
                    this.MaxTurniere++;
                    ergebnis = true;
                }
                else
                {
                    ergebnis = false;
                }
            }
            else
            {
                List<Gruppe> gruppensliste = new List<Gruppe>();
                if (items[0].Text != "bisher keine Teilnehmer")
                {
                    foreach (ListItem ls in items)
                    {
                        int grupid = Convert.ToInt32(ls.Text.Substring(0, ls.Text.IndexOf(",")));
                        string gruppNameSportArt = ls.Text.Substring(ls.Text.IndexOf(", ") + 2);
                        string gruppName = gruppNameSportArt.Substring(0, gruppNameSportArt.IndexOf(", "));
                        string gruppsportart = gruppNameSportArt.Substring(gruppNameSportArt.IndexOf(", ") + 2);
                        if (grupid < 0)
                        {
                            foreach (Gruppe grp in Gruppen)
                            {
                                if (grp.Name == gruppName && grp.Sportart.name == gruppsportart)
                                {
                                    gruppensliste.Add(grp);
                                }
                                else
                                { }
                            }
                        }
                        else
                        {
                            foreach (Gruppe grp in Gruppen)
                            {
                                if (grp.ID == grupid)
                                {
                                    gruppensliste.Add(grp);
                                }
                                else
                                { }
                            }
                        }
                    }
                }
                else
                { }
                //Dummy ohne ID!
                Turnier neu = new GruppenTurnier(Name, toadd, gruppensliste);

                if (neu.AddToDatabase())
                {
                    if (neu.ID == -1)
                    {
                        neu.ID = this.MaxTurniere;
                    }
                    else
                    { }
                    this.Turniere.Add(neu);
                    this.MaxTurniere++;
                    ergebnis = true;
                }
                else
                {
                    ergebnis = false;
                }
            }
            return ergebnis;
        }
        public bool AddTurnier(Turnier value)
        {
            bool ergebnis = false;

            if (value.AddToDatabase())
            {
                if (value.ID == -1)
                {
                    value.ID = this.MaxTurniere + 1;
                }
                else
                { }
                this.Turniere.Add(value);
                this.MaxTurniere++;
                ergebnis = true;
            }
            else
            {
                ergebnis = false;
            }

            return ergebnis;
        }
        public void Set_Spieltag_oder_Gruppe(int v)
        {
            if (SelectedTurnier is MannschaftsTurnier)
            {
                SelectedTurnierSpieltag = v;
                SelectedTurnierGruppe = -1;
            }
            else if (SelectedTurnier is GruppenTurnier)
            {
                SelectedTurnierGruppe = v;
                SelectedTurnierSpieltag = -1;
            }
        }
        public int Get_SelectedSpieltagoderGruppe()
        {
            if (SelectedTurnier is MannschaftsTurnier)
            {
                return SelectedTurnierSpieltag;
            }
            else if (SelectedTurnier is GruppenTurnier)
            {
                return SelectedTurnierGruppe;
            }
            else
            {
                return -1;
            }
        }
        public bool DeleteTurnier(int id)
        {
            bool ergebnis = false;

            if (this.Turniere[id].DeleteFromDB())
            {
                this.Turniere.RemoveAt(id);
            }
            else
            { }

            return ergebnis;
        }
        public bool ChangeTurnier(string txtNameTurnier, string selectedValue, ListItemCollection items, int typ)
        {
            bool ergebnis = true;
            sportart tochange = null;
            foreach (sportart spart in this.Sportarten)
            {
                if (spart.name == selectedValue)
                {
                    tochange = spart;
                    break;
                }
                else
                { }
            }
            this.Turniere[IndexEditTurnier].Bezeichnung = txtNameTurnier;
            this.Turniere[IndexEditTurnier].Sportart = tochange;
            this.Turniere[IndexEditTurnier].clearTeilnehmer();
            if (items[0].Text.Contains("bisher keine Teilnehmer") || items[0].Text.Contains("bisher keine Mannschaften"))
            {

            }
            else
            {
                foreach (ListItem ls in items)
                {
                    int id = Convert.ToInt32(ls.Text.Substring(0, ls.Text.IndexOf(",")));
                    string itemNameSportArt = ls.Text.Substring(ls.Text.IndexOf(", ") + 2);
                    string itemName = itemNameSportArt.Substring(0, itemNameSportArt.IndexOf(", "));
                    string itemsportart = itemNameSportArt.Substring(itemNameSportArt.IndexOf(", ") + 2);
                    if (typ == 0)
                    {
                        if (id < 0)
                        {
                            foreach (Mannschaft man in this.Mannschaften)
                            {
                                if (man.Name == itemName && man.Sportart.name == itemsportart)
                                {
                                    this.Turniere[IndexEditTurnier].addTeilnehmer(man);
                                }
                                else
                                { }
                            }
                        }
                        else
                        {
                            foreach (Mannschaft man in this.Mannschaften)
                            {
                                if (man.ID == id)
                                {
                                    this.Turniere[IndexEditTurnier].addTeilnehmer(man);
                                }
                                else
                                { }
                            }
                        }
                    }
                    else
                    {
                        if (id < 0)
                        {
                            foreach (Gruppe grp in this.Gruppen)
                            {
                                if (grp.Name == itemName && grp.Sportart.name == itemsportart)
                                {
                                    this.Turniere[IndexEditTurnier].addTeilnehmer(grp);
                                }
                                else
                                { }
                            }
                        }
                        else
                        {
                            foreach (Gruppe grp in this.Gruppen)
                            {
                                if (grp.ID == id)
                                {
                                    this.Turniere[IndexEditTurnier].addTeilnehmer(grp);
                                }
                                else
                                { }
                            }
                        }
                    }
                }
            }

            this.Turniere[IndexEditTurnier].ChangeInDB();
            return ergebnis;
        }
        public void SelectTurnier(string value)
        {
            int id = Convert.ToInt32(value.Substring(0, value.IndexOf(",")));
            foreach (Turnier turn in this.Turniere)
            {
                if (turn.ID == id)
                {
                    this.SelectedTurnier = turn;
                    break;
                }
                else
                { }
            }
        }
        public void SelectTurnierGruppe(string value)
        {
            int id = Convert.ToInt32(value.Substring(0, value.IndexOf(",")));
            foreach (Gruppe grp in this.SelectedTurnier.getTeilnemer())
            {
                if (grp.ID == id)
                {
                    this.SelectedTurnierGruppe = grp.ID;
                    break;
                }
                else
                { }
            }
        }
        #endregion

        #region Spiele
        public void AddSpielToMannschaftsTurnier(int number, string mannschaft1, string mannschaft2,string tore1,string tore2)
        {
            Mannschaft man1 = null;
            Mannschaft man2 = null;

            int id = Convert.ToInt32(mannschaft1.Substring(0, mannschaft1.IndexOf(",")));
            string rest = mannschaft1.Substring(mannschaft1.IndexOf(",") + 1);
            string name = rest.Substring(1);

            int id2 = Convert.ToInt32(mannschaft2.Substring(0, mannschaft2.IndexOf(",")));
            string rest2 = mannschaft2.Substring(mannschaft2.IndexOf(",") + 1);
            string name2 = rest2.Substring(1);

            foreach (Mannschaft man in this.Mannschaften)
            {
                if (man.ID.Equals(id) && man.Name.Equals(name))
                {
                    man1 = man;
                }
                else if (man.ID.Equals(id2) && man.Name.Equals(name2))
                {
                    man2 = man;
                }
                else
                { }
            }
            if (man1 == null || man2 == null || man1.Name.Equals(man2.Name))
            {
                return;
            }
            else
            { }

            Spiel neu = new Mannschaftsspiel(SelectedTurnier, man1, man2, number);

            if (SelectedTurnier.isSpielVorhanden(neu))
            {
                return;
            }
            else
            {
                neu.setErgebniswert1(tore1);
                neu.setErgebniswert2(tore2);
            }

            if (neu.AddToDatabase())
            {
                if (neu.ID == -1)
                {
                    neu.ID = ((MannschaftsTurnier)SelectedTurnier).Spiele.Count + 1;
                }
                else
                { }
                SelectedTurnier.addSpiel(neu);
            }
            else
            { }
            if (SelectedTurnierSpieltag == 0)
            {
                SelectedTurnierSpieltag = 1;
            }
            else
            { }
        }
        public void AddSpielToMannschaftsTurnier(Spiel neu)
        {
            if (SelectedTurnier.isSpielVorhanden(neu))
            {
                return;
            }
            else
            { }

            if (neu.AddToDatabase())
            {
                if (neu.ID == -1)
                {
                    neu.ID = ((MannschaftsTurnier)SelectedTurnier).Spiele.Count + 1;
                }
                else
                { }
                SelectedTurnier.addSpiel(neu);
            }
            else
            { }
            if (SelectedTurnierSpieltag == 0)
            {
                SelectedTurnierSpieltag = 1;
            }
            else
            { }
        }
        public void AddSpielToGruppenTurnier(int grpid, string teilnehmer1, string teilnehmer2)
        {
            Person pers1 = null;
            Person pers2 = null;

            int id = Convert.ToInt32(teilnehmer1.Substring(0, teilnehmer1.IndexOf(",")));
            string rest = teilnehmer1.Substring(teilnehmer1.IndexOf(",") + 2);
            string name = rest.Substring(0, rest.IndexOf(","));
            rest = rest.Substring(rest.IndexOf(",") + 2);
            string vorname = rest.Substring(0, rest.IndexOf(","));
            rest = rest.Substring(rest.IndexOf(",") + 1);
            DateTime gebDatum = Convert.ToDateTime(rest);
            int id2 = Convert.ToInt32(teilnehmer2.Substring(0, teilnehmer2.IndexOf(",")));
            string rest2 = teilnehmer2.Substring(teilnehmer2.IndexOf(",") + 2);
            string name2 = rest2.Substring(0, rest2.IndexOf(","));
            rest2 = rest2.Substring(rest2.IndexOf(",") + 2);
            string vorname2 = rest2.Substring(0, rest2.IndexOf(","));
            rest2 = rest2.Substring(rest2.IndexOf(",") + 1);
            DateTime gebDatum2 = Convert.ToDateTime(rest2);

            foreach (Person pers in this.Personen)
            {
                if (pers.ID.Equals(id) && pers.Name.Equals(name) && pers.Vorname.Equals(vorname) && pers.Geburtsdatum.Equals(gebDatum))
                {
                    pers1 = pers;
                }
                else if (pers.ID.Equals(id2) && pers.Name.Equals(name2) && pers.Vorname.Equals(vorname2) && pers.Geburtsdatum.Equals(gebDatum2))
                {
                    pers2 = pers;
                }
                else
                { }
            }

            if (pers1 == null || pers2 == null)
            {
                return;
            }
            else
            { }

            int gruppenid = SelectedTurnier.getTeilnemer()[grpid - 1].ID;

            Spiel neu = new Gruppenspiel(SelectedTurnier, gruppenid, pers1, pers2);

            if (this.SelectedTurnier.isSpielVorhanden(neu))
            {
                return;
            }
            else
            { }

            if (neu.AddToDatabase())
            {
                if (neu.ID == -1)
                {
                    neu.ID = ((GruppenTurnier)SelectedTurnier).Spiele.Count + 1;
                }
                else
                { }
                SelectedTurnier.addSpiel(neu);
            }
            else
            { }
        }
        public void ChangeSpielInTurnier(int id, string name1, string name2, string ergebnis1, string ergebnis2)
        {
            SelectedTurnier.ChangeSpiel(id, name1, name2, ergebnis1, ergebnis2);
        }
        public void DeleteSpielFromTurnier(int id)
        {
            this.SelectedTurnier.RemoveSpiel(id);
            if (this.SelectedTurnier.Get_Spiele().Count <= 0)
            {
                SelectedTurnierSpieltag = 0;
            }
            else
            { }
        }
        public void SpieltageAutomatik(bool hinundrueck)
        {
            if (this.SelectedTurnier is MannschaftsTurnier)
            {
                this.SelectedTurnier.ClearSpiele(-1);
                this.SelectedTurnierSpieltag = 0;
                int anzahlspieltage = (this.SelectedTurnier.getAnzahlTeilnehmer() - 1) * 2;
                int anzahlTeilnehmer = this.SelectedTurnier.getAnzahlTeilnehmer();
                Random Zufallszahl = new Random(DateTime.Now.GetHashCode());
                List<Teilnehmer> OrgListe = new List<Teilnehmer>(this.SelectedTurnier.getTeilnemer());

                List<Mannschaft> Liste = new List<Mannschaft>();
                Zufallszahl.Next();
                if (anzahlTeilnehmer < 2)
                {
                    return;
                }
                else
                { }
                //Zufällige Mannschaftsliste erzeugen
                for(int index = 0;index < anzahlTeilnehmer;index++)
                {
                    int nummer = Zufallszahl.Next(OrgListe.Count);
                    Liste.Add((Mannschaft)OrgListe[nummer]);
                    OrgListe.RemoveAt(nummer);
                }

                this.SelectedTurnierSpieltag = 1;
                this.SelectedTurnier.SetMaxSpieltag(0);
                //bool ok = false;
                Spiel neu = null;

                if(hinundrueck)//hin und Rück
                {
                    //Hin
                    for(int spieltag = 1;spieltag <= anzahlspieltage/2;spieltag++)
                    {
                        for (int teilnehmer1 = 1; teilnehmer1 <= anzahlTeilnehmer; teilnehmer1++)
                        {
                            bool gefunden = false;
                            for (int teilnehmer2 = 2; teilnehmer2 <= (anzahlTeilnehmer-1); teilnehmer2++)
                            {
                                if ((((teilnehmer1 + teilnehmer2) % (anzahlTeilnehmer - 1)) == spieltag ||
                                    ((((teilnehmer1 + teilnehmer2) % (anzahlTeilnehmer - 1)) == 0) && spieltag == anzahlspieltage/2)) && 
                                    !(teilnehmer1 == teilnehmer2) &&
                                    !this.SelectedTurnier.SindMannschaftenAmSpieltagVorhanden(teilnehmer1,teilnehmer2,Liste,spieltag))
                                {
                                    neu = new Mannschaftsspiel(this.SelectedTurnier, Liste[teilnehmer1 - 1], Liste[teilnehmer2 - 1], spieltag);
                                    gefunden = true;
                                    this.SelectedTurnier.addSpiel(neu);
                                    break;
                                }
                                else
                                {
                                    gefunden = false;
                                }
                            }
                            if(!gefunden &&
                               !this.SelectedTurnier.SindMannschaftenAmSpieltagVorhanden(teilnehmer1, 0, Liste, spieltag))
                            {
                                neu = new Mannschaftsspiel(this.SelectedTurnier, Liste[teilnehmer1 - 1], Liste[anzahlTeilnehmer - 1], spieltag);
                                gefunden = true;
                                this.SelectedTurnier.addSpiel(neu);
                            }
                            else
                            { }
                        }
                    }
                    //Rueck
                    List<Spiel> SpieleListe = new List<Spiel>(this.SelectedTurnier.Get_Spiele());
                    for (int spieltag = 18; spieltag <= anzahlspieltage; spieltag++)
                    {
                        for (int spiel = 1; spiel <= anzahlTeilnehmer / 2; spiel++)
                        {
                            neu = new Mannschaftsspiel(this.SelectedTurnier,
                                                       ((Mannschaft)SpieleListe[0].getTeilnehmer2()),
                                                       ((Mannschaft)SpieleListe[0].getTeilnehmer1()),
                                                       spieltag);
                            this.SelectedTurnier.addSpiel(neu);
                            SpieleListe.RemoveAt(0);
                        }
                    }
                }
                else //nur Hin
                {
                    for (int spieltag = 1; spieltag <= anzahlspieltage / 2; spieltag++)
                    {
                        for (int teilnehmer1 = 1; teilnehmer1 <= anzahlTeilnehmer; teilnehmer1++)
                        {
                            bool gefunden = false;
                            for (int teilnehmer2 = 2; teilnehmer2 <= (anzahlTeilnehmer - 1); teilnehmer2++)
                            {
                                if ((((teilnehmer1 + teilnehmer2) % (anzahlTeilnehmer - 1)) == spieltag ||
                                    ((teilnehmer1 + teilnehmer2) % (anzahlTeilnehmer - 1)) == 0) &&
                                    !(teilnehmer1 == teilnehmer2) &&
                                    !this.SelectedTurnier.SindMannschaftenAmSpieltagVorhanden(teilnehmer1, teilnehmer2, Liste, spieltag))
                                {
                                    neu = new Mannschaftsspiel(this.SelectedTurnier, Liste[teilnehmer1 - 1], Liste[teilnehmer2 - 1], spieltag);
                                    gefunden = true;
                                    this.SelectedTurnier.addSpiel(neu);
                                    break;
                                }
                                else
                                {
                                    gefunden = false;
                                }
                            }
                            if (!gefunden &&
                               !this.SelectedTurnier.SindMannschaftenAmSpieltagVorhanden(teilnehmer1, 0, Liste, spieltag))
                            {
                                neu = new Mannschaftsspiel(this.SelectedTurnier, Liste[teilnehmer1 - 1], Liste[anzahlTeilnehmer - 1], spieltag);
                                gefunden = true;
                                this.SelectedTurnier.addSpiel(neu);
                            }
                            else
                            { }
                        }
                    }
                }
            }
            else if (this.SelectedTurnier is GruppenTurnier) //Gruppen Turnier, Vorrunde
            {
                this.SelectedTurnier.ClearSpiele(this.SelectedTurnierGruppe);
                int anzahlTeilnehmer = this.SelectedTurnier.getAnzahlPersonenteilnehmer(this.SelectedTurnierGruppe);
                int maxrunde = ((anzahlTeilnehmer - 1) * anzahlTeilnehmer) / 2;
                List<Spiel> neueSpielehin = new List<Spiel>();
                List<Spiel> neueSpielerueck = new List<Spiel>();
                Random Zufallszahl = new Random(DateTime.Now.GetHashCode());
                Zufallszahl.Next();
                int teiln1 = -1;
                int teiln2 = -1;
                if (anzahlTeilnehmer < 2)
                {
                    return;
                }
                else
                { }

                bool ok = false;
                Spiel neu = null;

                //HinRunde
                for (int index1 = 1; index1 <= maxrunde; index1++)
                {
                    do
                    {
                        teiln1 = Zufallszahl.Next(anzahlTeilnehmer);
                        teiln2 = Zufallszahl.Next(anzahlTeilnehmer);
                        if (teiln1 != teiln2)
                        {
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                            continue;
                        }
                        int selectedGruppe = this.SelectedTurnier.getSelectedGruppe();
                        Teilnehmer pers1 = ((Gruppe)this.SelectedTurnier.getTeilnemer()[selectedGruppe - 1]).Mitglieder[teiln1];
                        Teilnehmer pers2 = ((Gruppe)this.SelectedTurnier.getTeilnemer()[selectedGruppe - 1]).Mitglieder[teiln2];
                        neu = new Gruppenspiel(this.SelectedTurnier, this.SelectedTurnierGruppe, pers1, pers2);

                        if (IsSpielVorhanden(neu, neueSpielehin) || IsMannschaftsKombiVorhanden(neu, neueSpielehin))
                        {
                            ok = false;
                        }
                        else
                        {
                            ok = true;
                        }
                    } while (!ok);
                    neueSpielehin.Add(neu);
                }
                //Rückrunde
                for (int index1 = 1; index1 <= maxrunde; index1++)
                {
                    do
                    {
                        teiln1 = Zufallszahl.Next(anzahlTeilnehmer);
                        teiln2 = Zufallszahl.Next(anzahlTeilnehmer);
                        if (teiln1 != teiln2)
                        {
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                            continue;
                        }

                        int selectedGruppe = this.SelectedTurnier.getSelectedGruppe();
                        Teilnehmer pers1 = ((Gruppe)this.SelectedTurnier.getTeilnemer()[selectedGruppe - 1]).Mitglieder[teiln1];
                        Teilnehmer pers2 = ((Gruppe)this.SelectedTurnier.getTeilnemer()[selectedGruppe - 1]).Mitglieder[teiln2];
                        neu = new Gruppenspiel(this.SelectedTurnier, this.SelectedTurnierGruppe, pers1, pers2);

                        if (IsSpielVorhanden(neu, neueSpielehin) || IsMannschaftsKombiVorhanden(neu, neueSpielerueck) ||
                            IsSpielVorhanden(neu, neueSpielerueck))
                        {
                            ok = false;
                        }
                        else
                        {
                            ok = true;
                        }
                    } while (!ok);
                    neueSpielerueck.Add(neu);
                }

                if (hinundrueck)
                {
                    foreach (Spiel sp in neueSpielehin)
                    {
                        this.SelectedTurnier.addSpiel(sp);
                    }
                    foreach (Spiel sp in neueSpielerueck)
                    {
                        this.SelectedTurnier.addSpiel(sp);
                    }
                }
                else
                {
                    foreach (Spiel sp in neueSpielehin)
                    {
                        this.SelectedTurnier.addSpiel(sp);
                    }
                }
            }
            else
            { }
        }
        private bool IsMannschaftsKombiVorhanden(Spiel neu, List<Spiel> value)
        {
            bool ergebnis = false;

            foreach (Spiel sp in value)
            {
                if ((sp.getMannschaftName1().Equals(neu.getMannschaftName2()) &&
                    sp.getMannschaftName2().Equals(neu.getMannschaftName1())))
                {
                    ergebnis = true;
                    break;
                }
                else
                {
                    ergebnis = false;
                }
            }

            return ergebnis;
        }
        private bool IsSpielVorhanden(Spiel neu, List<Spiel> value)
        {
            bool ergebnis = false;

            foreach (Spiel sp in value)
            {
                if ((sp.getMannschaftName1().Equals(neu.getMannschaftName1()) &&
                    sp.getMannschaftName2().Equals(neu.getMannschaftName2())))
                {
                    ergebnis = true;
                    break;
                }
                else
                {
                    ergebnis = false;
                }
            }

            return ergebnis;
        }
        #endregion

        #region PrüfMethoden
        public bool IsPersonInMannschaftoderGruppe(int nummer, ref string mannschaft)
        {
            foreach (Mannschaft man in this.Mannschaften)
            {
                if (man.Mitglieder.Contains(this.Personen[nummer - 1]))
                {
                    mannschaft = man.Name;
                    return true;
                }
                else
                { }
            }
            foreach (Gruppe grup in this.Gruppen)
            {
                if (grup.Mitglieder.Contains(this.Personen[nummer - 1]))
                {
                    mannschaft = grup.Name;
                    return true;
                }
                else
                { }
            }
            return false;
        }
        private bool IsPersonVorhanden(int PersonID)
        {
            if (PersonID >= 0)
            {
                foreach (Person pers in Personen)
                {
                    if (pers.ID == PersonID)
                    {
                        return true;
                    }
                    else
                    { }
                }
            }
            else
            {
                //ohne DB ID = -1 kein Vergleich!
                return false;
            }
            return false;
        }
        private bool IsPersonVorhanden(Person value)
        {
            bool ergebnis = false;

            if (this.Personen.Contains(value))
            {
                ergebnis = true;
            }
            else
            {
                ergebnis = false;
            }

            return ergebnis;
        }
        #endregion

        #region Sortieren/Vergleichen
        private int CompareByEinsatz(Teilnehmer pers1, Teilnehmer pers2)
        {
            if (Sortdirection)//aufwärts
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = 0;
                        retval = pers1.CompareByEinsatz(pers2);
                        return retval;
                    }
                }
            }
            else//abwärts
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return -1;
                    }
                    else
                    {
                        int retval = 0;
                        retval = pers1.CompareByEinsatz(pers2);
                        if (retval == 1)
                        {
                            retval = -1;
                        }
                        else if (retval == -1)
                        {
                            retval = 1;
                        }
                        else
                        { }
                        return retval;
                    }
                }
            }
        }
        private int CompareByAnzahlVereine(Teilnehmer pers1, Teilnehmer pers2)
        {
            if (Sortdirection)//aufwärts
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = 0;

                        retval = pers1.CompareByAnzahlVereine(pers2);

                        return retval;
                    }
                }
            }
            else//abwärts
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return -1;
                    }
                    else
                    {
                        int retval = 0;

                        retval = pers1.CompareByAnzahlVereine(pers2);
                        if (retval == 1)
                        {
                            retval = -1;
                        }
                        else if (retval == -1)
                        {
                            retval = 1;
                        }
                        else
                        { }
                        return retval;
                    }
                }
            }
        }
        private int CompareByAnzahlJahre(Teilnehmer pers1, Teilnehmer pers2)
        {
            if (Sortdirection)//aufwärts
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = 0;
                        retval = pers1.CompareByAnzahlJahre(pers2);
                        return retval;
                    }
                }
            }
            else//abwärts
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return -1;
                    }
                    else
                    {
                        int retval = 0;
                        retval = pers1.CompareByAnzahlJahre(pers2);
                        if (retval == 1)
                        {
                            retval = -1;
                        }
                        else if (retval == -1)
                        {
                            retval = 1;
                        }
                        else
                        { }
                        return retval;
                    }
                }
            }
        }
        private int CompareByGewonneneSpiele(Teilnehmer pers1, Teilnehmer pers2)
        {
            if (Sortdirection)//aufwärts
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = 0;
                        retval = pers1.CompareByGewonneneSpiele(pers2);
                        return retval;
                    }
                }
            }
            else//abwärts
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return -1;
                    }
                    else
                    {
                        int retval = 0;
                        retval = pers1.CompareByGewonneneSpiele(pers2);
                        if (retval == 1)
                        {
                            retval = -1;
                        }
                        else if (retval == -1)
                        {
                            retval = 1;
                        }
                        else
                        { }
                        return retval;
                    }
                }
            }
        }
        private int CompareByAnzahlSpiele(Teilnehmer pers1, Teilnehmer pers2)
        {
            if (Sortdirection)//aufwärts
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = 0;
                        retval = pers1.CompareByAnzahlspiele(pers2);
                        return retval;
                    }
                }
            }
            else//abwärts
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return -1;
                    }
                    else
                    {
                        int retval = 0;

                        retval = pers1.CompareByAnzahlspiele(pers2);

                        if (retval == 1)
                        {
                            retval = -1;
                        }
                        else if (retval == -1)
                        {
                            retval = 1;
                        }
                        else
                        { }
                        return retval;
                    }
                }
            }
        }

        public bool IsMannschaftoderGruppeInTurnier(int v, int type)
        {
            bool ergebnis = false;
            foreach (Turnier tun in this.Turniere)
            {
                if (type == 0 && tun is MannschaftsTurnier)//Mannschaft
                {
                    if (((MannschaftsTurnier)tun).Teilnehmer.Contains(this.Mannschaften[v]))
                    {
                        ergebnis = true;
                        break;
                    }
                    else
                    {
                        ergebnis = false;
                    }
                }
                else if (type == 1 && tun is GruppenTurnier)//Gruppen
                {
                    if (((GruppenTurnier)tun).getTeilnemer().Contains(this.Gruppen[v]))
                    {
                        ergebnis = true;
                        break;
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

            return ergebnis;
        }

        private int CompareByErzielteTore(Teilnehmer pers1, Teilnehmer pers2)
        {
            if (Sortdirection)//aufwärts
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = 0;
                        retval = pers1.CompareByErzielteTore(pers2);
                        return retval;
                    }
                }
            }
            else//abwärts
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return -1;
                    }
                    else
                    {
                        int retval = 0;
                        retval = pers1.CompareByErzielteTore(pers2);
                        if (retval == 1)
                        {
                            retval = -1;
                        }
                        else if (retval == -1)
                        {
                            retval = 1;
                        }
                        else
                        { }
                        return retval;
                    }
                }
            }
        }
        private int CompareByName(Teilnehmer pers1, Teilnehmer pers2)
        {
            if (Sortdirection)//aufwärts
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = pers1.CompareByName(pers2);

                        return retval;
                    }
                }
            }
            else//abwärts
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return -1;
                    }
                    else
                    {
                        int retval = pers1.CompareByName(pers2);
                        if (retval == 1)
                        {
                            retval = -1;
                        }
                        else if (retval == -1)
                        {
                            retval = 1;
                        }
                        else
                        { }
                        return retval;
                    }
                }
            }
        }
        private int CompareByVorName(Teilnehmer pers1, Teilnehmer pers2)
        {
            if (Sortdirection)
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = pers1.CompareByVorname(pers2);

                        return retval;
                    }
                }
            }
            else
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = pers1.CompareByVorname(pers2);
                        if (retval == 1)
                        {
                            retval = -1;
                        }
                        else if (retval == -1)
                        {
                            retval = 1;
                        }
                        else
                        { }
                        return retval;
                    }
                }
            }
        }
        private int CompareByID(Teilnehmer pers1, Teilnehmer pers2)
        {
            if (Sortdirection)
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = pers1.CompareByID(pers2);

                        return retval;
                    }
                }
            }
            else
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return -1;
                    }
                    else
                    {
                        int retval = pers1.CompareByID(pers2);
                        if (retval == 1)
                        {
                            retval = -1;
                        }
                        else if (retval == -1)
                        {
                            retval = 1;
                        }
                        else
                        { }
                        return retval;
                    }
                }
            }
        }
        private int CompareByGeb(Teilnehmer pers1, Teilnehmer pers2)
        {
            if (Sortdirection)
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = pers1.CompareByGeb(pers2);

                        return retval;
                    }
                }
            }
            else
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return -1;
                    }
                    else
                    {
                        int retval = pers1.CompareByGeb(pers2);
                        if (retval == 1)
                        {
                            retval = -1;
                        }
                        else if (retval == -1)
                        {
                            retval = 1;
                        }
                        else
                        { }
                        return retval;
                    }
                }
            }
        }
        private int CompareBySportart(Teilnehmer pers1, Teilnehmer pers2)
        {
            if (Sortdirection)
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = pers1.CompareBySportart(pers2);

                        return retval;
                    }
                }
            }
            else
            {
                if (pers1 == null)
                {
                    if (pers2 == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (pers2 == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = pers1.CompareBySportart(pers2);
                        if (retval == 1)
                        {
                            retval = -1;
                        }
                        else if (retval == -1)
                        {
                            retval = 1;
                        }
                        else
                        { }
                        return retval;
                    }
                }
            }
        }
        #endregion

        #region Export XML,JSON
        public void XMLSichern(string path, int art)
        {
            if (art == 1)//Personen
            {
                if (this.Personen.Count > 0)
                {
                    Type[] personTypes = { typeof(Person),
                                   typeof(Fussballspieler),
                                   typeof(Handballspieler),
                                   typeof(Tennisspieler),
                                    typeof(WeitererSpieler),
                                    typeof(Physiotherapeut),
                                    typeof(Trainer),
                                    typeof(AndereAufgaben)};
                    XmlSerializer serializer = new XmlSerializer(this.Personen.GetType(), personTypes);

                    StreamWriter SR = new StreamWriter(new FileStream(path, FileMode.Create), Encoding.UTF8);

                    serializer.Serialize(SR, this.Personen);
                    SR.Close();
                }
                else
                { }
            }
            else if (art == 2)//Mannschaften
            {
                if (this.Mannschaften.Count > 0)
                {
                    Type[] mannschaftTypes = { typeof(Mannschaft),
                                    typeof(Person),
                                   typeof(Fussballspieler),
                                   typeof(Handballspieler),
                                   typeof(Tennisspieler),
                                    typeof(WeitererSpieler),
                                    typeof(Physiotherapeut),
                                    typeof(Trainer),
                                    typeof(AndereAufgaben)};

                    XmlSerializer serializer = new XmlSerializer(this.Mannschaften.GetType(), mannschaftTypes);

                    StreamWriter SR = new StreamWriter(new FileStream(path, FileMode.Create), Encoding.UTF8);

                    serializer.Serialize(SR, this.Mannschaften);
                    SR.Close();
                }
                else
                { }
            }
            else if (art == 3)//Gruppen
            {
                if (this.Gruppen.Count > 0)
                {
                    Type[] gruppenTypes = { typeof(Gruppe),
                                    typeof(Person),
                                   typeof(Fussballspieler),
                                   typeof(Handballspieler),
                                   typeof(Tennisspieler),
                                    typeof(WeitererSpieler),
                                    typeof(Physiotherapeut),
                                    typeof(Trainer),
                                    typeof(AndereAufgaben)};

                    XmlSerializer serializer = new XmlSerializer(this.Gruppen.GetType(), gruppenTypes);

                    StreamWriter SR = new StreamWriter(new FileStream(path, FileMode.Create), Encoding.UTF8);

                    serializer.Serialize(SR, this.Gruppen);
                    SR.Close();
                }
                else
                { }
            }
            else
            { }
        }

        public void JSONSichern(string path, int art)
        {
            if (art == 1)
            {
                if (this.Personen.Count <= 0)
                {
                    return;
                }
                else
                { }
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                string Serialized = JsonConvert.SerializeObject(this.Personen, settings);

                StreamWriter SR = new StreamWriter(new FileStream(path, FileMode.Create), Encoding.UTF8);
                SR.Write(Serialized);
                SR.Close();
            }
            else if (art == 2)
            {
                if (this.Mannschaften.Count <= 0)
                {
                    return;
                }
                else
                { }
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                string Serialized = JsonConvert.SerializeObject(this.Mannschaften, settings);

                //List<Mannschaft> deserializedList = JsonConvert.DeserializeObject<List<Person>>(Serialized, settings);

                StreamWriter SR = new StreamWriter(new FileStream(path, FileMode.Create), Encoding.UTF8);
                SR.Write(Serialized);
                SR.Close();
            }
            else if (art == 3)
            {
                if (this.Gruppen.Count <= 0)
                {
                    return;
                }
                else
                { }
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                string Serialized = JsonConvert.SerializeObject(this.Gruppen, settings);

                //List<Gruppe> deserializedList = JsonConvert.DeserializeObject<List<Person>>(Serialized, settings);

                StreamWriter SR = new StreamWriter(new FileStream(path, FileMode.Create), Encoding.UTF8);
                SR.Write(Serialized);
                SR.Close();
            }
            else
            { }
        }

        #endregion

        #region Datenbank
        public void loadData()
        {
            this.DB_Status = false;
            MySqlConnection Conn2;
            try
            {
                Conn = new MySqlConnection();
                Conn.ConnectionString = this.MyConnectionString;
                Conn.Open();
                this.DB_Status = true;
                this.Dbwarnung = true;
            }
            catch (MySqlException)
            {
                this.DB_Status = false;
                return;
            }
            //TODO vorhanden ID´s mit -1 zur Datenbank synchen
            #region sportarten
            Sportarten.Clear();
            SqlString = "select * from sportarten";
            MySqlCommand command = new MySqlCommand(SqlString, Conn);

            MySqlDataReader rdr = command.ExecuteReader();
            while (rdr.Read())
            {
                sportart neus = new sportart();
                neus.id = Convert.ToInt32(rdr.GetValue(0).ToString());
                neus.name = rdr.GetValue(1).ToString();
                neus.Mannschaft = rdr.GetBoolean(2);
                neus.PluspunkteproSpiel = rdr.GetInt32(3);
                neus.UnentschiedenpunkteproSpiel = rdr.GetInt32(4);
                neus.MinupunkteproSpiel = rdr.GetInt32(5);
                Sportarten.Add(neus);
            }
            rdr.Close();
            #endregion

            #region Personen
            this.Personen.Clear();
            SqlString = "select personen.id,personen.name,personen.vorname,personen.geburtsdatum,sportarten.bezeichnung," +
                        "personentypen.bezeichnung,personen.details from personen left join sportarten on sportarten.id = personen.sportart " +
                        "left join personentypen on personentypen.id = personen.typ;";

            command = new MySqlCommand(SqlString, Conn);

            rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                Person neu = null;
                int id = Convert.ToInt32(rdr.GetValue(0).ToString());
                string name = rdr.GetValue(1).ToString();
                string vorname = rdr.GetValue(2).ToString();
                DateTime geburtsdatum = DateTime.Parse(rdr.GetValue(3).ToString());
                string spart = rdr.GetValue(4).ToString();
                sportart sportart = null;
                foreach (sportart spa in this.Sportarten)
                {
                    if (spa.name == spart)
                    {
                        sportart = spa;
                        break;
                    }
                    else
                    { }
                }
                string typ = rdr.GetValue(5).ToString();
                string detailidstring = rdr.GetValue(6).ToString();
                int detailid = -1;
                if (detailidstring != "")
                {
                    detailid = rdr.GetInt32(6);
                }
                else
                { }

                switch (typ)
                {
                    case "Fussballspieler":
                        string newsqlstring = "select anzahlspiele, geschossenetore, position from fussballspieler where id = " + detailid + ";";
                        int anzspiele = -1;
                        int geschtore = -1;
                        string position = "";
                        Conn2 = new MySqlConnection();
                        Conn2.ConnectionString = this.MyConnectionString;
                        Conn2.Open();
                        MySqlCommand command2 = new MySqlCommand(newsqlstring, Conn2);
                        MySqlDataReader rdr2 = command2.ExecuteReader();
                        while (rdr2.Read())
                        {
                            anzspiele = Convert.ToInt32(rdr2.GetValue(0).ToString());
                            geschtore = Convert.ToInt32(rdr2.GetValue(1).ToString());
                            position = rdr2.GetValue(2).ToString();
                        }
                        rdr2.Close();
                        Conn2.Close();
                        neu = new Fussballspieler(name, vorname, geburtsdatum, anzspiele, position, geschtore, sportart);
                        neu.ID = id;
                        this.Personen.Add(neu);
                        break;
                    case "Handballspieler":
                        newsqlstring = "select anzahlspiele, geworfenetore, einsatzbereich from handballspieler where id = " + detailid + ";";
                        anzspiele = -1;
                        int gewtore = -1;
                        string einsatz = "";
                        Conn2 = new MySqlConnection();
                        Conn2.ConnectionString = this.MyConnectionString;
                        Conn2.Open();
                        command2 = new MySqlCommand(newsqlstring, Conn2);
                        rdr2 = command2.ExecuteReader();
                        while (rdr2.Read())
                        {
                            anzspiele = Convert.ToInt32(rdr2.GetValue(0).ToString());
                            gewtore = Convert.ToInt32(rdr2.GetValue(1).ToString());
                            einsatz = rdr2.GetValue(2).ToString();
                        }
                        rdr2.Close();
                        Conn2.Close();

                        neu = new Handballspieler(name, vorname, geburtsdatum, anzspiele, einsatz, gewtore, sportart);
                        neu.ID = id;
                        this.Personen.Add(neu);
                        break;
                    case "Tennisspieler":
                        newsqlstring = "select anzahlspiele,gewonnenespiele from tennisspieler where id = " + detailid + ";";
                        anzspiele = -1;
                        int gewspiel = -1;
                        Conn2 = new MySqlConnection();
                        Conn2.ConnectionString = this.MyConnectionString;
                        Conn2.Open();
                        command2 = new MySqlCommand(newsqlstring, Conn2);
                        rdr2 = command2.ExecuteReader();
                        while (rdr2.Read())
                        {
                            anzspiele = Convert.ToInt32(rdr2.GetValue(0).ToString());
                            gewspiel = Convert.ToInt32(rdr2.GetValue(1).ToString());
                        }
                        rdr2.Close();
                        Conn2.Close();

                        neu = new Tennisspieler(name, vorname, geburtsdatum, anzspiele, gewspiel, sportart);
                        neu.ID = id;
                        this.Personen.Add(neu);
                        break;
                    case "WeitererSpieler":
                        newsqlstring = "select anzahlspiele, gewonnenespiele from weitererspieler where id = " + detailid + ";";
                        anzspiele = -1;
                        gewspiel = -1;
                        Conn2 = new MySqlConnection();
                        Conn2.ConnectionString = this.MyConnectionString;
                        Conn2.Open();
                        command2 = new MySqlCommand(newsqlstring, Conn2);
                        rdr2 = command2.ExecuteReader();
                        while (rdr2.Read())
                        {
                            anzspiele = Convert.ToInt32(rdr2.GetValue(0).ToString());
                            gewspiel = Convert.ToInt32(rdr2.GetValue(1).ToString());
                        }
                        rdr2.Close();
                        Conn2.Close();

                        neu = new WeitererSpieler(name, vorname, geburtsdatum, sportart, anzspiele, gewspiel);
                        neu.ID = id;
                        this.Personen.Add(neu);
                        break;
                    case "Physiotherapeut":
                        newsqlstring = "select anzahljahre from physiotherapeut where id = " + detailid + ";";
                        int anzjahre = -1;
                        Conn2 = new MySqlConnection();
                        Conn2.ConnectionString = this.MyConnectionString;
                        Conn2.Open();
                        command2 = new MySqlCommand(newsqlstring, Conn2);
                        rdr2 = command2.ExecuteReader();
                        while (rdr2.Read())
                        {
                            anzjahre = Convert.ToInt32(rdr2.GetValue(0).ToString());
                        }
                        rdr2.Close();
                        Conn2.Close();

                        neu = new Physiotherapeut(name, vorname, geburtsdatum, anzjahre, sportart);
                        neu.ID = id;
                        this.Personen.Add(neu);
                        break;
                    case "Trainer":
                        newsqlstring = "select anzahlvereine from trainer where id = " + detailid + ";";
                        int anzvereine = -1;
                        Conn2 = new MySqlConnection();
                        Conn2.ConnectionString = this.MyConnectionString;
                        Conn2.Open();
                        command2 = new MySqlCommand(newsqlstring, Conn2);
                        rdr2 = command2.ExecuteReader();
                        while (rdr2.Read())
                        {
                            anzvereine = Convert.ToInt32(rdr2.GetValue(0).ToString());
                        }
                        rdr2.Close();
                        Conn2.Close();

                        neu = new Trainer(name, vorname, geburtsdatum, anzvereine, sportart);
                        neu.ID = id;
                        this.Personen.Add(neu);
                        break;
                    case "AndereAufgaben":
                        newsqlstring = "select Einsatz from andereaufgaben where id = " + detailid + ";";
                        string aufgabe = "";
                        Conn2 = new MySqlConnection();
                        Conn2.ConnectionString = this.MyConnectionString;
                        Conn2.Open();
                        command2 = new MySqlCommand(newsqlstring, Conn2);
                        rdr2 = command2.ExecuteReader();
                        while (rdr2.Read())
                        {
                            aufgabe = rdr2.GetValue(0).ToString();
                        }
                        rdr2.Close();
                        Conn2.Close();

                        neu = new AndereAufgaben(name, vorname, geburtsdatum, sportart, aufgabe);
                        neu.ID = id;
                        this.Personen.Add(neu);
                        break;
                }
            }
            rdr.Close();
            Conn.Close();
            #endregion

            #region Mannschaften
            this.Mannschaften.Clear();
            try
            {
                Conn = new MySqlConnection();
                Conn.ConnectionString = this.MyConnectionString;
                Conn.Open();
            }
            catch (MySqlException)
            {
                return;
            }

            SqlString = "select mannschaften.id,mannschaften.name,sportarten.bezeichnung," +
                        "mannschaften.punkte,mannschaften.toreplus,mannschaften.toreminus," +
                        "mannschaften.gewonnenespiele,mannschaften.verlorenespiele,mannschaften.unentschieden " +
                        "from mannschaften " +
                        "left join sportarten on sportarten.id = mannschaften.sportart;";

            command = new MySqlCommand(SqlString, Conn);

            rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                Mannschaft neu = null;
                int id = rdr.GetInt32(0);
                string name = rdr.GetValue(1).ToString();
                string spart = rdr.GetValue(2).ToString();
                sportart sportart = null;
                foreach (sportart spa in this.Sportarten)
                {
                    if (spa.name == spart)
                    {
                        sportart = spa;
                        break;
                    }
                    else
                    { }
                }
                int punkte = rdr.GetInt32(3);
                int toreplus = rdr.GetInt32(4);
                int toreminus = rdr.GetInt32(5);
                int gewonnen = rdr.GetInt32(6);
                int verloren = rdr.GetInt32(7);
                int unentschieden = rdr.GetInt32(8);

                neu = new Mannschaft(name, sportart);
                neu.ID = id;
                neu.Punkte = punkte;
                neu.Toreminus = toreminus;
                neu.TorePlus = toreplus;
                neu.GewonneneSpiele = gewonnen;
                neu.VerloreneSpiele = verloren;
                neu.Unentschieden = unentschieden;
                neu.Anzahlspiele = neu.GewonneneSpiele + neu.VerloreneSpiele + neu.Unentschieden;
                //Mannschaftsmitglieder auslesen
                try
                {
                    Conn2 = new MySqlConnection();
                    Conn2.ConnectionString = this.MyConnectionString;
                    Conn2.Open();
                }
                catch (MySqlException)
                {
                    return;
                }
                string SqlString2 = "select mannschaftsmitglieder.person " +
                    "from mannschaftsmitglieder " +
                    "where mannschaftsmitglieder.mannschaft = '" + id + "';";
                MySqlCommand command2 = new MySqlCommand(SqlString2, Conn2);
                MySqlDataReader rdr2 = command2.ExecuteReader();
                while (rdr2.Read())
                {
                    int persid = rdr2.GetInt32(0);

                    foreach (Person pers in this.Personen)
                    {
                        if (pers.ID == persid)
                        {
                            neu.Mitglieder.Add(pers);
                            break;
                        }
                        else
                        { }
                    }
                }
                rdr2.Close();
                Conn2.Close();
                this.Mannschaften.Add(neu);
            }
            rdr.Close();
            Conn.Close();
            #endregion

            #region Gruppen
            this.Gruppen.Clear();
            try
            {
                Conn = new MySqlConnection();
                Conn.ConnectionString = this.MyConnectionString;
                Conn.Open();
            }
            catch (MySqlException)
            {
                return;
            }

            SqlString = "select gruppen.id,gruppen.name,sportarten.bezeichnung " +
                        "from gruppen " +
                        "left join sportarten on sportarten.id = gruppen.sportart;";

            command = new MySqlCommand(SqlString, Conn);

            rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                Gruppe neu = null;
                int id = rdr.GetInt32(0);
                string name = rdr.GetValue(1).ToString();
                string spart = rdr.GetValue(2).ToString();
                sportart sportart = null;
                foreach (sportart spa in this.Sportarten)
                {
                    if (spa.name == spart)
                    {
                        sportart = spa;
                        break;
                    }
                    else
                    { }
                }
                neu = new Gruppe(name, sportart);
                neu.ID = id;

                //Gruppenmitglieder auslesen
                try
                {
                    Conn2 = new MySqlConnection();
                    Conn2.ConnectionString = this.MyConnectionString;
                    Conn2.Open();
                }
                catch (MySqlException)
                {
                    return;
                }
                string SqlString2 = "select gruppenmitglieder.person " +
                    "from gruppenmitglieder " +
                    "where gruppenmitglieder.gruppe = '" + id + "';";
                MySqlCommand command2 = new MySqlCommand(SqlString2, Conn2);
                MySqlDataReader rdr2 = command2.ExecuteReader();
                while (rdr2.Read())
                {
                    int persid = rdr2.GetInt32(0);

                    foreach (Person pers in this.Personen)
                    {
                        if (pers.ID == persid)
                        {
                            neu.Mitglieder.Add(pers);
                            break;
                        }
                        else
                        { }
                    }
                }
                rdr2.Close();
                Conn2.Close();
                this.Gruppen.Add(neu);
            }
            rdr.Close();
            Conn.Close();
            #endregion

            #region Turniere
            try
            {
                Conn = new MySqlConnection();
                Conn.ConnectionString = this.MyConnectionString;
                Conn.Open();
            }
            catch (MySqlException)
            {
                return;
            }

            SqlString = "select turnier.id,turnier.bezeichnung,sportarten.bezeichnung, " +
                        "turnier.typ " +
                        "from turnier " +
                        "left join sportarten on sportarten.id = turnier.sportart;";

            command = new MySqlCommand(SqlString, Conn);

            rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                Turnier neu = null;
                int id = rdr.GetInt32(0);
                string name = rdr.GetValue(1).ToString();
                string spart = rdr.GetValue(2).ToString();
                sportart sportart = null;
                foreach (sportart spa in this.Sportarten)
                {
                    if (spa.name == spart)
                    {
                        sportart = spa;
                        break;
                    }
                    else
                    { }
                }
                int typ = rdr.GetInt32(3);
                //Turnierteilnehmer auslesen
                try
                {
                    Conn2 = new MySqlConnection();
                    Conn2.ConnectionString = this.MyConnectionString;
                    Conn2.Open();
                }
                catch (MySqlException)
                {
                    return;
                }
                if (typ == 1)
                {
                    neu = new MannschaftsTurnier(name, sportart, new List<Mannschaft>());
                    neu.ID = id;
                }
                else
                {
                    neu = new GruppenTurnier(name, sportart, new List<Gruppe>());
                    neu.ID = id;
                }

                string SqlString2 = "select * " +
                    "from turnierteilnehmer " +
                    "where turnierteilnehmer.turnier = '" + id + "';";
                MySqlCommand command2 = new MySqlCommand(SqlString2, Conn2);
                MySqlDataReader rdr2 = command2.ExecuteReader();
                //Alle Teilnehmer des Turniers auslesen
                while (rdr2.Read())
                {
                    int teilid = -1;
                    if (typ == 1)
                    {
                        if (rdr2.GetValue(1).ToString() != "")
                        {
                            teilid = rdr2.GetInt32(1);
                        }
                        else
                        {                        
                        }
                        foreach (Mannschaft man in this.Mannschaften)
                        {
                            if (man.ID == teilid)
                            {
                                ((MannschaftsTurnier)neu).Teilnehmer.Add(man);
                            }
                            else
                            { }
                        }
                    }
                    else
                    {
                        if (rdr2.GetValue(2).ToString() != "")
                        {
                            teilid = rdr2.GetInt32(2);
                        }
                        else
                        { }
                        foreach (Gruppe grp in this.Gruppen)
                        {
                            if (grp.ID == teilid)
                            {
                                ((GruppenTurnier)neu).getTeilnemer().Add(grp);
                            }
                            else
                            { }
                        }
                    }
                }
                rdr2.Close();
                Conn2.Close();
                this.Turniere.Add(neu);
            }
            rdr.Close();
            Conn.Close();
            #endregion
        }
        #endregion
        #endregion
    }
}
