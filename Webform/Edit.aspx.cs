using QLSP_NoSeparateForm.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLSP_NoSeparateForm.Webform
{
    public partial class Edit : System.Web.UI.Page
    {
        Database db = new Database();
        string currentMasp;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    currentMasp = Request.QueryString["id"];
                    LoadProductDetails(currentMasp);
                }
                else
                {
                    Response.Redirect("Edit.aspx");
                }
            }
        }

        private void LoadProductDetails(string masp)
        {
            db.OpenConn();
            DataTable dt = db.GetSanPhamById(masp);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txt_masp.Text = row["Masp"].ToString();
                txt_tensp.Text = row["Tensp"].ToString();
                txt_hangsx.Text = row["Hangsx"].ToString();
                txt_mota.Text = row["Mota"].ToString();
                txt_dongia.Text = row["Dongia"].ToString();
                imgCurrent.ImageUrl = "~/Images/" + row["Hinhanh"].ToString();
            }
            db.CloseConn();
        }

        protected void btn_CapNhat_Click(object sender, EventArgs e)
        {
            string masp = txt_masp.Text.Trim();
            string tensp = txt_tensp.Text.Trim();
            string hangsx = txt_hangsx.Text.Trim();
            string mota = txt_mota.Text.Trim();
            double dongia;

            if (string.IsNullOrEmpty(tensp) || string.IsNullOrEmpty(hangsx) ||
                string.IsNullOrEmpty(mota) || !double.TryParse(txt_dongia.Text.Trim(), out dongia))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Vui lòng điền đầy đủ thông tin!');", true);
                return;
            }

            string hinhAnh = null;
            if (FileUpload1.HasFile)
            {
                hinhAnh = FileUpload1.FileName;
                string filepath = MapPath("~/Images/" + hinhAnh);
                FileUpload1.SaveAs(filepath);
            }
            else
            {
                DataTable dt = db.GetSanPhamById(masp);
                if (dt.Rows.Count > 0)
                {
                    hinhAnh = dt.Rows[0]["Hinhanh"].ToString();
                }
            }

            db.OpenConn();
            Sanphams sp = new Sanphams(masp, tensp, hangsx, mota, dongia, DateTime.Now, hinhAnh);
            db.update(sp);
            db.CloseConn();

            ClientScript.RegisterStartupScript(this.GetType(), "alert",
                "alert('Cập nhật sản phẩm thành công!'); window.location='Sanpham.aspx';", true);
        }
    }
}