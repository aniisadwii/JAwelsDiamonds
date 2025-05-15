using JAwelsDiamonds.Models;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JAwelsDiamonds.Views.Customer
{
    public partial class MyOrders : System.Web.UI.Page
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

                LoadOrders();
            }
        }

        private void LoadOrders()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            using (var db = new DatabaseEntities1())
            {
                var orders = db.TransactionHeaders
                    .Where(t => t.UserID == userId)
                    .OrderByDescending(t => t.TransactionDate)
                    .Select(t => new
                    {
                        t.TransactionID,
                        t.TransactionDate,
                        t.PaymentMethod,
                        t.TransactionStatus
                    }).ToList();

                gvOrders.DataSource = orders;
                gvOrders.DataBind();
            }
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                Response.Redirect($"~/Views/Customer/TransactionDetail.aspx?TransactionID={e.CommandArgument}");
            }
            else if (e.CommandName == "Confirm" || e.CommandName == "Reject")
            {
                int transactionId = Convert.ToInt32(e.CommandArgument);
                string newStatus = e.CommandName == "Confirm" ? "Done" : "Rejected";

                using (var db = new DatabaseEntities1())
                {
                    var transaction = db.TransactionHeaders.Find(transactionId);
                    if (transaction != null && transaction.TransactionStatus == "Arrived")
                    {
                        transaction.TransactionStatus = newStatus;
                        db.SaveChanges();
                        lblMessage.Text = $"Order {transactionId} status updated to {newStatus}";
                    }
                    else
                    {
                        lblMessage.Text = "Invalid operation";
                    }
                }

                LoadOrders(); // Refresh data
                lblMessage.Visible = true;
            }
        }

        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = DataBinder.Eval(e.Row.DataItem, "TransactionStatus")?.ToString();
                Panel pnlActions = (Panel)e.Row.FindControl("pnlArrivedActions");

                if (pnlActions != null)
                {
                    pnlActions.Visible = status == "Arrived";
                }
            }
        }

    }
}