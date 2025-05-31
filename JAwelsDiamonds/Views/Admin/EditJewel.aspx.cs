using JAwelsDiamonds.Models;
using JAwelsDiamonds.Repositories;
using System;
using System.Linq;
using System.Web.UI;

namespace JAwelsDiamonds.Views.Admin
{
    public partial class EditJewel : Page
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

                if (Request.QueryString["JewelID"] == null)
                {
                    Response.Redirect("~/Views/Admin/ManageJewels.aspx");
                    return;
                }

                int jewelId = int.Parse(Request.QueryString["JewelID"]);
                LoadJewelData(jewelId);
                LoadCategories();
                LoadBrands();
            }
        }

        private void LoadJewelData(int jewelId)
        {
            using (var db = new DatabaseEntities1())
            {
                var jewel = db.MsJewels.FirstOrDefault(j => j.JewelID == jewelId);
                if (jewel != null)
                {
                    NameTb.Text = jewel.JewelName;
                    PriceTb.Text = jewel.JewelPrice.ToString();
                    ReleaseTb.Text = jewel.JewelReleaseYear.ToString();

                    if (jewel.CategoryID.HasValue)
                        CatDdl.SelectedValue = jewel.CategoryID.ToString();
                    if (jewel.BrandID.HasValue)
                        BrandDdl.SelectedValue = jewel.BrandID.ToString();
                }
                else
                {
                    Response.Redirect("~/Views/Admin/ManageJewels.aspx");
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

            bool isValid = true;
            string jewelName = NameTb.Text.Trim();
            string categoryId = CatDdl.SelectedValue;
            string brandId = BrandDdl.SelectedValue;
            decimal price = 0;
            int releaseYear = 0;
            int jewelId = int.Parse(Request.QueryString["JewelID"]);

            if (string.IsNullOrWhiteSpace(jewelName))
            {
                NameMessage.Text = "Jewel name is required!";
                isValid = false;
            }
            else if (jewelName.Length < 3 || jewelName.Length > 25)
            {
                NameMessage.Text = "Jewel name must be between 3-25 characters!";
                isValid = false;
            }

            if (string.IsNullOrEmpty(categoryId))
            {
                CategoryMessage.Text = "Please select a category!";
                isValid = false;
            }

            if (string.IsNullOrEmpty(brandId))
            {
                BrandMessage.Text = "Please select a brand!";
                isValid = false;
            }

            if (!decimal.TryParse(PriceTb.Text, out price))
            {
                PriceMessage.Text = "Price must be a valid number!";
                isValid = false;
            }
            else if (price <= 25)
            {
                PriceMessage.Text = "Price must be more than $25!";
                isValid = false;
            }

            if (!int.TryParse(ReleaseTb.Text, out releaseYear))
            {
                ReleaseYearMessage.Text = "Release year must be a valid number!";
                isValid = false;
            }
            else if (releaseYear > DateTime.Now.Year)
            {
                ReleaseYearMessage.Text = $"Release year must be {DateTime.Now.Year} or earlier!";
                isValid = false;
            }

            if (!isValid)
                return;

            try
            {
                using (var db = new DatabaseEntities1())
                {
                    var jewel = db.MsJewels.FirstOrDefault(j => j.JewelID == jewelId);
                    if (jewel != null)
                    {
                        jewel.JewelName = jewelName;
                        jewel.CategoryID = int.Parse(categoryId);
                        jewel.BrandID = int.Parse(brandId);
                        jewel.JewelPrice = price;
                        jewel.JewelReleaseYear = releaseYear;

                        db.SaveChanges();

                        SuccessMessage.Text = "Jewel updated successfully!";
                        SuccessMessage.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SuccessMessage.Text = "Error updating jewel: " + ex.Message;
                SuccessMessage.CssClass = "error-message";
                SuccessMessage.Visible = true;
            }
        }

        private void ClearErrorMessages()
        {
            NameMessage.Text = "";
            CategoryMessage.Text = "";
            BrandMessage.Text = "";
            PriceMessage.Text = "";
            ReleaseYearMessage.Text = "";
            SuccessMessage.Visible = false;
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Home.aspx");
        }
    }
}