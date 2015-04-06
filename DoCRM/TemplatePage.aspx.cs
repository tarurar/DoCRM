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
    public partial class TemplatePage : System.Web.UI.Page
    {
        UserData UserData;
        tAnyParamList dsParamsCB;
        tAnyParamList dsDataTypeCB;
        string ObjectRef = "";
        private List<TextBox> ParamList = new List<TextBox>();
        protected void Page_Load(object sender, EventArgs e)
        {
            ObjectRef = Request.QueryString["objref"];
            if (ObjectRef == null)
            {
                ObjectRef = "";
            };
            UserData = new UserData();
            UserData.CheckSession();
        }

        protected otTemplateShow GetServiceWS()
        {
            if (UserData == null)
            {
                ObjectRef = Request.QueryString["objref"];
                if (ObjectRef == null)
                {
                    ObjectRef = "";
                };
                UserData = new UserData();
                UserData.CheckSession();
                UserData = new UserData();
                UserData.CheckSession();
            }
            wsSky wsd = UserData.NewWS();
            otTemplateShow wssod;
            wssod = wsd.fTemplateShow(UserData.UserRef, ObjectRef);
            (Master.FindControl("lMasterTextTop") as Label).Text = wssod.prRespHeader.prRespMessage;
            ObjectRef = wssod.prTemplateRef;
            return wssod;
        }
        private void ShowFormFromWS()
        {
            otTemplateShow wssod = GetServiceWS();
            FillParamList();
            FillDataTypeList();
            CreateObjectHeader(wssod);
            CreateObjectContent(wssod);
        }


        protected void FillParamList()
        {
            wsSky wsd = UserData.NewWS();
            otAnyActionResp wssod;
            otAnyActionData AnyActionData = AnyActionDataForParamList();
            wssod = wsd.fAnyAction(UserData.UserRef, AnyActionData);

            otAnyActionParam[] ParamList = wssod.prAnyActionParams;
            dsParamsCB = new tAnyParamList(ParamList);

            cbParamList.DataSource = dsParamsCB;
            cbParamList.DataTextField = "prName";
            cbParamList.DataValueField = "prValue";
            cbParamList.DataBind();  
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
        private otAnyActionData AnyActionDataForParamList()
        {
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 116;
            otAnyActionParam[] prAAP = new otAnyActionParam[1];
            prAAP[0] = new otAnyActionParam();
            prAAP[0].prName = "";
            prAAP[0].prValue = "";
            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
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
        protected override void CreateChildControls()
        {
            if (Session["NewTemplateMode"] == null)
            {
                Session["NewTemplateMode"] = 0;
            }
            if ((int)Session["NewTemplateMode"] == 1)
            {
                hidformmode.Value = "mnew";
                ShowNewObjectForm();
            }
            else
            {
                //cbTemplates.Visible = false;
                ShowFormFromWS();
            }
            Session["NewTemplateMode"] = 0;
        }
        private void ShowNewObjectForm()
        {
            linkDelete.Visible = false;
            //cbTemplates.Visible = true;
            UserData = new UserData();
            UserData.CheckSession();
            (Master.FindControl("lMasterTextTop") as Label).Text = "";
            CreateObjectHeaderForNewObject();
            FillParamList();
            FillDataTypeList();
        }
        protected void CreateObjectHeaderForNewObject()
        {
            AddObjectHeaderValue("tmpl_name", "Наименование шаблона", "", false);
            AddObjectHeaderValue("tmpl_caption", "Заголовок", "", false);
            //AddObjectHeaderValue("tmpl_cr_date", "Дата создания", "", true);
            //AddObjectHeaderValue("tmpl_cr_user", "Создал", "", true);
        }
        protected void CreateObjectHeader(otTemplateShow wssod)
        {
            AddObjectHeaderValue("tmpl_name", "Наименование шаблона", wssod.prTemplateName, false);
            AddObjectHeaderValue("tmpl_caption", "Заголовок", wssod.prCaption, false);
            AddObjectHeaderValue("tmpl_cr_date", "Дата создания", wssod.prCreateDate, true);
            AddObjectHeaderValue("tmpl_cr_user", "Создал", wssod.prCreateUser, true);
        }
        protected void CreateObjectContent(otTemplateShow wssod)
        {
            Table Tab = tab_object_body;  
            AddObjectContentHeader(Tab, "Параметр", "Заголовок", "Тип данных", "Создал");
            otTemplParam[] pvs = wssod.prTemplParamList;
            for (int i = 0; i < pvs.Length; ++i)
            {
                AddObjectContentRow(Tab, pvs[i].prRowNum, pvs[i].prName, pvs[i].prCaption, pvs[i].prDataType, pvs[i].prCreateUser);
            }

        }
        protected void AddObjectHeaderValue(string prBoxID, string prCaption, string prValue, bool prReadOnly)
        {
            Table Tab = tab_object_header;
            TableRow TR = new TableRow();
            Tab.Controls.Add(TR);
            TableCell TC = new TableCell();
            TR.Controls.Add(TC);
            TC.Text = prCaption;

            if (prReadOnly)
            {
                TC = new TableCell();
                TR.Controls.Add(TC);
                TC.Text = prValue;
                TC.CssClass = "obj_atr_header";
            }
            else 
            {
                TC = new TableCell();
                TR.Controls.Add(TC);
                TextBox Box = new TextBox();
                Box.ID = "tb_" + prBoxID;
                Box.Text = prValue;
                Box.CssClass = "textEntry1";
                Box.Width = 600;
                Box.TextChanged += new EventHandler(TextBox_TextChanged);
                TC.Controls.Add(Box); 
            }

            //TC = new TableCell();
            //TR.Controls.Add(TC);
            //TC.Text = prValue;
        }
        protected void AddObjectContentRow(Table Tab, string prRowNum, string prName, string prCaption, string prDataType, string prCreateUser)
        {
            TableRow TR = new TableRow();
            Tab.Controls.Add(TR);
            string CurClass = "obj_param_header";

            TableCell TC = new TableCell();            
            TR.Controls.Add(TC);
            TC.Text = prName;
            TC.CssClass = CurClass;
            //DropDownList CB = new DropDownList();
            //CB.CssClass = "cbstyle";
            //CB.DataSource = dsParamsCB;
            //CB.DataTextField = "prName";
            //CB.DataValueField = "prValue";
            //CB.DataBind();
            //CB.SelectedValue = prRef;
            //TC.Controls.Add(CB);

            TC = new TableCell();
            TR.Controls.Add(TC);
            TC.Text = prCaption;
            TC.CssClass = CurClass;

            TC = new TableCell();
            TR.Controls.Add(TC);
            TC.Text = prDataType;
            TC.CssClass = CurClass;

            TC = new TableCell();
            TR.Controls.Add(TC);
            TC.Text = prCreateUser;
            TC.CssClass = CurClass;
            
            TC = new TableCell();
            TR.Controls.Add(TC);
            ImageButton IB = new ImageButton();
            IB.ImageUrl = "images/del_ico_1.ico";
            
            IB.Width = 12;
            IB.Width = 12;
            IB.ID = "ib_dp_" + prRowNum;
            IB.Click += new ImageClickEventHandler(ImageButton_Click);

            TC.Controls.Add(IB);

            //TC = new TableCell();
            //TR.Controls.Add(TC);
            //TextBox Box = new TextBox();
            //Box.ID = "tb_sv_" + prRef;
            //Box.Text = prCaption;
            //Box.CssClass = "textEntry1";
            //Box.Width = 600;
            //Box.TextChanged += new EventHandler(TextBox_TextChanged);
            //TC.Controls.Add(Box);

            //ParamValueList.Add(Box);
        }
        protected void AddObjectContentHeader(Table Tab, string Head1, string Head2, string Head3, string Head4)
        {
            TableRow TR = new TableRow();
            string CurClass = "tabcellheadstd";
            Tab.Controls.Add(TR);
            
            TableCell TC = new TableCell();
            TR.Controls.Add(TC);
            TC.Text = Head1;
            TC.CssClass = CurClass;
            TC.Width = 100;

            TC = new TableCell();
            TR.Controls.Add(TC);
            TC.Text = Head2;
            TC.CssClass = CurClass;
            TC.Width = 100;

            TC = new TableCell();
            TR.Controls.Add(TC);
            TC.Text = Head3;
            TC.CssClass = CurClass;
            TC.Width = 100;

            TC = new TableCell();
            TR.Controls.Add(TC);
            TC.Text = Head4;
            TC.CssClass = CurClass;
            TC.Width = 100;
            //ParamValueList.Add(Box);
        }
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            ParamList.Add((TextBox)sender);
        }
        protected void ImageButton_Click(object sender, EventArgs e)
        {
            ImageButton_Click_Action((ImageButton)sender);
        }
        protected void ImageButton_Click_Action(ImageButton IB)
        { 
            string ib_id = IB.ID;
            string prRef = ib_id.Substring(6, (ib_id.Length - 6));
            DeleteParamFromTemplate(prRef);
        }
        protected void DeleteParamFromTemplate(string ParamRef)
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
            otAnyActionData AnyActionData = AnyActionDataForDeleteParamFromTemplate(ObjectRef, ParamRef);
            wssod = wsd.fAnyAction(UserData.UserRef, AnyActionData);
            otAnyActionParam[] aap = wssod.prAnyActionParams;

            string objref = ApplTools.AnyActionDataValue(aap, "vRef");
            Response.Redirect("TemplatePage.aspx?objref=" + objref);
        }
        private otAnyActionData AnyActionDataForDeleteParamFromTemplate(string CurObjectRef, string ParamRef)
        {
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 120;                                 //УдалитьПараметрИзШаблона
            otAnyActionParam[] prAAP = new otAnyActionParam[2];     //размерность массива !   

            FillAAP(prAAP, 0, "vRef", CurObjectRef);
            FillAAP(prAAP, 1, "vParamRef", ParamRef);

            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
        }
        protected string SaveObject()
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
            otAnyActionData AnyActionData = AnyActionDataForSaveObjectAttrs(ObjectRef);
            wssod = wsd.fAnyAction(UserData.UserRef, AnyActionData);
            otAnyActionParam[] aap = wssod.prAnyActionParams;

            // - параметры пока сохраняются поштучно AnyActionData = AnyActionDataForSaveObjectContent(ObjectRef);
            //wssod = wsd.fAnyAction(UserData.UserRef, AnyActionData);
            //aap = wssod.prAnyActionParams;

            string objref = ApplTools.AnyActionDataValue(aap, "vRef");
            return objref;
        }
        protected void SaveObjectAndRedirect()
        {
            string objref = SaveObject();
            Response.Redirect("TemplatePage.aspx?objref=" + objref);
        }
        protected void AddParamToObject()
        {            
            if (hidformmode.Value == "mnew")
            {
                ObjectRef = SaveObject();
            }
                
            UserData = new UserData();
            UserData.CheckSession();
            wsSky wsd;
            wsd = new wsSky();
            otAnyActionResp wssod;
            ServicePointManager.Expect100Continue = false;
            wsd.UnsafeAuthenticatedConnectionSharing = true;
            wsd.Credentials = UserData.NetCred;
            wsd.Timeout = 2000000;
            otAnyActionData AnyActionData = AnyActionDataForAddParamToObject(ObjectRef);
            wssod = wsd.fAnyAction(UserData.UserRef, AnyActionData);
            otAnyActionParam[] aap = wssod.prAnyActionParams;

            string objref = ApplTools.AnyActionDataValue(aap, "vRef");
            Response.Redirect("TemplatePage.aspx?objref=" + objref);
        }
        protected void AddNewParamToObject()
        {            
            if (hidformmode.Value == "mnew")
            {
                ObjectRef = SaveObject();
            }
                
            UserData = new UserData();
            UserData.CheckSession();
            wsSky wsd;
            wsd = new wsSky();
            otAnyActionResp wssod;
            ServicePointManager.Expect100Continue = false;
            wsd.UnsafeAuthenticatedConnectionSharing = true;
            wsd.Credentials = UserData.NetCred;
            wsd.Timeout = 2000000;
            otAnyActionData AnyActionData = AnyActionDataForAddNewParamToObject(ObjectRef);
            wssod = wsd.fAnyAction(UserData.UserRef, AnyActionData);
            otAnyActionParam[] aap = wssod.prAnyActionParams;

            string objref = ApplTools.AnyActionDataValue(aap, "vRef");
            Response.Redirect("TemplatePage.aspx?objref=" + objref);
        }
        private otAnyActionData AnyActionDataForSaveObjectAttrs(string CurObjectRef)
        {
            string vFormMode = "";
            string vRef = "";
            if (hidformmode.Value == "mnew")
            {
                vFormMode = "mnew";
            }
            else
            {
                vRef = CurObjectRef;
            }
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 117;                                 //СохранитьРеквизитыШаблонаWS
            otAnyActionParam[] prAAP = new otAnyActionParam[4];     //размерность массива !   

            FillAAP(prAAP, 0, "vRef", vRef);
            FillAAP(prAAP, 1, "vFormMode", vFormMode);
            FillAAP(prAAP, 2, "vName", (FindControlRec(this, "tb_tmpl_name") as TextBox).Text);
            FillAAP(prAAP, 3, "vCaption", (FindControlRec(this, "tb_tmpl_caption") as TextBox).Text);

            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
        }
        private otAnyActionData AnyActionDataForAddParamToObject(string CurObjectRef)
        {
            string vRef = "";
            vRef = CurObjectRef;
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 118;                                 //ДобавитьПараметрВШаблонWS
            otAnyActionParam[] prAAP = new otAnyActionParam[2];     //размерность массива !   

            FillAAP(prAAP, 0, "vRef", vRef);
            FillAAP(prAAP, 1, "vParamRef", cbParamList.SelectedValue);

            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
        }
        private otAnyActionData AnyActionDataForAddNewParamToObject(string CurObjectRef)
        {
            string vRef = "";
            vRef = CurObjectRef;
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 123;                                 //ДобавитьПараметрВШаблонWS
            otAnyActionParam[] prAAP = new otAnyActionParam[4];     //размерность массива !   

            FillAAP(prAAP, 0, "vRef", vRef);
            FillAAP(prAAP, 1, "vParamDataType", cbDataTypeList.SelectedValue);
            FillAAP(prAAP, 2, "vParamName", tb_NewParamName.Text);
            FillAAP(prAAP, 3, "vParamCaption", tb_NewParamCaption.Text);

            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
        }
        
        private otAnyActionData AnyActionDataForDeleteObject(string CurObjectRef)
        {
            string vRef = "";
            vRef = CurObjectRef;
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 121;                                 //УдалитьОбъектWS
            otAnyActionParam[] prAAP = new otAnyActionParam[2];     //размерность массива !   

            FillAAP(prAAP, 0, "vRef", vRef);
            FillAAP(prAAP, 1, "vObjType", "1");

            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
        }
        private Control FindControlRec(Control rootControl, string id)
        {
            foreach (Control ctl in rootControl.Controls)
            {
                if (ctl.ID == id)
                {
                    return ctl;
                }
                else
                {
                    Control subCtl = FindControlRec(ctl, id);
                    if (subCtl != null)
                    {
                        return subCtl;
                    }
                }
            }
            return null;
        }
        private void FillAAP(otAnyActionParam[] prAAP, int i, string prName, string prValue)
        {
            prAAP[i] = new otAnyActionParam();
            prAAP[i].prName = prName;
            prAAP[i].prValue = prValue;
        }
        private otAnyActionData AnyActionDataForSaveObjectContent(string CurObjectRef)
        {
            string vFormMode = "";
            string vRef = "";
            //if (hidformmode.Value == "mnew")
            //{
            //    vFormMode = "mnew";
            //}
            //else
            //{
            //    vRef = CurObjectRef;
            //}
            vRef = CurObjectRef;
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 117;                                 //СохранитьРеквизитыШаблонаWS
            otAnyActionParam[] prAAP = new otAnyActionParam[4];     //размерность массива !   

            //Table tab1 = FindControl("table_main") as Table;
            //Table tab2 = FindControl("MainContent_tab_object_header") as Table;
            FillAAP(prAAP, 0, "vRef", vRef);
            FillAAP(prAAP, 1, "vFormMode", vFormMode);
            FillAAP(prAAP, 2, "vName", (FindControlRec(this, "tb_tmpl_name") as TextBox).Text);
            FillAAP(prAAP, 3, "vCaption", (FindControlRec(this, "tb_tmpl_caption") as TextBox).Text);

            //"actAdd"
            //"actDel"

            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
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
            else Response.Redirect("TemplateList.aspx");
        }
        protected void linkNew_Click(object sender, EventArgs e)
        {
            Session["NewTemplateMode"] = 1;
            Response.Redirect("TemplatePage.aspx");
        }

        protected void linkSave_Click(object sender, EventArgs e)
        {
            SaveObjectAndRedirect();
        }

        protected void linkDelete_Click(object sender, EventArgs e)
        {
            DeleteObject(ObjectRef);
        }
        protected void linkNewParam_Click(object sender, EventArgs e)
        {
            AddNewParamToObject();
        }
        protected void linkAddParam_Click(object sender, EventArgs e)
        {
            AddParamToObject();
        }

        protected void linkCommand1_Click(object sender, EventArgs e)
        {

        }

        protected void linkParamList_Click(object sender, EventArgs e)
        {
            Response.Redirect("ParamList.aspx");
        }


    }
}