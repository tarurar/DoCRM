using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoCRM
{
    public partial class FormMaster : System.Web.UI.MasterPage
    {
        public UserData UserData;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserData = new UserData();
                UserData.CheckSession();
                UserData.GetUserInfo();
                //lLogoText.Text = UserData.LogoText;
                linkPacientPage.Visible = true;
                linkPacientPage.Text = UserData.PacientLinkText;
                hidpacref.Value = UserData.CurPacientRef;
                //linkPacientPage.OnClientClick = "location='PacientPage.aspx?pacref=" + UserData.CurPacientRef + "'";
                //linkExit.Visible = !UserData.IsGuest;
                linkExit.Visible = true;
                //lMasterTextTop.Text = "";
                //lClientName.Text = UserData.CompanyName
                //    + " (" + UserData.UserName + ")";
            }
        }

        protected void linkPacients_Click(object sender, EventArgs e)
        {
            Response.Redirect("PacientList.aspx");
        }

        protected void linkServices_Click(object sender, EventArgs e)
        {
            Response.Redirect("ServiceList.aspx");
        }

        protected void linkTemplates_Click(object sender, EventArgs e)
        {
            Response.Redirect("TemplateList.aspx");
        }

        protected void linkParams_Click(object sender, EventArgs e)
        {
            
        }

        protected void linkExit_Click(object sender, EventArgs e)
        {
            if (UserData == null)
            {
                UserData = new UserData();
                UserData.CheckSession();
            }

            if (UserData.IsGuest)
                Response.Redirect("http://vizian.ru");
            UserData.DeleteSessionCookie();
            Response.Redirect("Login.aspx");
        }

        protected void linkPacientPage_Click(object sender, EventArgs e)
        {
            if (hidpacref.Value != "") {
                Response.Redirect("PacientPage.aspx?pacref=" + hidpacref.Value);
            }
        }

        protected void linkCustomize_Click(object sender, EventArgs e)
        {

        }
    }
}
