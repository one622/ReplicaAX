using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReplicaAX.UserControl
{
    public partial class ucPageNavigator : System.Web.UI.UserControl
    {
        public event DataGridPageChangedEventHandler PageIndexChanged;
        public string columnName = "page";
        DataGrid dtg;
        DataView dv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            else
            {
                if (Session["pageNavigate_dvdata" + this.ClientID] != null)
                    dv = (DataView)Session["pageNavigate_dvdata" + this.ClientID];
                if (Session["pageNavigate_dtgdata" + this.ClientID] != null)
                    dtg = (DataGrid)Session["pageNavigate_dtgdata" + this.ClientID];
            }
        }

        public DataView dvData
        {
            set
            {
                dv = value;
                Session.Add("pageNavigate_dvdata" + this.ClientID, dv);
            }
        }
        public DataGrid dtgData
        {
            set
            {
                dtg = value;
                Session.Add("pageNavigate_dtgdata" + this.ClientID, dtg);
                setProperty();
            }
        }

        private void setProperty()
        {
            LastPage = dtg.PageCount.ToString();
            CurPage = Convert.ToString(dtg.CurrentPageIndex + 1);
            FromItem = Convert.ToString((dtg.CurrentPageIndex * dtg.PageSize) + 1);
            ToItem = Convert.ToString((dtg.CurrentPageIndex * dtg.PageSize) + dtg.PageSize);
            if ((dtg.CurrentPageIndex * dtg.PageSize) + dtg.PageSize > dv.Count)
            {
                ToItem = dv.Count.ToString();
            }
            AllItem = dv.Count.ToString();
        }

        public string LastPage
        {
            set
            {
                int iCount;
                DataSet ds;
                DataSet dsTmp;
                lblAllPage.Text = value;
                ds = new DataSet();
                ds.Tables.Add();
                ds.Tables[0].Columns.Add("page");
                for (iCount = 0; iCount <= Convert.ToDouble(value) - 1; iCount++)
                {
                    DataRow dr;
                    dr = ds.Tables[0].NewRow();

                    dr[0] = iCount + 1;
                    ds.Tables[0].Rows.Add(dr);
                }
                //dsTmp = new DataSet();
                //dsTmp.Tables.Add(ds.Tables[0].Clone());    
                //dsTmp.Tables[0].Rows.Add(dsTmp.Tables[0].NewRow());
                repPage.DataSource = ds;
                repPage.DataBind();

            }
        }

        public string CurPage
        {
            set
            {

                int iPage = 0;
                LinkButton lnkCurPage;
                txtCurPage.Text = value;
                iPage = Convert.ToInt32(value);
                if (iPage <= Convert.ToInt32(lblAllPage.Text))
                {

                }
                else
                {
                    iPage = Convert.ToInt32(lblAllPage.Text);
                }
                if (iPage > 0)
                {
                    lnkCurPage = (LinkButton)repPage.Items[iPage - 1].Controls[1];
                    Page.RegisterStartupScript("pageNavigatorDisable", "<script>document.getElementById(\"" + lnkCurPage.ClientID + "\").removeAttribute(\"href\");\n document.getElementById(\"" + lnkCurPage.ClientID + "\").disabled=\"disabled\"; </script>");
                    //lnkCurPage.Enabled = false; 
                    dtg.CurrentPageIndex = iPage - 1;
                    FromItem = Convert.ToString(((iPage - 1) * dtg.PageSize) + 1);
                    if ((((iPage - 1) * dtg.PageSize) + dtg.PageSize) < dv.Table.Rows.Count)
                        ToItem = Convert.ToString(((iPage - 1) * dtg.PageSize) + dtg.PageSize);
                    else
                        ToItem = dv.Table.Rows.Count.ToString();
                }
                else
                    txtCurPage.Text = "0";
            }
            get
            {
                return txtCurPage.Text;
            }
        }

        public string FromItem
        {
            set
            {
                lblFromItem.Text = value;
            }
        }

        public string ToItem
        {
            set
            {
                lblToItem.Text = value;
            }
        }

        public string AllItem
        {
            set
            {
                lblAllItem.Text = value;
            }
        }
        public string showPageLabel(string strPage)
        {
            string strRet = "";
            strRet = "ไปหน้า: " + strPage;
            return strRet;
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            int iPage = 0;
            if (dtg != null && dtg.Items.Count > 0)
            {
                if (Convert.ToInt32(txtCurPage.Text) >= dtg.PageCount)
                {
                    iPage = dtg.PageCount;
                }
                else if (dtg.CurrentPageIndex < 0 || Convert.ToInt32(txtCurPage.Text) < 1)
                {
                    iPage = 1;
                }
                else
                    iPage = Convert.ToInt32(txtCurPage.Text) - 1;
            }
            else
            {
                iPage = 1;
            }

            CurPage = (iPage + 1).ToString();
            if (PageIndexChanged != null)
            {
                PageIndexChanged(dtg, new DataGridPageChangedEventArgs(dtg, iPage));

            }
        }
        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            int iPage = 0;
            if (dtg.CurrentPageIndex < dtg.PageCount - 1)
            {
                iPage = dtg.CurrentPageIndex + 1;
                //ShowData();
            }
            else
                iPage = dtg.PageCount - 1;

            CurPage = (iPage + 1).ToString();
            if (PageIndexChanged != null)
            {
                PageIndexChanged(dtg, new DataGridPageChangedEventArgs(dtg, iPage));
            }
        }
        protected void btnPrev_Click(object sender, ImageClickEventArgs e)
        {
            int iPage = 0;
            if (dtg.CurrentPageIndex > 0)
            {
                iPage = dtg.CurrentPageIndex - 1;
                //ShowData();
            }

            CurPage = (iPage + 1).ToString();
            if (PageIndexChanged != null)
            {
                PageIndexChanged(dtg, new DataGridPageChangedEventArgs(dtg, iPage));
            }
        }
        protected void Page_Click(object sender, System.EventArgs e)
        {
            LinkButton lnkTmp = (LinkButton)sender;
            int iPage = 0;
            if (lnkTmp != null)
            {
                lnkTmp.Text = lnkTmp.Text.Replace("</tr><tr>", "");
                iPage = Convert.ToInt32(lnkTmp.Text) - 1;
            }
            CurPage = (iPage + 1).ToString();
            if (PageIndexChanged != null)
            {
                PageIndexChanged(dtg, new DataGridPageChangedEventArgs(dtg, iPage));

            }
        }
        protected void repPage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemIndex + 1) % 15 == 0 && e.Item.ItemIndex > 0)
            {
                ((LinkButton)e.Item.Controls[1]).Text = ((LinkButton)e.Item.Controls[1]).Text + "</tr><tr>";
            }
        }
    }
}