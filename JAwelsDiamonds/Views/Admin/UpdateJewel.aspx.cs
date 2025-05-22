using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JAwelsDiamonds.Controllers;
using JAwelsDiamonds.Models;
using JAwelsDiamonds.Module;

namespace JAwelsDiamonds.Views
{
    public partial class UpdateJewel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserRole"]?.ToString() != "Admin")
                {
                    Response.Redirect("~/Views/Home.aspx");
                    return;
                }

                int jewelID;
                int.TryParse(Request.QueryString["JewelID"], out jewelID);

                Response<MsJewel> responseJewel = MsJewelController.GetJewel(jewelID);

                if (responseJewel.Success)
                {
                    LoadCategories();
                    LoadBrands();
                    NameTb.Attributes["placeholder"] = responseJewel.Payload.JewelName;
                    CatDdl.SelectedValue = responseJewel.Payload.CategoryID.ToString();
                    BrandDdl.SelectedValue = responseJewel.Payload.BrandID.ToString();
                    PriceTb.Attributes["placeholder"] = responseJewel.Payload.JewelPrice.ToString();
                    ReleaseTb.Attributes["placeholder"] = responseJewel.Payload.JewelReleaseYear.ToString();

                }
                else
                {
                    Response.Redirect("~/Views/Home.aspx");
                    return;
                }
                
            }

        }

        private void LoadCategories()
        {
            using (var db = new DatabaseEntities1())
            {
                CatDdl.DataSource = db.MsCategories.ToList();
                CatDdl.DataTextField = "CategoryName";
                CatDdl.DataValueField = "CategoryID";
                CatDdl.DataBind();
            }
        }

        private void LoadBrands()
        {
            using (var db = new DatabaseEntities1())
            {
                BrandDdl.DataSource = db.MsBrands.ToList();
                BrandDdl.DataTextField = "BrandName";
                BrandDdl.DataValueField = "BrandID";
                BrandDdl.DataBind();
            }
        }
        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            ClearErrorMessages();
            int jewelID;
            int.TryParse(Request.QueryString["JewelID"], out jewelID);

            string jewelName = NameTb.Text.Trim();
            int  categoryId = int.Parse(CatDdl.SelectedValue);
            int brandId = int.Parse(BrandDdl.SelectedValue);
            string price = PriceTb.Text;
            string releaseYear = ReleaseTb.Text;

            Response<MsJewel> response = MsJewelController.UpdateJewel(jewelID, jewelName, categoryId, brandId, price, releaseYear);

            if (response.Success)
            {
                SuccessMessage.Text = "Jewel added successfully!";
                SuccessMessage.ForeColor = Color.Green;
                Response.Redirect("~/Views/Home.aspx");
            }
            else
            {
                SuccessMessage.Text = response.Message;
                SuccessMessage.ForeColor = Color.Red;
            }

        }
        private void ClearErrorMessages()
        {
            SuccessMessage.Text = "";
        }
        private void ClearForm()
        {
            NameTb.Text = "";
            CatDdl.SelectedIndex = 0;
            BrandDdl.SelectedIndex = 0;
            PriceTb.Text = "";
            ReleaseTb.Text = "";
        }
        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Home.aspx");
        }
    }
}