using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net;
using System.Data;
using System.ComponentModel;
using System.Configuration;
using System.Web.Security;
//using System.Web.SessionState.HttpSessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using DoCRM.wsSkyRef;


namespace DoCRM
{
    static class StringExtensions
    {
        public static string Reverse(this string input)
        {
            return new string(input.ToCharArray().Reverse().ToArray());
        }
    }
    static class ApplTools
    {
        public static string AnyActionDataValue(otAnyActionParam[] aap, string prName)
        {
            string res = "";
            for (int i = 0; i < aap.Length; ++i)
            {
                if (aap[i].prName == prName)
                {
                    res = aap[i].prValue;
                }
            }
            return res;
        }
    }


    //-- UserData --
    public class UserData
    {
        public int AdminUser;

        public bool Blocked;
        public string PacientLinkText;
        public string CompanyName;
        public string CurPacientRef;
        public bool IsGuest;
        public string LastEvent;
        public string LastEventDate;
        public int LastEventStyle;
        public string LoginText;
        public string SessionUID;
        public DateTime SessionDate;
        public string UploadPath;
        public string UserRef;
        public int UserID;
        public string UserName;
        private string UserRef1C;
        public NetworkCredential NetCred;
        private SqlConnection SQLConn;
        public UserData()
        {            
            string login_1c, pass_1c;
            UserRef = "";
            IsGuest = false;
            Blocked = false;
            SQLConn = null;            
            login_1c = System.Web.Configuration.WebConfigurationManager.AppSettings["login_1c"];
            pass_1c = System.Web.Configuration.WebConfigurationManager.AppSettings["pass_1c"];
            NetCred = new NetworkCredential(login_1c, pass_1c);
        }
        public wsSky NewWS()
        {
            wsSky wsd;
            wsd = new wsSky();

            ServicePointManager.Expect100Continue = false;
            wsd.UnsafeAuthenticatedConnectionSharing = true;
            wsd.Credentials = NetCred;
            wsd.Timeout = 2000000;

            return wsd;
        }
        public void DeleteSessionCookie()
        {
            if (HttpContext.Current.Request.Cookies["SessionUId"] != null)
            {
                string _SessionID = HttpContext.Current.Request.Cookies["SessionUId"].Value;

                HttpCookie SessionCookie = new HttpCookie("SessionUId", _SessionID);
                SessionCookie.Expires = DateTime.Now.AddYears(-10);
                HttpContext.Current.Response.SetCookie(SessionCookie);
            }
        }
        //private int GetUserParamBySessionUID(string SessionUId_)
        //{
        //    //по UserID находим UserRef и полeчаем все параметры пользователя в UserData из fShowUserInfo
        //    int UserIDSQL = -1;
        //    SqlCommand SQLProc = NewSQLProc("dbo.pr_GetUserParamBySessionUID");
        //    SQLProc.Parameters.Add("@SessionUID", SqlDbType.VarChar).Value = SessionUId_;
        //    SQLProc.Parameters.Add("@ResultVar", SqlDbType.Int).Value = -1;
        //    SQLConn.Open();
        //    SqlDataReader Reader = SQLProc.ExecuteReader();
        //    while (Reader.Read())
        //    {
        //        UserIDSQL = (int)Reader["UserID"];
        //        UserRef = (string)Reader["UserRefSQL"];
        //        SessionDate = (DateTime)Reader["CreateDate"];
        //        LoginText = (string)Reader["LoginText"];
        //        AdminUser = (int)Reader["AdminUser"];
        //        CurPacientRef = (string)Reader["CurPacientRef"];

