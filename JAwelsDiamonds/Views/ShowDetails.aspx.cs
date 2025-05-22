using JAwelsDiamonds.Handlers;
using JAwelsDiamonds.Models;
using JAwelsDiamonds.Repositories;
using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;

namespace JAwelsDiamonds.Views

{
    public partial class ShowDetails : Page
    {
        private CartHandler _cartHandler;

        protected void Page_Load(object sender, EventArgs e)
        {
            string role = Session["UserRole"]?.ToString();
            if (role == "Customer")
            {
                btnAddToCart.Visible = true;
                lnkViewCart.Visible = false; 
                pnlAdminActions.Visible = false;
            }
            else if (role == "Admin")
            {
                btnAddToCart.Visible = false;
                pnlAdminActions.Visible = true;
            }
            else
            {
                btnAddToCart.Visible = false;
                pnlAdminActions.Visible = false;
            }

            var db = new DatabaseEntities1();
            var repo = new CartRepository(db);
            _cartHandler = new CartHandler(repo);

            if (!IsPostBack)
            {
                if (Request.QueryString["JewelID"] != null)
                {
                    int jewelId = Convert.ToInt32(Request.QueryString["JewelID"]);
                    LoadJewelDetails(jewelId);
                }
            }
        }

        private void LoadJewelDetails(int jewelId)
        {
            var db = new DatabaseEntities1();
            var jewel = db.MsJewels.FirstOrDefault(j => j.JewelID == jewelId);

            if (jewel != null)
            {
                lblJewelName.Text = jewel.JewelName;
                lblPrice.Text = ((decimal)jewel.JewelPrice).ToString("C");
                lblCategory.Text = jewel.MsCategory?.CategoryName ?? "N/A";
                lblBrand.Text = jewel.MsBrand?.BrandName ?? "N/A";
                lblOrigin.Text = jewel.MsBrand?.BrandCountry ?? "N/A";
                lblClass.Text = jewel.MsBrand?.BrandClass ?? "N/A";
                lblReleaseYear.Text = jewel.JewelReleaseYear.ToString();
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Views/Guest/Login.aspx");
                return;
            }

            try
            {
                int jewelId = Convert.ToInt32(Request.QueryString["JewelID"]);
                int userId = Convert.ToInt32(Session["UserID"]);

                bool success = _cartHandler.AddToCart(userId, jewelId, quantity: 1);

                if (success)
                {
                    pnlSuccess.Visible = true;
                    pnlError.Visible = false;
                    lnkViewCart.Visible = true;
                }
                else
                {
                    pnlError.Visible = true;
                    lblError.Text = "Failed to add item to cart. Please try again.";
                    pnlSuccess.Visible = false;
                }
            }
            catch (Exception ex)
            {
                pnlError.Visible = true;
                lblError.Text = "Error: " + ex.Message;
                pnlSuccess.Visible = false;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int jewelId = Convert.ToInt32(Request.QueryString["JewelID"]);
            Response.Redirect("~/Views/Admin/UpdateJewel.aspx?JewelID=" + jewelId);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int jewelId = Convert.ToInt32(Request.QueryString["JewelID"]);
                var db = new DatabaseEntities1();
                var jewel = db.MsJewels.FirstOrDefault(j => j.JewelID == jewelId);

                if (jewel != null)
                {
                    db.MsJewels.Remove(jewel);
                    db.SaveChanges();
                    Response.Redirect("~/Views/Admin/ListJewel.aspx"); // redirect setelah delete
                }
                else
                {
                    pnlError.Visible = true;
                    lblError.Text = "Jewel not found.";
                }
            }
            catch (Exception ex)
            {
                pnlError.Visible = true;
                lblError.Text = "Error deleting jewel: " + ex.Message;
            }
        }


    }
}