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
    public partial class FormParamList : System.Web.UI.Page
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

            tObjectList dsGridDetail = new tObjectList(DetailList(UserRef));
            GridView1.DataSource = dsGridDetail;
            GridView1.DataBind();
            PagerDraw(RecordCount, PageNumber, RowsPerPage);
        }
        private otObjectRow[] DetailList(string UserRef)
        {
            wsSky wsd;
            wsd = new wsSky();
            otObjectList wssod;
            ServicePointManager.Expect100Continue = false;
            wsd.UnsafeAuthenticatedConnectionSharing = true;
            wsd.Credentials = UserData.NetCred;
            wsd.Timeout = 2000000;
            //int vStateID = (int)Session["OrdersStateFilter"];
            otAnyActionData AnyActionData = AnyActionDataForObjectList();
            wssod = wsd.fObjectListPage(UserRef, AnyActionData);
            //***RecordCount = wssod.prRecordCount;
            (Master.FindControl("lMasterTextTop") as Label).Text = wssod.prRespHeader.prRespMessage;

            otObjectRow[] DetailList = wssod.prObjectRows;
            return DetailList;
        }
        private otAnyActionData AnyActionDataForObjectList()
        {
            otAnyActionData XDTO_Root = new otAnyActionData();
            XDTO_Root.prWSID = 125;                                 //ВыводСтраницыСписокПараметров
            otAnyActionParam[] prAAP = new otAnyActionParam[2];     //!!! размер массива

            FillAAP(prAAP, 0, "vPageNum", "1");
            FillAAP(prAAP, 1, "vRowsPerPage", "20");
            XDTO_Root.prAnyActionParams = prAAP;
            return XDTO_Root;
        }
        private void FillAAP(otAnyActionParam[] prAAP, int i, string prName, string prValue)
        {
            prAAP[i] = new otAnyActionParam();
            prAAP[i].prName = prName;
            prAAP[i].prValue = prValue;
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
                string ParamRef = ((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString();
                //e.Row.Attributes["onClick"] = "location.href='Default.aspx?id=" + abc + "'";
                e.Row.Attributes.Add("onclick", "location='ParamPage.aspx?objref=" + ParamRef + "'");
            }
            //e.Row.Attributes.Add("onclick", "UsePacient()");
            //e.Row.Attributes.Add("onclick", "location='ServiceList.aspx'");

            //e.Row.Attributes.Add("onclick", "location='default.aspx?id=" + e.Row.Cells[0].Text + "'");
        }



        protected void linkNew_Click(object sender, EventArgs e)
        {

        }

        protected void linkPrew_Click(object sender, EventArgs e)
        {

        }

        protected void linkNext_Click(object sender, EventArgs e)
        {

        }

        protected void linkRPP5_Click(object sender, EventArgs e)
        {

        }

        protected void linkRPP10_Click(object sender, EventArgs e)
        {

        }

        protected void linkRPP20_Click(object sender, EventArgs e)
        {

        }

        protected void linkRPP50_Click(object sender, EventArgs e)
        {

        }
    }
}