        //    }
        //    Reader.Close();
        //    SQLConn.Close();
        //    return UserIDSQL;
        //}       //надо бы ее удалить
        public int GetUserInfo()
        {
            int _Result = -1;
            wsSky ws;
            ws = new wsSky();
            otShowUserInfo wsres;
            ServicePointManager.Expect100Continue = false;
            ws.UnsafeAuthenticatedConnectionSharing = true;
            ws.Credentials = NetCred;
            ws.Timeout = 2000000;
            wsres = ws.fShowUserInfo(UserRef, CurPacientRef);
            _Result = wsres.prRespHeader.prRespResult;
            if (_Result > 0)
            {
                CompanyName = wsres.prCompanyName;
                UserName = wsres.prUserName;
                IsGuest = (wsres.prIsGuest == 1);
                LastEvent = wsres.prLastEvent;
                LastEventDate = wsres.prLastEventDate;
                LastEventStyle = wsres.prLastEventStyle;
                PacientLinkText = wsres.prPacientLinkText;

                UploadPath = wsres.prUploadPath;
                if ((UploadPath.Length > 2) && (UploadPath.Reverse().Substring(1, 1) != "\\"))
                {
                    UploadPath = UploadPath + "\\";
                }

            }

            else
            {
                CompanyName = wsres.prRespHeader.prRespMessage;
                UserName = "";
            }
            return _Result;
        }
        public bool CheckSessionDB(string SessionUId_)
        {
            //по UserID находим UserRef и полeчаем все параметры пользователя в UserData из fShowUserInfo
            bool ResultVar = false;
            string ResultMessage = "";
            SqlCommand SQLProc = NewSQLProc("dbo.pr_GetUserParamBySessionUID");
            //SqlCommand SQLProc = NewSQLProc("online_db.dbo.pr_GetUserParamBySessionUID");
            SQLProc.Parameters.Add("@SessionUID", SqlDbType.VarChar).Value = SessionUId_;
            SQLProc.Parameters.Add("@ResultVar", SqlDbType.Int).Value = -1;
            SQLConn.Open();
            SqlDataReader Reader = SQLProc.ExecuteReader();
            while (Reader.Read())
            {
                ResultVar = ((int)Reader["ResultVar"] == 1);
                ResultMessage = (string)Reader["ResultMessage"];
                UserID = (int)Reader["UserID"];
                UserRef = (string)Reader["UserRefSQL"];
                IsGuest = ((int)Reader["IsGuest"] == 1);
                Blocked = ((int)Reader["UserID"] == 1);
                SessionDate = (DateTime)Reader["CreateDate"];
                LoginText = (string)Reader["LoginText"];
                AdminUser = (int)Reader["AdminUser"];
                CurPacientRef = (string)Reader["CurPacientRef"];

            }
            Reader.Close();
            SQLConn.Close();
            return ResultVar;
        }
        public void CheckSession()
        {
            CheckSession("");
        }

        public void CheckSession(string SUID)
        {
            string CookSUID;
            String LoginMsg;
            bool IsSUID = ((SUID != "") & (SUID != null));
            if (IsCookie())     //  есть ли кука у клиента в браузере
            {
                CookSUID = HttpContext.Current.Request.Cookies["SessionUId"].Value;
                if (CheckSessionDB(CookSUID))
                {       //  Кука есть в базе и она не заблокирована
                    if (IsGuest)
                    {
                        if (!CheckSessionGuest(CookSUID))
                        {
                            //Session["LoginMsg"] = "551";
                            //HttpContext.Current.Response.Redirect("login.aspx");

                            LoginMsg = "Code: 555";
                            HttpContext.Current.Response.Redirect("login.aspx?msg=" + LoginMsg);
                        }
                    }

                }
                else        //  Куки нет в базе или она заблокирована
                {   //  если куки нет в базе надо проверить IP-шник, и если с ним все в порядке можно что-то предпринять...
                    if (Blocked)
                    {
                        LoginMsg = "Code: 564";
                        HttpContext.Current.Response.Redirect("login.aspx?msg=" + LoginMsg);
                    }
                    else
                    {
                        if (IsSUID)
                        { LoginMsg = "Code: 570"; HttpContext.Current.Response.Redirect("login.aspx?msg=" + LoginMsg); }  ////HttpContext.Current.Response.Redirect("http://vizian.ru");
                        else
                        {
                            LoginMsg = "Code: 574";
                            HttpContext.Current.Response.Redirect("login.aspx?msg=" + LoginMsg);
                        }
                    }
                }
            }
            else   //   нет куки в браузере
            {
                if (IsSUID)
                {
                    if (CheckSessionGuest(SUID))
                        TryCreateSessionGuest(SUID);
                    else
                    {
                        LoginMsg = "Code:  588";
                        HttpContext.Current.Response.Redirect("login.aspx?msg=" + LoginMsg);
                    }
                }
                else
                {
                    LoginMsg = "Code: 594";
                    HttpContext.Current.Response.Redirect("login.aspx?msg=" + LoginMsg);
                }
            }

            if (UserID < 0)
            {
                {
                    LoginMsg = "Code: 602";
                    HttpContext.Current.Response.Redirect("login.aspx?msg=" + LoginMsg);
                }
            }
        }
        public bool CheckGuestSearchAllow()
        {
            bool ResultVar = false;
            string ResultMessage = "";
            string HostName = HttpContext.Current.Request.UserHostAddress;
            SqlCommand SQLProc = NewSQLProc("dbo.pr_CheckGuestSearchAllow");
            SQLProc.Parameters.Add("@HostName", SqlDbType.VarChar).Value = HostName;
            SQLProc.Parameters.Add("@ResultVar", SqlDbType.Int).Value = -1;
            SQLConn.Open();
            SqlDataReader Reader = SQLProc.ExecuteReader();
            while (Reader.Read())
            {
                ResultVar = ((int)Reader["ResultVar"] == 1);
                ResultMessage = (string)Reader["ResultMessage"];
            }
            Reader.Close();
            SQLConn.Close();
            return ResultVar;
        }
        public void RegQueryTextHost(string QueryText)
        {
            string HostName = HttpContext.Current.Request.UserHostAddress;
            string SQLText = "exec pr_RegQueryTextHost '" + HostName + "', '" + QueryText + "'";
            SqlCommand SQLProc = NewSQLComm(SQLText);
            SQLConn.Open();
            SQLProc.ExecuteNonQuery();
            SQLConn.Close();
        }
        //public bool CheckSessionF()
        //{
        //    if (!IsCookie())
        //    {
        //        return false;
        //    }

