using QLSP_NoSeparateForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QLSP_NoSeparateForm.Webform
{
    public partial class Add : System.Web.UI.Page
    {
        protected TextBox txt_masp;
        protected TextBox txt_tensp;
        protected TextBox txt_hangsx;
        protected TextBox txt_mota;
        protected TextBox txt_dongia;
        protected FileUpload FileUpload1;
        protected Button btn_Them;
        protected Button btn_nhaplai;
        protected Button btn_quaylai;
        Database db = new Database();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialization code if needed
            }
        }

        protected void btn_Them_Click(object sender, EventArgs e)
        {
            string masp = txt_masp.Text.Trim();
            string tensp = txt_tensp.Text.Trim();
            string hangsx = txt_hangsx.Text.Trim();
            string mota = txt_mota.Text.Trim();
            double dongia;

            if (string.IsNullOrEmpty(masp) || string.IsNullOrEmpty(tensp) ||
                string.IsNullOrEmpty(hangsx) || string.IsNullOrEmpty(mota) ||
                !double.TryParse(txt_dongia.Text.Trim(), out dongia))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Vui lòng điền đầy đủ thông tin!');", true);
                return;
            }

            if (FileUpload1.HasFile)
            {
                string hinhAnh = FileUpload1.FileName;
                string filepath = MapPath("~/Images/" + hinhAnh);
                FileUpload1.SaveAs(filepath);

                if (!db.CheckMa(masp))
                {
                    db.OpenConn();
                    Sanphams sp = new Sanphams(masp, tensp, hangsx, mota, dongia,
                        DateTime.Now, hinhAnh);
                    db.insert(sp);
                    db.CloseConn();
                    ClearFields();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Thêm sản phẩm thành công!'); window.location='Sanpham.aspx';", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('Mã sản phẩm đã tồn tại!');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "alert('Vui lòng chọn hình ảnh!');", true);
            }
        }

        protected void btn_nhaplai_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txt_masp.Text = "";
            txt_tensp.Text = "";
            txt_hangsx.Text = "";
            txt_mota.Text = "";
            txt_dongia.Text = "";
            FileUpload1.Attributes.Clear();
        }
    }
}