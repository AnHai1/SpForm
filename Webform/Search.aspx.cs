//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace QLSP_NoSeparateForm.Webform
//{
//    public partial class Search : System.Web.UI.Page
//    {
//        Database db = new Database();

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                LoadHangsx();
//                LoadLuoi();
//            }
//        }

//        private void LoadHangsx()
//        {
//            db.OpenConn();
//            ddlHangsx.DataTextField = "Hangsx";
//            ddlHangsx.DataValueField = "Hangsx";
//            ddlHangsx.DataBind();
//            ddlHangsx.Items.Insert(0, new ListItem("Tất cả hãng", ""));
//            db.CloseConn();
//        }

//        private void LoadLuoi()
//        {
//            db.OpenConn();
//            gvSearchResults.DataSource = db.Get_AllSanPham();
//            gvSearchResults.DataBind();
//            db.CloseConn();
//        }

//        protected void btnSearch_Click(object sender, EventArgs e)
//        {
//            string searchText = txtSearch.Text.Trim();
//            string hangsx = ddlHangsx.SelectedValue;

//            db.OpenConn();
//            // Giả sử bạn có phương thức tìm kiếm với các tham số
//            DataTable searchResults = db.SearchSanPham(searchText, hangsx);
//            gvSearchResults.DataSource = searchResults;
//            gvSearchResults.DataBind();
//            db.CloseConn();
//        }

//        protected void gvSearchResults_RowCommand(object sender, GridViewCommandEventArgs e)
//        {
//            if (e.CommandName == "Delete")
//            {
//                string masp = e.CommandArgument.ToString();
//                db.OpenConn();
//                db.delete(masp);
//                db.CloseConn();
//                LoadLuoi();
//                ClientScript.RegisterStartupScript(this.GetType(), "alert",
//                    "alert('Xóa sản phẩm thành công!');", true);
//            }
//        }

//        protected void ck_all_CheckedChanged(object sender, EventArgs e)
//        {
//            CheckBox ck_all = (CheckBox)gvSearchResults.HeaderRow.FindControl("ck_all");
//            foreach (GridViewRow row in gvSearchResults.Rows)
//            {
//                CheckBox ck = (CheckBox)row.FindControl("ckb_ma");
//                ck.Checked = ck_all.Checked;
//            }
//        }
//    }
//}