using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020.Views
{
    public partial class Turnierverwaltung : System.Web.UI.Page
    {
        #region Eigenschaften
        private Controller _verwalter;
        #endregion

        #region Accessoren/Modifier
        public Controller Verwalter { get => _verwalter; set => _verwalter = value; }
        #endregion

        #region Konstruktoren
        public Turnierverwaltung()
        {

        }
        #endregion

        #region Worker
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
            if (this.IsPostBack)
            {

            }
            else
            {
                LoadSportarten();
                LoadTeilnehmer();
            }

            LoadTurniere();
        }

        private void LoadTurniere()
        {
            int index = 1;

            #region Headersetzen zum anclicken
            //ID
            Button newButton = new Button();
            newButton.Text = "ID";
            newButton.Width = Unit.Percentage(90);
            newButton.BackColor = this.tblTurnierAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnID";
            this.tblTurnierAnzeige.Rows[0].Cells[0].Controls.Add(newButton);
            //Name
            newButton = new Button();
            newButton.Text = "Name";
            newButton.Width = Unit.Percentage(90);
            newButton.BackColor = this.tblTurnierAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnName";
            newButton.Style.Add("text-align", "left");
            this.tblTurnierAnzeige.Rows[0].Cells[1].Controls.Add(newButton);
            //Sportart
            newButton = new Button();
            newButton.Text = "Sportart";
            newButton.Width = Unit.Percentage(95);
            newButton.BackColor = this.tblTurnierAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnSp";
            newButton.Style.Add("text-align", "left");
            this.tblTurnierAnzeige.Rows[0].Cells[2].Controls.Add(newButton);
            //Mitglieder
            newButton = new Button();
            newButton.Text = "Mitglieder";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblTurnierAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnMitglieder";
            newButton.Style.Add("white-space", "normal");
            this.tblTurnierAnzeige.Rows[0].Cells[3].Controls.Add(newButton);
            //Typ
            newButton = new Button();
            newButton.Text = "Typ";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblTurnierAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnTyp";
            newButton.Style.Add("white-space", "normal");
            this.tblTurnierAnzeige.Rows[0].Cells[4].Controls.Add(newButton);
            //Edit
            newButton = new Button();
            newButton.Text = "Edit";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblTurnierAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnEdit";
            newButton.Style.Add("white-space", "normal");
            this.tblTurnierAnzeige.Rows[0].Cells[5].Controls.Add(newButton);
            //Delete
            newButton = new Button();
            newButton.Text = "Del";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblTurnierAnzeige.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnDelete";
            newButton.Style.Add("white-space", "normal");
            this.tblTurnierAnzeige.Rows[0].Cells[6].Controls.Add(newButton);
            #endregion

            foreach (Turnier turnier in this.Verwalter.Turniere)
            {
                TableRow neueRow = new TableRow();
                //ID
                TableCell neueCell = new TableCell();
                neueCell.Text = turnier.ID.ToString();
                neueCell.HorizontalAlign = HorizontalAlign.Center;
                neueRow.Cells.Add(neueCell);
                //Name
                neueCell = new TableCell();
                neueCell.Text = turnier.Bezeichnung;
                neueCell.Wrap = false;
                neueRow.Cells.Add(neueCell);
                //Sportart
                neueCell = new TableCell();
                neueCell.Text = turnier.Sportart.name;
                neueCell.HorizontalAlign = HorizontalAlign.Center;
                neueRow.Cells.Add(neueCell);
                //Mitglieder
                neueCell = new TableCell();
                DropDownList neuedropdownliste = new DropDownList();
                if (turnier is MannschaftsTurnier)
                {
                    foreach (Mannschaft man in ((MannschaftsTurnier)turnier).Teilnehmer)
                    {
                        neuedropdownliste.Items.Add(man.ID + ", " + man.Name + ", " + man.Sportart.name);
                    }
                }
                else
                {
                    foreach (Gruppe grp in ((GruppenTurnier)turnier).getTeilnemer())
                    {
                        neuedropdownliste.Items.Add(grp.ID + ", " + grp.Name + ", " + grp.Sportart.name);
                    }
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
                //Typ
                neueCell = new TableCell();
                if (turnier is MannschaftsTurnier)
                {
                    neueCell.Text = "Mannschaften";
                }
                else
                {
                    neueCell.Text = "Gruppen";
                }
                neueCell.HorizontalAlign = HorizontalAlign.Center;
                neueRow.Cells.Add(neueCell);
                //Stift
                neueCell = new TableCell();
                neueCell.HorizontalAlign = HorizontalAlign.Center;
                ImageButton btnicon = new ImageButton();
                if (turnier is MannschaftsTurnier)
                {
                    btnicon.ID = "bearbman" + index;
                }
                else
                {
                    btnicon.ID = "bearbgrp" + index;
                }
                btnicon.ImageUrl = "~/Images/stift.png";
                btnicon.Click += Icon_Click;
                neueCell.Controls.Add(btnicon);
                neueRow.Cells.Add(neueCell);
                //Mülleimer
                neueCell = new TableCell();
                neueCell.HorizontalAlign = HorizontalAlign.Center;
                btnicon = new ImageButton();
                if (turnier is MannschaftsTurnier)
                {
                    btnicon.ID = "loeschman" + index;
                    index++;
                }
                else
                {
                    btnicon.ID = "loeschgrp" + index;
                    index++;
                }
                btnicon.ImageUrl = "~/Images/muelleimer.png";
                btnicon.Click += Icon_Click;
                neueCell.Controls.Add(btnicon);
                neueRow.Cells.Add(neueCell);
                this.tblTurnierAnzeige.Rows.Add(neueRow);
            }
        }

        private void Icon_Click(object sender, ImageClickEventArgs e)
        {
            string id = ((ImageButton)sender).ID;
            int index = -1;

            if (id.Contains("man"))//Mannschaften
            {
                if (id.Contains("bearb"))
                {
                    //Daten des Turniers lesen
                    this.rbListArt.Items[0].Selected = true;
                    this.rbListArt.Items[1].Selected = false;
                    index = Convert.ToInt32(id.Substring(8));
                    this.Verwalter.IndexEditTurnier = index - 1;
                    this.txtNameTurnier.Text = this.Verwalter.Turniere[this.Verwalter.IndexEditTurnier].Bezeichnung;
                    int sportindex = 0;
                    foreach (sportart sp in this.Verwalter.Sportarten)
                    {
                        if (sp.name == this.Verwalter.Turniere[this.Verwalter.IndexEditTurnier].Sportart.name)
                        {
                            this.drpSportart1.SelectedIndex = sportindex;
                            break;
                        }
                        else
                        {
                            sportindex++;
                        }
                    }
                    this.tblEingabetabellegr.Rows[0].Cells[0].Text = "vorhandene Mannschaften";
                    this.tblEingabetabellegr.Rows[0].Cells[2].Text = "verfügbare Mannschaften:";
                    this.lstVorhandeneTeilnehmer.Items.Clear();
                    if (this.Verwalter.Turniere[this.Verwalter.IndexEditTurnier].getAnzahlTeilnehmer() > 0)
                    {
                        foreach (Mannschaft man in this.Verwalter.Turniere[this.Verwalter.IndexEditTurnier].getTeilnemer())
                        {
                            string it = man.ID + ", " + man.Name + ", " + man.Sportart;
                            this.lstVorhandeneTeilnehmer.Items.Add(it);
                        }
                    }
                    else
                    {
                        this.lstVorhandeneTeilnehmer.Items.Add("bisher keine Mannschaften");
                    }
                    this.lstVerfuegbareTeilnehmer.Items.Clear();
                    foreach (Mannschaft man in this.Verwalter.Mannschaften)
                    {
                        string item = man.ID + ", " + man.Name + ", " + man.Sportart;
                        lstVerfuegbareTeilnehmer.Items.Add(item);
                    }
                    foreach (ListItem ls in lstVorhandeneTeilnehmer.Items)
                    {
                        if (lstVerfuegbareTeilnehmer.Items.Contains(ls))
                        {
                            lstVerfuegbareTeilnehmer.Items.Remove(ls);
                        }
                    }
                    if (lstVerfuegbareTeilnehmer.Items.Count < 1)
                    {
                        lstVerfuegbareTeilnehmer.Items.Add("keine weiteren Mannschaften vorhanden");
                    }
                    else
                    { }
                    this.btnTurnierHinzufuegen.Text = "Turnier ändern";
                }
                else if (id.Contains("loesch"))
                {
                    //Turnier löschen
                    index = Convert.ToInt32(id.Substring(9));
                    this.Verwalter.DeleteTurnier(index - 1);
                    Response.Redirect(Request.RawUrl);
                }
                else
                {

                }
            }
            else //Gruppen
            {
                if (id.Contains("bearb"))
                {
                    //Daten der Gruppe lesen
                    this.rbListArt.Items[0].Selected = false;
                    this.rbListArt.Items[1].Selected = true;
                    index = Convert.ToInt32(id.Substring(8));
                    this.Verwalter.IndexEditTurnier = index - 1;
                    this.txtNameTurnier.Text = this.Verwalter.Turniere[this.Verwalter.IndexEditTurnier].Bezeichnung;
                    int sportindex = 0;
                    foreach (sportart sp in this.Verwalter.Sportarten)
                    {
                        if (sp.name == this.Verwalter.Turniere[this.Verwalter.IndexEditTurnier].Sportart.name)
                        {
                            this.drpSportart1.SelectedIndex = sportindex;
                            break;
                        }
                        else
                        {
                            sportindex++;
                        }
                    }
                    this.tblEingabetabellegr.Rows[0].Cells[0].Text = "vorhandene Gruppen";
                    this.tblEingabetabellegr.Rows[0].Cells[2].Text = "verfügbare Gruppen:";
                    this.lstVorhandeneTeilnehmer.Items.Clear();
                    if (this.Verwalter.Turniere[this.Verwalter.IndexEditTurnier].getAnzahlTeilnehmer() > 0)
                    {
                        foreach (Gruppe grp in this.Verwalter.Turniere[this.Verwalter.IndexEditTurnier].getTeilnemer())
                        {
                            string it = grp.ID + ", " + grp.Name + ", " + grp.Sportart;
                            this.lstVorhandeneTeilnehmer.Items.Add(it);
                        }
                    }
                    else
                    {
                        this.lstVorhandeneTeilnehmer.Items.Add("bisher keine Teilnehmer");
                    }
                    this.lstVerfuegbareTeilnehmer.Items.Clear();
                    foreach (Gruppe grp in this.Verwalter.Gruppen)
                    {
                        string item = grp.ID + ", " + grp.Name + ", " + grp.Sportart;
                        lstVerfuegbareTeilnehmer.Items.Add(item);
                    }
                    foreach (ListItem ls in lstVorhandeneTeilnehmer.Items)
                    {
                        if (lstVerfuegbareTeilnehmer.Items.Contains(ls))
                        {
                            lstVerfuegbareTeilnehmer.Items.Remove(ls);
                        }
                    }
                    if (lstVerfuegbareTeilnehmer.Items.Count < 1)
                    {
                        lstVerfuegbareTeilnehmer.Items.Add("keine weiteren Teilnehmer vorhanden");
                    }
                    else
                    { }
                    this.btnTurnierHinzufuegen.Text = "Turnier ändern";
                }
                else if (id.Contains("loesch"))
                {
                    //Gruppe löschen
                    index = Convert.ToInt32(id.Substring(9));
                    this.Verwalter.DeleteTurnier(index - 1);
                    Response.Redirect(Request.RawUrl);
                }
                else
                {

                }
            }
        }

        private void LoadSportarten()
        {
            this.drpSportart1.Items.Clear();
            foreach (sportart sp in this.Verwalter.Sportarten)
            {
                this.drpSportart1.Items.Add(sp.name);
            }
        }
        private void LoadTeilnehmer()
        {
            this.lstVerfuegbareTeilnehmer.Items.Clear();
            this.lstVorhandeneTeilnehmer.Items.Clear();
            this.tblEingabetabellegr.Rows[0].Cells[0].Text = "vorhandene Mannschaften:";
            this.lstVorhandeneTeilnehmer.Items.Add("bisher keine Mannschaften");
            this.tblEingabetabellegr.Rows[0].Cells[2].Text = "verfügbare Mannschaften:";
            this.Verwalter.MannschaftOderGruppe = true;
            foreach (Mannschaft man in this.Verwalter.Mannschaften)
            {
                this.lstVerfuegbareTeilnehmer.Items.Add(man.ID + ", " + man.Name + ", " + man.Sportart.name);
            }
        }

        protected void rbListArt_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lstVerfuegbareTeilnehmer.Items.Clear();
            this.lstVorhandeneTeilnehmer.Items.Clear();
            this.txtNameTurnier.Text = "";
            this.btnTurnierHinzufuegen.Text = "Turnier hinzufügen";
            this.btnTurnierHinzufuegen.BackColor = Color.Lime;
            if (this.rbListArt.Items[0].Selected)//Mannschaft
            {
                this.Verwalter.MannschaftOderGruppe = true;
                this.tblEingabetabellegr.Rows[0].Cells[0].Text = "vorhandene Mannschaften:";
                this.lstVorhandeneTeilnehmer.Items.Add("bisher keine Mannschaften");
                this.tblEingabetabellegr.Rows[0].Cells[2].Text = "verfügbare Mannschaften:";
                foreach (Mannschaft man in this.Verwalter.Mannschaften)
                {
                    this.lstVerfuegbareTeilnehmer.Items.Add(man.ID + ", " + man.Name + ", " + man.Sportart);
                }
            }
            else //Gruppe
            {
                this.Verwalter.MannschaftOderGruppe = false;
                this.tblEingabetabellegr.Rows[0].Cells[0].Text = "vorhandene Gruppen:";
                this.lstVorhandeneTeilnehmer.Items.Add("bisher keine Teilnehmer");
                this.tblEingabetabellegr.Rows[0].Cells[2].Text = "verfügbare Gruppen:";
                foreach (Gruppe grp in this.Verwalter.Gruppen)
                {
                    this.lstVerfuegbareTeilnehmer.Items.Add(grp.ID + ", " + grp.Name + ", " + grp.Sportart);
                }
            }
        }

        protected void btnTurnierHinzufuegenAendern_Click(object sender, EventArgs e)
        {
            if (this.btnTurnierHinzufuegen.Text != "Turnier ändern")
            {
                if (this.rbListArt.Items[0].Selected)
                {
                    if (this.Verwalter.AddTurnier(this.txtNameTurnier.Text, this.drpSportart1.SelectedValue, this.lstVorhandeneTeilnehmer.Items, 0))
                    {

                    }
                    else
                    {
                        //Fehlermeldung
                    }
                }
                else
                {
                    if (this.Verwalter.AddTurnier(this.txtNameTurnier.Text, this.drpSportart1.SelectedValue, this.lstVorhandeneTeilnehmer.Items, 1))
                    {

                    }
                    else
                    {
                        //Fehlermeldung
                    }
                }
                this.btnTurnierHinzufuegen.Text = "Turnier hinzufügen";
            }
            else
            {
                if (this.rbListArt.Items[0].Selected)
                {
                    if (this.Verwalter.ChangeTurnier(this.txtNameTurnier.Text, this.drpSportart1.SelectedValue, this.lstVorhandeneTeilnehmer.Items, 0))
                    {

                    }
                    else
                    {
                        //Fehlermeldung
                    }
                }
                else
                {
                    if (this.Verwalter.ChangeTurnier(this.txtNameTurnier.Text, this.drpSportart1.SelectedValue, this.lstVorhandeneTeilnehmer.Items, 1))
                    {

                    }
                    else
                    {
                        //Fehlermeldung
                    }
                }
            }
            Response.Redirect(Request.RawUrl);
        }
        protected void imgBtnhinzu1_Click(object sender, ImageClickEventArgs e)
        {
            if (this.lstVerfuegbareTeilnehmer.SelectedItem != null &&
                !(this.lstVerfuegbareTeilnehmer.SelectedValue == "keine Mannschaften vorhanden" ||
                 this.lstVerfuegbareTeilnehmer.SelectedValue == "keine Teilnehmer vorhanden"))
            {
                if (this.lstVorhandeneTeilnehmer.Items[0].ToString() == "bisher keine Mannschaften" ||
                    this.lstVorhandeneTeilnehmer.Items[0].ToString() == "bisher keine Teilnehmer")
                {
                    this.lstVorhandeneTeilnehmer.Items.RemoveAt(0);
                }
                else
                { }
                if (!this.lstVorhandeneTeilnehmer.Items.Contains(this.lstVerfuegbareTeilnehmer.SelectedItem))
                {
                    this.lstVorhandeneTeilnehmer.Items.Add(this.lstVerfuegbareTeilnehmer.SelectedValue);
                    this.lstVerfuegbareTeilnehmer.Items.Remove(this.lstVerfuegbareTeilnehmer.SelectedItem);
                }
                else
                { }


                if (this.lstVerfuegbareTeilnehmer.Items.Count > 0)
                {
                    this.lstVerfuegbareTeilnehmer.Items[0].Selected = true;
                }
                else
                {
                    if (this.rbListArt.Items[0].Selected)
                    {
                        this.lstVerfuegbareTeilnehmer.Items.Add("keine weiteren Mannschaften vorhanden");
                    }
                    else
                    {
                        this.lstVerfuegbareTeilnehmer.Items.Add("keine weiteren Teilnehmer vorhanden");
                    }
                }
            }
            else
            { }
        }
        protected void imgBtnweg1_Click(object sender, ImageClickEventArgs e)
        {
            if (this.lstVorhandeneTeilnehmer.SelectedItem != null &&
                !(this.lstVorhandeneTeilnehmer.SelectedValue == "bisher keine Mannschaften" ||
                    this.lstVorhandeneTeilnehmer.SelectedValue == "bisher keine Teilnehmer"))
            {
                if (this.lstVerfuegbareTeilnehmer.Items[0].ToString() == "keine Mannschaften vorhanden" ||
                    this.lstVerfuegbareTeilnehmer.Items[0].ToString() == "keine Teilnehmer vorhanden" ||
                    this.lstVerfuegbareTeilnehmer.Items[0].ToString() == "keine weiteren Mannschaften vorhanden" ||
                    this.lstVerfuegbareTeilnehmer.Items[0].ToString() == "keine weiteren Teilnehmer vorhanden")
                {
                    this.lstVerfuegbareTeilnehmer.Items.RemoveAt(0);
                }
                else
                { }

                if (!this.lstVerfuegbareTeilnehmer.Items.Contains(this.lstVorhandeneTeilnehmer.SelectedItem))
                {
                    this.lstVerfuegbareTeilnehmer.Items.Add(this.lstVorhandeneTeilnehmer.SelectedValue);
                }
                else
                { }
                this.lstVorhandeneTeilnehmer.Items.Remove(this.lstVorhandeneTeilnehmer.SelectedItem);
                if (this.lstVorhandeneTeilnehmer.Items.Count > 0)
                {
                    this.lstVorhandeneTeilnehmer.Items[0].Selected = true;
                }
                else
                {
                    if (this.rbListArt.Items[0].Selected)
                    {
                        this.lstVorhandeneTeilnehmer.Items.Add("bisher keine Mannschaften");
                    }
                    else
                    {
                        this.lstVorhandeneTeilnehmer.Items.Add("bisher keine Teilnehmer");
                    }
                }
                if (this.lstVerfuegbareTeilnehmer.Items.Count > 0)
                {
                    this.lstVerfuegbareTeilnehmer.Items[0].Selected = true;
                }
                else
                {
                    if (this.rbListArt.Items[0].Selected)
                    {
                        this.lstVerfuegbareTeilnehmer.Items.Add("bisher keine Mannschaften");
                    }
                    else
                    {
                        this.lstVerfuegbareTeilnehmer.Items.Add("bisher keine Teilnehmer");
                    }
                }
            }
            else
            { }
        }
        #endregion
    }
}