        //    SessionUID = HttpContext.Current.Request.Cookies["SessionUId"].Value;
        //    //в docSession ищем SessionID, по нему выбираем UserID
        //    UserID = GetUserParamBySessionUID(SessionUID);
        //    //если нет сесии то Redirect("login.aspx");
        //    if (UserID < 0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        public bool IsCookie()
        {
            return (!(HttpContext.Current.Request.Cookies["SessionUId"] == null));

        }
        public bool CheckSessionGuestSQL(string SUID, string HostName)
        {
            bool ResultVar = false;
            string ResultMessage = "";
            SqlCommand SQLProc = NewSQLProc("dbo.pr_CheckSessionGuest");
            SQLProc.Parameters.Add("@SessionUID", SqlDbType.VarChar).Value = SUID;
            SQLProc.Parameters.Add("@HostName", SqlDbType.VarChar).Value = HostName;
            SQLProc.Parameters.Add("@ResultVar", SqlDbType.Int).Value = -1;
            SQLConn.Open();
            SqlDataReader Reader = SQLProc.ExecuteReader();
            while (Reader.Read())
            {
                ResultVar = ((int)Reader["ResultVar"] == 1);
                ResultMessage = (string)Reader["ResultMessage"];
                UserID = (int)Reader["UserID"];
                UserRef = (string)Reader["UserRefSQL"];
                IsGuest = ((int)Reader["IsGuest"] == 1);
                Blocked = ((int)Reader["UserID"] == 1);
                SessionDate = (DateTime)Reader["CreateDate"];
                LoginText = (string)Reader["LoginText"];
                AdminUser = (int)Reader["AdminUser"];

            }
            Reader.Close();
            SQLConn.Close();
            return ResultVar;
        }

