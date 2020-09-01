using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020.Views
{
    public partial class Spieleverwaltung : System.Web.UI.Page
    {
        private Controller _verwalter;

        public Controller Verwalter { get => _verwalter; set => _verwalter = value; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session.Count > 0)
            {
                this.Verwalter = (Controller)this.Session["Verwalter"];
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
                btnNeu.Enabled = false;
                btnAutomatik.Enabled = false;
                CheckBox1.Enabled = false;
                CheckBox2.Enabled = false;
            }
            if (this.IsPostBack)
            {
                //Hier landen alle postbacks, z.B. ButtonClicks u.s.w.
                string temp = this.Request.Form["ctl00$MainContent$drplistSpieltag"];
                if (this.Request.Form["ctl00$MainContent$drplistSpieltag"] != "-" &&
                    this.Request.Form["ctl00$MainContent$drplistSpieltag"] != null)
                {

                    this.Verwalter.SelectedTurnierSpieltag = Convert.ToInt32(this.Request.Form["ctl00$MainContent$drplistSpieltag"]);
                    if (this.Verwalter.SelectedTurnier is GruppenTurnier)
                    {
                        this.Verwalter.SelectedTurnierGruppe = this.Verwalter.SelectedTurnierSpieltag;
                        this.Verwalter.SelectedTurnier.setSelectedGruppe(this.Verwalter.SelectedTurnierSpieltag);
                    }
                    else
                    {
                        this.Verwalter.SelectedTurnierGruppe = 0;
                    }
                    this.drplistSpieltag.SelectedIndex = this.Verwalter.SelectedTurnierSpieltag - 1;
                }
                else
                { }

                object tempcheck1 = this.Request.Form["ctl00$MainContent$CheckBox1"];
                object tempcheck2 = this.Request.Form["ctl00$MainContent$CheckBox2"];
                if (tempcheck1 == null && tempcheck2 != null)
                {
                    this.CheckBox1.Checked = false;
                    this.CheckBox2.Checked = true;
                }
                else if (tempcheck1 != null && tempcheck2 == null)
                {
                    this.CheckBox1.Checked = true;
                    this.CheckBox2.Checked = false;
                }
                else
                { }
            }
            else
            {
                //hier normale reloads...
                LoadTurniere();
                if (this.Verwalter.SelectedTurnierIndex > 0)
                {
                    this.Verwalter.SelectedTurnier = this.Verwalter.Turniere[this.Verwalter.SelectedTurnierIndex - 1];
                    this.drpListTurniere.SelectedIndex = this.Verwalter.SelectedTurnierIndex;
                }
                else
                { }
                if (this.Verwalter.hinundrueck)
                {
                    this.CheckBox1.Checked = true;
                    this.CheckBox2.Checked = false;
                }
                else
                {
                    this.CheckBox1.Checked = false;
                    this.CheckBox2.Checked = true;
                }
            }

            if (this.Verwalter.SelectedTurnier != null)
            {
                this.lblTitelTable.Visible = true;
                this.tblSpiele.Visible = true;
                this.btnAutomatik.Visible = true;
                this.btnNeu.Visible = true;
                this.lblspieltag.Visible = true;
                this.drplistSpieltag.Visible = true;
                this.btnspieltagauswaehlen.Visible = true;
                this.CheckBox1.Visible = true;
                this.CheckBox2.Visible = true;
                if (this.Verwalter.SelectedTurnierSpieltag < 0)
                {
                    this.Verwalter.SelectedTurnierSpieltag = 0;
                }
                else
                { }

                if (this.Verwalter.SelectedTurnier is MannschaftsTurnier)
                {
                    this.lblTitelTable.Text = "Vorhandene Spiele des " + this.Verwalter.SelectedTurnier.Sportart.name + " - Turniers '" +
                        this.Verwalter.SelectedTurnier.Bezeichnung +
                        "', Spieltag: " + this.Verwalter.SelectedTurnierSpieltag +
                        "(" + this.Verwalter.SelectedTurnier.Get_MaxRunden() + ")";
                    this.Verwalter.SelectedTurnierGruppe = 0;

                }
                else if (this.Verwalter.SelectedTurnier is GruppenTurnier)
                {
                    this.lblTitelTable.Text = "Vorhandene Spiele des " + this.Verwalter.SelectedTurnier.Sportart.name + " - Turniers '" +
                        this.Verwalter.SelectedTurnier.Bezeichnung +
                        "', Gruppe: " + this.Verwalter.SelectedTurnierGruppe +
                        "(" + this.Verwalter.SelectedTurnier.getTeilnemer().Count + ")";
                    this.lblspieltag.Text = "Gruppe:";

                }
                else
                { }
                LoadSpiele();
            }
            else
            {

            }
        }

        //Spieletabelle eines Spieltages füllen
        private void LoadSpiele()
        {
            TableRow neueRow;
            TableCell neueCell;
            int index = 0;

            #region Headersetzen zum anclicken
            //ID
            Button newButton = new Button();
            newButton.Text = "ID";
            newButton.Width = Unit.Percentage(90);
            newButton.BackColor = this.tblSpiele.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnIDturn";
            this.tblSpiele.Rows[0].Cells[0].Controls.Add(newButton);
            //Mannschaft1
            newButton = new Button();
            newButton.Text = this.Verwalter.SelectedTurnier.getTeilnehmerbezeichnung() + "1";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblSpiele.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btn1";
            newButton.Style.Add("white-space", "normal");
            this.tblSpiele.Rows[0].Cells[1].Controls.Add(newButton);
            //Mannschaft2
            newButton = new Button();
            newButton.Text = this.Verwalter.SelectedTurnier.getTeilnehmerbezeichnung() + "2";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblSpiele.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btn2";
            newButton.Style.Add("white-space", "normal");
            this.tblSpiele.Rows[0].Cells[2].Controls.Add(newButton);
            //Ergebnis1
            newButton = new Button();
            newButton.Text = "Ergebnis1";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblSpiele.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnErgebnis1";
            newButton.Style.Add("white-space", "normal");
            this.tblSpiele.Rows[0].Cells[3].Controls.Add(newButton);
            //Ergebnis2
            newButton = new Button();
            newButton.Text = "Ergebnis2";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblSpiele.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnErgebnis2";
            newButton.Style.Add("white-space", "normal");
            this.tblSpiele.Rows[0].Cells[4].Controls.Add(newButton);
            //Edit
            newButton = new Button();
            newButton.Text = "Edit";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblSpiele.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnEdit";
            newButton.Style.Add("white-space", "normal");
            this.tblSpiele.Rows[0].Cells[5].Controls.Add(newButton);
            //Delete
            newButton = new Button();
            newButton.Text = "Del";
            newButton.Width = Unit.Percentage(95);
            newButton.Height = Unit.Percentage(95);
            newButton.BackColor = this.tblSpiele.Rows[0].BackColor;
            newButton.BorderStyle = BorderStyle.None;
            newButton.ID = "btnDelete";
            newButton.Style.Add("white-space", "normal");
            this.tblSpiele.Rows[0].Cells[6].Controls.Add(newButton);
            #endregion

            if (this.Verwalter.SelectedTurnier != null)
            {
                if (this.Verwalter.SelectedTurnier is MannschaftsTurnier)
                {
                    foreach (Spiel spiel in this.Verwalter.SelectedTurnier.Get_Spiele())
                    {
                        if (spiel.Get_Spieltag() == this.Verwalter.SelectedTurnierSpieltag)
                        {
                            neueRow = new TableRow();
                            //ID
                            neueCell = new TableCell();
                            neueCell.Text = spiel.ID.ToString();
                            neueCell.HorizontalAlign = HorizontalAlign.Center;
                            neueRow.Cells.Add(neueCell);
                            //Mannschaft1
                            neueCell = new TableCell();
                            neueCell.Text = spiel.getMannschaftName1();
                            neueCell.HorizontalAlign = HorizontalAlign.Center;
                            neueRow.Cells.Add(neueCell);
                            //Mannschaft2
                            neueCell = new TableCell();
                            neueCell.Text = spiel.getMannschaftName2();
                            neueCell.HorizontalAlign = HorizontalAlign.Center;
                            neueRow.Cells.Add(neueCell);
                            //Ergebnis1
                            neueCell = new TableCell();
                            if (spiel.getErgebniswert1() == "-1")
                            {
                                neueCell.Text = "-";
                            }
                            else
                            {
                                neueCell.Text = spiel.getErgebniswert1();
                            }
                            neueCell.HorizontalAlign = HorizontalAlign.Center;
                            neueRow.Cells.Add(neueCell);
                            //Ergebnis2                        
                            neueCell = new TableCell();
                            if (spiel.getErgebniswert2() == "-1")
                            {
                                neueCell.Text = "-";
                            }
                            else
                            {
                                neueCell.Text = spiel.getErgebniswert2();
                            }
                            neueCell.HorizontalAlign = HorizontalAlign.Center;
                            neueRow.Cells.Add(neueCell);
                            //Stift
                            neueCell = new TableCell();
                            neueCell.HorizontalAlign = HorizontalAlign.Center;
                            ImageButton btnicon = new ImageButton();
                            index++;
                            btnicon.ID = "bearbspiel" + index;
                            btnicon.ImageUrl = "~/Images/stift.png";
                            btnicon.Click += Icon_Click;
                            neueCell.Controls.Add(btnicon);
                            neueRow.Cells.Add(neueCell);
                            //Mülleimer
                            neueCell = new TableCell();
                            neueCell.HorizontalAlign = HorizontalAlign.Center;
                            btnicon = new ImageButton();
                            btnicon.ID = "loeschspiel" + index;
                            btnicon.ImageUrl = "~/Images/muelleimer.png";
                            btnicon.Click += Icon_Click;
                            neueCell.Controls.Add(btnicon);
                            neueRow.Cells.Add(neueCell);
                            this.tblSpiele.Rows.Add(neueRow);
                        }
                        else
                        {

                        }
                    }
                    if (this.Verwalter.SelectedTurnier.Get_Spiele().Count < 1)
                    {
                        this.drplistSpieltag.Items.Add("-");
                    }
                    else
                    {
                        this.drplistSpieltag.Items.Clear();
                        for (index = 1; index <= this.Verwalter.SelectedTurnier.Get_MaxRunden(); index++)
                        {
                            this.drplistSpieltag.Items.Add(index.ToString());
                        }
                        this.drplistSpieltag.SelectedIndex = this.Verwalter.Get_SelectedSpieltagoderGruppe() - 1;
                    }
                }
                else if (this.Verwalter.SelectedTurnier is GruppenTurnier)
                {
                    foreach (Spiel spiel in this.Verwalter.SelectedTurnier.Get_Spiele())
                    {
                        if (spiel.getGruppe() == this.Verwalter.SelectedTurnierGruppe)
                        {
                            neueRow = new TableRow();
                            //ID
                            neueCell = new TableCell();
                            neueCell.Text = spiel.ID.ToString();
                            neueCell.HorizontalAlign = HorizontalAlign.Center;
                            neueRow.Cells.Add(neueCell);
                            //Teilnehmer1
                            neueCell = new TableCell();
                            neueCell.Text = spiel.getMannschaftName1();
                            neueCell.HorizontalAlign = HorizontalAlign.Center;
                            neueRow.Cells.Add(neueCell);
                            //Teilnehmer2
                            neueCell = new TableCell();
                            neueCell.Text = spiel.getMannschaftName2();
                            neueCell.HorizontalAlign = HorizontalAlign.Center;
                            neueRow.Cells.Add(neueCell);
                            //Ergebnis
                            neueCell = new TableCell();
                            if (spiel.getErgebniswert1() == "-1")
                            {
                                neueCell.Text = "-";
                            }
                            else
                            {
                                neueCell.Text = spiel.getErgebniswert1();
                            }
                            neueCell.HorizontalAlign = HorizontalAlign.Center;
                            neueRow.Cells.Add(neueCell);
                            //Ergebnis2                        
                            neueCell = new TableCell();
                            if (spiel.getErgebniswert2() == "-1")
                            {
                                neueCell.Text = "-";
                            }
                            else
                            {
                                neueCell.Text = spiel.getErgebniswert2();
                            }
                            neueCell.HorizontalAlign = HorizontalAlign.Center;
                            neueRow.Cells.Add(neueCell);
                            //Stift
                            neueCell = new TableCell();
                            neueCell.HorizontalAlign = HorizontalAlign.Center;
                            ImageButton btnicon = new ImageButton();
                            index++;
                            btnicon.ID = "bearbspiel" + index;
                            btnicon.ImageUrl = "~/Images/stift.png";
                            btnicon.Click += Icon_Click;
                            neueCell.Controls.Add(btnicon);
                            neueRow.Cells.Add(neueCell);
                            //Mülleimer
                            neueCell = new TableCell();
                            neueCell.HorizontalAlign = HorizontalAlign.Center;
                            btnicon = new ImageButton();
                            btnicon.ID = "loeschspiel" + index;
                            btnicon.ImageUrl = "~/Images/muelleimer.png";
                            btnicon.Click += Icon_Click;
                            neueCell.Controls.Add(btnicon);
                            neueRow.Cells.Add(neueCell);
                            this.tblSpiele.Rows.Add(neueRow);
                        }
                        else
                        {

                        }
                    }
                    if (((GruppenTurnier)this.Verwalter.SelectedTurnier).AnzahlGruppen < 1)
                    {
                        this.drplistSpieltag.Items.Add("-");
                    }
                    else
                    {
                        this.drplistSpieltag.Items.Clear();
                        for (index = 1; index <= ((GruppenTurnier)this.Verwalter.SelectedTurnier).AnzahlGruppen; index++)
                        {
                            this.drplistSpieltag.Items.Add(index.ToString());
                        }
                        this.drplistSpieltag.SelectedIndex = this.Verwalter.Get_SelectedSpieltagoderGruppe() - 1;
                    }
                }
                else
                { }
            }
            else
            { }
        }

        //Stift(Edit) oder Mülleimer(delete) angeclickt
        private void Icon_Click(object sender, ImageClickEventArgs e)
        {
            int index = -1;
            string id = ((ImageButton)sender).ID;
            int spielid = -1;

            if (this.Verwalter.AuthentifactionRole && this.Verwalter.UserAuthentificated)
            {
                if (id.Contains("loeschspiel"))
                {
                    index = Convert.ToInt32(id.Substring(11));
                    spielid = Convert.ToInt32(this.tblSpiele.Rows[index].Cells[0].Text);
                    this.Verwalter.DeleteSpielFromTurnier(spielid);
                    Response.Redirect(Request.RawUrl);
                }
                else if (id.Contains("bearbspiel"))
                {
                    index = Convert.ToInt32(id.Substring(10));
                    spielid = Convert.ToInt32(this.tblSpiele.Rows[index].Cells[0].Text);
                    Spiel Change = this.Verwalter.SelectedTurnier.getSpiel(spielid);
                    this.Verwalter.EditSpielID = spielid;
                    if (Change != null)
                    {
                        this.Verwalter.EditSpiel = true;
                        this.Teilnehmer1.Text = Change.getMannschaftName1();
                        this.Teilnehmer2.Text = Change.getMannschaftName2();
                        if (Change.getErgebniswert1() != "-1")
                        {
                            this.Ergebnis1.Text = Change.getErgebniswert1();
                        }
                        else
                        {
                            this.Ergebnis1.Text = "-";
                        }
                        if (Change.getErgebniswert2() != "-1")
                        {
                            this.Ergebnis2.Text = Change.getErgebniswert2();
                        }
                        else
                        {
                            this.Ergebnis2.Text = "-";
                        }
                        mpe3.Show();
                    }
                    else
                    { }
                }
                else
                { }
            }
            else
            { }
        }

        //Dropdownlist Turniere füllen
        private void LoadTurniere()
        {
            if (this.Verwalter.Turniere.Count > 0)
            {
                this.drpListTurniere.Items.Clear();
                this.drpListTurniere.Items.Add("wählen Sie ein Turnier aus!");
                foreach (Turnier turn in this.Verwalter.Turniere)
                {
                    this.drpListTurniere.Items.Add(turn.ID + ", " + turn.Bezeichnung + ", " + turn.getAnzahlTeilnehmer() + " " + turn.GetTypus());
                }
                this.drpListTurniere.SelectedIndex = this.Verwalter.SelectedTurnierIndex;
            }
            else
            { }
        }

        protected void btnTurnierAuswahl_Click(object sender, EventArgs e)
        {
            if (this.drpListTurniere.SelectedIndex > 0)
            {
                this.Session["selectedTurnier"] = this.Verwalter.Turniere[this.drpListTurniere.SelectedIndex - 1];
                this.Verwalter.SelectedTurnier = this.Verwalter.Turniere[this.drpListTurniere.SelectedIndex - 1];
                this.Verwalter.SelectedTurnierIndex = this.drpListTurniere.SelectedIndex;
                if (this.Verwalter.SelectedTurnier is MannschaftsTurnier)
                {
                    if (this.Verwalter.SelectedTurnier.Get_Spiele().Count > 0)
                    {
                        this.Verwalter.SelectedTurnierSpieltag = 1;
                        this.Verwalter.SelectedTurnierGruppe = 0;
                    }
                    else
                    {
                        this.Verwalter.SelectedTurnierSpieltag = 0;
                    }
                }
                else if (this.Verwalter.SelectedTurnier is GruppenTurnier)
                {
                    if (this.Verwalter.SelectedTurnier.getAnzahlTeilnehmer() > 0)
                    {
                        this.Verwalter.SelectedTurnierSpieltag = -1;
                        if (this.drplistSpieltag.SelectedValue != "-")
                        {
                            this.Verwalter.SelectedTurnierGruppe = Convert.ToInt32(this.drplistSpieltag.SelectedValue);
                            this.Verwalter.SelectedTurnier.setSelectedGruppe(this.Verwalter.SelectedTurnierGruppe);
                        }
                        else
                        {
                            this.Verwalter.SelectedTurnierGruppe = 1;
                            this.Verwalter.SelectedTurnier.setSelectedGruppe(1);
                        }
                    }
                    else
                    {
                        this.Verwalter.SelectedTurnierGruppe = 0;
                        this.Verwalter.SelectedTurnier.setSelectedGruppe(0);
                    }
                }
                else
                { }
            }
            else
            {

            }
            Response.Redirect(Request.RawUrl);
        }

        protected void btnNeu_Click(object sender, EventArgs e)
        {
            if (this.Verwalter.SelectedTurnier is MannschaftsTurnier)
            {
                this.drplstMannschaft1.Items.Clear();
                this.drplstMannschaft2.Items.Clear();

                foreach (Mannschaft man in this.Verwalter.SelectedTurnier.getTeilnemer())
                {
                    this.drplstMannschaft1.Items.Add(man.ID + ", " + man.Name);
                    this.drplstMannschaft2.Items.Add(man.ID + ", " + man.Name);
                }

                this.mpe.Show();
            }
            else if (this.Verwalter.SelectedTurnier is GruppenTurnier)
            {
                this.drplstGruppe1.Items.Clear();
                this.drplstGruppe2.Items.Clear();

                foreach (Person pers in ((Gruppe)this.Verwalter.SelectedTurnier.getTeilnemer()[this.Verwalter.SelectedTurnierSpieltag - 1]).Mitglieder)
                {
                    this.drplstGruppe1.Items.Add(pers.ID + ", " + pers.Name + ", " + pers.Vorname + ", " + pers.Geburtsdatum.ToShortDateString());
                    this.drplstGruppe2.Items.Add(pers.ID + ", " + pers.Name + ", " + pers.Vorname + ", " + pers.Geburtsdatum.ToShortDateString());
                }

                this.mpe2.Show();
            }
            else
            { }
        }

        protected void btnSpeichern_Click(object sender, EventArgs e)
        {
            int number = Convert.ToInt32(this.Request.Form["ctl00$MainContent$txtnumber"]);
            string mannschaft1 = this.Request.Form["ctl00$MainContent$drplstMannschaft1"];
            string mannschaft2 = this.Request.Form["ctl00$MainContent$drplstMannschaft2"];
            if (mannschaft1 != mannschaft2)
            {
                if (!this.Verwalter.EditSpiel)
                {
                    this.Verwalter.AddSpielToMannschaftsTurnier(number, mannschaft1, mannschaft2);
                }
                else
                {
                    Spiel Change = this.Verwalter.SelectedTurnier.getSpiel(this.Verwalter.EditSpielID);


                    this.Verwalter.ChangeSpielInTurnier(this.Verwalter.EditSpielID, Change.getMannschaftName1(), Change.getMannschaftName2(), Change.getErgebniswert1(), Change.getErgebniswert2());
                    this.Verwalter.EditSpiel = false;
                    this.Verwalter.EditSpielID = -1;
                }
            }
            else
            { }
            Response.Redirect(Request.RawUrl);
        }

        protected void btnSpeichern2_Click(object sender, EventArgs e)
        {
            string teilnehmer1 = this.Request.Form["ctl00$MainContent$drplstGruppe1"];
            string teilnehmer2 = this.Request.Form["ctl00$MainContent$drplstGruppe2"];
            if (teilnehmer1 != teilnehmer2)
            {
                if (!this.Verwalter.EditSpiel)
                {
                    this.Verwalter.AddSpielToGruppenTurnier(this.Verwalter.SelectedTurnierGruppe,
                                 teilnehmer1, teilnehmer2);
                }
                else
                {
                    Spiel Change = this.Verwalter.SelectedTurnier.getSpiel(this.Verwalter.EditSpielID);


                    this.Verwalter.ChangeSpielInTurnier(this.Verwalter.EditSpielID, Change.getMannschaftName1(), Change.getMannschaftName2(), Change.getErgebniswert1(), Change.getErgebniswert2());
                    this.Verwalter.EditSpiel = false;
                    this.Verwalter.EditSpielID = -1;
                }
            }
            else
            { }

            Response.Redirect(Request.RawUrl);
        }

        protected void btnSpeichern3_Click(object sender, EventArgs e)
        {
            int id = this.Verwalter.EditSpielID;
            string name1 = this.Teilnehmer1.Text;
            string name2 = this.Teilnehmer2.Text;
            string ergebnis1 = this.Ergebnis1.Text;
            string ergebnis2 = this.Ergebnis2.Text;

            this.Verwalter.ChangeSpielInTurnier(id, name1, name2, ergebnis1, ergebnis2);
            this.Verwalter.EditSpiel = false;
            this.Verwalter.EditSpielID = -1;
            Response.Redirect(Request.RawUrl);
        }

        protected void btnAutomatik_Click(object sender, EventArgs e)
        {
            bool hinundrueck = false;

            if (this.CheckBox1.Checked && !this.CheckBox2.Checked)
            {
                hinundrueck = true;
            }
            else if (this.CheckBox2.Checked && !this.CheckBox1.Checked)
            {
                hinundrueck = false;
            }
            else
            {
                hinundrueck = false;
            }

            this.Verwalter.SpieltageAutomatik(hinundrueck);

            //reload
            Response.Redirect(Request.RawUrl);
        }

        protected void btnspieltagauswaehlen_Click(object sender, EventArgs e)
        {
            if (this.drplistSpieltag.SelectedValue != "-")
            {
                this.Verwalter.Set_Spieltag_oder_Gruppe(Convert.ToInt32(this.drplistSpieltag.SelectedValue));
            }
            else
            { }

            Response.Redirect(Request.RawUrl);
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                CheckBox2.Checked = false;
                this.Verwalter.hinundrueck = true;
            }
            else
            {
                CheckBox2.Checked = true;
                this.Verwalter.hinundrueck = false;
            }
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox2.Checked)
            {
                CheckBox1.Checked = false;
                this.Verwalter.hinundrueck = false;
            }
            else
            {
                CheckBox1.Checked = true;
                this.Verwalter.hinundrueck = true;
            }
        }
    }
}