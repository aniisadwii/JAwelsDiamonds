using JAwelsDiamonds.Models;
using System;
using System.Linq;

namespace JAwelsDiamonds.Views.Customer
{
    public partial class TransactionDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["TransactionID"] == null ||
                    !int.TryParse(Request.QueryString["TransactionID"], out int transactionId))
                {
                    Response.Redirect("~/Views/Customer/MyOrders.aspx");
                    return;
                }

                LoadTransactionDetails(transactionId);
            }
        }

        private void LoadTransactionDetails(int transactionId)
        {
            using (var db = new DatabaseEntities1())
            {
                // Get header info
                var header = db.TransactionHeaders.Find(transactionId);
                if (header == null)
                {
                    Response.Redirect("~/Views/Customer/MyOrders.aspx");
                    return;
                }

                lblTransactionID.Text = transactionId.ToString();

                // Get details
                var details = db.TransactionDetails
                    .Where(td => td.TransactionID == transactionId)
                    .Select(td => new
                    {
                        td.MsJewel.JewelName,
                        td.Quantity,
                        Price = td.MsJewel.JewelPrice,
                        Subtotal = td.MsJewel.JewelPrice * td.Quantity
                    }).ToList();

                gvDetails.DataSource = details;
                gvDetails.DataBind();
            }
        }
    }
}