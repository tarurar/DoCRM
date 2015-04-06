using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoCRM
{
    public partial class LoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String LoginMsg = Request.QueryString["msg"];
            if ((LoginMsg != null) & (LoginMsg != ""))
                lRespMessage.Text = LoginMsg;
            else
                lRespMessage.Text = "";


            //if (Session["LoginMsg"] != null)
            //    lRespMessage.Text = (string)Session["LoginMsg"];
            //else
            //    lRespMessage.Text = "";

        }

        protected void btEntry_Click(object sender, EventArgs e)
        {
            int RespResult = -1;
            string RespMessage = "";
            string LoginText = tbText1.Text;
            string PasswordText = tbText2.Text;

            UserData UserData = new UserData();
            UserData.CheckUser(LoginText, PasswordText, ref RespResult, ref RespMessage);
            if (RespResult < 0)
            {
                lRespMessage.Text = RespMessage;
                return;
            }
            else
            {
                lRespMessage.Text = "";
            }
            //tbText1.Text = UserData.UserRef;
            //UserData.CheckSession();
            //tbText2.Text = UserData.SessionID;
            Response.Redirect("ServiceList.aspx");

        }
    }
}