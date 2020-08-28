using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Turnierverwaltung2020.Views
{
    public partial class Verwaltung : System.Web.UI.Page
    {
        private Controller _Verwalter;

        public Controller Verwalter { get => _Verwalter; set => _Verwalter = value; }
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
            { }
        }

    }
}