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

        protected void JewelGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection newDirection = GetNewSortDirection(e.SortExpression);

            BindJewelData(e.SortExpression, newDirection);

            JewelGridView.HeaderStyle.CssClass = (newDirection == SortDirection.Ascending) ?
                "sorted-asc" : "sorted-desc";
        }

        private SortDirection GetNewSortDirection(string column)
        {
            if (ViewState["SortExpression"] as string == column)
            {
                return (SortDirection)ViewState["SortDirection"] == SortDirection.Ascending ?
                    SortDirection.Descending : SortDirection.Ascending;
            }

            ViewState["SortExpression"] = column;
            ViewState["SortDirection"] = SortDirection.Ascending;
            return SortDirection.Ascending;
        }

        public string FormatPrice(decimal price)
        {
            return "$" + price.ToString("N2");
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