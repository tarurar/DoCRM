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
    public partial class FormServiceList : System.Web.UI.Page
    {
        int PageNumber = 1;
        int RowsPerPage = 10;
        int RecordCount = 0;
        string PacientRef = "";
        UserData UserData;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserData = new UserData();
            if (Session["ServicesPageNumber"] == null)
            {
                Session["ServicesPageNumber"] = PageNumber;
                Session["ServicesRowsPerPage"] = RowsPerPage;
            }
            else
            {
                PageNumber = (int)Session["ServicesPageNumber"];
                RowsPerPage = (int)Session["ServicesRowsPerPage"];
            }
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
                ShowServiceListInGrid(UserData.UserRef);
            }
        }
        private void ShowServiceListInGrid(string UserRef)
        {
            tServiceDetailList dsOrderDetail = new tServiceDetailList(DetailList(UserRef));
            GridView1.DataSource = dsOrderDetail;
            GridView1.DataBind();
            PagerDraw(RecordCount, PageNumber, RowsPerPage);
        }
        private otServiceListRow[] DetailList(string UserRef)
        {
            wsSky wsd;
            wsd = new wsSky();
            otServiceListPage wssod;
            ServicePointManager.Expect100Continue = false;
            wsd.UnsafeAuthenticatedConnectionSharing = true;
            wsd.Credentials = UserData.NetCred;
            wsd.Timeout = 2000000;
            //int vStateID = (int)Session["OrdersStateFilter"];
            string Date1 = "";
            string Date2 = "";
            wssod = wsd.fServiceListPage(UserRef, PageNumber, RowsPerPage, PacientRef, Date1, Date2);
            RecordCount = wssod.prRecordCount;
            (Master.FindControl("lMasterTextTop") as Label).Text = wssod.prRespHeader.prRespMessage;
            //(Master.FindControl("lGridTextBottom") as Label).Text = wssod.prRespHeader.prRespInfo;
            otServiceListRow[] DetailList = wssod.prServiceListRows;
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
            panPager.Visible = (PageCount > 1);
            lRowCount.Text = Convert.ToString(RecordCount);
            lPageCount.Text = PageCount.ToString();
            lPageNumber.Text = PageNumber.ToString();
            linkPrew.Visible = (PageNumber != 1);
            linkNext.Visible = (PageNumber != PageCount);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string serref = ((GridView)sender).DataKeys[e.Row.RowIndex].Values[0].ToString();
                string pacref = ((GridView)sender).DataKeys[e.Row.RowIndex].Values[1].ToString();
                //e.Row.Attributes["onClick"] = "location.href='Default.aspx?id=" + abc + "'";
                e.Row.Attributes.Add("onclick", "location='ServicePage.aspx?serref=" + serref + "&pacref=" + pacref + "'");
            }
            //e.Row.Attributes.Add("onclick", "UsePacient()");
            //e.Row.Attributes.Add("onclick", "location='ServiceList.aspx'");

            //e.Row.Attributes.Add("onclick", "location='default.aspx?id=" + e.Row.Cells[0].Text + "'");
        }

        protected void linkNew_Click(object sender, EventArgs e)
        {
            Session["NewServiceMode"] = 1;
            Response.Redirect("ServicePage.aspx");
        }

        protected void linkDelete_Click(object sender, EventArgs e)
        {

        }

        protected void linkCommand1_Click(object sender, EventArgs e)
        {

        }

        protected void linkCommand2_Click(object sender, EventArgs e)
        {

        }

        protected void linkPrew_Click(object sender, EventArgs e)
        {
            PageNumber = PageNumber - 1;
            Session["ServicesPageNumber"] = PageNumber;
            ShowServiceListInGrid(UserData.UserRef);
        }

        protected void linkNext_Click(object sender, EventArgs e)
        {
            PageNumber = PageNumber + 1;
            Session["ServicesPageNumber"] = PageNumber;
            ShowServiceListInGrid(UserData.UserRef);
        }

        protected void linkRPP5_Click(object sender, EventArgs e)
        {
            RowsPerPage = 5;
            PageNumber = 1;
            Session["ServicesPageNumber"] = PageNumber;
            Session["ServicesRowsPerPage"] = RowsPerPage;
            ShowServiceListInGrid(UserData.UserRef);
        }

        protected void linkRPP10_Click(object sender, EventArgs e)
        {
            RowsPerPage = 10;
            PageNumber = 1;
            Session["ServicesPageNumber"] = PageNumber;
            Session["ServicesRowsPerPage"] = RowsPerPage;
            ShowServiceListInGrid(UserData.UserRef);
        }

        protected void linkRPP20_Click(object sender, EventArgs e)
        {
            RowsPerPage = 20;
            PageNumber = 1;
            Session["ServicesPageNumber"] = PageNumber;
            Session["ServicesRowsPerPage"] = RowsPerPage;
            ShowServiceListInGrid(UserData.UserRef);
        }

        protected void linkRPP50_Click(object sender, EventArgs e)
        {
            RowsPerPage = 50;
            PageNumber = 1;
            Session["ServicesPageNumber"] = PageNumber;
            Session["ServicesRowsPerPage"] = RowsPerPage;
            ShowServiceListInGrid(UserData.UserRef);
        }

    }
}
