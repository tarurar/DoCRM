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
    public partial class ParamPage : System.Web.UI.Page
    {
        UserData UserData;
        tAnyParamList dsDataTypeCB;
        string ObjectRef = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ObjectRef = Request.QueryString["objref"];
            if (ObjectRef == null)
            {
                ObjectRef = "";
            };
            UserData = new UserData();
            UserData.CheckSession();
            if (!IsPostBack)
            {
                //ObjectRef = Request.QueryString["objref"];
                //if (ObjectRef == null)
                //{
                //    ObjectRef = "";
                //}
                if (Session["NewParamMode"] == null)
                {
                    Session["NewParamMode"] = 0;
                }
                if ((int)Session["NewParamMode"] == 1)
                {
                    hidformmode.Value = "mnew";
                    ShowNewObjectForm();
                }
                else
                {
                    ShowFormFromWS(UserData.UserRef);
                }
                Session["NewPacientMode"] = 0;
            }
        }

        private void ShowNewObjectForm()
        {
            linkDelete.Visible = false;
            (Master.FindControl("lMasterTextTop") as Label).Text = "";
        }

        private void ShowFormFromWS(string UserRef)
        {
            FillDataTypeList();
            CallWS(UserRef);
            linkDelete.Visible = true;
            PagerDraw();
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
        private otAnyActionData AnyActionDataForParamPage()
        {
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 126;
            otAnyActionParam[] prAAP = new otAnyActionParam[1];
            prAAP[0] = new otAnyActionParam();
            prAAP[0].prName = "vObjectRef";
            prAAP[0].prValue = ObjectRef;
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
            otAnyActionData AnyActionData = AnyActionDataForParamPage();
            wssod = wsd.fAnyAction(UserRef, AnyActionData);
            (Master.FindControl("lMasterTextTop") as Label).Text = wssod.prRespHeader.prRespMessage;
            //tb_SurName.Text = wssod.prAnyActionParams[0].prValue;
            otAnyActionParam[] aap = wssod.prAnyActionParams;
            tb_ParamName.Text = ApplTools.AnyActionDataValue(aap, "prName");
            tb_ParamCaption.Text = ApplTools.AnyActionDataValue(aap, "prCaption");
            cbDataTypeList.SelectedValue = ApplTools.AnyActionDataValue(aap, "prDateTypeRef");
            tb_ParamCreateUser.Text = ApplTools.AnyActionDataValue(aap, "prCreateUser");
            tb_ParamCreateDate.Text = ApplTools.AnyActionDataValue(aap, "prCreateDate");

        }
        protected void FillDataTypeList()
        {
            wsSky wsd = UserData.NewWS();
            otAnyActionResp wssod;
            otAnyActionData AnyActionData = AnyActionDataForDataTypeList();
            wssod = wsd.fAnyAction(UserData.UserRef, AnyActionData);

            otAnyActionParam[] DataTypeList = wssod.prAnyActionParams;
            dsDataTypeCB = new tAnyParamList(DataTypeList);

            cbDataTypeList.DataSource = dsDataTypeCB;
            cbDataTypeList.DataTextField = "prName";
            cbDataTypeList.DataValueField = "prValue";
            cbDataTypeList.DataBind();
        }
        private otAnyActionData AnyActionDataForDataTypeList()
        {
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 122;
            otAnyActionParam[] prAAP = new otAnyActionParam[1];
            prAAP[0] = new otAnyActionParam();
            prAAP[0].prName = "";
            prAAP[0].prValue = "";
            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
        }
        protected void SaveObject()
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
            otAnyActionData AnyActionData = AnyActionDataForSaveObject(ObjectRef);
            wssod = wsd.fAnyAction(UserData.UserRef, AnyActionData);
            if (wssod.prRespHeader.prRespResult < 0)
            {
                (Master.FindControl("lMasterTextTop") as Label).Text = wssod.prRespHeader.prRespMessage;
            }
            else
            {
                otAnyActionParam[] aap = wssod.prAnyActionParams;
                string objref = ApplTools.AnyActionDataValue(aap, "prObjectRef");
                Response.Redirect("ParamPage.aspx?objref=" + objref);
            }
        }
        private otAnyActionData AnyActionDataForSaveObject(string CurPacientRef)
        {
            string vFormMode = "";
            string vObjectRef = "";
            if (hidformmode.Value == "mnew")
            {
                vFormMode = "mnew";
            }
            else
            {
                vObjectRef = ObjectRef;
            }
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 127;
            otAnyActionParam[] prAAP = new otAnyActionParam[12];
            FillAAP(prAAP, 0, "prRef", vObjectRef);
            FillAAP(prAAP, 1, "vFormMode", vFormMode);
            FillAAP(prAAP, 2, "prName", tb_ParamName.Text);
            FillAAP(prAAP, 3, "prCaption", tb_ParamCaption.Text);
            FillAAP(prAAP, 4, "prDateTypeRef", cbDataTypeList.SelectedValue);
            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
        }
        private void FillAAP(otAnyActionParam[] prAAP, int i, string prName, string prValue)
        {
            prAAP[i] = new otAnyActionParam();
            prAAP[i].prName = prName;
            prAAP[i].prValue = prValue;
        }
        private void DeleteObject(string vRef)
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
            otAnyActionData AnyActionData = AnyActionDataForDeleteObject(ObjectRef);
            wssod = wsd.fAnyAction(UserData.UserRef, AnyActionData);
            if (wssod.prRespHeader.prRespResult < 0)
            {
                (Master.FindControl("lMasterTextTop") as Label).Text = wssod.prRespHeader.prRespMessage;
            }
            else Response.Redirect("ParamList.aspx");
        }
        private otAnyActionData AnyActionDataForDeleteObject(string CurObjectRef)
        {
            string vRef = "";
            vRef = CurObjectRef;
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 121;                                 //УдалитьОбъектWS
            otAnyActionParam[] prAAP = new otAnyActionParam[2];     //размерность массива !   

            FillAAP(prAAP, 0, "vRef", vRef);
            FillAAP(prAAP, 1, "vObjType", "2");

            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
        }

        protected void linkNew_Click(object sender, EventArgs e)
        {

        }

        protected void linkSave_Click(object sender, EventArgs e)
        {
            SaveObject();
        }

        protected void linkDelete_Click(object sender, EventArgs e)
        {
            DeleteObject(ObjectRef);
        }

        protected void linkParamList_Click(object sender, EventArgs e)
        {
            Response.Redirect("ParamList.aspx");
        }
    }
}