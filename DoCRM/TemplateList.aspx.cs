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
    public partial class FormTemplateList : System.Web.UI.Page
    {
        int PageNumber = 1;
        int RowsPerPage = 10;
        int RecordCount = 0;
        UserData UserData;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserData = new UserData();
            UserData.CheckSession();
            if (!IsPostBack)
            {
                ShowListInGrid(UserData.UserRef);
            }
        }
        private void ShowListInGrid(string UserRef)
        {
            //tPacientList dsGridDetail = new tPacientList(DetailList(UserRef));
            tAnyParamList dsGridDetail = new tAnyParamList(DetailList(UserRef));
            GridView1.DataSource = dsGridDetail;
            GridView1.DataBind();
            PagerDraw(RecordCount, PageNumber, RowsPerPage);
        }
        private otAnyActionParam[] DetailList(string UserRef)
        {
            wsSky wsd;
            wsd = new wsSky();
            otAnyActionResp wssod;
            ServicePointManager.Expect100Continue = false;
            wsd.UnsafeAuthenticatedConnectionSharing = true;
            wsd.Credentials = UserData.NetCred;
            wsd.Timeout = 2000000;
            //int vStateID = (int)Session["OrdersStateFilter"];
            otAnyActionData AnyActionData = AnyActionDataForTemplateList();
            wssod = wsd.fAnyAction(UserRef, AnyActionData);
            //***RecordCount = wssod.prRecordCount;
            (Master.FindControl("lMasterTextTop") as Label).Text = wssod.prRespHeader.prRespMessage;

            otAnyActionParam[] DetailList = wssod.prAnyActionParams;
            return DetailList;
        }
        private void PagerDraw(int RecordCount, int PageNumber, int RowPerPage)
        {
            int PageCount;
            int Inc = 0;
            double PageCountD;
            PageCountD = RecordCount / RowPerPage;
            if ((RowPerPage * PageCountD) != RecordCount)
            { Inc = 1; }
            PageCount = (int)Math.Floor(PageCountD) + Inc;
            //panPager.Visible = (RecordCount > 0);
            //lRowCount.Text = Convert.ToString(RecordCount);
            //lPageCount.Text = PageCount.ToString();
            //lPageNumber.Text = PageNumber.ToString();
            //linkPrew.Visible = (PageNumber != 1);
            //linkNext.Visible = (PageNumber != PageCount);
        }
        private otAnyActionData AnyActionDataForTemplateList()
        {
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 113;
            otAnyActionParam[] prAAP = new otAnyActionParam[1];
            prAAP[0] = new otAnyActionParam();
            prAAP[0].prName = "prName";
            prAAP[0].prValue = "prValue";
            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
        }        

        protected void linkUse_Click(object sender, EventArgs e)
        {

        }

        protected void linkNew_Click(object sender, EventArgs e)
        {
            Session["NewTemplateMode"] = 1;
            Response.Redirect("TemplatePage.aspx");
        }

        protected void linkOpen_Click(object sender, EventArgs e)
        {

        }

        protected void linkDelete_Click(object sender, EventArgs e)
        {

        }

        //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string CurPacientRef = GridView1.SelectedDataKey.Value.ToString();
        //    if (CurPacientRef != "-1")
        //    {
        //        Session["CurPacientRef"] = 1;
        //        UserData.SetUserParam(2, CurPacientRef);
        //    }
        //    Response.Redirect("ServiceList.aspx");
        //}

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string TemplateRef = ((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString();
                //e.Row.Attributes["onClick"] = "location.href='Default.aspx?id=" + abc + "'";
                e.Row.Attributes.Add("onclick", "location='TemplatePage.aspx?objref=" + TemplateRef + "'");
            }
            //e.Row.Attributes.Add("onclick", "UsePacient()");
            //e.Row.Attributes.Add("onclick", "location='ServiceList.aspx'");

            //e.Row.Attributes.Add("onclick", "location='default.aspx?id=" + e.Row.Cells[0].Text + "'");
        }

        protected void UsePacient()
        {
            Response.Redirect("ServiceList.aspx");
        }

        protected void linkParamList_Click(object sender, EventArgs e)
        {
            Response.Redirect("ParamList.aspx");
        }
    }
}