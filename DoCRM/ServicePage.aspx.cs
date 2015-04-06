using AjaxControlToolkit;
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
    public partial class ServicePage : System.Web.UI.Page
    {
        UserData UserData;
        tAnyParamList dsTemplates;
        string ServiceRef = "";
        string PacientRef = "";
        string FormMode = "";
        private List<TextBox> ParamValueList = new List<TextBox>();
        //delegate void tbDel_TextChanged(object sender, EventArgs e);
        protected void Page_Load(object sender, EventArgs e)
        {
            ServiceRef = Request.QueryString["serref"];
            if (ServiceRef == null)
            {
                ServiceRef = "";
            };
            UserData = new UserData();
            UserData.CheckSession();
            PacientRef = Request.QueryString["pacref"];
            if (PacientRef != null)
            {
                UserData.SetUserParam(2, PacientRef);
            }
            else
            {
                PacientRef = "";
            }
        }

        protected otServiceShow GetServiceWS()
        {
            if (UserData == null)
            {
                ServiceRef = Request.QueryString["serref"];
                if (ServiceRef == null)
                {
                    ServiceRef = "";
                };
                UserData = new UserData();
                UserData.CheckSession();
                PacientRef = Request.QueryString["pacref"];
                if (PacientRef != null)
                {
                    UserData.SetUserParam(2, PacientRef);
                }
                else
                {
                    PacientRef = "";
                }
                UserData = new UserData();
                UserData.CheckSession();
            }
            wsSky wsd = UserData.NewWS();
            otServiceShow wssod;
            wssod = wsd.fServiceShow(UserData.UserRef, ServiceRef);
            ServiceRef = wssod.prServiceRef;
            return wssod; 
        }
        private void ShowFormFromWS()
        {
            otServiceShow wssod = GetServiceWS();
            (Master.FindControl("lMasterTextTop") as Label).Text = wssod.prRespHeader.prRespMessage;
            CreateServiceHeader(wssod);
            CreateServiceBody(wssod);
        }
        protected override void CreateChildControls()
        {
            if (Session["NewServiceMode"] == null)
            {
                Session["NewServiceMode"] = 0;
            }
            if ((int)Session["NewServiceMode"] == 1)
            {
                hidformmode.Value = "mnew";
                ShowNewObjectForm();
            }
            else
            {
                cbTemplates.Visible = false;
                ShowFormFromWS();
            }
            Session["NewServiceMode"] = 0;
        }
        private void ShowNewObjectForm()
        {
            linkDelete.Visible = false;
            cbTemplates.Visible = true;
            UserData = new UserData();
            UserData.CheckSession();
            (Master.FindControl("lMasterTextTop") as Label).Text = "";
            FillTemplateList();
        }
        private otAnyActionData AnyActionDataForTemplateList()
        {
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 113;
            otAnyActionParam[] prAAP = new otAnyActionParam[1];
            prAAP[0] = new otAnyActionParam();
            prAAP[0].prName = "vServiceRef";
            prAAP[0].prValue = ServiceRef;
            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
        } 
        protected void FillTemplateList()
        {
            wsSky wsd = UserData.NewWS();
            otAnyActionResp wssod;
            otAnyActionData AnyActionData = AnyActionDataForTemplateList();
            wssod = wsd.fAnyAction(UserData.UserRef, AnyActionData);


            otAnyActionParam[] TemplateList = wssod.prAnyActionParams;
            dsTemplates = new tAnyParamList(TemplateList);


            cbTemplates.DataSource = dsTemplates;
            cbTemplates.DataTextField = "prName";
            cbTemplates.DataValueField = "prValue";
            cbTemplates.DataBind();   
     
            //NewRow = new otStringRow();
            //NewRow.prRowText = ReasonRef;                                      
            //NewRow.prRowValue
        }
        protected void TextBox_TextChanged(object sender, EventArgs e)
        {
            ParamValueList.Add((TextBox)sender);
        }
        protected void AddServiceValue(Table Tab, string prRef, int prDataType, string prCaption, string prValue)
        {
            ToolkitScriptManager SM;
            bool SMCreated = false;
            TableRow TR = new TableRow();
            Tab.Controls.Add(TR);
            TableCell TC = new TableCell();
            TR.Controls.Add(TC);
            TC.Text = prCaption;
            TC.CssClass = "obj_param_header";
            TC = new TableCell();
            TR.Controls.Add(TC);
            TextBox Box = new TextBox();
            Box.ID = "tb_sv_" + prRef;
            Box.CssClass = "str_input";
            Box.Text = prValue;
            Box.TextChanged += new EventHandler(TextBox_TextChanged);
            TC.Controls.Add(Box);                 
            switch (prDataType)
            {
                case 1:     //Число
                    Box.Width = 100;
                    break;
                case 2:     //Строка
                    Box.Width = 600;
                    break;
                case 3:     //Текст
                    Box.Width = 600;
                    Box.Height = 60;
                    Box.TextMode = TextBoxMode.MultiLine;
                    Box.CssClass = "text_input";
                    break;
                case 4:     //Булево
                    Box.Width = 100;
                    break;
                case 5:     //ДатаВремя
                    Box.Width = 200;
                    break;
                case 6:     //Дата
                    Box.Width = 100;
                    if (!SMCreated) {
                        SM = NewToolkitScriptManager();
                        TC.Controls.Add(SM);
                    }
                    MaskedEditExtender ME = NewDateMaskControl(Box.ID);
                    TC.Controls.Add(ME);
                    break;
                case 7:     //Время
                    Box.Width = 100;
                    break;
                default:
                    Box.Width = 600;                    
                    break;
            }

            //ParamValueList.Add(Box);

            //Наименование	Код
            //Число	1
            //Строка	2
            //Текст	3
            //Булево	4
            //ДатаВремя	5
            //Дата	6
            //Время	7

        }
        protected MaskedEditExtender NewDateMaskControl(string TargetID)
        { 
            string Mask = "99/99/9999";
            MaskedEditType MaskType = MaskedEditType.Date;
            MaskedEditExtender ME = NewMaskControl(TargetID, Mask, MaskType);
            return ME;
        }
        protected MaskedEditExtender NewMaskControl(string TargetID, string Mask, MaskedEditType MaskType)
        {
            MaskedEditExtender ME = new MaskedEditExtender();
            ME.TargetControlID = TargetID;
            ME.Mask = Mask;
            ME.MessageValidatorTip = true;
            ME.OnFocusCssClass = "MaskedEditFocus";
            ME.OnInvalidCssClass = "MaskedEditError";
            ME.MaskType = MaskType;  
            ME.InputDirection = MaskedEditInputDirection.RightToLeft;
            ME.AcceptNegative = MaskedEditShowSymbol.Left;
            ME.DisplayMoney = MaskedEditShowSymbol.None;
            ME.ErrorTooltipEnabled = true;
            return ME;
        }
        protected ToolkitScriptManager NewToolkitScriptManager()
        {
            ToolkitScriptManager SM = new ToolkitScriptManager();
            SM.ID = "ToolkitScriptManager1";
            SM.EnableScriptGlobalization = true;
            SM.EnableScriptLocalization = true;
            return SM;
        }

        protected void CreateServiceHeader(otServiceShow wssod)
        {
            AddServiceHeaderValue("", wssod.prDate + " (" + wssod.prServiceNum + ")", "1");
            //AddServiceHeaderValue("", wssod.prServiceNum);
            AddServiceHeaderValue("", wssod.prCaption, "2");
            AddServiceHeaderValue("", wssod.prUserName, "2");
        }
        protected void CreateServiceBody(otServiceShow wssod)
        {
            otParamValueShow[] pvs = wssod.prParamValuesShow;
            for (int i = 0; i < pvs.Length; ++i)
            {
                AddServiceBodyValue(pvs[i].prRef, Convert.ToInt16(pvs[i].prDataType), pvs[i].prCaption, pvs[i].prValue);                
            }
        }
        protected void AddServiceHeaderValue(string prCaption, string prValue, string vStyle)
        {
            Table Tab = tab_service_header;
            TableRow TR = new TableRow();
            Tab.Controls.Add(TR);
            TableCell TC = new TableCell();
            TR.Controls.Add(TC);
            TC.Text = prValue;
            TC.CssClass = "obj_header_value_" + vStyle;
            TC = new TableCell();
            TR.Controls.Add(TC);
            TC.Text = prCaption;
        }
        protected void AddServiceBodyValue(string prRef, int prDataType, string prCaption, string prValue)
        {
            Table Tab = tab_service_body;
            AddServiceValue(Tab, prRef, prDataType, prCaption, prValue);
        }
        protected void SaveService()
        {
            string vFormMode = hidformmode.Value;
            if (vFormMode == "mnew")
                SaveNewService();
            else
                SaveServiceParams();
        }
        protected void SaveNewService()
        {
            UserData = new UserData();
            UserData.CheckSession();
            wsSky wsd = UserData.NewWS();
            otAnyActionResp wssod;
            otAnyActionData AnyActionData = AnyActionDataForNewService();
            wssod = wsd.fAnyAction(UserData.UserRef, AnyActionData);
            //tb_SurName.Text = wssod.prAnyActionParams[0].prValue;
            otAnyActionParam[] aap = wssod.prAnyActionParams;
            string NewServiceRef = ApplTools.AnyActionDataValue(aap, "vServiceRef");
            Response.Redirect("ServicePage.aspx?serref=" + NewServiceRef);
        }
        private otAnyActionData AnyActionDataForNewService()
        {
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 107;
            otAnyActionParam[] prAAP = new otAnyActionParam[2];
            prAAP[0] = new otAnyActionParam();
            prAAP[0].prName = "vTemplateRef";
            prAAP[0].prValue = cbTemplates.SelectedValue;           
            prAAP[1] = new otAnyActionParam();
            prAAP[1].prName = "vPacientRef";
            prAAP[1].prValue = (Master.FindControl("hidpacref") as HiddenField).Value;
            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
        } 
        protected void SaveServiceParams()
        {
            otParamValueEdit[] PVE = ParseParamControls();
            UserData = new UserData();
            UserData.CheckSession();
            wsSky wsd = UserData.NewWS();
            otAnyActionResp wssod;
            otAnyActionParam[] vServiceHeaderParams = new otAnyActionParam[2];
            vServiceHeaderParams[0] = new otAnyActionParam();
            vServiceHeaderParams[0].prName = "vFormMode";
            vServiceHeaderParams[0].prValue = "1";
            vServiceHeaderParams[1] = new otAnyActionParam();
            vServiceHeaderParams[1].prName = "vServiceRef";
            vServiceHeaderParams[1].prValue = ServiceRef;

            wssod = wsd.fServiceEdit(UserData.UserRef, vServiceHeaderParams, PVE);
            
        }
     
        protected otParamValueEdit[] ParseParamControls()
        {
            int i = 0;
            otParamValueEdit[] PVE = new otParamValueEdit[ParamValueList.Count];
            foreach (TextBox tb in ParamValueList) 
            {
                PVE[i] = new otParamValueEdit();
                PVE[i].prEdited = 1;
                PVE[i].prRef = tb.ID.Substring(6, tb.ID.Length - 6);
                PVE[i].prValue = tb.Text;
                i = i + 1;
            }
            return PVE;
        }
        protected void ParseHiidenParamArray()
        {

            //int pos2 = 0;
            //List<string> pl;
            //pl = new List<string>();
            //string ch;
            //string cp;
            //string hv = hidprmarray.Value;
            //while (pos1 > 0)
            //{
            //    pos1 = hv.IndexOf(",", 0);
            //    if (pos1 > 0)
            //    {
            //        cp = hv.Substring(0, pos1);
            //        pl.Add(cp);
            //        hv.Substring(pos1, hv.Length - pos1);
            //    }
            //}
        }
  

        protected void linkNew_Click(object sender, EventArgs e)
        {
            Session["NewServiceMode"] = 1;
            Response.Redirect("ServicePage.aspx");
        }

        protected void linkSave_Click(object sender, EventArgs e)
        {
            SaveService();
        }

        protected void linkDelete_Click(object sender, EventArgs e)
        {

        }

        protected void linkPacientServices_Click(object sender, EventArgs e)
        {
            string pacref = (Master.FindControl("hidpacref") as HiddenField).Value;
            Response.Redirect("ServiceList.aspx?pacref=" + pacref);
        }

        protected void linkNewPacientService_Click(object sender, EventArgs e)
        {
            Session["NewServiceMode"] = 1;
            Response.Redirect("ServicePage.aspx");
        }


    }
}