        public bool CheckSessionGuest(string SUID)
        {
            //Проверить таблицу сессий на наличие такого SUID.. если нет, то отправить на авторизацию
            //Если есть, то проверить параметры сессии, как на сайте... сравнить ip в sql с текущим ip
            string HostName;
            //if (HttpContext.Current.Request.Cookies["SessionUId"] == null)
            //{
            //    HttpContext.Current.Response.Redirect("login.aspx");
            //}            
            HostName = HttpContext.Current.Request.UserHostAddress;
            if (!CheckSessionGuestSQL(SUID, HostName))
            {
                return false;
            }
            if (UserID < 0)
            {
                return false;
            }
            return true;
        }
        private void CreateSessionCookie()
        {
            SessionUID = Encrypt(Guid.NewGuid().ToString());
            CreateSessionCookie(SessionUID, 1);
        }
        private void CreateSessionCookie(string SUID, int ProcID)
        {
            string HostName, BrowserName;
            HttpCookie SessionCookie = new HttpCookie("SessionUId", SUID);
            SessionCookie.Expires = DateTime.Now.AddMonths(3);
            HttpContext.Current.Response.SetCookie(SessionCookie);
            //вставить запись о куке а базу pr_InsertUserSession(UserID, SessionUID, HostName, BrowserName)
            //(если сессия на такого юзера есть, ее надо закрыть)
            HostName = HttpContext.Current.Request.UserHostAddress;
            BrowserName = HttpContext.Current.Request.UserAgent;
            InsertUserSession(UserID, SUID, HostName, BrowserName, ProcID);
        }
        public void TryCreateSessionGuest(string SUID)
        {
            CreateSessionCookie(SUID, 2);
        }
        private void InsertUserSession(int UserID, string SessionUID, string HostName, string BrowserName, int ProcID)
        {
            string BrowserName128 = BrowserName;//.Substring(1, 128);
            string ProcName = "";
            if (ProcID == 1)
                ProcName = "pr_InsertUserSession";
            if (ProcID == 2)
                ProcName = "spr_InsertUserSession";

            SqlCommand SQLProc = NewSQLProc("dbo." + ProcName);

            //SqlCommand SQLProc = NewSQLProc("online_db.dbo.pr_InsertUserSession");
            SQLProc.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserID;
            SQLProc.Parameters.Add("@SessionUID", SqlDbType.VarChar).Value = SessionUID;
            SQLProc.Parameters.Add("@HostName", SqlDbType.VarChar).Value = HostName;
            SQLProc.Parameters.Add("@BrowserName", SqlDbType.VarChar).Value = BrowserName128;
            SQLProc.Parameters.Add("@ResultVar", SqlDbType.Int).Value = -1;
            SQLConn.Open();
            SQLProc.ExecuteNonQuery();
            //if ((int)SqlCmd.Parameters["@return_value"].Value < 0)
            //{
            //    HttpContext.Current.Response.Redirect("Login.aspx");
            //}
            //else
            //{
            SQLConn.Close();
        }
        public bool IsTextDigit(string Text)
        {
            for (int i = 0; i < Text.Length; i++)
            {
                if (!Char.IsNumber(Text, i))
                {
                    return false;
                }
            }
            return true;
        }
        private static string Encrypt(string cleanString)
        {
            string Result;
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
            Result = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            clearBytes = null;
            hashedBytes = null;
            return Result;
        }
        public void CheckUser(string LoginText, string PasswordText, ref int RespResult, ref string RespMessage)
        {
            string UserRefSQL = "";
            //CheckUserSQL(LoginText, PasswordText, RespResult, RespMessage, UserRefSQL);
            CheckUserSQL(LoginText, PasswordText, ref RespResult, ref RespMessage, ref UserRefSQL);
            if (RespResult < 0)
            {
                DeleteSessionCookie();
                return;
            }
            if (CampareRef(LoginText, PasswordText, UserRefSQL) > 0)
            {
                CreateSessionCookie();
                UserRef = UserRefSQL;
            }
            else
            {
                HttpContext.Current.Response.Redirect("Error1.aspx");
            }
        }
        private void CreateSqlConnection()
        {
            string ConnStr;
            if (SQLConn == null)
            {
                ConnStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["db_Conn"].ConnectionString;
                SQLConn = new SqlConnection(ConnStr);
            }
        }
        private void CreateObjectSQL()
        {
            CreateSqlConnection();
        }
        private SqlCommand NewSQLComm(string CommText)
        {
            CreateObjectSQL();
            SqlCommand SqlCmd = new SqlCommand(CommText, SQLConn);
            return SqlCmd;
        }
        private SqlCommand NewSQLProc(string ProcName)
        {
            SqlCommand SQLProc = NewSQLComm(ProcName);
            SQLProc.CommandType = CommandType.StoredProcedure;
            return SQLProc;
        }
        public void RegSearchQuery(string QueryText)
        {
            string userIDStr = Convert.ToString(UserID);
            string SQLText = "exec pr_RegQueryText " + userIDStr + ", '" + QueryText + "'";
            SqlCommand SQLProc = NewSQLComm(SQLText);
            SQLConn.Open();
            SQLProc.ExecuteNonQuery();
            SQLConn.Close();
        }
        public static void SendFile(System.Web.UI.Page callingPage, string DownloadFileName, string ShortFileName)
        {
            //string tempFileName = Path.GetTempFileName();

            try
            {
                // Generate file using ExcelPackage
                //GenerateExcelDoc(tempFileName, data, worksheetTitle);

                callingPage.Response.AddHeader("Content-Disposition", "attachment;filename=" + ShortFileName);
                callingPage.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                callingPage.Response.AddHeader("Content-Length", new FileInfo(DownloadFileName).Length.ToString());
                callingPage.Response.TransmitFile(DownloadFileName);
                callingPage.Response.Flush();  //This is what I needed
            }
            finally
            {
                //if (File.Exists(DownloadFileName))
                //    File.Delete(DownloadFileName);
            }
        }
        public void SendFileA(System.Web.UI.Page CallingPage, string ShortFileName)
        {
            string FileName = UploadPath + ShortFileName;
            SendFile(CallingPage, FileName, ShortFileName);
        }
        //public bool GetFileFromWS(ref string ShortFileName, int FileType)
        //{
        //    ShortFileName = "";
        //    string FileRef = "";
        //    wsDoc wsObj;
        //    wsObj = new wsDoc();
        //    otGetFile wsResp;
        //    ServicePointManager.Expect100Continue = false;
        //    wsObj.UnsafeAuthenticatedConnectionSharing = true;
        //    wsObj.Credentials = NetCred;
        //    wsObj.Timeout = 2000000;
        //    wsResp = wsObj.fGetFile(UserRef, FileRef, FileType);
        //    ShortFileName = wsResp.prFileName;
        //    if ((wsResp.prRespHeader.prRespResult < 0) | (ShortFileName == ""))
        //        return false;
        //    else
        //        return true;
        //}
        //public void UploadFileWS(string FileName, int FileType)
        //{
        //    wsDoc wsObj;
        //    wsObj = new wsDoc();
        //    otUploadFile wsResp;
        //    ServicePointManager.Expect100Continue = false;
        //    wsObj.UnsafeAuthenticatedConnectionSharing = true;
        //    wsObj.Credentials = NetCred;
        //    wsObj.Timeout = 2000000;
        //    wsResp = wsObj.fUploadFile(UserRef, FileName, FileType);
        //}
        //public void WSAction(int ActionCode)
        //{
        //    wsDoc wsObj;
        //    wsObj = new wsDoc();
        //    otAction wsResp;
        //    ServicePointManager.Expect100Continue = false;
        //    wsObj.UnsafeAuthenticatedConnectionSharing = true;
        //    wsObj.Credentials = NetCred;
        //    wsObj.Timeout = 2000000;
        //    wsResp = wsObj.fAction(UserRef, ActionCode);

