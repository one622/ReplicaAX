using ReplicaAX.UserControl;
using Sucarcane;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReplicaAX
{
    
    public partial class FromAX2 : System.Web.UI.Page
    {
        clsPostges mm = new clsPostges();
       

        public string[] ColumnName = {"Mark", "RafNo", "truckWH", "TransDate", "oilamoun",
                                     "approvestatus", "approver","Edit"};
        public string[] ColumnType = {  "string","string", "string", "string", "string", "string",
                                     "string"};
        public string[] ColumnTitle = { "Mark", "RafNo", "truckWH", "TransDate", "oilamoun", "approvestatus", "approver", "Save/Edit" };
        string[] ColumnWidth = { "10%", "10%", "10%", "15%", "25%", "15%", "20%", "20%", "10%" };
        string[] ColumnAlign = { "center", "center", "left", "left", "left", "left", "left", "center", "center" };
        public static int icolumnVisible = 8;
        DataView dv = new DataView();
        clsMasterData cm = new clsMasterData();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsDetail;

            if (!IsPostBack)
            {
                initialDropdownList();
                initialDataGrid();
                dsDetail = new DataSet();
                dsDetail = cm.getDataSucarcane(txtDate.Text, ddlVehicle_code.SelectedValue, ddlCropYear.SelectedValue);
                docToUI(dsDetail);
            }
        }
        protected void btnSeacrch_Click(object sender, EventArgs e)
        {
            string strCropYear = ddlCropYear.SelectedValue;
            string strVehicle_code = ddlVehicle_code.SelectedValue;
            string strDate = txtDate.Text;
            //string strItem = ddlCropYear.SelectedItem.ToString();
          
            //String txtQuery = " SELECT truckoil, vehicle_code, transdate::date,oilamoun,uid,approvestatus,approver"
            //                + " FROM public.sugarcanetransoil where dataareaid = '102'"
            //                + " and public.sugarcanetransoil.dimension ='" + strItem + "'"
            //                + " ORDER BY UID"
            //                ;

            DataSet ds = cm.getDataSucarcane(strDate, strVehicle_code, strCropYear);
            docToUI(ds);
        }

        public void initialDropdownList()
        {
            //Dropdownรหัสรถ
            DataSet ds;
            ds = cm.getDataddlVehicle_codeSucarcane();
            ddlVehicle_code.DataSource = ds;
            ddlVehicle_code.DataTextField = "TruckWH";
            ddlVehicle_code.DataValueField = "RefNo";
            ddlVehicle_code.DataBind();
            ddlVehicle_code.Items.Insert(0, new ListItem(" -- รหัสรถ -- ", ""));

       

            //Dropdownเลือกฤดูหีบ
            ddlCropYear.Items.Insert(0, new ListItem("67/68", "67/68"));
            ddlCropYear.Items.Insert(0, new ListItem("66/67", "66/67"));
            ddlCropYear.Items.Insert(0, new ListItem("65/66", "65/66"));
            ddlCropYear.Items.Insert(0, new ListItem("64/65", "64/65"));
            ddlCropYear.Items.Insert(0, new ListItem(" -- ปีฤดูหีบ -- ", ""));


            ds = cm.getDataddlVehicle_codeSucarcane();
            รหัสรถ.DataSource = ds;
            รหัสรถ.DataTextField = "TruckWH";
            รหัสรถ.DataValueField = "RefNo";
            รหัสรถ.DataBind();
            รหัสรถ.Items.Insert(0, new ListItem(" -- รหัสรถ -- ", ""));
        }
    

        public string showCalendarEdit(object objRateID)
        {
            string strRet = "";
            if (objRateID.ToString() == "")
                strRet = "";
            else
                strRet = "view";
            return strRet;
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


        protected void PageIndexChanged(Object source, DataGridPageChangedEventArgs e)
        {
            DataView dv = new DataView();
            DataSet ds = new DataSet();
            if (!(ViewState["DSDetailTable"] == null))
            {
                ds = (DataSet)ViewState["DSDetailTable"];
                if (e.NewPageIndex > dtgData.PageCount - 1)
                    dtgData.CurrentPageIndex = dtgData.PageCount - 1;
                else
                    dtgData.CurrentPageIndex = e.NewPageIndex;

                String sortBy = "";
                if (!(ViewState["DSDetailTable"] == null))
                {
                    sortBy = (string)(ViewState["SortString"]);
                }
                dv.Table = ds.Tables[0];
                dv.Sort = sortBy;
                ShowDataDetail();

            }
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
            litJavaScript.Text += "</script>";
            setMode();
        }
        protected void imgSave_Click(object sender, ImageClickEventArgs e)
        {
            litJavaScript.Text = "";
            saveDetail();
            litJavaScript.Text += "<script>";
            litJavaScript.Text += " document.getElementById('divDetail').style.display = 'inline';";
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

            if (ds.Tables[0].Rows[0]["vehicle_code"].ToString() == "")
            {
                ds.Tables[0].Rows.RemoveAt(d);
            }
            dtgData.EditItemIndex = -1;
            ViewState["DSDetailTable"] = ds;
            ShowDataDetail();
            litJavaScript.Text += "<script>";
            litJavaScript.Text += " document.getElementById('divDetail').style.display = 'inline';";
            litJavaScript.Text += "</script>";
            setMode();
        }

        public void ShowDataDetail()
        {
            DataSet ds = new DataSet();
            if (ViewState["DSDetailTable"] != null)
                ds = (DataSet)ViewState["DSDetailTable"];

            string sortBy = "";

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
            ucPageNavigator.dvData = ds.Tables[0].DefaultView;
            ucPageNavigator.dtgData = dtgData;
        }
        public bool saveDetail()
        {
            int d = dtgData.EditItemIndex;
            DataSet ds = new DataSet();
            bool isValidate = true;

            litJavaScript.Text += "<script>";
            if (d >= 0)
            {

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
        public string showProvince(object objProvinceCode)
        {
            clsMasterData cm = new clsMasterData();
            DataSet ds;
            string strRet = "";
            // ds = cm.getDataProvinceByID(objProvinceCode.ToString());
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    strRet = ds.Tables[0].Rows[0]["province_desc"].ToString();
            //}
            return strRet;
        }
        public string showStyleEdit(object objRateID)
        {
            string strRet = "TEXTBOX";
            //if (objRateID.ToString() == "")
            //    strRet = "TEXTBOX";
            //else
            //    strRet = "TEXTBOXDIS";
            return strRet;
        }
        
        protected void dtgData_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

            string xx = string.Empty;
            clsMasterData cm = new clsMasterData();
            //RadioButton rdoTruck = (RadioButton)e.Item.FindControl("rdoTruck");
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                //if (rdoTruck.Checked)
                //    xx  = " rdoTruck_Click('','" + rdoTruck.UniqueID + "');";
            }
        }

        public void setMode()
        {

            litJavaScript.Text += "</script>";
        }

        public void docToUI(DataSet dsDetail)
        {
            string xx = txtDate.Text;
            ViewState.Add("DSDetailTable", dsDetail);
            ShowDataDetail();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            clsMasterData cm = new clsMasterData();
            DataSet ds;
            string strRet = "";

            string xx = txtDate.Text;
            litJavaScript.Text = "<script>";
            ds = UIToDoc();

            strRet = cm.editSucarcaneData(ds);
            if (strRet.StartsWith("ERROR") || strRet.StartsWith("1"))
            {

                litJavaScript.Text += "alert('ไม่สามารถบันทึกข้อมูลได้');";
            }
            else
            {
                litJavaScript.Text += "alert('บันทึกข้อมูลเรียบร้อย');";
                litJavaScript.Text += "alert('" + strRet + "');";


            }
            litJavaScript.Text += "</script>";
        }

        public DataSet UIToDoc()
        {

            DataTable dt;
            clsMasterUtil cmUtil = new clsMasterUtil();


            DataSet dsDetail = new DataSet();




            DataSet ds = new DataSet();
            dt = new DataTable();
            if (ViewState["DSDetailTable"] != null)
                dsDetail = (DataSet)ViewState["DSDetailTable"];
            dt = dsDetail.Tables[0].Clone();
            dt.TableName = "detail";
            ds.Tables.Add(dt);
            for (int i = 0; i <= dsDetail.Tables[0].Rows.Count - 1; i++)
            {
                CheckBox chkTruck = (CheckBox)dtgData.Items[i].FindControl("chkTruck");
                ds.Tables["detail"].Rows.Add(ds.Tables["detail"].NewRow());
                ds.Tables["detail"].Rows[ds.Tables["detail"].Rows.Count - 1]["flag"] = (chkTruck.Checked == true ? "Y" : "N");
                ds.Tables["detail"].Rows[ds.Tables["detail"].Rows.Count - 1]["truckoil"] = dsDetail.Tables[0].Rows[i]["truckoil"].ToString();
                ds.Tables["detail"].Rows[ds.Tables["detail"].Rows.Count - 1]["vehicle_code"] = dsDetail.Tables[0].Rows[i]["vehicle_code"].ToString();
                ds.Tables["detail"].Rows[ds.Tables["detail"].Rows.Count - 1]["transdate"] = dsDetail.Tables[0].Rows[i]["transdate"].ToString();
                ds.Tables["detail"].Rows[ds.Tables["detail"].Rows.Count - 1]["oilamoun"] = dsDetail.Tables[0].Rows[i]["oilamoun"].ToString();
                ds.Tables["detail"].Rows[ds.Tables["detail"].Rows.Count - 1]["uid"] = dsDetail.Tables[0].Rows[i]["uid"].ToString();
                ds.Tables["detail"].Rows[ds.Tables["detail"].Rows.Count - 1]["approvestatus"] = dsDetail.Tables[0].Rows[i]["approvestatus"].ToString();
                ds.Tables["detail"].Rows[ds.Tables["detail"].Rows.Count - 1]["approver"] = dsDetail.Tables[0].Rows[i]["approver"].ToString();
            }

            return ds;
        }

        public bool ShowFlagRdo(object objRdoValue)
        {
            bool isRet = false;
            if (objRdoValue.ToString() == "Y")
                isRet = true;
            return isRet;
        }

    }
}