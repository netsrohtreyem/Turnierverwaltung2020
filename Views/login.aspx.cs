using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020.Views
{
    public partial class login : System.Web.UI.Page
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
            this.txtBenutzername.Focus();
            this.btnlogin.TabIndex = 1;
            if(Verwalter.UserAuthentificated)
            {
                this.lblBenutzername.Visible = false;
                this.txtBenutzername.Visible = false;
                this.lblPaswort.Visible = false;
                this.txtpasswd.Visible = false;

                this.btnlogin.Text = "logout";
            }
            else
            {

            }
            if(this.IsPostBack)
            {

            }
            else
            {
                if(Verwalter.DBFailed)
                {
                    this.lblStatus.Visible = true;
                    this.lblStatus.Text = "Datenbank nicht erreichbar";
                }
                else
                {
                    this.lblStatus.Visible = false;
                    this.lblStatus.Text = "";
                }
            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            if(this.btnlogin.Text == "login")
            {
                if(this.Verwalter.login(txtBenutzername.Text,txtpasswd.Text))
                {
                    this.lblStatus.Visible = false;
                    this.Response.Redirect(@"~\Default.aspx");
                }
                else
                {

                }                
            }
            else
            {
                if(this.Verwalter.logout())
                {
                    this.lblStatus.Visible = false;
                }
                else
                { }
            }
            Response.Redirect(Request.RawUrl);
        }
    }
}