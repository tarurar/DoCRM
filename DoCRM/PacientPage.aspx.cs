using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DoCRM.wsSkyRef;

namespace DoCRM
{
    public partial class PacientPageForm : System.Web.UI.Page
    {
        UserData UserData;
        string PacientRef = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            UserData = new UserData();
            UserData.CheckSession();
            if (!IsPostBack)
            {
                PacientRef = Request.QueryString["pacref"];
                if (PacientRef != null)
                {
                    UserData.SetUserParam(2, PacientRef);
                }
                else
                {
                    PacientRef = "";
                }
                if (Session["NewPacientMode"] == null)
                {
                    Session["NewPacientMode"] = 0;
                }
                if ((int)Session["NewPacientMode"] == 1) 
                {
                    hidformmode.Value = "mnew";
                    ShowNewPacientForm();
                }
                else
                {
                    ShowFormFromWS(UserData.UserRef);
                }
                Session["NewPacientMode"] = 0;
            }
        }
        private void ShowFormFromWS(string UserRef)
        {
            CallWS(UserRef);
            linkDelete.Visible = true;
            PagerDraw();
        }
        private otAnyActionData AnyActionDataForPacientPage()
        {
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 110;
            otAnyActionParam[] prAAP = new otAnyActionParam[1];
            prAAP[0] = new otAnyActionParam();
            prAAP[0].prName = "vPacientRef";
            prAAP[0].prValue = PacientRef;
            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
        }        
        private void CallWS(string UserRef)
        {
            wsSky wsd;
            wsd = new wsSky();
            otAnyActionResp wssod;
            ServicePointManager.Expect100Continue = false;
            wsd.UnsafeAuthenticatedConnectionSharing = true;
            wsd.Credentials = UserData.NetCred;
            wsd.Timeout = 2000000;
            otAnyActionData AnyActionData = AnyActionDataForPacientPage();
            wssod = wsd.fAnyAction(UserRef, AnyActionData);
            (Master.FindControl("lMasterTextTop") as Label).Text = wssod.prRespHeader.prRespMessage;
            //tb_SurName.Text = wssod.prAnyActionParams[0].prValue;
            otAnyActionParam[] aap = wssod.prAnyActionParams;
            tb_SurName.Text = ApplTools.AnyActionDataValue(aap, "vSurName");
            tb_PhoneNumber.Text = ApplTools.AnyActionDataValue(aap, "vPhoneNumber");
            tb_FirstName.Text = ApplTools.AnyActionDataValue(aap, "vFirstName");
            tb_EMail.Text = ApplTools.AnyActionDataValue(aap, "vEMail");
            tb_FatherName.Text = ApplTools.AnyActionDataValue(aap, "vFatherName");
            tb_CreateDate.Text = ApplTools.AnyActionDataValue(aap, "vCreateDate");
            tb_DateBirth.Text = ApplTools.AnyActionDataValue(aap, "vDateBirth");
            tb_CreateUser.Text = ApplTools.AnyActionDataValue(aap, "vCreateUser");
            tb_Sex.Text = ApplTools.AnyActionDataValue(aap, "vSex");
            tb_Source.Text = ApplTools.AnyActionDataValue(aap, "vSource");
            tb_Passport.Text = ApplTools.AnyActionDataValue(aap, "vPassport");
            tb_LastDate.Text = ApplTools.AnyActionDataValue(aap, "vLastDate");
            tb_CardCode.Text = ApplTools.AnyActionDataValue(aap, "vCardCode");
            tb_AddressReg.Text = ApplTools.AnyActionDataValue(aap, "vAddressReg");
            tb_AddressHome.Text = ApplTools.AnyActionDataValue(aap, "vAddressHome");
            tb_Comment.Text = ApplTools.AnyActionDataValue(aap, "vComment");
            tb_BloodGroup.Text = ApplTools.AnyActionDataValue(aap, "vBloodGroup");
            tb_BloodRezus.Text = ApplTools.AnyActionDataValue(aap, "vBloodRezus");            
        }
        private otAnyActionData AnyActionDataForSavePacient(string CurPacientRef)
        {
            string vFormMode = "";
            string vPacientRef = "";
            if (hidformmode.Value == "mnew")
            {
                vFormMode = "mnew";
            }
            else
            {
                vPacientRef = CurPacientRef;
            }
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 111;
            otAnyActionParam[] prAAP = new otAnyActionParam[12];
            FillAAP(prAAP, 0, "vPacientRef", vPacientRef);
            FillAAP(prAAP, 1, "vFormMode", vFormMode);
            FillAAP(prAAP, 2, "vSurName", tb_SurName.Text);
            FillAAP(prAAP, 3, "vFirstName", tb_FirstName.Text);
            FillAAP(prAAP, 4, "vFatherName", tb_FatherName.Text);
            FillAAP(prAAP, 5, "vDateBirth", tb_DateBirth.Text);
            FillAAP(prAAP, 6, "vComment", tb_Comment.Text);
            FillAAP(prAAP, 7, "vAddressReg", tb_AddressReg.Text);
            FillAAP(prAAP, 8, "vAddressHome", tb_AddressHome.Text);
            FillAAP(prAAP, 9, "vPhoneNumber", tb_PhoneNumber.Text);
            FillAAP(prAAP, 10, "vEMail", tb_EMail.Text);
            FillAAP(prAAP, 11, "vCardCode", tb_CardCode.Text);
            //FillAAP(prAAP, 7, "xxx", xxx.Text);
            //FillAAP(prAAP, 7, "xxx", xxx.Text);
            //FillAAP(prAAP, 7, "xxx", xxx.Text);
            //FillAAP(prAAP, 7, "xxx", xxx.Text);
            //FillAAP(prAAP, 7, "xxx", xxx.Text);
            
            //prAAP[0] = new otAnyActionParam();
            //prAAP[0].prName = "vPacientRef";
            //prAAP[0].prValue = PacientRef;
            //prAAP[1] = new otAnyActionParam();
            //prAAP[1].prName = "vFormMode";
            //prAAP[1].prValue = "mnew";
            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
        }  

