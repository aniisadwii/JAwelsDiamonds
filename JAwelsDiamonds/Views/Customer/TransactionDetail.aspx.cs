using JAwelsDiamonds.Models;
using JAwelsDiamonds.Repositories;
using System;
using System.Linq;
using System.Web.UI;

namespace JAwelsDiamonds.Views.Customer
{
    public partial class TransactionDetail : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Views/Guest/Login.aspx");
                    return;
                }

                if (Request.QueryString["transactionId"] == null)
                {
                    Response.Redirect("~/Views/Customer/MyOrders.aspx");
                    return;
                }

                LoadTransactionDetails();
            }
        }

        private void LoadTransactionDetails()
        {
            try
            {
                int transactionId = int.Parse(Request.QueryString["transactionId"]);
                int userId = (int)Session["UserID"];

                var transaction = TransactionRepository.GetUserTransaction(userId, transactionId);
                if (transaction == null)
                {
                    Response.Redirect("~/Views/Customer/MyOrders.aspx");
                    return;
                }

                TransactionIdLabel.Text = "Transaction ID: " + transactionId;

                var details = TransactionRepository.GetTransactionDetails(transactionId)
                    .Select(td => new
                    {
                        td.MsJewel.JewelName,
                        td.Quantity,
                        Price = td.MsJewel.JewelPrice,
                        Subtotal = (decimal)(td.Quantity * td.MsJewel.JewelPrice)
                    }).ToList();

                TransactionDetailGV.DataSource = details;
                TransactionDetailGV.DataBind();

                decimal total = details.Sum(d => d.Subtotal);
                TotalLabel.Text = "Total: " + total.ToString("C");
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Views/Customer/MyOrders.aspx");
            }
        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Customer/MyOrders.aspx");
        }
    }
}