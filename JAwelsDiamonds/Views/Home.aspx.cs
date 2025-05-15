using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using JAwelsDiamonds.Models;

namespace JAwelsDiamonds.Views
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindJewelData();
            }
        }

        private void BindJewelData(string sortExpression = "JewelID", SortDirection sortDirection = SortDirection.Ascending)
        {
            using (var db = new DatabaseEntities1())
            {
                IQueryable<MsJewel> query = db.MsJewels;

                // Implementasi sorting
                switch (sortExpression)
                {
                    case "JewelID":
                        query = (sortDirection == SortDirection.Ascending) ?
                            query.OrderBy(j => j.JewelID) :
                            query.OrderByDescending(j => j.JewelID);
                        break;
                    case "JewelName":
                        query = (sortDirection == SortDirection.Ascending) ?
                            query.OrderBy(j => j.JewelName) :
                            query.OrderByDescending(j => j.JewelName);
                        break;
                    case "JewelPrice":
                        query = (sortDirection == SortDirection.Ascending) ?
                            query.OrderBy(j => j.JewelPrice) :
                            query.OrderByDescending(j => j.JewelPrice);
                        break;
                    default:
                        query = query.OrderBy(j => j.JewelID);
                        break;
                }

                JewelGridView.DataSource = query.ToList();
                JewelGridView.DataBind();
            }
        }

        // Method handler untuk sorting
        protected void JewelGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Dapatkan sort direction yang baru (toggle antara Ascending dan Descending)
            SortDirection newDirection = GetNewSortDirection(e.SortExpression);

            // Bind data dengan sorting baru
            BindJewelData(e.SortExpression, newDirection);

            // Set sort direction untuk header grid
            JewelGridView.HeaderStyle.CssClass = (newDirection == SortDirection.Ascending) ?
                "sorted-asc" : "sorted-desc";
        }

        private SortDirection GetNewSortDirection(string column)
        {
            if (ViewState["SortExpression"] as string == column)
            {
                // Jika kolom yang sama diklik, toggle direction
                return (SortDirection)ViewState["SortDirection"] == SortDirection.Ascending ?
                    SortDirection.Descending : SortDirection.Ascending;
            }

            // Default ascending untuk kolom baru
            ViewState["SortExpression"] = column;
            ViewState["SortDirection"] = SortDirection.Ascending;
            return SortDirection.Ascending;
        }

        protected void DetailBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string jewelId = JewelGridView.DataKeys[row.RowIndex].Value.ToString();
            Response.Redirect($"~/Views/ShowDetails.aspx?JewelID={jewelId}");
        }
    }
}