using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020.Views
{
    public partial class RankingAnzeige : System.Web.UI.Page
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


            if (this.IsPostBack)
            {
                if (this.drpListTurniere.SelectedIndex > 0)
                {
                    this.Verwalter.SelectedTurnierIndex = this.drpListTurniere.SelectedIndex;
                    this.Verwalter.SelectedTurnier = this.Verwalter.Turniere[this.Verwalter.SelectedTurnierIndex - 1];
                    if (this.Verwalter.SelectedTurnier is GruppenTurnier)
                    {
                        string drplisteintrag = this.Request.Form["ctl00$MainContent$drplistGruppen"];
                        if (drplisteintrag != null)
                        {
                            if (drplisteintrag != "keine Gruppen vorhanden")
                            {
                                int gruppenindex = Convert.ToInt32(drplisteintrag.Substring(0, drplisteintrag.IndexOf(",")));
                                if (this.Verwalter.SelectedTurnier is GruppenTurnier)
                                {
                                    this.Verwalter.SelectedTurnierGruppe = gruppenindex;
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
                    else
                    {
                        this.Verwalter.SelectedTurnierGruppe = 0;
                    }
                }
                else
                {
                    if (this.Verwalter.SelectedTurnier != null)
                    {
                        this.drpListTurniere.SelectedIndex = this.Verwalter.SelectedTurnierIndex;
                    }
                    else
                    { }
                }
            }
            else
            {
                LoadTurniere();
                if (this.Verwalter.SelectedTurnierIndex > 0)
                {
                    this.Verwalter.SelectedTurnier = this.Verwalter.Turniere[this.Verwalter.SelectedTurnierIndex - 1];
                    this.drpListTurniere.SelectedIndex = this.Verwalter.SelectedTurnierIndex;
                }
                else
                { }
            }
            if (this.Verwalter.SelectedTurnier != null)
            {
                this.lblTurniername.Visible = true;
                this.lblTurniername.Text = "Tabelle für das " + this.Verwalter.SelectedTurnier.Sportart.name + " - Turnier '" + this.Verwalter.SelectedTurnier.Bezeichnung + "'";
                this.tblTurnier.Visible = true;
                if (this.Verwalter.SelectedTurnier is GruppenTurnier)
                {
                    this.pnlGruppen.Visible = true;
                    int selectedgruppe = this.Verwalter.SelectedTurnierGruppe;
                    int anzahlGruppen = this.Verwalter.SelectedTurnier.getTeilnemer().Count;
                    if (anzahlGruppen > 0)
                    {
                        if (selectedgruppe <= 0)
                        {
                            selectedgruppe = this.Verwalter.SelectedTurnierGruppe = 1;
                        }
                        else
                        { }
                        this.lblTurniername.Text += (" , Gruppe '" +
                        this.Verwalter.SelectedTurnier.getGruppe(selectedgruppe - 1).Name + "', " +
                        selectedgruppe +
                        "(" + anzahlGruppen + ")");
                    }
                    else
                    {

                    }

                    LoadGruppen();
                }
                else
                {
                    this.pnlGruppen.Visible = false;
                    this.lblTurniername.Text += (" , mit " +
                        this.Verwalter.SelectedTurnier.getTeilnemer().Count + " Mannschaften");
                }
                LoadRanking();
            }
            else
            {
            }
        }

        private void LoadGruppen()
        {
            if (this.Verwalter.SelectedTurnier.getTeilnemer().Count > 0)
            {
                this.drplistGruppen.Items.Clear();
                foreach (Gruppe grp in this.Verwalter.SelectedTurnier.getTeilnemer())
                {
                    this.drplistGruppen.Items.Add(grp.ID + ", " + grp.Name);
                }
                this.drplistGruppen.SelectedIndex = this.Verwalter.SelectedTurnierGruppe - 1;
            }
            else
            {
                this.drplistGruppen.Items.Clear();
                this.drplistGruppen.Items.Add("keine Gruppen vorhanden");
            }
        }

        private void LoadTurniere()
        {
            if (this.Verwalter.Turniere.Count > 0)
            {
                this.drpListTurniere.Items.Clear();
                this.drpListTurniere.Items.Add("wählen Sie ein Turnier aus!");
                foreach (Turnier turn in this.Verwalter.Turniere)
                {
                    if (turn is MannschaftsTurnier)
                    {
                        this.drpListTurniere.Items.Add(turn.ID + ", " + turn.Bezeichnung + ", Mannschaftsturnier");
                    }
                    else if (turn is GruppenTurnier)
                    {
                        this.drpListTurniere.Items.Add(turn.ID + ", " + turn.Bezeichnung + ", Gruppenturnier");
                    }
                    else
                    {
                        //Fehler
                    }
                }
                this.drpListTurniere.SelectedIndex = this.Verwalter.SelectedTurnierIndex;
            }
            else
            { }
        }

        private void LoadRanking()
        {
            Ranking Tabelle = this.Verwalter.SelectedTurnier.GetRanking(this.Verwalter.SelectedTurnierGruppe);
            if (Tabelle != null)
            {
                foreach (string value in Tabelle.Titelzeile)
                {
                    TableCell neu = new TableCell();
                    neu.Text = value;
                    HeaderRow.Cells.Add(neu);
                }
                foreach (TableRow row in Tabelle.Zeilen)
                {
                    tblTurnier.Rows.Add(row);
                }
            }
            else
            { }
        }
    }
}