        //}
        public void CheckUserSQL(string LoginText, string PasswordText, ref int RespResult, ref string RespMessage, ref string UserRefSQL)
        {
            UserRefSQL = "";
            RespResult = -1;
            UserID = -1;
            SqlCommand SQLProc = NewSQLProc("dbo.pr_CheckLogin");
            //SqlCommand SQLProc = NewSQLProc("online_db.dbo.pr_CheckLogin");  
            SQLProc.Parameters.Add("@Login", SqlDbType.VarChar).Value = LoginText;
            SQLProc.Parameters.Add("@Password", SqlDbType.VarChar).Value = PasswordText;
            SQLProc.Parameters.Add("@ResultVar", SqlDbType.Int).Value = -1;
            //SqlCmd.Parameters.Add("@return_value", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            SQLConn.Open();
            //SqlCmd.ExecuteNonQuery();
            //if ((int)SqlCmd.Parameters["@return_value"].Value < 0)
            //{
            //    HttpContext.Current.Response.Redirect("Login.aspx");
            //}
            //else
            //{

            SqlDataReader Reader = SQLProc.ExecuteReader();
            while (Reader.Read())
            {
                RespResult = (int)Reader["ResultVar"];
                UserRefSQL = (string)Reader["UserRefSQL"];
                UserID = (int)Reader["UserID"];
            }
            Reader.Close();
            SQLConn.Close();
            if (RespResult < 0)
            {
                RespMessage = "Неверный логин или пароль";
            }
        }
        protected int CampareRef(string LoginText, string PasswordText, string UserRefSQL)
        {
            UserRef1C = "";
            int _Result;
            _Result = WSCallCheckUser(LoginText, PasswordText);
            if (_Result > 0)
            {
                if (UserRef1C != UserRefSQL)
                {
                    HttpContext.Current.Response.Redirect("Error2.aspx");
                }
            }
            return _Result;
        }
        protected int WSCallCheckUser(string LoginText, string PasswordText)
        {
            int _Result;
            wsSky ws;
            ws = new wsSky();
            otCheckUser wsres;
            ServicePointManager.Expect100Continue = false;
            ws.UnsafeAuthenticatedConnectionSharing = true;
            ws.Credentials = NetCred;
            ws.Timeout = 2000000;
            wsres = ws.fCheckUser(LoginText, PasswordText);
            _Result = wsres.prRespHeader.prRespResult;
            if (_Result > 0)
            {
                UserRef1C = wsres.prUserRef;
            }

            else
            {
                UserRef = "";
            }
            return _Result;
        }
        public void SetUserParam(int ParamID, string ParamValue)
        {
            string userIDStr = Convert.ToString(UserID);
            string SQLText = "exec pr_SetUserParam " + Convert.ToString(UserID) + ", " + Convert.ToString(ParamID) + ", '" + ParamValue + "'";
            SqlCommand SQLProc = NewSQLComm(SQLText);
            SQLConn.Open();
            SQLProc.ExecuteNonQuery();
            SQLConn.Close();
        }
        public static void StyleForRowByState(GridViewRow Row, int vStateID)
        {
            switch (vStateID)
            {
                case 10:
                    Row.BackColor = Color.FromArgb(230, 185, 184);  // PaleVioletRed;    //E6A0AA    230, 185, 184
                    //(string)(Row.Cells[0].Attributes["StateID"]) = "999";
                    break;
                case 20:
                    Row.BackColor = Color.FromArgb(219, 238, 243);  //Lavender;
                    Row.ForeColor = Color.FromArgb(255, 0, 0);  //Red
                    break;
                case 25:
                    Row.BackColor = Color.FromArgb(0, 255, 153);  //MediumSpringGreen;
                    break;
                case 30:
                    Row.BackColor = Color.FromArgb(219, 229, 241);  //Lavender;
                    break;
                case 40:
                    Row.BackColor = Color.FromArgb(255, 0, 0);  //Red;
                    break;
                case 50:
                    Row.BackColor = Color.FromArgb(204, 192, 218);  //Thistle;
                    break;
                case 60:
                    Row.BackColor = Color.FromArgb(153, 204, 255);  //SkyBlue;
                    break;
                case 65:
                    Row.BackColor = Color.FromArgb(255, 192, 0);  //Gold;
                    break;
                case 67:
                    Row.BackColor = Color.FromArgb(253, 233, 217);  //MistyRose;
                    break;
                case 70:
                    Row.BackColor = Color.FromArgb(255, 255, 0);  //Yellow;
                    Row.Font.Bold = true;
                    break;
                case 75:
                    Row.BackColor = Color.FromArgb(255, 255, 0);  //Yellow;
                    Row.ForeColor = Color.FromArgb(192, 0, 0);  //Brown
                    Row.Font.Bold = true;
                    break;
                case 80:
                    Row.BackColor = Color.FromArgb(255, 255, 0);  //Yellow;
                    break;
                case 83:
                    Row.BackColor = Color.FromArgb(0, 255, 153);  //MediumSpringGreen;
                    break;
                case 86:
                    Row.BackColor = Color.White;  //Yellow;
                    break;
                case 90:
                    Row.BackColor = Color.FromArgb(51, 204, 51);  //MediumSeaGreen;
                    break;
                case 100:
                    Row.BackColor = Color.FromArgb(191, 191, 191);  //Gainsboro;
                    break;
            }
        }
    }