        private void ShowNewPacientForm()
        {
            linkDelete.Visible = false;
            (Master.FindControl("lMasterTextTop") as Label).Text = "";
        }
        private void PagerDraw()
        {
            //panPager.Visible = (RecordCount > 0);
            //lRowCount.Text = Convert.ToString(RecordCount);
            //lPageCount.Text = PageCount.ToString();
            //lPageNumber.Text = PageNumber.ToString();
            //linkPrew.Visible = (PageNumber != 1);
            //linkNext.Visible = (PageNumber != PageCount);
        }
        private void FillAAP(otAnyActionParam[] prAAP, int i, string prName, string prValue)
        {
            prAAP[i] = new otAnyActionParam();
            prAAP[i].prName = prName;
            prAAP[i].prValue = prValue;
        }
   

        protected void SavePacient()
        {
            UserData = new UserData();
            UserData.CheckSession();            
            wsSky wsd;
            wsd = new wsSky();
            otAnyActionResp wssod;
            ServicePointManager.Expect100Continue = false;
            wsd.UnsafeAuthenticatedConnectionSharing = true;
            wsd.Credentials = UserData.NetCred;
            wsd.Timeout = 2000000;
            otAnyActionData AnyActionData = AnyActionDataForSavePacient(UserData.CurPacientRef);
            wssod = wsd.fAnyAction(UserData.UserRef, AnyActionData);
            if (wssod.prRespHeader.prRespResult < 0) {
                (Master.FindControl("lMasterTextTop") as Label).Text = wssod.prRespHeader.prRespMessage;
            }
            else {
                otAnyActionParam[] aap = wssod.prAnyActionParams;
                string pacref = ApplTools.AnyActionDataValue(aap, "vPacientRef");
                Response.Redirect("PacientPage.aspx?pacref=" + pacref);
            }

        }

        protected void linkPacientServices_Click(object sender, EventArgs e)
        {
            string pacref = (Master.FindControl("hidpacref") as HiddenField).Value;
            Response.Redirect("ServiceList.aspx?pacref=" + pacref);
        }

        protected void linkNew_Click(object sender, EventArgs e)
        {
            Session["NewPacientMode"] = 1;
            Response.Redirect("PacientPage.aspx");
        }

        protected void linkDelete_Click(object sender, EventArgs e)
        {

        }

        protected void linkSave_Click(object sender, EventArgs e)
        {
            SavePacient();
        }

        protected void linkNewPacientService_Click(object sender, EventArgs e)
        {
            Session["NewServiceMode"] = 1;
            Response.Redirect("ServicePage.aspx");
        }
    }
}