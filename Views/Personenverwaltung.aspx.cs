using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace Turnierverwaltung2020.Views
{
    public partial class Personenverwaltung : Page
    {
        #region Eigenschaften
        private Controller _verwalter;
        #endregion

        #region Accessoren/Modifier
        public Controller Verwalter { get => _verwalter; set => _verwalter = value; }
        #endregion

        #region Konstruktoren
        public Personenverwaltung()
        {

        }
        #endregion

        #region PAGE Worker
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session.Count > 0)
            {
                this.Verwalter = (Controller)this.Session["Verwalter"];
                this.Verwalter.MannschaftOderGruppe = true;
            }
            else
            {
                this.Response.Redirect(@"~\Default.aspx");
                return;
            }
            if (!this.Verwalter.UserAuthentificated)
            {
                this.Response.Redirect(@"~\Default.aspx");
                return;
            }
            else
            {
                if(!this.Verwalter.AuthentifactionRole)
                {
                    this.rdbtnList1.Visible = false;
                    this.btnBestaetigen.Visible = false;
                    this.tblEingabetabelle.Visible = false;
                    this.btnSichern.Visible = false;
                    this.lbltitel1.Text = "Verfügbare Personen:";
                    this.lbltitel2.Visible = false;
                    this.lbltitel3.Visible = false;
                    this.lbltitel4.Visible = false;
                    this.lbltitel5.Visible = false;
                }
            }
            if (this.IsPostBack)
            {

            }
            else
            {

            }
            LoadPersonen();
        }
        #endregion

        #region Worker
        private void LoadEingabeFelder()
        {
            if (this.rdbtnList1.SelectedItem == null)
            {
                return;
            }
            else
            { }

            if (this.tblEingabetabelle.Rows.Count > 1)
            {
                this.tblEingabetabelle.Rows.RemoveAt(1);
            }
            else
            { }

            TableRow neuerow = new TableRow();

            if (this.rdbtnList1.Items[0].Selected) //Fussballspieler
            {
                this.Verwalter.NeuesMitglied = new Fussballspieler();
                do
                {
                    foreach (sportart spart in this.Verwalter.Sportarten)
                    {
                        if (spart.name == "Fussball")
                        {
                            this.Verwalter.NeuesMitglied.Sportart = spart;
                        }
                        else
                        {
                        }
                    }
                    if (this.Verwalter.NeuesMitglied.Sportart == null)
                    {
                        this.Verwalter.AddSportArt(new sportart("Fussball", true, false, 3, 0, 1));
                    }
                    else
                    { }
                } while (this.Verwalter.NeuesMitglied.Sportart == null);

                TableCell neuecell = new TableCell();
                neuecell.Text = "Anzahl Spiele";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(25);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                TextBox neueBox = new TextBox();
                neueBox.ID = "txtfussballAnzSp";
                neueBox.AutoCompleteType = AutoCompleteType.Disabled;
                neuecell.Controls.Add(neueBox);
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Text = "Geschossene Tore";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(20);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neueBox = new TextBox();
                neueBox.ID = "txtfussballGesTore";
                neueBox.AutoCompleteType = AutoCompleteType.Disabled;
                neuecell.Controls.Add(neueBox);
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Text = "Spielposition";
                neuecell.Width = Unit.Percentage(20);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(25);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neueBox = new TextBox();
                neueBox.ID = "txtfussballSpielPos";
                neueBox.AutoCompleteType = AutoCompleteType.Disabled;
                neuecell.Controls.Add(neueBox);
                neuerow.Cells.Add(neuecell);
            }
            else if (this.rdbtnList1.Items[1].Selected) //Handballspieler
            {
                this.Verwalter.NeuesMitglied = new Handballspieler();
                do
                {
                    foreach (sportart spart in this.Verwalter.Sportarten)
                    {
                        if (spart.name == "Handball")
                        {
                            this.Verwalter.NeuesMitglied.Sportart = spart;
                        }
                        else
                        {
                        }
                    }
                    if (this.Verwalter.NeuesMitglied.Sportart == null)
                    {
                        this.Verwalter.AddSportArt(new sportart("Handball", true, false, 2, 2, 1));
                    }
                    else
                    { }
                } while (this.Verwalter.NeuesMitglied.Sportart == null);
                TableCell neuecell = new TableCell();
                neuecell.Text = "Anzahl Spiele";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(25);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                TextBox neueBox = new TextBox();
                neueBox.ID = "txthandballAnzSp";
                neueBox.AutoCompleteType = AutoCompleteType.Disabled;
                neuecell.Controls.Add(neueBox);
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Text = "Geworfene Tore";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(20);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neueBox = new TextBox();
                neueBox.ID = "txthandballGewTore";
                neueBox.AutoCompleteType = AutoCompleteType.Disabled;
                neuecell.Controls.Add(neueBox);
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Text = "Einsatzbereich";
                neuecell.Width = Unit.Percentage(20);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(25);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neueBox = new TextBox();
                neueBox.ID = "txthandballEinsatz";
                neueBox.AutoCompleteType = AutoCompleteType.Disabled;
                neuecell.Controls.Add(neueBox);
                neuerow.Cells.Add(neuecell);
            }
            else if (this.rdbtnList1.Items[2].Selected) //Tennisspieler
            {
                this.Verwalter.NeuesMitglied = new Tennisspieler();
                do
                {
                    foreach (sportart spart in this.Verwalter.Sportarten)
                    {
                        if (spart.name == "Tennis")
                        {
                            this.Verwalter.NeuesMitglied.Sportart = spart;
                        }
                        else
                        {
                        }
                    }
                    if (this.Verwalter.NeuesMitglied.Sportart == null)
                    {
                        this.Verwalter.AddSportArt(new sportart("Tennis", true, true, 0, 0, 0));
                    }
                    else
                    { }
                } while (this.Verwalter.NeuesMitglied.Sportart == null);
                TableCell neuecell = new TableCell();
                neuecell.Text = "Anzahl Spiele";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(25);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                TextBox neueBox = new TextBox();
                neueBox.ID = "txttennisAnzSp";
                neueBox.AutoCompleteType = AutoCompleteType.Disabled;
                neuecell.Controls.Add(neueBox);
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Text = "Gewonnene Spiele";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(20);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neueBox = new TextBox();
                neueBox.ID = "txttennisGewSpiele";
                neueBox.AutoCompleteType = AutoCompleteType.Disabled;
                neuecell.Controls.Add(neueBox);
                neuerow.Cells.Add(neuecell);
            }
            else if (this.rdbtnList1.Items[3].Selected)//weiterer Spielertyp
            {
                this.Verwalter.NeuesMitglied = new WeitererSpieler();
                TableCell neuecell = new TableCell();
                neuecell.Text = "Anzahl Spiele";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(25);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                TextBox neueBox = new TextBox();
                neueBox.ID = "txtweitererAnzSp";
                neueBox.AutoCompleteType = AutoCompleteType.Disabled;
                neuecell.Controls.Add(neueBox);
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Text = "Gewonnene Spiele";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(25);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neueBox = new TextBox();
                neueBox.ID = "txtweitererGewSp";
                neueBox.AutoCompleteType = AutoCompleteType.Disabled;
                neuecell.Controls.Add(neueBox);
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Text = "Sportart";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(20);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                DropDownList neueListe = new DropDownList();
                neueListe.ID = "lstweitererSportart";
                foreach (sportart sp in this.Verwalter.Sportarten)
                {
                    if (sp.name != "Fussball" && sp.name != "Handball" && sp.name != "Tennis")
                    {
                        neueListe.Items.Add(sp.name);
                    }
                    else
                    {
                    }
                }
                if (neueListe.Items.Count <= 0)
                {
                    neueListe.Items.Add("keine weitere Sportart vorhanden");
                }
                else
                { }
                neuecell.Controls.Add(neueListe);
                neuerow.Cells.Add(neuecell);
            }
            else if (this.rdbtnList1.Items[4].Selected) //Physiotherapeut
            {
                this.Verwalter.NeuesMitglied = new Physiotherapeut();
                TableCell neuecell = new TableCell();
                neuecell.Text = "Anzahl Jahre";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(25);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                TextBox neueBox = new TextBox();
                neueBox.ID = "txtphysioAnzJa";
                neueBox.AutoCompleteType = AutoCompleteType.Disabled;
                neuecell.Controls.Add(neueBox);
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Text = "Sportart";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(20);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                DropDownList neueListe = new DropDownList();
                neueListe.ID = "lstPhysioSportart";
                foreach (sportart sp in this.Verwalter.Sportarten)
                {
                    neueListe.Items.Add(sp.name);
                }
                neuecell.Controls.Add(neueListe);
                neuerow.Cells.Add(neuecell);
            }
            else if (this.rdbtnList1.Items[5].Selected) //Trainer
            {
                this.Verwalter.NeuesMitglied = new Trainer();
                TableCell neuecell = new TableCell();
                neuecell.Text = "Anzahl Vereine";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(25);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                TextBox neueBox = new TextBox();
                neueBox.ID = "txttrainerAnzVer";
                neueBox.AutoCompleteType = AutoCompleteType.Disabled;
                neuecell.Controls.Add(neueBox);
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Text = "Sportart";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(20);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                DropDownList neueListe = new DropDownList();
                neueListe.ID = "lstTrainerSportart";
                foreach (sportart sp in this.Verwalter.Sportarten)
                {
                    neueListe.Items.Add(sp.name);
                }
                neuecell.Controls.Add(neueListe);
                neuerow.Cells.Add(neuecell);
            }
            else if (this.rdbtnList1.Items[6].Selected)//Person mit anderen Aufgaben
            {
                this.Verwalter.NeuesMitglied = new AndereAufgaben();
                TableCell neuecell = new TableCell();
                neuecell.Text = "Aufgabe";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(25);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                TextBox neueBox = new TextBox();
                neueBox.ID = "txtandereAufgabe";
                neueBox.AutoCompleteType = AutoCompleteType.Disabled;
                neuecell.Controls.Add(neueBox);
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Text = "Sportart";
                neuecell.Width = Unit.Percentage(10);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                neuerow.Cells.Add(neuecell);

                neuecell = new TableCell();
                neuecell.Width = Unit.Percentage(20);
                neuecell.Font.Bold = true;
                neuecell.Font.Size = FontUnit.Medium;
                DropDownList neueListe = new DropDownList();
                neueListe.ID = "lstandereSportart";
                foreach (sportart sp in this.Verwalter.Sportarten)
                {
                    neueListe.Items.Add(sp.name);
                }
                neuecell.Controls.Add(neueListe);
                neuerow.Cells.Add(neuecell);
            }
            else
            {

            }

            this.tblEingabetabelle.Rows.Add(neuerow);
        }
        private void LoadPersonen()
        {
            int index = 1;

            #region Headersetzen zum anclicken
            //Name
            Button newButton = new Button();
            newButton.Text = "Name";
            newButton.Width = Unit.Percentage(90);
            newButton.Click += HeaderClick;
            newButton.BackColor = this.tblAusgabetabelle.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnName";
            newButton.Style.Add("text-align", "left");
            this.tblAusgabetabelle.Rows[0].Cells[1].Controls.Add(newButton);
            //Vorname
            newButton = new Button();
            newButton.Text = "Vorname";
            newButton.Width = Unit.Percentage(95);
            newButton.Click += HeaderClick;
            newButton.BackColor = this.tblAusgabetabelle.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnVorName";
            newButton.Style.Add("text-align", "left");
            this.tblAusgabetabelle.Rows[0].Cells[2].Controls.Add(newButton);
            //ID
            newButton = new Button();
            newButton.Text = "ID";
            newButton.Width = Unit.Percentage(90);
            newButton.Click += HeaderClick;
            newButton.BackColor = this.tblAusgabetabelle.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnID";
            this.tblAusgabetabelle.Rows[0].Cells[0].Controls.Add(newButton);

            //Geburtsdatum
            newButton = new Button();
            newButton.Text = "Geburtsdatum";
            newButton.Width = Unit.Percentage(95);
            newButton.Click += HeaderClick;
            newButton.BackColor = this.tblAusgabetabelle.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnGeb";
            newButton.Style.Add("text-align", "left");
            this.tblAusgabetabelle.Rows[0].Cells[3].Controls.Add(newButton);
            //Sportart
            newButton = new Button();
            newButton.Text = "Sportart";
            newButton.Width = Unit.Percentage(95);
            newButton.Click += HeaderClick;
            newButton.BackColor = this.tblAusgabetabelle.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnSp";
            newButton.Style.Add("text-align", "left");
            this.tblAusgabetabelle.Rows[0].Cells[4].Controls.Add(newButton);
            //Anzahl Spiele
            newButton = new Button();
            newButton.Text = "Anzahl Spiele";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.Click += HeaderClick;
            newButton.BackColor = this.tblAusgabetabelle.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnAnzSp";
            newButton.Style.Add("white-space", "normal");
            this.tblAusgabetabelle.Rows[0].Cells[5].Controls.Add(newButton);
            //Erzielte Tore
            newButton = new Button();
            newButton.Text = "Erzielte Tore";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.Click += HeaderClick;
            newButton.BackColor = this.tblAusgabetabelle.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnErzTore";
            newButton.Style.Add("white-space", "normal");
            this.tblAusgabetabelle.Rows[0].Cells[6].Controls.Add(newButton);
            //Gewonnene Spiele
            newButton = new Button();
            newButton.Text = "Gewonnene Spiele";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.Click += HeaderClick;
            newButton.BackColor = this.tblAusgabetabelle.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnGewSp";
            newButton.Style.Add("white-space", "normal");
            this.tblAusgabetabelle.Rows[0].Cells[7].Controls.Add(newButton);
            //Anzahl Jahre
            newButton = new Button();
            newButton.Text = "Anzahl Jahre";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.Click += HeaderClick;
            newButton.BackColor = this.tblAusgabetabelle.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnAnzJahre";
            newButton.Style.Add("white-space", "normal");
            this.tblAusgabetabelle.Rows[0].Cells[8].Controls.Add(newButton);
            //Anzahl Vereine
            newButton = new Button();
            newButton.Text = "Anzahl Vereine";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.Click += HeaderClick;
            newButton.BackColor = this.tblAusgabetabelle.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnAnzVereine";
            newButton.Style.Add("white-space", "normal");
            this.tblAusgabetabelle.Rows[0].Cells[9].Controls.Add(newButton);
            //Einsatzbereich
            newButton = new Button();
            newButton.Text = "Einsatz Bereich";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.Click += HeaderClick;
            newButton.BackColor = this.tblAusgabetabelle.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnEinsatz";
            newButton.Style.Add("white-space", "normal");
            this.tblAusgabetabelle.Rows[0].Cells[10].Controls.Add(newButton);
            //Edit
            newButton = new Button();
            newButton.Text = "Edit";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.Click += HeaderClick;
            newButton.BackColor = this.tblAusgabetabelle.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnEdit";
            newButton.Style.Add("white-space", "normal");
            this.tblAusgabetabelle.Rows[0].Cells[11].Controls.Add(newButton);
            //Delete
            newButton = new Button();
            newButton.Text = "Del";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.Click += HeaderClick;
            newButton.BackColor = this.tblAusgabetabelle.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnDelete";
            newButton.Style.Add("white-space", "normal");
            this.tblAusgabetabelle.Rows[0].Cells[12].Controls.Add(newButton);
            #endregion

            //Daten
            foreach (Teilnehmer pers in this.Verwalter.Personen)
            {
                TableRow neueZeile = new TableRow();
                //ID
                TableCell neueZelle = new TableCell();
                neueZelle.Text = pers.ID.ToString();
                neueZeile.Cells.Add(neueZelle);
                //Name
                neueZelle = new TableCell();
                neueZelle.Text = pers.Name;
                neueZeile.Cells.Add(neueZelle);
                //Vorname
                neueZelle = new TableCell();
                neueZelle.Text = ((Person)pers).Vorname;
                neueZeile.Cells.Add(neueZelle);
                //Geburtsdatum
                neueZelle = new TableCell();
                neueZelle.Text = ((Person)pers).Geburtsdatum.ToShortDateString();
                neueZeile.Cells.Add(neueZelle);

                //Sportart
                neueZelle = new TableCell();
                neueZelle.Text = pers.Sportart.name;
                neueZeile.Cells.Add(neueZelle);
                //AnzahlSpiele
                neueZelle = new TableCell();
                neueZelle.HorizontalAlign = HorizontalAlign.Center;
                neueZelle.Text = pers.Anzahlspiele.ToString();
                neueZeile.Cells.Add(neueZelle);
                if (pers is Fussballspieler)
                {
                    //Erfolge
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = ((Fussballspieler)pers).Geschossenentore.ToString();
                    neueZeile.Cells.Add(neueZelle);
                    //Gewonnene Spiele
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = "";
                    neueZeile.Cells.Add(neueZelle);
                    //Anzahl Jahre
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = "";
                    neueZeile.Cells.Add(neueZelle);
                    //Anzahl Vereine
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = "";
                    neueZeile.Cells.Add(neueZelle);
                    //Einsatzbereich
                    neueZelle = new TableCell();
                    neueZelle.Text = ((Fussballspieler)pers).Position;
                    neueZeile.Cells.Add(neueZelle);
                }
                else if (pers is Handballspieler)
                {
                    //Erfolge
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = ((Handballspieler)pers).Geworfenetore.ToString(); ;
                    neueZeile.Cells.Add(neueZelle);
                    //Gewonnene Spiele
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = "";
                    neueZeile.Cells.Add(neueZelle);
                    //Anzahl Jahre
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = "";
                    neueZeile.Cells.Add(neueZelle);
                    //Anzahl Vereine
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = "";
                    neueZeile.Cells.Add(neueZelle);
                    //Einsatzbereich
                    neueZelle = new TableCell();
                    neueZelle.Text = ((Handballspieler)pers).Einsatzbereich;
                    neueZeile.Cells.Add(neueZelle);
                }
                else if (pers is Tennisspieler)
                {
                    //Tore
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = "";
                    neueZeile.Cells.Add(neueZelle);
                    //Erfolge
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = ((Tennisspieler)pers).GewonneneSpiele.ToString();
                    neueZeile.Cells.Add(neueZelle);
                    //Anzahl Jahre
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = "";
                    neueZeile.Cells.Add(neueZelle);
                    //Anzahl Vereine
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = "";
                    neueZeile.Cells.Add(neueZelle);
                    //Einsatzbereich
                    neueZelle = new TableCell();
                    neueZelle.Text = "Spieler";
                    neueZeile.Cells.Add(neueZelle);
                }
                else if (pers is WeitererSpieler)
                {
                    //Tore
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = "";
                    neueZeile.Cells.Add(neueZelle);
                    //Erfolge
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = ((WeitererSpieler)pers).GewonneneSpiele.ToString();
                    neueZeile.Cells.Add(neueZelle);
                    //Anzahl Jahre
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = "";
                    neueZeile.Cells.Add(neueZelle);
                    //Anzahl Vereine
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = "";
                    neueZeile.Cells.Add(neueZelle);
                    //Einsatzbereich
                    neueZelle = new TableCell();
                    neueZelle.Text = "Spieler";
                    neueZeile.Cells.Add(neueZelle);
                }
                else
                {
                    //Erzielte Tore
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = "";
                    neueZeile.Cells.Add(neueZelle);
                    //Gew. Spiele
                    neueZelle = new TableCell();
                    neueZelle.HorizontalAlign = HorizontalAlign.Center;
                    neueZelle.Text = "";
                    neueZeile.Cells.Add(neueZelle);
                    if (pers is Physiotherapeut)
                    {
                        //Anzahl Jahre
                        neueZelle = new TableCell();
                        neueZelle.HorizontalAlign = HorizontalAlign.Center;
                        neueZelle.Text = ((Physiotherapeut)pers).Anzahljahre.ToString();
                        neueZeile.Cells.Add(neueZelle);
                        //Anzahl Vereine
                        neueZelle = new TableCell();
                        neueZelle.HorizontalAlign = HorizontalAlign.Center;
                        neueZelle.Text = "";
                        neueZeile.Cells.Add(neueZelle);
                        //Einsatzbereich
                        neueZelle = new TableCell();
                        neueZelle.Text = "Physio";
                        neueZeile.Cells.Add(neueZelle);
                    }
                    else if (pers is Trainer)
                    {
                        //Anzahl Jahre
                        neueZelle = new TableCell();
                        neueZelle.HorizontalAlign = HorizontalAlign.Center;
                        neueZelle.Text = "";
                        neueZeile.Cells.Add(neueZelle);
                        //Anzahl Vereine
                        neueZelle = new TableCell();
                        neueZelle.HorizontalAlign = HorizontalAlign.Center;
                        neueZelle.Text = ((Trainer)pers).Anzahlvereine.ToString();
                        neueZeile.Cells.Add(neueZelle);
                        //Einsatzbereich
                        neueZelle = new TableCell();
                        neueZelle.Text = "Trainer";
                        neueZeile.Cells.Add(neueZelle);
                    }
                    else if (pers is AndereAufgaben)
                    {
                        //Anzahl Jahre
                        neueZelle = new TableCell();
                        neueZelle.HorizontalAlign = HorizontalAlign.Center;
                        neueZelle.Text = "";
                        neueZeile.Cells.Add(neueZelle);
                        //Anzahl Vereine
                        neueZelle = new TableCell();
                        neueZelle.HorizontalAlign = HorizontalAlign.Center;
                        neueZelle.Text = "";
                        neueZeile.Cells.Add(neueZelle);
                        //Einsatzbereich
                        neueZelle = new TableCell();
                        neueZelle.Text = ((AndereAufgaben)pers).Einsatz;
                        neueZeile.Cells.Add(neueZelle);
                    }
                    else
                    { }
                }
                //Stift
                neueZelle = new TableCell();
                neueZelle.HorizontalAlign = HorizontalAlign.Center;
                ImageButton btnicon = new ImageButton();
                btnicon.ID = "bearb" + index;
                btnicon.ImageUrl = "~/Images/stift.png";
                btnicon.Click += Btnicon_Click;
                neueZelle.Controls.Add(btnicon);
                neueZeile.Cells.Add(neueZelle);
                //Mülleimer
                neueZelle = new TableCell();
                neueZelle.HorizontalAlign = HorizontalAlign.Center;
                btnicon = new ImageButton();
                btnicon.ID = "loesch" + index;
                index++;
                btnicon.ImageUrl = "~/Images/muelleimer.png";
                btnicon.Click += Btnicon_Click;
                neueZelle.Controls.Add(btnicon);
                neueZeile.Cells.Add(neueZelle);
                this.tblAusgabetabelle.Rows.Add(neueZeile);
            }
        }
        private void ClearEingabe()
        {
            this.txtVorname.Text = "";
            this.txtName.Text = "";
            this.rdbtnList1.SelectedValue = null;
        }
        protected void btnSichern_Click(object sender, EventArgs e)
        {
            if (rdbtnList1.SelectedItem == null)
            {
                return;
            }
            else
            {
                if (this.Verwalter.NeuesMitglied != null)
                {
                    string nam = this.Request.Form["ctl00$MainContent$txtName"];
                    string vornam = this.Request.Form["ctl00$MainContent$txtVorname"];
                    string gebdat = this.Request.Form["ctl00$MainContent$txtGeburtsdatum"];

                    this.Verwalter.NeuesMitglied.Name = nam;
                    this.Verwalter.NeuesMitglied.Vorname = vornam;
                    DateTime temp;
                    if (DateTime.TryParse(gebdat, out temp))
                    {
                        this.Verwalter.NeuesMitglied.Geburtsdatum = temp;
                    }
                    else
                    {
                        this.Verwalter.NeuesMitglied.Geburtsdatum = DateTime.Parse("1.1.2000");
                    }

                    if (this.Verwalter.NeuesMitglied is Fussballspieler)
                    {
                        if (this.Request.Form["ctl00$MainContent$txtfussballAnzSp"].All(Char.IsDigit))
                        {
                            if (this.Request.Form["ctl00$MainContent$txtfussballAnzSp"] != "")
                            {
                                ((Fussballspieler)this.Verwalter.NeuesMitglied).Anzahlspiele = Convert.ToInt32(this.Request.Form["ctl00$MainContent$txtfussballAnzSp"]);
                            }
                            else
                            {
                                ((Fussballspieler)this.Verwalter.NeuesMitglied).Anzahlspiele = 0;
                            }
                        }
                        else
                        {
                            ((Fussballspieler)this.Verwalter.NeuesMitglied).Anzahlspiele = 0;
                        }
                        if (this.Request.Form["ctl00$MainContent$txtfussballGesTore"].All(Char.IsDigit))
                        {
                            if (this.Request.Form["ctl00$MainContent$txtfussballGesTore"] != "")
                            {
                                ((Fussballspieler)this.Verwalter.NeuesMitglied).Geschossenentore = Convert.ToInt32(this.Request.Form["ctl00$MainContent$txtfussballGesTore"]);
                            }
                            else
                            {
                                ((Fussballspieler)this.Verwalter.NeuesMitglied).Geschossenentore = 0;
                            }
                        }
                        else
                        {
                            ((Fussballspieler)this.Verwalter.NeuesMitglied).Geschossenentore = 0;
                        }
                        ((Fussballspieler)this.Verwalter.NeuesMitglied).Position = this.Request.Form["ctl00$MainContent$txtfussballSpielPos"];
                    }
                    else if (this.Verwalter.NeuesMitglied is Handballspieler)
                    {
                        if (this.Request.Form["ctl00$MainContent$txthandballAnzSp"].All(Char.IsDigit))
                        {
                            if (this.Request.Form["ctl00$MainContent$txthandballAnzSp"] != "")
                            {
                                if (this.Request.Form["ctl00$MainContent$txthandballAnzSp"] != "")
                                {
                                    ((Handballspieler)this.Verwalter.NeuesMitglied).Anzahlspiele = Convert.ToInt32(this.Request.Form["ctl00$MainContent$txthandballAnzSp"]);
                                }
                                else
                                {
                                    ((Handballspieler)this.Verwalter.NeuesMitglied).Anzahlspiele = 0;
                                }
                            }
                            else
                            {
                                ((Handballspieler)this.Verwalter.NeuesMitglied).Anzahlspiele = 0;
                            }
                        }
                        else
                        {
                            ((Handballspieler)this.Verwalter.NeuesMitglied).Anzahlspiele = 0;
                        }
                        if (this.Request.Form["ctl00$MainContent$txthandballGewTore"].All(Char.IsDigit))
                        {
                            if (this.Request.Form["ctl00$MainContent$txthandballGewTore"] != "")
                            {
                                if (this.Request.Form["ctl00$MainContent$txthandballGewTore"] != "")
                                {
                                    ((Handballspieler)this.Verwalter.NeuesMitglied).Geworfenetore = Convert.ToInt32(this.Request.Form["ctl00$MainContent$txthandballGewTore"]);
                                }
                                else
                                {
                                    ((Handballspieler)this.Verwalter.NeuesMitglied).Geworfenetore = 0;
                                }
                            }
                            else
                            {
                                ((Handballspieler)this.Verwalter.NeuesMitglied).Geworfenetore = 0;
                            }
                        }
                        else
                        {
                            ((Handballspieler)this.Verwalter.NeuesMitglied).Geworfenetore = 0;
                        }
                        ((Handballspieler)this.Verwalter.NeuesMitglied).Einsatzbereich = this.Request.Form["ctl00$MainContent$txthandballEinsatz"];
                    }
                    else if (this.Verwalter.NeuesMitglied is Tennisspieler)
                    {
                        if (this.Request.Form["ctl00$MainContent$txttennisAnzSp"].All(Char.IsDigit))
                        {
                            if (this.Request.Form["ctl00$MainContent$txttennisAnzSp"] != "")
                            {
                                if (this.Request.Form["ctl00$MainContent$txttennisAnzSp"] != "")
                                {
                                    ((Tennisspieler)this.Verwalter.NeuesMitglied).Anzahlspiele = Convert.ToInt32(this.Request.Form["ctl00$MainContent$txttennisAnzSp"]);
                                }
                                else
                                {
                                    ((Tennisspieler)this.Verwalter.NeuesMitglied).Anzahlspiele = 0;
                                }
                            }
                            else
                            {
                                ((Tennisspieler)this.Verwalter.NeuesMitglied).Anzahlspiele = 0;
                            }
                        }
                        else
                        {
                            ((Tennisspieler)this.Verwalter.NeuesMitglied).Anzahlspiele = 0;
                        }
                        if (this.Request.Form["ctl00$MainContent$txttennisGewSpiele"].All(Char.IsDigit))
                        {
                            if (this.Request.Form["ctl00$MainContent$txttennisGewSpiele"] != "")
                            {
                                ((Tennisspieler)this.Verwalter.NeuesMitglied).GewonneneSpiele = Convert.ToInt32(this.Request.Form["ctl00$MainContent$txttennisGewSpiele"]);
                            }
                            else
                            {
                                ((Tennisspieler)this.Verwalter.NeuesMitglied).GewonneneSpiele = 0;
                            }
                        }
                        else
                        {
                            ((Tennisspieler)this.Verwalter.NeuesMitglied).GewonneneSpiele = 0;
                        }
                    }
                    else if (this.Verwalter.NeuesMitglied is WeitererSpieler)
                    {
                        if (this.Request.Form["ctl00$MainContent$txtweitererAnzSp"].All(Char.IsDigit))
                        {
                            if (this.Request.Form["ctl00$MainContent$txtweitererAnzSp"] != "")
                            {
                                if (this.Request.Form["ctl00$MainContent$txtweitererAnzSp"] != "")
                                {
                                    ((WeitererSpieler)this.Verwalter.NeuesMitglied).Anzahlspiele = Convert.ToInt32(this.Request.Form["ctl00$MainContent$txtweitererAnzSp"]);
                                }
                                else
                                {
                                    ((WeitererSpieler)this.Verwalter.NeuesMitglied).Anzahlspiele = 0;

                                }
                            }
                            else
                            {
                                ((WeitererSpieler)this.Verwalter.NeuesMitglied).Anzahlspiele = 0;
                            }
                        }
                        else
                        {
                            ((WeitererSpieler)this.Verwalter.NeuesMitglied).Anzahlspiele = 0;
                        }
                        if (this.Request.Form["ctl00$MainContent$txtweitererGewSp"].All(Char.IsDigit))
                        {
                            if (this.Request.Form["ctl00$MainContent$txtweitererGewSp"] != "")
                            {
                                ((WeitererSpieler)this.Verwalter.NeuesMitglied).GewonneneSpiele = Convert.ToInt32(this.Request.Form["ctl00$MainContent$txtweitererGewSp"]);
                            }
                            else
                            {
                                ((WeitererSpieler)this.Verwalter.NeuesMitglied).GewonneneSpiele = 0;
                            }
                        }
                        else
                        {
                            ((WeitererSpieler)this.Verwalter.NeuesMitglied).GewonneneSpiele = 0;
                        }
                        string spart = this.Request.Form["ctl00$MainContent$lstweitererSportart"];
                        sportart toadd = null;
                        foreach (sportart spr in this.Verwalter.Sportarten)
                        {
                            if (spr.name == spart)
                            {
                                toadd = spr;
                                break;
                            }
                            else
                            { }
                        }
                        if (toadd != null)
                        {
                            ((WeitererSpieler)this.Verwalter.NeuesMitglied).Sportart = toadd;
                        }
                        else
                        {
                            this.Verwalter.NeuesMitglied = null;
                        }
                    }
                    else if (this.Verwalter.NeuesMitglied is Physiotherapeut)
                    {
                        if (this.Request.Form["ctl00$MainContent$txtphysioAnzJa"].All(Char.IsDigit))
                        {
                            if (this.Request.Form["ctl00$MainContent$txtphysioAnzJa"] != "")
                            {
                                if (this.Request.Form["ctl00$MainContent$txtphysioAnzJa"] != "")
                                {
                                    ((Physiotherapeut)this.Verwalter.NeuesMitglied).Anzahljahre = Convert.ToInt32(this.Request.Form["ctl00$MainContent$txtphysioAnzJa"]);
                                }
                                else
                                {
                                    ((Physiotherapeut)this.Verwalter.NeuesMitglied).Anzahljahre = 0;
                                }
                            }
                            else
                            {
                                ((Physiotherapeut)this.Verwalter.NeuesMitglied).Anzahljahre = 0;
                            }
                        }
                        else
                        {
                            ((Physiotherapeut)this.Verwalter.NeuesMitglied).Anzahljahre = 0;
                        }
                        string spart = this.Request.Form["ctl00$MainContent$lstPhysioSportart"];
                        sportart toadd = null;
                        foreach (sportart spr in this.Verwalter.Sportarten)
                        {
                            if (spr.name == spart)
                            {
                                toadd = spr;
                                break;
                            }
                            else
                            { }
                        }
                        ((Physiotherapeut)this.Verwalter.NeuesMitglied).Sportart = toadd;
                    }
                    else if (this.Verwalter.NeuesMitglied is Trainer)
                    {
                        if (this.Request.Form["ctl00$MainContent$txttrainerAnzVer"].All(Char.IsDigit))
                        {
                            if (this.Request.Form["ctl00$MainContent$txttrainerAnzVer"] != "")
                            {
                                if (this.Request.Form["ctl00$MainContent$txttrainerAnzVer"] != "")
                                {
                                    ((Trainer)this.Verwalter.NeuesMitglied).Anzahlvereine = Convert.ToInt32(this.Request.Form["ctl00$MainContent$txttrainerAnzVer"]);
                                }
                                else
                                {
                                    ((Trainer)this.Verwalter.NeuesMitglied).Anzahlvereine = 0;
                                }
                            }
                            else
                            {
                                ((Trainer)this.Verwalter.NeuesMitglied).Anzahlvereine = 0;
                            }
                        }
                        else
                        {
                            ((Trainer)this.Verwalter.NeuesMitglied).Anzahlvereine = 0;
                        }
                        string spart = this.Request.Form["ctl00$MainContent$lstTrainerSportart"];
                        sportart toadd = null;
                        foreach (sportart spr in this.Verwalter.Sportarten)
                        {
                            if (spr.name == spart)
                            {
                                toadd = spr;
                                break;
                            }
                            else
                            { }
                        }
                        ((Trainer)this.Verwalter.NeuesMitglied).Sportart = toadd;
                    }
                    else if (this.Verwalter.NeuesMitglied is AndereAufgaben)
                    {
                        ((AndereAufgaben)this.Verwalter.NeuesMitglied).Einsatz = this.Request.Form["ctl00$MainContent$txtandereAufgabe"];
                        string spart = this.Request.Form["ctl00$MainContent$lstandereSportart"];
                        sportart toadd = null;
                        foreach (sportart spr in this.Verwalter.Sportarten)
                        {
                            if (spr.name == spart)
                            {
                                toadd = spr;
                                break;
                            }
                            else
                            { }
                        }
                        ((AndereAufgaben)this.Verwalter.NeuesMitglied).Sportart = toadd;
                    }
                    else
                    { }

                    if (this.Verwalter.NeuesMitglied != null)
                    {
                        ClearEingabe();

                        this.tblEingabetabelle.Enabled = false;
                        this.btnSichern.Enabled = false;

                        if (!this.Verwalter.EditPerson)
                        {
                            this.Verwalter.AddPerson(this.Verwalter.NeuesMitglied);
                        }
                        else
                        {
                            this.Verwalter.NeuesMitglied.ID = this.Verwalter.EditPersonID;
                            this.Verwalter.ChangePerson(this.Verwalter.NeuesMitglied);
                        }
                        this.Verwalter.NeuesMitglied = null;
                    }
                    else
                    { }

                    Response.Redirect(Request.RawUrl);
                }
                else
                {

                }
            }
            this.btnSichern.BackColor = Color.Green;
        }
        protected void btnBestaetigen_Click(object sender, EventArgs e)
        {
            this.tblEingabetabelle.Enabled = true;
            this.btnSichern.Enabled = true;
            this.txtName.Text = "";
            this.txtVorname.Text = "";
            this.txtGeburtsdatum.Text = "";
            this.btnSichern.BackColor = Color.Red;
            LoadEingabeFelder();
        }
        protected void Btnicon_Click(object sender, ImageClickEventArgs e)
        {
            if (this.Verwalter.AuthentifactionRole)
            {

                int nummer = -1;
                string typ = "none";
                string objekt = ((ImageButton)sender).ClientID.Substring(((ImageButton)sender).ClientID.IndexOf("_") + 1);
                string mannschaft = "";
                if (objekt.Contains("loesch"))
                {
                    nummer = Convert.ToInt32(objekt.Substring(6));
                    typ = "delete";
                }
                else if (objekt.Contains("bearb"))
                {
                    nummer = Convert.ToInt32(objekt.Substring(5));
                    typ = "edit";
                }
                else
                {
                    nummer = -1;
                    typ = "none";
                }

                bool gefunden = false;
                if (typ != "none" && nummer != -1)
                {
                    if (typ == "delete")
                    {
                        gefunden = this.Verwalter.IsPersonInMannschaftoderGruppe(nummer, ref mannschaft);
                        if (!gefunden)
                        {
                            this.Verwalter.DeletePerson(nummer);
                            Response.Redirect(Request.RawUrl);
                        }
                        else
                        {
                            //Meldung
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ((Person)this.Verwalter.Personen[nummer - 1]).Vorname + " " +
                                this.Verwalter.Personen[nummer - 1].Name + " ist noch Mitglied in der Mannschaft oder Gruppe: " + mannschaft + ", bitte vorher entfernen');", true);
                        }
                        gefunden = false;
                    }
                    else if (typ == "edit")
                    {
                        Teilnehmer zuAendern = this.Verwalter.getPerson(nummer);
                        foreach (ListItem li in rdbtnList1.Items)
                        {
                            li.Selected = false;
                        }
                        if (zuAendern is Fussballspieler)
                        {
                            this.rdbtnList1.Items[0].Selected = true;
                        }
                        else if (zuAendern is Handballspieler)
                        {
                            this.rdbtnList1.Items[1].Selected = true;
                        }
                        else if (zuAendern is Tennisspieler)
                        {
                            this.rdbtnList1.Items[2].Selected = true;
                        }
                        else if (zuAendern is WeitererSpieler)
                        {
                            this.rdbtnList1.Items[3].Selected = true;
                        }
                        else if (zuAendern is Physiotherapeut)
                        {
                            this.rdbtnList1.Items[4].Selected = true;
                        }
                        else if (zuAendern is Trainer)
                        {
                            this.rdbtnList1.Items[5].Selected = true;
                        }
                        else if (zuAendern is AndereAufgaben)
                        {
                            this.rdbtnList1.Items[6].Selected = true;
                        }
                        else
                        { }

                        this.btnSichern.BackColor = Color.Red;
                        this.tblEingabetabelle.Enabled = true;
                        this.btnSichern.Enabled = true;

                        //Vorherige Werte eintragen
                        txtName.Text = zuAendern.Name;
                        txtVorname.Text = ((Person)zuAendern).Vorname;
                        txtGeburtsdatum.Text = ((Person)zuAendern).Geburtsdatum.ToString("yyyy-MM-dd");

                        this.LoadEingabeFelder();

                        if (zuAendern is Fussballspieler)
                        {
                            ((TextBox)this.tblEingabetabelle.Rows[1].Cells[1].Controls[0]).Text = ((Fussballspieler)zuAendern).Anzahlspiele.ToString();
                            ((TextBox)this.tblEingabetabelle.Rows[1].Cells[3].Controls[0]).Text = ((Fussballspieler)zuAendern).Geschossenentore.ToString();
                            ((TextBox)this.tblEingabetabelle.Rows[1].Cells[5].Controls[0]).Text = ((Fussballspieler)zuAendern).Position.ToString();
                        }
                        else if (zuAendern is Handballspieler)
                        {
                            ((TextBox)this.tblEingabetabelle.Rows[1].Cells[1].Controls[0]).Text = ((Handballspieler)zuAendern).Anzahlspiele.ToString();
                            ((TextBox)this.tblEingabetabelle.Rows[1].Cells[3].Controls[0]).Text = ((Handballspieler)zuAendern).Geworfenetore.ToString();
                            ((TextBox)this.tblEingabetabelle.Rows[1].Cells[5].Controls[0]).Text = ((Handballspieler)zuAendern).Einsatzbereich.ToString();
                        }
                        else if (zuAendern is Tennisspieler)
                        {
                            ((TextBox)this.tblEingabetabelle.Rows[1].Cells[1].Controls[0]).Text = ((Tennisspieler)zuAendern).Anzahlspiele.ToString();
                            ((TextBox)this.tblEingabetabelle.Rows[1].Cells[3].Controls[0]).Text = ((Tennisspieler)zuAendern).GewonneneSpiele.ToString();
                        }
                        else if (zuAendern is WeitererSpieler)
                        {
                            ((TextBox)this.tblEingabetabelle.Rows[1].Cells[1].Controls[0]).Text = ((WeitererSpieler)zuAendern).Anzahlspiele.ToString();
                            ((TextBox)this.tblEingabetabelle.Rows[1].Cells[3].Controls[0]).Text = ((WeitererSpieler)zuAendern).GewonneneSpiele.ToString();
                            foreach (ListItem li in ((DropDownList)this.tblEingabetabelle.Rows[1].Cells[5].Controls[0]).Items)
                            {

                                if (li.Text == ((WeitererSpieler)zuAendern).Sportart.name)
                                {
                                    ((DropDownList)this.tblEingabetabelle.Rows[1].Cells[5].Controls[0]).SelectedValue = ((WeitererSpieler)zuAendern).Sportart.name;
                                    break;
                                }
                                else
                                { }
                            }
                        }
                        else if (zuAendern is Physiotherapeut)
                        {

                            ((TextBox)this.tblEingabetabelle.Rows[1].Cells[1].Controls[0]).Text = ((Physiotherapeut)zuAendern).Anzahljahre.ToString();
                            foreach (ListItem li in ((DropDownList)this.tblEingabetabelle.Rows[1].Cells[3].Controls[0]).Items)
                            {

                                if (li.Text == ((Physiotherapeut)zuAendern).Sportart.name)
                                {
                                    ((DropDownList)this.tblEingabetabelle.Rows[1].Cells[3].Controls[0]).SelectedValue = ((Physiotherapeut)zuAendern).Sportart.name;
                                    gefunden = true;
                                    break;
                                }
                                else
                                { }
                            }
                        }
                        else if (zuAendern is Trainer)
                        {
                            gefunden = false;
                            ((TextBox)this.tblEingabetabelle.Rows[1].Cells[1].Controls[0]).Text = ((Trainer)zuAendern).Anzahlvereine.ToString();
                            foreach (ListItem li in ((DropDownList)this.tblEingabetabelle.Rows[1].Cells[3].Controls[0]).Items)
                            {

                                if (li.Text == ((Trainer)zuAendern).Sportart.name)
                                {
                                    ((DropDownList)this.tblEingabetabelle.Rows[1].Cells[3].Controls[0]).SelectedValue = ((Trainer)zuAendern).Sportart.name;
                                    gefunden = true;
                                    break;
                                }
                                else
                                { }
                            }
                        }
                        else if (zuAendern is AndereAufgaben)
                        {
                            gefunden = false;
                            ((TextBox)this.tblEingabetabelle.Rows[1].Cells[1].Controls[0]).Text = ((AndereAufgaben)zuAendern).Einsatz;
                            foreach (ListItem li in ((DropDownList)this.tblEingabetabelle.Rows[1].Cells[3].Controls[0]).Items)
                            {

                                if (li.Text == ((AndereAufgaben)zuAendern).Sportart.name)
                                {
                                    ((DropDownList)this.tblEingabetabelle.Rows[1].Cells[3].Controls[0]).SelectedValue = ((AndereAufgaben)zuAendern).Sportart.name;
                                    gefunden = true;
                                    break;
                                }
                                else
                                { }
                            }
                        }
                        else
                        { }
                        this.Verwalter.EditPerson = true;
                        this.Verwalter.EditPersonIndex = nummer;
                        this.Verwalter.EditPersonID = zuAendern.ID;
                    }
                    else
                    {
                        gefunden = false;
                    }
                }
                else
                {
                    gefunden = false;
                }
            }
            else
            { }
        }
        protected void btnXML_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/Import_Export/Personen_" + DateTime.Today.ToShortDateString() + ".xml");

            this.Verwalter.XMLSichern(path, 1);
        }
        protected void btnJSON_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/Import_Export/Personen_" + DateTime.Today.ToShortDateString() + ".json");

            this.Verwalter.JSONSichern(path, 1);
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            /*   string path = Server.MapPath("~/Import_Export");
               string neupath = path + @"/" + this.FileUploadControl1.FileName;

               if (this.FileUploadControl1.FileName.Contains(".xml"))
               {
                   //this.FileUploadControl1.SaveAs(neupath);
                   this.Verwalter.XMLLaden(this.FileUploadControl1.FileContent,1);
                   this.FileUploadControl1.FileContent.Close();
               }
               else if (this.FileUploadControl1.FileName.Contains(".json"))
               {
                   //this.FileUploadControl1.SaveAs(neupath);
                   StreamReader SR = new StreamReader(this.FileUploadControl1.FileContent);
                   string json = SR.ReadToEnd();
                   SR.Close();
                   this.Verwalter.JSONLaden(json,1);
               }
               else
               {
               }
               // Reload
               Response.Redirect(Request.RawUrl);*/
        }
        protected void HeaderClick(object sender, EventArgs e)
        {
            if (this.Verwalter.AuthentifactionRole)
            {
                string ID = "";
                if (sender is Button)
                {
                    ID = ((Button)sender).ID;
                }
                else
                { }
                ID = ID.Substring(3);
                //Sortieren
                this.Verwalter.PersonenSortieren(ID);
                //Reload
                Response.Redirect(Request.RawUrl);
            }
            else
            { }
        }
        #endregion
    }
}


