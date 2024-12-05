using QLSP_NoSeparateForm.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static QLSP_NoSeparateForm.Webform.Sanpham;

namespace QLSP_NoSeparateForm.Webform
{
    public partial class Sanpham : System.Web.UI.Page
    {
            Database db = new Database();

            protected void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {
                    LoadLuoi();
                }
            }

            public void LoadLuoi()
            {
                db.OpenConn();
                gvSanPham.DataSource = db.Get_AllSanPham();
                gvSanPham.DataBind();
                db.CloseConn();
            }
            protected void gvSanPham_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                if (e.CommandName == "Delete")
                {
                    string masp = e.CommandArgument.ToString();
                    db.OpenConn();
                    db.delete(masp);
                    db.CloseConn();
                    LoadLuoi();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Xóa sản phẩm thành công!');", true);
                }
            }

            protected void ck_all_CheckedChanged(object sender, EventArgs e)
            {
                CheckBox ck_all = (CheckBox)gvSanPham.HeaderRow.FindControl("ck_all");
                foreach (GridViewRow row in gvSanPham.Rows)
                {
                    CheckBox ck = (CheckBox)row.FindControl("ckb_ma");
                    ck.Checked = ck_all.Checked;
                }
            }
    }
}