using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sucarcane;

namespace ReplicaAX
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        clsPostges mm = new clsPostges();
        public string[] ColumnName = { "truckoil", "vehicle_code", "transdate", "oilamoun", "uid",
                                     "approvestatus", "approver"};
        public string[] ColumnType = { "string", "string", "string", "string", "string",
                                     "string", "string"};
        public string[] ColumnTitle = { "truckoil", "vehicle_code", "transdate", "oilamoun", "uid", "approvestatus", "approver", "Save/Edit" };
        string[] ColumnWidth = { "5%", "10%", "15%", "25%", "15%", "20%", "20%", "10%" };
        string[] ColumnAlign = { "center", "left", "left", "left", "left", "left", "center", "center" };
        public static int icolumnVisible = 6;
        DataView dv = new DataView();
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    DataSet dsDetail;

        //    clsMasterData cm = new clsMasterData();
        //    if (!IsPostBack)
        //    {
        //        initialDropdownList();
        //        initialDataGrid();
        //        dsDetail = new DataSet();
        //        //dsDetail = cm.getDataSucarcane();
        //        //docToUI(dsDetail);
        //    }
        //}

        protected void btnSeacrch_Click(object sender, EventArgs e)
        {

            string strItem = ddlCropYear.SelectedItem.ToString();
            //String txtQuery = @"select my_location_id, my_location_name from farmarea where  dataareaid='104' and crop_year ='"+ strItem + "' limit 10 ";
            String txtQuery = " SELECT truckoil, vehicle_code, transdate::date,oilamoun,uid,approvestatus,approver"
                            + " FROM public.sugarcanetransoil where dataareaid = '102'"
                            + " and public.sugarcanetransoil.dimension ='" + strItem + "'"
                            //+ " and public.sugarcanetransoil.approvestatus = 0"
                            //+ " and public.sugarcanetransoil.approvestatus = 0"
                            //+ " and public.sugarcanetransoil.truckoil = 'O0001'"
                            //+ " and public.sugarcanetransoil.finish_addfuel = 1"
                            + " ORDER BY UID"
                            ;
            DataTable dtSugarcane = mm.SelectDataFromPostgreSQL(txtQuery, mm.strPostgreSQLConnection);
            GridView1.DataSource = dtSugarcane;
            GridView1.DataBind();
        }
        public void initialDropdownList()
        {

            //string strSQL = string.Empty;
            //strSQL = "select * from Year";
            //clsPostges mm = new clsPostges();

            //DataTable dt;
            //dt = mm.SelectDataFromPostgreSQL(strSQL, "");
            //ddlCropYear.DataSource = dt;
            //ddlCropYear.DataTextField = "ityp_namee";
            //ddlCropYear.DataValueField = "ityp_iddesc";
            //ddlCropYear.DataBind();
            //ddlCropYear.Items.Insert(0, new ListItem(" -- เลือก -- ", ""));


            ddlCropYear.Items.Insert(0, new ListItem("65/66", "65/66"));
            ddlCropYear.Items.Insert(0, new ListItem("66/67", "66/67"));
            ddlCropYear.Items.Insert(0, new ListItem("67/68", "67/68"));

        }

        public void initialDataGrid()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            clsMasterUtil cmUtil = new clsMasterUtil();
            int iPageSize = 0;
            dt = cmUtil.AddColumnToDataTable(dt, ColumnType, ColumnName);
            ds.Tables.Add(dt);
            ViewState.Add("DSDetailTable", ds);
            ShowDataDetail();
            GridViewUtil.setGridStyle(dtgData, ColumnWidth, ColumnAlign, ColumnTitle);
            try
            {
                iPageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pageSize"].ToString());
            }
            catch (Exception e)
            {
                iPageSize = 10;
            }
            dtgData.PageSize = iPageSize;


        }
        protected void btnAddDetail_Click(object sender, EventArgs e)
        {
            int d = dtgData.EditItemIndex;
            DataSet ds = new DataSet();
            litJavaScript.Text = "";
            if (saveDetail())
            {
                if (ViewState["DSDetailTable"] != null)
                    ds = (DataSet)ViewState["DSDetailTable"];
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                dtgData.EditItemIndex = ds.Tables[0].Rows.Count - 1;
                ViewState["DSDetailTable"] = ds;
                ShowDataDetail();
                litJavaScript.Text += "<script>";
                litJavaScript.Text += " document.getElementById('divDetail').style.display = 'inline';";
                litJavaScript.Text += " location.hash = '#aBranch';\n";
                litJavaScript.Text += "</script>";
            }
            setMode();
        }
        protected void btnEditDetail_Click(object sender, EventArgs e)
        {
            int d = dtgData.EditItemIndex;
            DataSet ds = new DataSet();
            litJavaScript.Text += "<script>";
            litJavaScript.Text += " document.getElementById('divDetail').style.display = 'inline';";
            litJavaScript.Text += " location.hash = '#aBranch';\n";
            litJavaScript.Text += "</script>";
            setMode();
        }
        protected void imgEdit_Click(object sender, ImageClickEventArgs e)
        {
            litJavaScript.Text = "";
            if (saveDetail())
            {
                dtgData.EditItemIndex = int.Parse(((ImageButton)sender).Attributes["index"]);
                ShowDataDetail();
                litJavaScript.Text += "<script>";
                litJavaScript.Text += " document.getElementById('divDetail').style.display = 'inline';";
                litJavaScript.Text += " location.hash = '#aBranch';\n";
                litJavaScript.Text += "</script>";
            }
            setMode();
        }
        protected void imgDelete_Click(object sender, ImageClickEventArgs e)
        {
            DataSet ds = new DataSet();
            int d = int.Parse(((ImageButton)sender).Attributes["index"]);
            litJavaScript.Text = "";
            if (ViewState["DSDetailTable"] != null)
                ds = (DataSet)ViewState["DSDetailTable"];

            ds.Tables[0].Rows.RemoveAt(d);
            dtgData.EditItemIndex = -1;
            ViewState["DSDetailTable"] = ds;
            ShowDataDetail();
            litJavaScript.Text += "<script>";
            litJavaScript.Text += " document.getElementById('divDetail').style.display = 'inline';";
            litJavaScript.Text += " location.hash = '#aBranch';\n";
            litJavaScript.Text += "</script>";
            setMode();
        }
        protected void imgSave_Click(object sender, ImageClickEventArgs e)
        {
            litJavaScript.Text = "";
            saveDetail();
            litJavaScript.Text += "<script>";
            litJavaScript.Text += " document.getElementById('divDetail').style.display = 'inline';";
            litJavaScript.Text += " location.hash = '#aBranch';\n";
            litJavaScript.Text += "</script>";
            setMode();
        }
        protected void imgCancel_Click(object sender, ImageClickEventArgs e)
        {
            DataSet ds = new DataSet();
            int d = int.Parse(((ImageButton)sender).Attributes["index"]);
            litJavaScript.Text = "";
            if (ViewState["DSDetailTable"] != null)
                ds = (DataSet)ViewState["DSDetailTable"];

            if (ds.Tables[0].Rows[0]["bran_code"].ToString() == "")
            {
                ds.Tables[0].Rows.RemoveAt(d);
            }
            dtgData.EditItemIndex = -1;
            ViewState["DSDetailTable"] = ds;
            ShowDataDetail();
            litJavaScript.Text += "<script>";
            litJavaScript.Text += " document.getElementById('divDetail').style.display = 'inline';";
            litJavaScript.Text += " location.hash = '#aBranch';\n";
            litJavaScript.Text += "</script>";
            setMode();
        }

        public void ShowDataDetail()
        {
            DataSet ds = new DataSet();
            if (ViewState["DSDetailTable"] != null)
                ds = (DataSet)ViewState["DSDetailTable"];

            string sortBy = "";
            //if (ViewState["SortString"] != null)
            //    sortBy = (String)ViewState["SortString"];

            if (ds.Tables.Count > 0)
            {
                dv.Table = ds.Tables[0];
                dv.Sort = sortBy;

                int newPageCount = (Int32)(Math.Ceiling((double)dv.Table.Rows.Count / (double)dtgData.PageSize));
                if (dtgData.CurrentPageIndex >= newPageCount && newPageCount > 0)
                    dtgData.CurrentPageIndex = newPageCount - 1;
            }
            dtgData.DataSource = dv;
            dtgData.DataBind();

            GridViewUtil.ItemDataBound(dtgData.Items);
        }
        public bool saveDetail()
        {
            int d = dtgData.EditItemIndex;
            DataSet ds = new DataSet();
            bool isValidate = true;
            string strMsg = "";
            litJavaScript.Text += "<script>";
            if (d >= 0)
            {
                if (((TextBox)dtgData.Items[d].Cells[1].FindControl("txtDetailCode")).Text == "")
                {
                    isValidate = false;
                    strMsg += "\\t- รหัสสาขา\\n";
                }
                if (((TextBox)dtgData.Items[d].Cells[1].FindControl("txtDetailDesc")).Text == "")
                {
                    isValidate = false;
                    strMsg += "\\t- ชื่อสาขา\\n";
                }
                if (((TextBox)dtgData.Items[d].Cells[1].FindControl("txtDetailAddress1")).Text == "")
                {
                    isValidate = false;
                    strMsg += "\\t- ที่อยู่\\n";
                }
                if (((DropDownList)dtgData.Items[d].Cells[1].FindControl("ddlDetailProvince")).SelectedValue == "")
                {
                    isValidate = false;
                    strMsg += "\\t- ที่อยู่(จังหวัด)\\n";
                }
                //if (((TextBox)dtgData.Items[d].Cells[1].FindControl("txtDetailPostCode")).Text == "")
                //{
                //    isValidate = false;
                //    strMsg += "\\t- ที่อยู่(รหัสไปรษณีย์)\\n";
                //}
                if (((TextBox)dtgData.Items[d].Cells[1].FindControl("txtDetailTel")).Text == "")
                {
                    isValidate = false;
                    strMsg += "\\t- เบอร์โทรศัพท์\\n";
                }
                if (isValidate == false)
                {
                    litJavaScript.Text += "alert('! กรุณาระบุข้อมูลสาขา\\n" + strMsg + "'); ";
                }

                else
                {
                    if (ViewState["DSDetailTable"] != null)
                        ds = (DataSet)ViewState["DSDetailTable"];

                    if (d >= 0)
                    {
                        ds.Tables[0].Rows[d]["bran_code"] = ((TextBox)dtgData.Items[d].Cells[1].FindControl("txtDetailCode")).Text;
                        ds.Tables[0].Rows[d]["bran_name"] = ((TextBox)dtgData.Items[d].Cells[1].FindControl("txtDetailDesc")).Text;
                        ds.Tables[0].Rows[d]["bran_address1"] = ((TextBox)dtgData.Items[d].Cells[1].FindControl("txtDetailAddress1")).Text;
                        ds.Tables[0].Rows[d]["bran_address2"] = ((TextBox)dtgData.Items[d].Cells[1].FindControl("txtDetailAddress2")).Text;
                        ds.Tables[0].Rows[d]["province_code"] = ((DropDownList)dtgData.Items[d].Cells[1].FindControl("ddlDetailProvince")).SelectedValue;
                        ds.Tables[0].Rows[d]["bran_postcode"] = ((TextBox)dtgData.Items[d].Cells[1].FindControl("txtDetailPostCode")).Text;
                        ds.Tables[0].Rows[d]["bran_tel"] = ((TextBox)dtgData.Items[d].Cells[1].FindControl("txtDetailTel")).Text;
                        ds.Tables[0].Rows[d]["bran_remark"] = ((TextBox)dtgData.Items[d].Cells[1].FindControl("txtDetailRemark")).Text;
                    }
                    dtgData.EditItemIndex = -1;
                    ViewState["DSDetailTable"] = ds;
                    ShowDataDetail();
                }
            }

            litJavaScript.Text += "</script>";
            return isValidate;
        }
        public bool showEdit(object objRateID)
        {
            bool isRet = false;
            //if (objRateID.ToString() == "")
            //    isRet = false;
            //else
            //    isRet = true;
            return isRet;
        }
        //public string showProvince(object objProvinceCode)
        //{
        //    clsMasterData cm = new clsMasterData();
        //    DataSet ds;
        //    string strRet = "";
        //    // ds = cm.getDataProvinceByID(objProvinceCode.ToString());
        //    //if (ds.Tables[0].Rows.Count > 0)
        //    //{
        //    //    strRet = ds.Tables[0].Rows[0]["province_desc"].ToString();
        //    //}
        //    return strRet;
        //}
        public string showStyleEdit(object objRateID)
        {
            string strRet = "TEXTBOX";
            //if (objRateID.ToString() == "")
            //    strRet = "TEXTBOX";
            //else
            //    strRet = "TEXTBOXDIS";
            return strRet;
        }
        public string showCalendarEdit(object objRateID)
        {
            string strRet = "";
            //if (objRateID.ToString() == "")
            //    strRet = "";
            //else
            //    strRet = "view";
            return strRet;
        }
        //protected void dtgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        //{
        //    DropDownList TmpDdl;
        //    TextBox TmpTxt;

        //    clsMasterData cm = new clsMasterData();
        //    DataSet ds;
        //    if (e.Item.ItemType == ListItemType.EditItem)
        //    {
        //        //TmpDdl = (DropDownList)e.Item.FindControl("ddlDetailProvince");
        //        //TmpTxt = (TextBox)e.Item.FindControl("txtDetailProvinceCode");
        //        //ds = cm.getAllDataProvince();
        //        //TmpDdl.DataSource = ds;
        //        //TmpDdl.DataTextField = "province_desc";
        //        //TmpDdl.DataValueField = "province_code";
        //        //TmpDdl.DataBind();
        //        //TmpDdl.Items.Insert(0, new ListItem(" -- เลือก -- ", ""));
        //        //if (TmpTxt.Text != "")
        //        //    TmpDdl.SelectedValue = TmpTxt.Text;
        //        //TmpDdl.Enabled = (TmpTxt.ReadOnly == true ? false : true);
        //    }
        //}

        public void setMode()
        {

            litJavaScript.Text += "</script>";
        }
    }
}