    //-- otAnyParamList --
    [System.ComponentModel.DataObject]
    public class tAnyParamList : Component, IListSource
    {
        private otAnyActionParam[] ODList;
        public tAnyParamList() { }
        public tAnyParamList(otAnyActionParam[] ODList)
        {
            this.ODList = ODList;
        }
        public tAnyParamList(IContainer container)
        {
            container.Add(this);
        }
        #region IListSource Members
        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }
        System.Collections.IList IListSource.GetList()
        {
            BindingList<otAnyActionParam> BList = new BindingList<otAnyActionParam>();

            if (!this.DesignMode)
            {
                foreach (otAnyActionParam Row in ODList)
                {
                    BList.Add(Row);
                }
            }
            return BList;
        }
        #endregion
    }


    //-- otObjectList --
    [System.ComponentModel.DataObject]
    public class tObjectList : Component, IListSource
    {
        private otObjectRow[] ODList;
        public tObjectList() { }
        public tObjectList(otObjectRow[] ODList)
        {
            this.ODList = ODList;
        }
        public tObjectList(IContainer container)
        {
            container.Add(this);
        }
        #region IListSource Members
        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }
        System.Collections.IList IListSource.GetList()
        {
            BindingList<otObjectRow> BList = new BindingList<otObjectRow>();

            if (!this.DesignMode)
            {
                foreach (otObjectRow Row in ODList)
                {
                    BList.Add(Row);
                }
            }
            return BList;
        }
        #endregion
    }


    //-- otPacientList --
    [System.ComponentModel.DataObject]
    public class tPacientList : Component, IListSource
    {
        private otPacientListRow[] ODList;
        public tPacientList() { }
        public tPacientList(otPacientListRow[] ODList)
        {
            this.ODList = ODList;
        }
        public tPacientList(IContainer container)
        {
            container.Add(this);
        }

        #region IListSource Members
        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }
        System.Collections.IList IListSource.GetList()
        {
            BindingList<otPacientListRow> BList = new BindingList<otPacientListRow>();

            if (!this.DesignMode)
            {
                foreach (otPacientListRow Row in ODList)
                {
                    BList.Add(Row);
                }
            }
            return BList;
        }
        #endregion
    }
    //-- otServiceList --
    [System.ComponentModel.DataObject]
    public class tServiceDetailList : Component, IListSource
    {
        private otServiceListRow[] ODList;
        public tServiceDetailList() { }
        public tServiceDetailList(otServiceListRow[] ODList)
        {
            this.ODList = ODList;
        }
        public tServiceDetailList(IContainer container)
        {
            container.Add(this);
        }
        #region IListSource Members
        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }
        System.Collections.IList IListSource.GetList()
        {
            BindingList<otServiceListRow> BList = new BindingList<otServiceListRow>();

            if (!this.DesignMode)
            {
                foreach (otServiceListRow Row in ODList)
                {
                    BList.Add(Row);
                }
            }
            return BList;
        }
        #endregion
    }

    //-- otTemplateContent --
    [System.ComponentModel.DataObject]
    public class tTemplateContent : Component, IListSource
    {
        private otTemplParam[] ODList;
        public tTemplateContent() { }
        public tTemplateContent(otTemplParam[] ODList)
        {
            this.ODList = ODList;
        }
        public tTemplateContent(IContainer container)
        {
            container.Add(this);
        }
        #region IListSource Members
        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }
        System.Collections.IList IListSource.GetList()
        {
            BindingList<otTemplParam> BList = new BindingList<otTemplParam>();

            if (!this.DesignMode)
            {
                foreach (otTemplParam Row in ODList)
                {
                    BList.Add(Row);
                }
            }
            return BList;
        }
        #endregion
    }

    //-- otStringRow --
    public partial class otStringRow
    {

        private string prRowTextField;

        private string prRowValueField;

        /// <remarks/>
        public string prRowText
        {
            get
            {
                return this.prRowTextField;
            }
            set
            {
                this.prRowTextField = value;
            }
        }

        /// <remarks/>
        public string prRowValue
        {
            get
            {
                return this.prRowValueField;
            }
            set
            {
                this.prRowValueField = value;
            }
        }
    }

    [System.ComponentModel.DataObject]
    public class tStringRow : Component, IListSource
    {
        private otStringRow[] SDList;
        public tStringRow() { }
        public tStringRow(otStringRow[] SDList)
        {
            this.SDList = SDList;
        }
        public tStringRow(IContainer container)
        {
            container.Add(this);
        }
        #region IListSource Members
        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }
        System.Collections.IList IListSource.GetList()
        {
            BindingList<otStringRow> BList = new BindingList<otStringRow>();

            if (!this.DesignMode)
            {
                foreach (otStringRow Row in SDList)
                {
                    BList.Add(Row);
                }
            }
            return BList;
        }
        #endregion
    }


}