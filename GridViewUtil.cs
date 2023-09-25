using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Sucarcane
{
    public static class GridViewUtil
    {

        static public void setGridStyle(GridView dtg, string[] columnWidth, string[] columnAlign, string[] columnTitle)
        {
            int iLength = dtg.Columns.Count - 1;
            if (dtg.Columns.Count > columnTitle.Length)
                iLength = columnTitle.Length - 1;
            for (int i = 0; i <= iLength; i++)
            {
                dtg.Columns[i].HeaderText = columnTitle[i];
                if (columnAlign[i].ToLower() == "center")
                    dtg.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                else if (columnAlign[i].ToLower() == "right")
                    dtg.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                else if (columnAlign[i].ToLower() == "left")
                    dtg.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                if (columnWidth[i].EndsWith("%"))
                    dtg.Columns[i].HeaderStyle.Width = Unit.Percentage(Convert.ToDouble(columnWidth[i].Substring(0, columnWidth[i].Length - 1)));
                else
                    dtg.Columns[i].HeaderStyle.Width = Unit.Parse(columnWidth[i]);
            }
        }
        static public void setGridStyle(DataGrid dtg, string[] columnWidth, string[] columnAlign, string[] columnTitle)
        {
            for (int i = 0; i <= dtg.Columns.Count - 1; i++)
            {
                dtg.Columns[i].HeaderText = columnTitle[i];
                dtg.Columns[i].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                if (columnAlign[i].ToLower() == "center")
                    dtg.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                else if (columnAlign[i].ToLower() == "right")
                    dtg.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                else if (columnAlign[i].ToLower() == "left")
                    dtg.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                if (columnWidth[i].EndsWith("%"))
                    dtg.Columns[i].HeaderStyle.Width = Unit.Percentage(Convert.ToDouble(columnWidth[i].Substring(0, columnWidth[i].Length - 1)));
                else
                    dtg.Columns[i].HeaderStyle.Width = Unit.Parse(columnWidth[i]);
            }
        }
        static public void ItemDataBound(DataGridItemCollection itemGrid)
        {

            for (int i = 0; i < itemGrid.Count; i++)
            {
                System.Web.UI.WebControls.DataGridItemEventArgs e = new DataGridItemEventArgs(itemGrid[i]);
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    e.Item.Attributes.Add("onmouseover", "this.className='gridrowSelect'");
                    if (e.Item.ItemType == ListItemType.Item)
                        e.Item.Attributes.Add("onmouseout", "this.className='gridrow0'");
                    if (e.Item.ItemType == ListItemType.AlternatingItem)
                        e.Item.Attributes.Add("onmouseout", "this.className='gridrow1'");
                }
            }
        }
        static public void setGridStyleRow(DataGridItemCollection itemGrid)
        {
            //for (int i = 0; i < itemGrid.Count; i++)
            //{
            //    Double Yardamt = 0;
            //    Double Min = 0;
            //    Yardamt = Convert.ToDouble(((Label)itemGrid[i].Cells[8].FindControl("lblDetailYard")).Text);
            //    Min = Convert.ToDouble(((Label)itemGrid[i].Cells[9].FindControl("lblDetailMin")).Text);
            //    if (Yardamt < Min)
            //    {
            //        //itemGrid[i].Cells[0].ForeColor = System.Drawing.Color.Red;
            //        //itemGrid[i].Cells[1].ForeColor = System.Drawing.Color.Red;
            //        itemGrid[i].Cells[2].ForeColor = System.Drawing.Color.Red;
            //        itemGrid[i].Cells[3].ForeColor = System.Drawing.Color.Red;
            //        itemGrid[i].Cells[4].ForeColor = System.Drawing.Color.Red;
            //        itemGrid[i].Cells[5].ForeColor = System.Drawing.Color.Red;
            //        itemGrid[i].Cells[6].ForeColor = System.Drawing.Color.Red;
            //        itemGrid[i].Cells[7].ForeColor = System.Drawing.Color.Red;
            //        itemGrid[i].Cells[8].ForeColor = System.Drawing.Color.Red;
            //        itemGrid[i].Cells[9].ForeColor = System.Drawing.Color.Red;
            //    }
            //}

        }

    }
}