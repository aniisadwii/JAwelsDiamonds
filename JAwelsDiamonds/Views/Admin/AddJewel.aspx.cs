using JAwelsDiamonds.Models;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JAwelsDiamonds.Views.Admin
{
    public partial class AddJewel : System.Web.UI.Page
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

                LoadCategories();
                LoadBrands();
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

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            ClearErrorMessages();

            bool isValid = true;
            string jewelName = NameTb.Text.Trim();
            string categoryId = CatDdl.SelectedValue;
            string brandId = BrandDdl.SelectedValue;
            decimal price = 0; 
            int releaseYear = 0;

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

            if (!isValid) return;

            try
            {
                using (var db = new DatabaseEntities1())
                {
                    var newJewel = new MsJewel
                    {
                        JewelName = jewelName,
                        CategoryID = int.Parse(categoryId),
                        BrandID = int.Parse(brandId),
                        JewelPrice = price,
                        JewelReleaseYear = releaseYear,
                    };

                    db.MsJewels.Add(newJewel);
                    db.SaveChanges();
                }

                SuccessMessage.Text = "Jewel added successfully!";
                ClearForm();
            }
            catch (Exception ex)
            {
                GeneralMessage.Text = "An error occurred while adding the jewel. Please try again.";
            }
        }
        private void ClearErrorMessages()
        {
            NameMessage.Text = "";
            CategoryMessage.Text = "";
            BrandMessage.Text = "";
            PriceMessage.Text = "";
            ReleaseYearMessage.Text = "";
            SuccessMessage.Text = "";
            GeneralMessage.Text = "";
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
