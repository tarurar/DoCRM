using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DoCRM.wsSkyRef;
using System.Data;
using System.ComponentModel;

namespace DoCRM
{
    public partial class FormPacientList : System.Web.UI.Page
    {
        int PageNumber = 1;
        int RowsPerPage = 50;
        int RecordCount = 0;
        UserData UserData;
        Image sortImage = new Image();
        private string _sortDirection;

        public string SortDireaction
        {
            get
            {
                return ViewState["SortDireaction"] != null ? ViewState["SortDireaction"].ToString() : String.Empty;
            }
            set
            {
                ViewState["SortDireaction"] = value;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            UserData = new UserData();
            UserData.CheckSession();
            ShowListInGrid(UserData.UserRef);
        }
        private void ShowListInGrid(string UserRef)
        {
            tPacientList dsOrderDetail = new tPacientList(DetailList(UserRef));
            var orderDetailListSource = dsOrderDetail as IListSource;
            if (orderDetailListSource != null) 
            {
                var orderDetailList = orderDetailListSource.GetList().Cast<otPacientListRow>().ToList();
                GridView1.DataSource = orderDetailList.ToDataTable<otPacientListRow>(); ;
                GridView1.DataBind();
                PagerDraw(RecordCount, PageNumber, RowsPerPage);
            }
        }
        private otPacientListRow[] DetailList(string UserRef)
        {
            wsSky wsd;
            wsd = new wsSky();
            otPacientListPage wssod;
            ServicePointManager.Expect100Continue = false;
            wsd.UnsafeAuthenticatedConnectionSharing = true;
            wsd.Credentials = UserData.NetCred;
            wsd.Timeout = 2000000;
            //int vStateID = (int)Session["OrdersStateFilter"];
            int OrderField = 0;
            string SearchText = FilterTextBox.Text;
            int SearchField = 0;
            string Date1 = "";
            string Date2 = "";
            wssod = wsd.fPacientListPage(UserRef, PageNumber, RowsPerPage, OrderField, SearchText, SearchField, Date1, Date2);
            RecordCount = wssod.prRecordCount;
            (Master.FindControl("lMasterTextTop") as Label).Text = wssod.prRespHeader.prRespMessage;
            //(Master.FindControl("lGridTextBottom") as Label).Text = wssod.prRespHeader.prRespInfo;
            otPacientListRow[] DetailList = wssod.prPacientListRows;
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

        protected void linkUse_Click(object sender, EventArgs e)
        {

        }

        protected void linkNew_Click(object sender, EventArgs e)
        {
            Session["NewPacientMode"] = 1;
            Response.Redirect("PacientPage.aspx");
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
                string pacref = ((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString();
                //e.Row.Attributes["onClick"] = "location.href='Default.aspx?id=" + abc + "'";
                e.Row.Attributes.Add("onclick", "location='ServiceList.aspx?pacref=" + pacref + "'");
            }
            //e.Row.Attributes.Add("onclick", "UsePacient()");
            //e.Row.Attributes.Add("onclick", "location='ServiceList.aspx'");
            
            //e.Row.Attributes.Add("onclick", "location='default.aspx?id=" + e.Row.Cells[0].Text + "'");
        }

        protected void UsePacient()
        {
            Response.Redirect("ServiceList.aspx");
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            SetSortDirection(SortDireaction);
            var dt = GridView1.DataSource as DataTable;
            if (dt != null)
            {
                dt.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                SortDireaction = _sortDirection;
                int columnIndex = 0;
                foreach (DataControlFieldHeaderCell headerCell in GridView1.HeaderRow.Cells)
                {
                    if (headerCell.ContainingField.SortExpression == e.SortExpression)
                    {
                        columnIndex = GridView1.HeaderRow.Cells.GetCellIndex(headerCell);
                    }
                }

                GridView1.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
            }
        }

        protected void SetSortDirection(string sortDirection)
        {
            if (sortDirection == "ASC")
            {
                _sortDirection = "DESC";
                sortImage.ImageUrl = "~/Images/view_sort_ascending.png";

            }
            else
            {
                _sortDirection = "ASC";
                sortImage.ImageUrl = "~/Images/view_sort_descending.png";
            }
        }
    }
}