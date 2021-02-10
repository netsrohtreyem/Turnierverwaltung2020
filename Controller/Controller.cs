//dateikopf
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace Turnierverwaltung2020
{
    public class Controller
    {
        #region Eigenschaften
        private string _HTTPSession;
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
        private int _maxSportarten;
        private int _maxSpiele;
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
        public string HTTPSession { get => _HTTPSession; set => _HTTPSession = value; }
        public int MaxSportarten { get => _maxSportarten; set => _maxSportarten = value; }
        public int MaxSpiele { get => _maxSpiele; set => _maxSpiele = value; }
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
            MyConnectionString = Properties.Settings.Default.Connectionstring;
            SelectedTurnier = null;
            MaxGruppen = 0;
            MaxMannschaften = 0;
            MaxPersonen = 0;
            MaxTurniere = 0;
            MaxSportarten = 0;
            MaxSpiele = 0;
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
            foreach(sportart sp in this.Sportarten)
            {
                if (sp.name.Equals(value.name))
                {
                    return;
                }
                else
                { }
            }
            if (value.id == -1)
            {
                MaxSportarten++;
                value.id = MaxSportarten;
                this.Sportarten.Add(value);
            }
            else
            {
                value.id = MaxSportarten + 1;
                MaxSportarten++;
                this.Sportarten.Add(value);
            }
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
                ergebnis = this.Sportarten.Remove(loesch);
            }
            else
            {
                ergebnis = false;
                
            }
            return ergebnis;
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
                }
                else
                { }
            }
        }
        private void SetSportartenMax()
        {
            MaxSportarten = 0;
            foreach (sportart sp in Sportarten)
            {
                if (sp.id > MaxSportarten)
                {
                    MaxSportarten = sp.id;
                }
                else
                { }
            }
        }
        #endregion

        #region Person
        public bool AddPerson(Person value)
        {
            foreach(Person pers in this.Personen)
            {
                if(pers.Name.Equals(value.Name) && pers.getSportart().Equals(value.getSportart()))
                {
                    return false;
                }
                else
                { }
            }
            bool ergebnis = true;
            if (value.ID == -1)
            {
                value.ID = this.MaxPersonen + 1;
                this.MaxPersonen++;
                this.Personen.Add(value);
            }
            else
            {
                this.MaxPersonen++;
                value.ID = this.MaxPersonen;
                this.Personen.Add(value);
            }
            return ergebnis;
        }
        public bool DeletePerson(int nummer)
        {
            if (nummer > 0 && nummer <= this.Personen.Count)
            {
                this.Personen.RemoveAt(nummer - 1);
                return true;
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
        private void SetPersonenMax()
        {
            MaxPersonen = 0;
            foreach (Person pers in Personen)
            {
                if (pers.ID > MaxPersonen)
                {
                    MaxSportarten = pers.ID;
                }
                else
                { }
            }
        }
        #endregion

        #region Mannschaft
        public bool AddMannschaft(Mannschaft value)
        {
            bool ergebnis = true;
            foreach(Mannschaft man in this.Mannschaften)
            {
                if(man.Name.Equals(value.Name) && man.Sportart.name.Equals(value.Sportart.name))
                {
                    ergebnis = false;
                    break;
                }
                else
                {
                    ergebnis = true;
                }
            }

            if (ergebnis)
            {
                if (value.ID == -1)
                {
                    value.ID = this.MaxMannschaften + 1;
                    this.Mannschaften.Add(value);
                    this.MaxMannschaften++;
                }
                else
                {
                    this.MaxMannschaften++;
                    value.ID = this.MaxMannschaften;
                    this.Mannschaften.Add(value);
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
            foreach (Mannschaft man in this.Mannschaften)
            {
                if (man.Name.Equals(value.Name) && man.Sportart.name.Equals(value.Sportart.name))
                {
                    ergebnis = false;
                    break;
                }
                else
                {
                    ergebnis = true;
                }
            }
            if (ergebnis)
            {
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


                if (value.ID == -1)
                {
                    value.ID = this.MaxMannschaften + 1;
                    this.Mannschaften.Add(value);
                    this.MaxMannschaften++;
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

            }
            else
            { }

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

            this.Mannschaften.RemoveAt(index - 1);
            ergebnis = true;


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
        private void SetMannschaftenMax()
        {
            MaxMannschaften = 0;
            foreach (Mannschaft man in Mannschaften)
            {
                if (man.ID > MaxMannschaften)
                {
                    MaxMannschaften = man.ID;
                }
                else
                { }
            }
        }
        #endregion

        #region Gruppe
        public bool AddGruppe(Gruppe value, ListItemCollection mitgliedervalue)
        {
            bool ergebnis = true;
            foreach (Gruppe grp in this.Gruppen)
            {
                if (grp.Name.Equals(value.Name) && grp.Sportart.name.Equals(value.Sportart.name))
                {
                    ergebnis = false;
                    break;
                }
                else
                {
                    ergebnis = true;
                }
            }
            if (ergebnis)
            {
                List<int> Mitgliederliste = new List<int>();
                if (mitgliedervalue == null)
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

                if (value.ID == -1)
                {
                    value.ID = this.MaxGruppen + 1;
                    MaxGruppen++;
                    this.Gruppen.Add(value);
                }
                else
                {
                    MaxGruppen++;
                    value.ID = this.MaxGruppen;
                    this.Gruppen.Add(value);
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
            }
            else
            { }
            return ergebnis;
        }
        public bool AddGruppe(Gruppe value)
        {
            bool ergebnis = true;
            foreach (Gruppe grp in this.Gruppen)
            {
                if (grp.Name.Equals(value.Name) && grp.Sportart.name.Equals(value.Sportart.name))
                {
                    ergebnis = false;
                    break;
                }
                else
                {
                    ergebnis = true;
                }
            }

            if (ergebnis)
            {
                if (value.ID == -1)
                {
                    value.ID = this.MaxGruppen + 1;
                    this.Gruppen.Add(value);
                    this.MaxGruppen++;
                }
                else
                {
                    this.MaxGruppen++;
                    value.ID = this.MaxGruppen;
                    this.Gruppen.Add(value);
                }
            }
            else
            { }

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

            this.Gruppen.RemoveAt(index - 1);
            ergebnis = true;

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
        private void SetGruppenMax()
        {
            MaxGruppen = 0;
            foreach (Gruppe grp in Gruppen)
            {
                if (grp.ID > MaxGruppen)
                {
                    MaxGruppen = grp.ID;
                }
                else
                { }
            }
        }
        #endregion

        #region Turnier
        public bool AddTurnier(Turnier value)
        {
            bool ergebnis = false;
            sportart toadd = null;
            foreach (sportart spart in this.Sportarten)
            {
                if (spart.name == value.Sportart.name)
                {
                    toadd = spart;
                    break;
                }
                else
                {

                }
            }
            if (toadd == null)//neue Sportart!
            {
                toadd = value.Sportart;
                this.AddSportArt(toadd);
            }
            else
            { }
            if (value is MannschaftsTurnier)
            {
                //unbekannte Personen in den Mannschaften
                //Unbekannte Mannschaft vorhanden?
                foreach (Teilnehmer tln in ((MannschaftsTurnier)value).Teilnehmer)
                {
                    bool persongefunden = false;
                    foreach (Person pers in (((Mannschaft)tln).Mitglieder))
                    {
                        foreach (Teilnehmer persvorh in this.Personen)
                        {
                            if (persvorh.Name.Equals(pers.Name) && persvorh.Sportart.name.Equals(pers.Sportart.name))
                            {
                                persongefunden = true;
                                break;
                            }
                            else
                            { }
                        }
                        if (!persongefunden)
                        {
                            this.AddPerson(pers);
                        }
                        else
                        {
                            persongefunden = false;
                        }
                    }

                    bool gefunden = false;
                    foreach(Mannschaft man in this.Mannschaften)
                    {
                        if(tln.Name.Equals(man.Name))
                        {
                            gefunden = true;
                            break;
                        }
                        else
                        { }
                    }
                    if(gefunden == false)
                    {
                        //hinzufuegen                       
                        this.AddMannschaft((Mannschaft)tln);
                    }
                    else
                    { }
                }
            }
            else if(value is GruppenTurnier)
            {
                foreach (Teilnehmer tln in ((GruppenTurnier)value).Gruppen)
                {
                    bool persongefunden = false;
                    foreach (Person teiln in (((Gruppe)tln).Mitglieder))
                    {
                        foreach (Teilnehmer teilnvorh in this.Personen)
                        {
                            if (teilnvorh.Name.Equals(teiln.Name) && teilnvorh.Sportart.name.Equals(teiln.Sportart.name))
                            {
                                persongefunden = true;
                                break;
                            }
                            else
                            { }
                        }
                        if (!persongefunden)
                        {
                            this.AddPerson(teiln);
                        }
                        else
                        {
                            persongefunden = false;
                        }
                    }
                }
            }
            else
            {}

            if (value.ID == -1)
            {
                value.ID = this.MaxTurniere + 1;
                    this.Turniere.Add(value);
                this.MaxTurniere++;
            }
            else
            {
                bool gefunden = false;
                foreach(Turnier trn in this.Turniere)
                {
                    if(value.ID == trn.ID)
                    {
                        value.ID = this.MaxTurniere + 1;
                        this.Turniere.Add(value);
                        this.MaxTurniere++;
                        gefunden = true;
                        break;
                    }
                    else
                    { }
                }
                if(!gefunden)
                {
                    value.ID = this.MaxTurniere + 1;
                    this.Turniere.Add(value);
                    this.MaxTurniere++;
                }
                else
                { }
            }


            ergebnis = true;

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

            this.Turniere.RemoveAt(id);

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
            foreach (Gruppe grp in this.SelectedTurnier.getTeilnehmer())
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
        private void SetMaxTurniere()
        {
            MaxTurniere = 0;
            foreach (Turnier turn in Turniere)
            {
                if (turn.ID > MaxTurniere)
                {
                    MaxTurniere = turn.ID;
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


            if (neu.ID == -1)
            {
                neu.ID = ((MannschaftsTurnier)SelectedTurnier).Spiele.Count + 1;
            }
            else
            { }

            SelectedTurnier.addSpiel(neu);

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


            if (neu.ID == -1)
            {
                neu.ID = ((MannschaftsTurnier)SelectedTurnier).Spiele.Count + 1;
            }
            else
            { }

            SelectedTurnier.addSpiel(neu);


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

            int gruppenid = SelectedTurnier.getTeilnehmer()[grpid - 1].ID;

            Spiel neu = new Gruppenspiel(SelectedTurnier, gruppenid, pers1, pers2);

            if (this.SelectedTurnier.isSpielVorhanden(neu))
            {
                return;
            }
            else
            { }

            if (neu.ID == -1)
            {
                neu.ID = ((GruppenTurnier)SelectedTurnier).Spiele.Count + 1;
            }
            else
            { }
            SelectedTurnier.addSpiel(neu);
        }
        public void AddSpielToGruppenTurnier(Spiel neu)
        {
            if (SelectedTurnier.isSpielVorhanden(neu))
            {
                return;
            }
            else
            { }


            if (neu.ID == -1)
            {
                neu.ID = ((GruppenTurnier)SelectedTurnier).Spiele.Count + 1;
            }
            else
            { }

            SelectedTurnier.addSpiel(neu);


            if (SelectedTurnierSpieltag == 0)
            {
                SelectedTurnierSpieltag = 1;
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
                List<Teilnehmer> OrgListe = new List<Teilnehmer>(this.SelectedTurnier.getTeilnehmer());

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
                        Teilnehmer pers1 = ((Gruppe)this.SelectedTurnier.getTeilnehmer()[selectedGruppe - 1]).Mitglieder[teiln1];
                        Teilnehmer pers2 = ((Gruppe)this.SelectedTurnier.getTeilnehmer()[selectedGruppe - 1]).Mitglieder[teiln2];
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
                        Teilnehmer pers1 = ((Gruppe)this.SelectedTurnier.getTeilnehmer()[selectedGruppe - 1]).Mitglieder[teiln1];
                        Teilnehmer pers2 = ((Gruppe)this.SelectedTurnier.getTeilnehmer()[selectedGruppe - 1]).Mitglieder[teiln2];
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
        private void SetMaxSpiele()
        {
            MaxSpiele = 0;
            foreach (Spiel sp in SelectedTurnier.Spiele)
            {
                if (sp.ID > MaxSpiele)
                {
                    MaxSpiele = sp.ID;
                }
                else
                { }
            }
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
                    if (((GruppenTurnier)tun).getTeilnehmer().Contains(this.Gruppen[v]))
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
        public void SportartenAlsXMLSichern(Page view)
        {
            if (Sportarten.Count > 0)
            {
                string FileName = "Sportartenliste-" + DateTime.Now.ToShortDateString() + ".xml";
                string FilePath = view.Server.MapPath("~/" + FileName);
                //XML File erzeugen
                XmlSerializer serializer = new XmlSerializer(typeof(List<sportart>));

                StreamWriter SR = new StreamWriter(new FileStream(FilePath, FileMode.Create), Encoding.UTF8);
                try
                {
                    serializer.Serialize(SR, this.Sportarten);
                }
                catch (Exception)
                {
                    SR.Close();
                    return;
                }
                SR.Close();
                //File senden
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "text/xml";
                response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
                response.TransmitFile(FilePath);
                response.Flush();
                //File löschen
                FileInfo fi = new FileInfo(FilePath);
                fi.Delete();
                //File.Delete(view.Server.MapPath("~/" + FileName));
                response.End();
            }
            else
            { }
        }

        public void SportartenAlsXMLLaden(FileUpload upload, Page view)
        {
            //File uploaden
            string Path = view.Server.MapPath("~/") + upload.FileName;
            if (upload.PostedFile.ContentType == "text/xml")
            {
                upload.SaveAs(Path);
            }
            else
            { }

            //in Liste laden
            XmlSerializer serializer = new XmlSerializer(typeof(List<sportart>));

            StreamReader SR = new StreamReader(new FileStream(Path, FileMode.Open), Encoding.UTF8);
            try
            {
                this.Sportarten.Clear();
                this.Sportarten =  (List<sportart>)serializer.Deserialize(SR);
                this.SetSportartenMax();
            }
            catch (Exception)
            {
                SR.Close();
                return;
            }
            SR.Close();
            SR.Dispose();
            //File löschen
            FileInfo fi = new FileInfo(Path);
            fi.Delete();
        }

        public void PersonenAlsXMLSichern(Page view)
        {
            if (Personen.Count > 0)
            {
                string FileName = "Teilnehmerliste-" + DateTime.Now.ToShortDateString() + ".xml";
                string FilePath = view.Server.MapPath("~/" + FileName);
                //XML File erzeugen
                Type[] personTypes = { typeof(Teilnehmer),
                                       typeof(Person),
                                       typeof(Fussballspieler),
                                       typeof(Handballspieler),
                                       typeof(Tennisspieler),
                                        typeof(WeitererSpieler),
                                        typeof(Physiotherapeut),
                                        typeof(Trainer),
                                        typeof(AndereAufgaben)};
                XmlSerializer serializer = new XmlSerializer(this.Personen.GetType(), personTypes);

                StreamWriter SR = new StreamWriter(new FileStream(FilePath, FileMode.Create), Encoding.UTF8);
                try
                {
                    serializer.Serialize(SR, this.Personen);
                }
                catch (Exception)
                {
                    SR.Close();
                    return;
                }
                SR.Close();
                //File senden
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "text/xml";
                response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
                response.TransmitFile(FilePath);
                response.Flush();
                //File löschen
                FileInfo fi = new FileInfo(FilePath);
                fi.Delete();
                response.End();
            }
            else
            { }
        }

        public void PersonenAlsXMLLaden(FileUpload upload, Page view)
        {
            //File uploaden
            string Path = view.Server.MapPath("~/") + upload.FileName;
            if (upload.PostedFile.ContentType == "text/xml")
            {
                upload.SaveAs(Path);
            }
            else
            { }

            //in Liste laden
            Type[] personTypes = {     typeof(Teilnehmer),                                        
                                        typeof(Person),
                                       typeof(Fussballspieler),
                                       typeof(Handballspieler),
                                       typeof(Tennisspieler),
                                        typeof(WeitererSpieler),
                                        typeof(Physiotherapeut),
                                        typeof(Trainer),
                                        typeof(AndereAufgaben)};
            XmlSerializer serializer = new XmlSerializer(this.Personen.GetType(), personTypes);

            StreamReader SR = new StreamReader(new FileStream(Path, FileMode.Open), Encoding.UTF8);
            try
            {
                this.Personen.Clear();
                this.Personen = (List<Teilnehmer>)serializer.Deserialize(SR);
                this.SetPersonenMax();
            }
            catch (Exception)
            {
                SR.Close();
                return;
            }
            SR.Close();
            SR.Dispose();
            //File löschen
            FileInfo fi = new FileInfo(Path);
            fi.Delete();
        }

        public void MannschaftenAlsXMLSichern(Page view)
        {
            if (Mannschaften.Count > 0)
            {
                string FileName = "Mannschaftsliste-" + DateTime.Now.ToShortDateString() + ".xml";
                string FilePath = view.Server.MapPath("~/" + FileName);
                //XML File erzeugen
                Type[] manTypes = { typeof(Mannschaft),
                                        typeof(Teilnehmer),
                                       typeof(Person),
                                       typeof(Fussballspieler),
                                       typeof(Handballspieler),
                                       typeof(Tennisspieler),
                                        typeof(WeitererSpieler),
                                        typeof(Physiotherapeut),
                                        typeof(Trainer),
                                        typeof(AndereAufgaben)};
                XmlSerializer serializer = new XmlSerializer(this.Mannschaften.GetType(), manTypes);

                StreamWriter SR = new StreamWriter(new FileStream(FilePath, FileMode.Create), Encoding.UTF8);
                try
                {
                    serializer.Serialize(SR, this.Mannschaften);
                }
                catch (Exception)
                {
                    SR.Close();
                    return;
                }
                SR.Close();
                //File senden
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "text/xml";
                response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
                response.TransmitFile(FilePath);
                response.Flush();
                //File löschen
                FileInfo fi = new FileInfo(FilePath);
                fi.Delete();
                response.End();
            }
            else
            { }
        }

        public void MannschaftenAlsXMLLaden(FileUpload upload, Page view)
        {
            //File uploaden
            string Path = view.Server.MapPath("~/") + upload.FileName;
            if (upload.PostedFile.ContentType == "text/xml")
            {
                upload.SaveAs(Path);
            }
            else
            { }

            //in Liste laden
            Type[] manTypes = { typeof(Mannschaft),
                                    typeof(Teilnehmer),
                                        typeof(Person),
                                       typeof(Fussballspieler),
                                       typeof(Handballspieler),
                                       typeof(Tennisspieler),
                                        typeof(WeitererSpieler),
                                        typeof(Physiotherapeut),
                                        typeof(Trainer),
                                        typeof(AndereAufgaben)};
            XmlSerializer serializer = new XmlSerializer(this.Mannschaften.GetType(), manTypes);

            StreamReader SR = new StreamReader(new FileStream(Path, FileMode.Open), Encoding.UTF8);
            try
            {
                this.Mannschaften.Clear();
                this.Mannschaften = (List<Mannschaft>)serializer.Deserialize(SR);
                this.SetMannschaftenMax();
            }
            catch (Exception)
            {
                SR.Close();
                return;
            }
            SR.Close();
            SR.Dispose();
            //File löschen
            FileInfo fi = new FileInfo(Path);
            fi.Delete();
        }

        public void GruppenAlsXMLSichern(Page view)
        {
            if (Gruppen.Count > 0)
            {
                string FileName = "Gruppenliste-" + DateTime.Now.ToShortDateString() + ".xml";
                string FilePath = view.Server.MapPath("~/" + FileName);
                //XML File erzeugen
                Type[] grpTypes = { typeof(Gruppe),
                                        typeof(Teilnehmer),
                                       typeof(Person),
                                       typeof(Fussballspieler),
                                       typeof(Handballspieler),
                                       typeof(Tennisspieler),
                                        typeof(WeitererSpieler),
                                        typeof(Physiotherapeut),
                                        typeof(Trainer),
                                        typeof(AndereAufgaben)};
                XmlSerializer serializer = new XmlSerializer(this.Gruppen.GetType(), grpTypes);

                StreamWriter SR = new StreamWriter(new FileStream(FilePath, FileMode.Create), Encoding.UTF8);
                try
                {
                    serializer.Serialize(SR, this.Gruppen);
                }
                catch (Exception)
                {
                    SR.Close();
                    return;
                }
                SR.Close();
                //File senden
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "text/xml";
                response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
                response.TransmitFile(FilePath);
                response.Flush();
                //File löschen
                FileInfo fi = new FileInfo(FilePath);
                fi.Delete();
                response.End();
            }
            else
            { }
        }

        public void GruppenAlsXMLLaden(FileUpload upload, Page view)
        {
            //File uploaden
            string Path = view.Server.MapPath("~/") + upload.FileName;
            if (upload.PostedFile.ContentType == "text/xml")
            {
                upload.SaveAs(Path);
            }
            else
            { }

            //in Liste laden
            Type[] grpTypes = { typeof(Gruppe),
                                    typeof(Teilnehmer),
                                        typeof(Person),
                                       typeof(Fussballspieler),
                                       typeof(Handballspieler),
                                       typeof(Tennisspieler),
                                        typeof(WeitererSpieler),
                                        typeof(Physiotherapeut),
                                        typeof(Trainer),
                                        typeof(AndereAufgaben)};
            XmlSerializer serializer = new XmlSerializer(this.Gruppen.GetType(), grpTypes);

            StreamReader SR = new StreamReader(new FileStream(Path, FileMode.Open), Encoding.UTF8);
            try
            {
                this.Gruppen.Clear();
                this.Gruppen = (List<Gruppe>)serializer.Deserialize(SR);
                this.SetGruppenMax();
            }
            catch (Exception)
            {
                SR.Close();
                return;
            }
            SR.Close();
            SR.Dispose();
            //File löschen
            FileInfo fi = new FileInfo(Path);
            fi.Delete();
        }

        public void TurniereAlsXMLSichern(Page view)
        {
            if (this.SelectedTurnier.getAnzahlTeilnehmer()> 0)
            {
                string FileName = "Turnier-"+SelectedTurnier.Bezeichnung + "-" + DateTime.Now.ToShortDateString() + ".xml";
                FileName = FileName.Replace('\\', '_');
                FileName = FileName.Replace('/', '_');
                string FilePath = view.Server.MapPath("~/" + FileName);
                //XML File erzeugen
                Type[] turnTypes = { typeof(Turnier),
                                     typeof(MannschaftsTurnier),
                                     typeof(GruppenTurnier),
                                     typeof(Spiel),
                                     typeof(Mannschaftsspiel),
                                     typeof(Gruppenspiel),
                                     typeof(sportart),
                                     typeof(Teilnehmer),
                                     typeof(Mannschaft),
                                     typeof(Gruppe),
                                     typeof(Person),
                                     typeof(Fussballspieler),
                                     typeof(Handballspieler),
                                     typeof(Tennisspieler),
                                     typeof(WeitererSpieler),
                                     typeof(Physiotherapeut),
                                     typeof(Trainer),
                                     typeof(AndereAufgaben)};
                XmlSerializer serializer = new XmlSerializer(this.SelectedTurnier.GetType(), turnTypes);

                StreamWriter SR = new StreamWriter(new FileStream(FilePath, FileMode.Create), Encoding.UTF8);
                try
                {
                    serializer.Serialize(SR, this.SelectedTurnier);
                }
                catch (Exception)
                {
                    SR.Close();
                    return;
                }
                SR.Close();
                //File senden
                HttpResponse response = HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "text/xml";
                response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
                response.TransmitFile(FilePath);
                response.Flush();
                //File löschen
                FileInfo fi = new FileInfo(FilePath);
                fi.Delete();
                response.End();
            }
            else
            { }
        }

        public void TurniereAlsXMLLaden(FileUpload upload, Page view)
        {
            //File uploaden
            string Path = view.Server.MapPath("~/") + upload.FileName;
            if (upload.PostedFile.ContentType == "text/xml")
            {
                upload.SaveAs(Path);
            }
            else
            { }

            //in Liste laden
            Type[] turnTypes = {     typeof(MannschaftsTurnier),
                                     typeof(GruppenTurnier),
                                     typeof(Turnier),
                                     typeof(Spiel),
                                     typeof(Mannschaftsspiel),
                                     typeof(Gruppenspiel),
                                     typeof(sportart),
                                     typeof(Teilnehmer),
                                     typeof(Mannschaft),
                                     typeof(Gruppe),
                                     typeof(Person),
                                     typeof(Fussballspieler),
                                     typeof(Handballspieler),
                                     typeof(Tennisspieler),
                                     typeof(WeitererSpieler),
                                     typeof(Physiotherapeut),
                                     typeof(Trainer),
                                     typeof(AndereAufgaben)};

            XmlSerializer serializer = null;
            StreamReader SR = new StreamReader(new FileStream(Path, FileMode.Open), Encoding.UTF8);
            SR.ReadLine();
            string typus = SR.ReadLine();
            SR.Close();
            SR = new StreamReader(new FileStream(Path, FileMode.Open), Encoding.UTF8);
            if (typus.Contains("MannschaftsTurnier"))
            {
                serializer = new XmlSerializer(typeof(MannschaftsTurnier), turnTypes);
            }
            else if(typus.Contains("GruppenTurnier"))
            {
                serializer = new XmlSerializer(typeof(GruppenTurnier), turnTypes);
            }
            else
            {
                return;
            }
            object neu = null;
            try
            {
                neu = serializer.Deserialize(SR);

            }
            catch (Exception)
            {
                SR.Close();
                return;
            }
            //Gibt es das Turnier schon?
            foreach(Turnier turn in this.Turniere)
            {
                if(turn.Bezeichnung.Equals(((Turnier)neu).Bezeichnung))
                {
                    ((Turnier)neu).Bezeichnung = ((Turnier)neu).Bezeichnung + "-Copy";
                }
                else
                { }
            }            

            SR.Close();
            SR.Dispose();
            //File löschen
            FileInfo fi = new FileInfo(Path);
            fi.Delete();
            if (neu is MannschaftsTurnier)
            {
                //Sportarten
                this.AddSportArt(((MannschaftsTurnier)neu).Sportart);

                //Personen + Mannschaften
                foreach (Teilnehmer tln in ((MannschaftsTurnier)neu).Teilnehmer)
                {
                    Mannschaft man = (Mannschaft)tln;
                    foreach(Person pers in man.Mitglieder)
                    {
                        this.AddPerson(pers);
                    }
                    this.AddMannschaft(man);
                }
                //Turnier
                this.AddTurnier((Turnier)neu);
                SelectedTurnierSpieltag = 1;
                SelectedTurnierIndex = 0;
                SelectedTurnier = null;
                int index = 1;
                foreach (Turnier turn in this.Turniere)
                {
                    if (turn.Bezeichnung.Equals(((Turnier)neu).Bezeichnung) && turn.Sportart.name.Equals(((Turnier)neu).Sportart.name))
                    {
                        this.SelectedTurnierIndex = index;
                        this.SelectedTurnier = turn;
                        break;
                    }
                    else
                    { }
                    index++;
                }
                //Spiele
                foreach(Spiel sp in ((MannschaftsTurnier)neu).Spiele)
                {
                    this.AddSpielToMannschaftsTurnier(sp);
                }
            }
            else
            {
                this.AddSportArt(((GruppenTurnier)neu).Sportart);
                //Gruppen hinzufügen
                foreach (Teilnehmer tln in ((GruppenTurnier)neu).Gruppen)
                {
                    Gruppe grp = (Gruppe)tln;
                    foreach (Person pers in grp.Mitglieder)
                    {
                        this.AddPerson(pers);
                    }
                    AddGruppe(grp);
                }
                //Turnier
                ((Turnier)neu).setSelectedGruppe(1);
                this.AddTurnier((Turnier)neu);
                SelectedTurnierSpieltag = 1;
                SelectedTurnierGruppe = 1;
                SelectedTurnierIndex = 0;
                SelectedTurnier = null;
                int index = 1;
                foreach (Turnier turn in this.Turniere)
                {
                    if (turn.Bezeichnung.Equals(((Turnier)neu).Bezeichnung) && turn.Sportart.name.Equals(((Turnier)neu).Sportart.name))
                    {
                        this.SelectedTurnierIndex = index;
                        this.SelectedTurnier = turn;
                        break;
                    }
                    else
                    { }
                    index++;
                }
                //Spiele
                foreach (Spiel sp in ((GruppenTurnier)neu).Spiele)
                {
                    this.AddSpielToGruppenTurnier(sp);
                }
            }

            this.SelectedTurnierSpieltag = 1;
            this.SelectedTurnierIndex = -1;
            SelectedTurnierGruppe = -1;
            this.SelectedTurnier = null;
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

        public Ranking GetRanking()
        {
           Ranking neu = this.SelectedTurnier.GetRanking(this.SelectedTurnierGruppe);


            return neu;
        }

        #endregion
    }
}
