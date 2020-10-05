using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020.Views
{
    public partial class Mannschaftsverwaltung : Page
    {
        #region Eigenschaften
        private Controller _verwalter;
        #endregion

        #region Accessoren/Modifier
        public Controller Verwalter { get => _verwalter; set => _verwalter = value; }
        #endregion

        #region Konstruktoren
        public Mannschaftsverwaltung()
        {

        }
        #endregion

        #region PAGEWorker
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Session
            if (this.Session.Count > 0)
            {
                this.Verwalter = (Controller)this.Session["Verwalter"];
            }
            else
            {
                this.Response.Redirect(@"~\Default.aspx");
                return;
            }
            #endregion

            #region postback
            if (this.IsPostBack)
            {
                //Hier landen alle postbacks, z.B. ButtonClicks u.s.w.
                if (rbListArt.Items[0].Selected)
                {
                    this.Verwalter.MannschaftOderGruppe = true;
                    lblsportart.Text = "Eine Sportart für die Mannschaft auswählen";
                    lblAnzeige.Text = "verfügbare Mannschaften:";
                    ((LiteralControl)this.tblEingabetabelle.Rows[0].Cells[0].Controls[0]).Text = "Mannschaftsname: ";
                    this.tblEingabetabelle2.Rows[0].Cells[0].Text = "Mitglieder der Mannschaft:";
                }
                else
                {
                    this.Verwalter.MannschaftOderGruppe = false;
                    lblsportart.Text = "Eine Sportart für die Gruppe auswählen";
                    lblAnzeige.Text = "verfügbare Gruppen:";
                    ((LiteralControl)this.tblEingabetabelle.Rows[0].Cells[0].Controls[0]).Text = "Gruppenname: ";
                    this.tblEingabetabelle2.Rows[0].Cells[0].Text = "Teilnehmer der Gruppe:";
                }
            }
            else
            {
                //hier normale reloads...
                this.LoadPersonen();
                this.LoadSportarten();
            }
            #endregion

            if (this.Verwalter.MannschaftOderGruppe)
            {
                rbListArt.Items[0].Selected = true;
                rbListArt.Items[1].Selected = false;
                rbListArt.Items[0].Attributes.Add("style", "background-color: lime;");
                rbListArt.Items[1].Attributes.Remove("style");
                lblsportart.Text = "Eine Sportart für die Mannschaft auswählen";
                if (btnHinzufuegenAendern.Text == "Gruppe hinzufügen")
                {
                    btnHinzufuegenAendern.Text = "Mannschaft hinzufügen";
                }
                else
                { }
                lblAnzeige.Text = "verfügbare Mannschaften:";
                ((LiteralControl)this.tblEingabetabelle.Rows[0].Cells[0].Controls[0]).Text = "Mannschaftsname: ";
                if (this.lstVorhandeneMitglieder.Items.Count <= 0)
                {
                    this.lstVorhandeneMitglieder.Items.Add("bisher keine Mitglieder");
                }
                else
                { }
                this.tblEingabetabelle2.Rows[0].Cells[0].Text = "Mitglieder der Mannschaft:";
                LoadMannschaften();
            }
            else
            {
                rbListArt.Items[0].Selected = false;
                rbListArt.Items[1].Selected = true;
                rbListArt.Items[0].Attributes.Remove("style");
                rbListArt.Items[1].Attributes.Add("style", "background-color: lime;");
                lblsportart.Text = "Eine Sportart für die Gruppe auswählen";
                if (btnHinzufuegenAendern.Text == "Mannschaft hinzufügen")
                {
                    btnHinzufuegenAendern.Text = "Gruppe hinzufügen";
                }
                else
                { }
                lblAnzeige.Text = "verfügbare Gruppen:";
                if (this.lstVorhandeneMitglieder.Items.Count <= 0)
                {
                    this.lstVorhandeneMitglieder.Items.Add("bisher keine Teilnehmer");
                }
                else if (this.lstVorhandeneMitglieder.Items.Count == 1 && this.lstVorhandeneMitglieder.Items[0].Text == "bisher keine Mitglieder")
                {
                    this.lstVorhandeneMitglieder.Items.Clear();
                    this.lstVorhandeneMitglieder.Items.Add("bisher keine Teilnehmer");
                }
                else
                { }

                ((LiteralControl)this.tblEingabetabelle.Rows[0].Cells[0].Controls[0]).Text = "Gruppenname: ";
                this.tblEingabetabelle2.Rows[0].Cells[0].Text = "Teilnehmer der Gruppe:";
                LoadGruppen();
            }
        }
        #endregion

        #region Worker
        #region Datenladen
        private void LoadPersonen()
        {
            if (this.Verwalter.Personen.Count > 0)
            {
                this.lstVorhandenePersonen.Items.Clear();
                foreach (Person pers in this.Verwalter.Personen)
                {
                    this.lstVorhandenePersonen.Items.Add(pers.GetListData());
                }
            }
            else
            {
                this.lstVorhandenePersonen.Items.Clear();
                this.lstVorhandenePersonen.Items.Add("keine vorhanden");
            }
        }
        private void LoadGruppen()
        {
            TableRow neueRow;
            TableCell neueCell;
            int index = 1;

            #region Headersetzen zum anclicken
            //ID
            Button newButton = new Button();
            newButton.Text = "ID";
            newButton.Width = Unit.Percentage(90);
            newButton.BackColor = this.tblAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btngrID";
            this.tblAnzeige.Rows[0].Cells[0].Controls.Add(newButton);
            //Name
            newButton = new Button();
            newButton.Text = "Name";
            newButton.Width = Unit.Percentage(90);
            newButton.BackColor = this.tblAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btngrName";
            newButton.Style.Add("text-align", "left");
            this.tblAnzeige.Rows[0].Cells[1].Controls.Add(newButton);
            //Sportart
            newButton = new Button();
            newButton.Text = "Sportart";
            newButton.Width = Unit.Percentage(95);
            newButton.BackColor = this.tblAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btngrSp";
            newButton.Style.Add("text-align", "left");
            this.tblAnzeige.Rows[0].Cells[2].Controls.Add(newButton);
            //Mitglieder
            newButton = new Button();
            newButton.Text = "Mitglieder";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btngrMitglieder";
            newButton.Style.Add("white-space", "normal");
            this.tblAnzeige.Rows[0].Cells[3].Controls.Add(newButton);
            //Edit
            newButton = new Button();
            newButton.Text = "Edit";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btngrEdit";
            newButton.Style.Add("white-space", "normal");
            this.tblAnzeige.Rows[0].Cells[4].Controls.Add(newButton);
            //Delete
            newButton = new Button();
            newButton.Text = "Del";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btngrDelete";
            newButton.Style.Add("white-space", "normal");
            this.tblAnzeige.Rows[0].Cells[5].Controls.Add(newButton);
            #endregion

            foreach (Gruppe grup in this.Verwalter.Gruppen)
            {
                neueRow = new TableRow();
                //ID
                neueCell = new TableCell();
                neueCell.Text = grup.ID.ToString();
                neueCell.HorizontalAlign = HorizontalAlign.Center;
                neueRow.Cells.Add(neueCell);
                //Name
                neueCell = new TableCell();
                neueCell.Text = grup.Name;
                neueRow.Cells.Add(neueCell);
                //Sportart
                neueCell = new TableCell();
                neueCell.Text = grup.Sportart.name;
                neueCell.HorizontalAlign = HorizontalAlign.Center;
                neueRow.Cells.Add(neueCell);
                //Mitglieder
                neueCell = new TableCell();
                DropDownList neuedropdownliste = new DropDownList();
                foreach (Person pers in grup.Mitglieder)
                {
                    neuedropdownliste.Items.Add(pers.Name + ", " + pers.Vorname + ", " + pers.Geburtsdatum.ToShortDateString());
                }
                if (neuedropdownliste.Items.Count <= 0)
                {
                    neuedropdownliste.Items.Add("bisher keine Teilnehmer");
                }
                else
                { }
                neueCell.Controls.Add(neuedropdownliste);
                neueRow.Cells.Add(neueCell);
                //Stift
                neueCell = new TableCell();
                neueCell.HorizontalAlign = HorizontalAlign.Center;
                ImageButton btnicon = new ImageButton();
                btnicon.ID = "bearbgr" + index;
                btnicon.ImageUrl = "~/Images/stift.png";
                btnicon.Click += Icon_Click;
                neueCell.Controls.Add(btnicon);
                neueRow.Cells.Add(neueCell);
                //Mülleimer
                neueCell = new TableCell();
                neueCell.HorizontalAlign = HorizontalAlign.Center;
                btnicon = new ImageButton();
                btnicon.ID = "loeschgr" + index;
                index++;
                btnicon.ImageUrl = "~/Images/muelleimer.png";
                btnicon.Click += Icon_Click;
                neueCell.Controls.Add(btnicon);
                neueRow.Cells.Add(neueCell);
                this.tblAnzeige.Rows.Add(neueRow);
            }
        }
        private void LoadMannschaften()
        {
            TableRow neueRow;
            TableCell neueCell;
            int index = 1;

            #region Headersetzen zum anclicken
            //ID
            Button newButton = new Button();
            newButton.Text = "ID";
            newButton.Width = Unit.Percentage(90);
            newButton.BackColor = this.tblAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnIDman";
            this.tblAnzeige.Rows[0].Cells[0].Controls.Add(newButton);
            //Name
            newButton = new Button();
            newButton.Text = "Name";
            newButton.Width = Unit.Percentage(90);
            newButton.BackColor = this.tblAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnNameman";
            newButton.Style.Add("text-align", "left");
            this.tblAnzeige.Rows[0].Cells[1].Controls.Add(newButton);
            //Sportart
            newButton = new Button();
            newButton.Text = "Sportart";
            newButton.Width = Unit.Percentage(95);
            newButton.BackColor = this.tblAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnSpman";
            newButton.Style.Add("text-align", "left");
            this.tblAnzeige.Rows[0].Cells[2].Controls.Add(newButton);
            //Mitglieder
            newButton = new Button();
            newButton.Text = "Mitglieder";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnMitgliederman";
            newButton.Style.Add("white-space", "normal");
            this.tblAnzeige.Rows[0].Cells[3].Controls.Add(newButton);
            //Edit
            newButton = new Button();
            newButton.Text = "Edit";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnEditman";
            newButton.Style.Add("white-space", "normal");
            this.tblAnzeige.Rows[0].Cells[4].Controls.Add(newButton);
            //Delete
            newButton = new Button();
            newButton.Text = "Del";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnDeleteman";
            newButton.Style.Add("white-space", "normal");
            this.tblAnzeige.Rows[0].Cells[5].Controls.Add(newButton);
            #endregion

            foreach (Mannschaft man in this.Verwalter.Mannschaften)
            {
                neueRow = new TableRow();
                //ID
                neueCell = new TableCell();
                neueCell.Text = man.ID.ToString();
                neueCell.HorizontalAlign = HorizontalAlign.Center;
                neueRow.Cells.Add(neueCell);
                //Name
                neueCell = new TableCell();
                neueCell.Text = man.Name;
                neueCell.Wrap = false;
                neueRow.Cells.Add(neueCell);
                //Sportart
                neueCell = new TableCell();
                neueCell.Text = man.Sportart.name;
                neueCell.HorizontalAlign = HorizontalAlign.Center;
                neueRow.Cells.Add(neueCell);
                //Mitglieder
                neueCell = new TableCell();
                DropDownList neuedropdownliste = new DropDownList();
                foreach (Person pers in man.Mitglieder)
                {
                    neuedropdownliste.Items.Add(pers.Name + ", " + pers.Vorname + ", " + pers.Geburtsdatum.ToShortDateString() + ", " + pers.GetType().Name + ", " + pers.Sportart.name);
                }
                if (neuedropdownliste.Items.Count <= 0)
                {
                    neuedropdownliste.Items.Add("bisher keine Mitglieder");
                }
                else
                { }
                neueCell.Controls.Add(neuedropdownliste);
                neueCell.Wrap = true;
                neueRow.Cells.Add(neueCell);
                //Stift
                neueCell = new TableCell();
                neueCell.HorizontalAlign = HorizontalAlign.Center;
                ImageButton btnicon = new ImageButton();
                btnicon.ID = "bearbman" + index;
                btnicon.ImageUrl = "~/Images/stift.png";
                btnicon.Click += Icon_Click;
                neueCell.Controls.Add(btnicon);
                neueRow.Cells.Add(neueCell);
                //Mülleimer
                neueCell = new TableCell();
                neueCell.HorizontalAlign = HorizontalAlign.Center;
                btnicon = new ImageButton();
                btnicon.ID = "loeschman" + index;
                index++;
                btnicon.ImageUrl = "~/Images/muelleimer.png";
                btnicon.Click += Icon_Click;
                neueCell.Controls.Add(btnicon);
                neueRow.Cells.Add(neueCell);
                this.tblAnzeige.Rows.Add(neueRow);
            }
        }
        private void LoadSportarten()
        {
            this.drpSportart1.Items.Clear();
            if (this.Verwalter.Sportarten.Count <= 0)
            {
                this.drpSportart1.Items.Add("Keine Sportart vorhanden");
            }
            else
            { }
            foreach (sportart sp in this.Verwalter.Sportarten)
            {
                this.drpSportart1.Items.Add(sp.name);
            }
        }
        #endregion

        #region ButtonClicks
        protected void imgBtnhinzu1_Click(object sender, ImageClickEventArgs e)
        {
            if (this.lstVorhandenePersonen.SelectedItem != null &&
               this.lstVorhandenePersonen.SelectedValue != "keine vorhanden")
            {
                if (this.lstVorhandeneMitglieder.Items[0].ToString() == "bisher keine Mitglieder" ||
                   this.lstVorhandeneMitglieder.Items[0].ToString() == "bisher keine Teilnehmer")
                {
                    this.lstVorhandeneMitglieder.Items.RemoveAt(0);
                }
                else
                { }

                this.lstVorhandeneMitglieder.Items.Add(this.lstVorhandenePersonen.SelectedValue);
                this.lstVorhandenePersonen.Items.Remove(this.lstVorhandenePersonen.SelectedItem);

                if (this.lstVorhandenePersonen.Items.Count > 0)
                {
                    this.lstVorhandenePersonen.Items[0].Selected = true;
                }
                else
                {
                    this.lstVorhandenePersonen.Items.Add("keine vorhanden");
                }
            }
            else
            { }
        }
        protected void imgBtnweg1_Click(object sender, ImageClickEventArgs e)
        {
            if (this.lstVorhandeneMitglieder.SelectedItem != null &&
                (!((this.lstVorhandeneMitglieder.SelectedValue == "bisher keine Mitglieder") ||
                   (this.lstVorhandeneMitglieder.SelectedValue == "bisher keine Teilnehmer"))))
            {
                if (this.lstVorhandenePersonen.Items[0].ToString() == "keine vorhanden")
                {
                    this.lstVorhandenePersonen.Items.RemoveAt(0);
                }
                else
                { }
                this.lstVorhandenePersonen.Items.Add(this.lstVorhandeneMitglieder.SelectedValue);
                this.lstVorhandeneMitglieder.Items.Remove(this.lstVorhandeneMitglieder.SelectedItem);
                if (this.lstVorhandeneMitglieder.Items.Count > 0)
                {
                    this.lstVorhandeneMitglieder.Items[0].Selected = true;
                }
                else
                {
                    if (this.Verwalter.MannschaftOderGruppe)
                    {
                        this.lstVorhandeneMitglieder.Items.Add("bisher keine Mitglieder");
                    }
                    else
                    {
                        this.lstVorhandeneMitglieder.Items.Add("bisher keine Teilnehmer");
                    }
                }
            }
            else
            { }
            if (this.lstVorhandeneMitglieder.Items.Count <= 0)
            {
                if (this.Verwalter.MannschaftOderGruppe)
                {
                    this.lstVorhandeneMitglieder.Items.Add("bisher keine Mitglieder");
                }
                else
                {
                    this.lstVorhandeneMitglieder.Items.Add("bisher keine Teilnehmer");
                }
            }
            else
            { }
        }
        protected void rbListArt_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lstVorhandenePersonen.Items.Clear();
            this.lstVorhandeneMitglieder.Items.Clear();
            this.txtName.Text = "";

            if (this.rbListArt.Items[0].Selected)//Mannschaft
            {
                this.Verwalter.MannschaftOderGruppe = true;
                rbListArt.Items[0].Attributes.Add("style", "background-color: lime;");
                lblsportart.Text = "Eine Sportart für die Mannschaft auswählen";
                btnHinzufuegenAendern.Text = "Mannschaft hinzufügen";
                lblAnzeige.Text = "verfügbare Mannschaften:";
                ((LiteralControl)this.tblEingabetabelle.Rows[0].Cells[0].Controls[0]).Text = "Mannschaftsname: ";
                this.tblEingabetabelle2.Rows[0].Cells[0].Text = "Mitglieder der Mannschaft:";
                this.lstVorhandeneMitglieder.Items.Add("bisher keine Mitglieder");
                if (this.Verwalter.Personen.Count <= 0)
                {
                    this.lstVorhandenePersonen.Items.Add("niemand vorhanden");
                }
                else
                {
                    foreach (Person pers in this.Verwalter.Personen)
                    {
                        this.lstVorhandenePersonen.Items.Add(pers.GetListData());
                    }
                }
            }
            else //Gruppe
            {
                this.Verwalter.MannschaftOderGruppe = false;
                rbListArt.Items[1].Attributes.Add("style", "background-color: lime;");
                lblsportart.Text = "Eine Sportart für die Gruppe auswählen";
                btnHinzufuegenAendern.Text = "Gruppe hinzufügen";
                lblAnzeige.Text = "verfügbare Gruppen:";
                ((LiteralControl)this.tblEingabetabelle.Rows[0].Cells[0].Controls[0]).Text = "Gruppenname: ";
                this.tblEingabetabelle2.Rows[0].Cells[0].Text = "Teilnehmer der Gruppe:";
                this.lstVorhandeneMitglieder.Items.Add("bisher keine Teilnehmer");
                if (this.Verwalter.Personen.Count <= 0)
                {
                    this.lstVorhandenePersonen.Items.Add("niemand vorhanden");
                }
                else
                {
                    foreach (Person pers in this.Verwalter.Personen)
                    {
                        this.lstVorhandenePersonen.Items.Add(pers.GetListData());
                    }
                }
            }
        }
        protected void btnHinzufuegen_Aendern_Click(object sender, EventArgs e)
        {
            if (rbListArt.Items[0].Selected)
            {
                Mannschaft neu;
                if (this.txtName.Text == "" ||
                    this.drpSportart1.SelectedValue == "Keine Sportart vorhanden" ||
                    this.drpSportart1.SelectedValue == "")
                {
                    this.txtName.Text = "";
                    this.lstVorhandeneMitglieder.Items.Clear();
                    this.lstVorhandeneMitglieder.Items.Add("bisher keine Mitglieder");
                    this.LoadPersonen();
                    return;
                }
                else
                { }

                if (this.btnHinzufuegenAendern.Text != "Änderung speichern") //Neue Mannschaft
                {
                    neu = new Mannschaft();
                    neu.Name = this.txtName.Text;
                    sportart toadd = null;
                    foreach (sportart spart in this.Verwalter.Sportarten)
                    {
                        if (spart.name == this.drpSportart1.SelectedValue)
                        {
                            toadd = spart;
                            break;
                        }
                        else
                        { }
                    }
                    neu.Sportart = toadd;
                    if (Verwalter.IsMannschVorhanden(neu.Name, neu.Sportart))
                    {
                        this.txtName.Text = "ist schon vorhanden!!";
                        this.lstVorhandeneMitglieder.Items.Clear();
                        this.lstVorhandeneMitglieder.Items.Add("bisher keine Mitglieder");
                        this.btnHinzufuegenAendern.Text = "Mannschaft hinzufügen";
                        Response.Redirect(Request.RawUrl);
                        return;
                    }
                    else
                    {
                        neu.Name = this.txtName.Text;
                        toadd = null;
                        foreach (sportart spart in this.Verwalter.Sportarten)
                        {
                            if (spart.name == this.drpSportart1.SelectedValue)
                            {
                                toadd = spart;
                                break;
                            }
                            else
                            { }
                        }
                        neu.Sportart = toadd;
                    }
                    //Mitglieder
                    foreach (ListItem li in this.lstVorhandeneMitglieder.Items)
                    {
                        if (li.Text.Contains("bisher keine Mitglieder"))
                        {
                            break;
                        }
                        else
                        {
                        }
                    }

                    if (!this.Verwalter.AddMannschaft(neu, this.lstVorhandeneMitglieder.Items))
                    {
                        return;
                    }
                    else
                    { }

                    this.txtName.Text = "";
                    this.lstVorhandeneMitglieder.Items.Clear();
                    this.lstVorhandeneMitglieder.Items.Add("bisher keine Mitglieder");
                }
                //Aendern einer Mannschaft
                else
                {
                    if (!this.Verwalter.ChangeMannschaft(this.txtName.Text, this.drpSportart1.SelectedValue, this.lstVorhandeneMitglieder.Items))
                    {
                        return;
                    }
                    else
                    { }
                    this.txtName.Text = "";
                    this.lstVorhandeneMitglieder.Items.Clear();
                    this.lstVorhandeneMitglieder.Items.Add("bisher keine Mitglieder");
                }

                this.btnHinzufuegenAendern.Text = "Mannschaft hinzufügen";
            }
            else
            {
                Gruppe neu;
                if (this.txtName.Text == "" ||
                    this.drpSportart1.SelectedValue == "Keine Sportart vorhanden" ||
                    this.drpSportart1.SelectedValue == "")
                {
                    this.txtName.Text = "";
                    this.lstVorhandeneMitglieder.Items.Clear();
                    this.lstVorhandeneMitglieder.Items.Add("bisher keine Teilnehmer");
                    this.LoadPersonen();
                    return;
                }
                else
                { }

                #region neue Gruppe
                if (this.btnHinzufuegenAendern.Text != "Änderung speichern")
                {
                    neu = new Gruppe();
                    neu.Name = this.txtName.Text;
                    sportart toadd = null;
                    foreach (sportart spart in this.Verwalter.Sportarten)
                    {
                        if (spart.name == this.drpSportart1.SelectedValue)
                        {
                            toadd = spart;
                            break;
                        }
                        else
                        { }
                    }
                    neu.Sportart = toadd;
                    if (Verwalter.IsGruppeVorhanden(neu.Name, neu.Sportart))
                    {
                        this.txtName.Text = "ist schon vorhanden!!";
                        this.lstVorhandeneMitglieder.Items.Clear();
                        this.lstVorhandeneMitglieder.Items.Add("bisher keine Teilnehmer");
                        this.btnHinzufuegenAendern.Text = "Gruppe hinzufügen";
                        Response.Redirect(Request.RawUrl);
                        return;
                    }
                    else
                    {
                        neu.Name = this.txtName.Text;
                        toadd = null;
                        foreach (sportart spart in this.Verwalter.Sportarten)
                        {
                            if (spart.name == this.drpSportart1.SelectedValue)
                            {
                                toadd = spart;
                                break;
                            }
                            else
                            { }
                        }
                        neu.Sportart = toadd;
                    }
                    //Mitglieder
                    foreach (ListItem li in this.lstVorhandeneMitglieder.Items)
                    {
                        if (li.Text.Contains("bisher keine Teilnehmer"))
                        {
                            break;
                        }
                        else
                        {
                        }
                    }

                    if (!this.Verwalter.AddGruppe(neu, this.lstVorhandeneMitglieder.Items))
                    {
                        return;
                    }
                    else
                    { }

                    this.txtName.Text = "";
                    this.lstVorhandeneMitglieder.Items.Clear();
                    this.lstVorhandeneMitglieder.Items.Add("bisher keine Teilnehmer");
                }
                #endregion

                #region Gruppe ändern
                else //Änderung
                {
                    if (!this.Verwalter.ChangeGruppe(this.txtName.Text, this.drpSportart1.SelectedValue, this.lstVorhandeneMitglieder.Items))
                    {
                        return;
                    }
                    else
                    { }
                    this.txtName.Text = "";
                    this.lstVorhandeneMitglieder.Items.Clear();
                    this.lstVorhandeneMitglieder.Items.Add("bisher keine Teilnehmer");
                }
                #endregion


                this.txtName.Text = "";
                this.lstVorhandeneMitglieder.Items.Clear();
                this.lstVorhandeneMitglieder.Items.Add("bisher keine Teilnehmer");
                this.btnHinzufuegenAendern.Text = "Gruppe hinzufügen";
            }
            this.rbListArt_SelectedIndexChanged(null, null);
            Response.Redirect(Request.RawUrl);
        }
        //Edit oder Delete
        private void Icon_Click(object sender, EventArgs e)
        {
            int mannschaftsid = -1;
            int gruppenid = -1;
            int index = -1;
            string id = ((ImageButton)sender).ID;

            #region Mannschaft
            if (id.Contains("man"))
            {
                if (id.Contains("bearb"))
                {
                    this.btnHinzufuegenAendern.Text = "Änderung speichern";
                    this.txtName.Text = "";
                    this.lstVorhandeneMitglieder.Items.Clear();
                    index = Convert.ToInt32(id.Substring(8));
                    mannschaftsid = this.Verwalter.Mannschaften[index - 1].ID;
                    this.Verwalter.EditMannschaft = mannschaftsid;

                    //Daten laden
                    this.txtName.Text = this.Verwalter.Mannschaften[index - 1].Name;
                    this.drpSportart1.SelectedValue = this.Verwalter.Mannschaften[index - 1].Sportart.name;
                    foreach (Person pers in this.Verwalter.Mannschaften[index - 1].Mitglieder)
                    {
                        string zeile = "";

                        zeile = pers.ID + ", " + pers.Name + ", " + pers.Vorname + ", " + pers.Geburtsdatum.ToShortDateString() + ", " + pers.GetType().Name + ", " + pers.Sportart.name;

                        this.lstVorhandeneMitglieder.Items.Add(zeile);
                        //LoadPersonen();
                        foreach (ListItem ls in this.lstVorhandenePersonen.Items)
                        {
                            if (ls.Text == zeile)
                            {
                                this.lstVorhandenePersonen.Items.Remove(ls);
                                break;
                            }
                            else
                            { }
                        }
                        if (this.lstVorhandenePersonen.Items.Count <= 0)
                        {
                            this.lstVorhandenePersonen.Items.Add("keine vorhanden");
                        }
                        else
                        { }
                    }

                    if (this.Verwalter.Mannschaften[index - 1].Mitglieder.Count <= 0)
                    {
                        this.lstVorhandeneMitglieder.Items.Add("bisher keine Mitglieder");
                    }
                    else
                    { }
                }
                else if (id.Contains("loesch"))
                {
                    string num = id.Substring(id.IndexOf("man") + 3);
                    int index1 = Convert.ToInt32(num);
                    bool vorhanden = this.Verwalter.IsMannschaftoderGruppeInTurnier(index1 - 1, 0);
                    if (!vorhanden)
                    {
                        this.Verwalter.DeleteMannschaft(index1);
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        //Meldung
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Die Mannschaft ist noch Mitglied in einem Turnier bitte vorher entfernen');", true);
                    }
                }
                else
                { }
            }
            #endregion
            #region Gruppe
            else if (id.Contains("gr"))
            {
                if (id.Contains("bearb"))
                {
                    this.btnHinzufuegenAendern.Text = "Änderung speichern";
                    this.txtName.Text = "";
                    this.lstVorhandeneMitglieder.Items.Clear();
                    index = Convert.ToInt32(id.Substring(7));
                    gruppenid = this.Verwalter.Gruppen[index - 1].ID;
                    this.Verwalter.EditGruppe = gruppenid;

                    //Daten laden
                    this.txtName.Text = this.Verwalter.Gruppen[index - 1].Name;
                    this.drpSportart1.SelectedValue = this.Verwalter.Gruppen[index - 1].Sportart.name;
                    foreach (Person pers in this.Verwalter.Gruppen[index - 1].Mitglieder)
                    {
                        string zeile = "";

                        zeile = pers.ID + ", " + pers.Name + ", " + pers.Vorname + ", " + pers.Geburtsdatum.ToShortDateString() + ", " + pers.GetType().Name + ", " + pers.Sportart.name;

                        this.lstVorhandeneMitglieder.Items.Add(zeile);
                        //this.LoadPersonen();
                        foreach (ListItem ls in this.lstVorhandenePersonen.Items)
                        {
                            if (ls.Text == zeile)
                            {
                                this.lstVorhandenePersonen.Items.Remove(ls);
                                break;
                            }
                            else
                            { }
                        }
                        if (this.lstVorhandenePersonen.Items.Count <= 0)
                        {
                            this.lstVorhandenePersonen.Items.Add("keine vorhanden");
                        }
                        else
                        { }
                    }

                    if (this.Verwalter.Gruppen[index - 1].Mitglieder.Count <= 0)
                    {
                        this.lstVorhandeneMitglieder.Items.Add("bisher keine Teilnehmer");
                    }
                    else
                    { }
                }
                else if (id.Contains("loesch"))
                {
                    string num = id.Substring(id.IndexOf("gr") + 2);
                    int index1 = Convert.ToInt32(num);
                    bool vorhanden = this.Verwalter.IsMannschaftoderGruppeInTurnier(index1 - 1, 1);
                    if (!vorhanden)
                    {
                        this.Verwalter.DeleteGruppe(index1);
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        //Meldung
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Die Mannschaft oder Gruppe ist noch Mitglied in einem Turnier bitte vorher entfernen');", true);
                    }
                }
                else
                { }
            }
            #endregion
            else
            {
                //Fehler
            }
        }
        #endregion

        #endregion


    }
}