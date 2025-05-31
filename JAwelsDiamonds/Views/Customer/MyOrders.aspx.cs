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
                return;
            }

            int userId = Convert.ToInt32(Session["UserID"]);
            int transactionId = Convert.ToInt32(e.CommandArgument);

            try
            {
                using (var db = new DatabaseEntities1())
                {
                    var transaction = db.TransactionHeaders
                        .FirstOrDefault(t => t.TransactionID == transactionId && t.UserID == userId);

                    if (transaction == null)
                    {
                        lblMessage.Text = "Order not found or doesn't belong to you";
                        lblMessage.Visible = true;
                        return;
                    }

                    if (transaction.TransactionStatus != "Arrived")
                    {
                        lblMessage.Text = "This order cannot be confirmed/rejected in its current status";
                        lblMessage.Visible = true;
                        return;
                    }

                    string newStatus = e.CommandName == "Confirm" ? "Done" : "Rejected";
                    transaction.TransactionStatus = newStatus;
                    db.SaveChanges();

                    lblMessage.Text = $"Order #{transactionId} has been {newStatus.ToLower()}";
                    lblMessage.CssClass = "success-message";
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error processing request: {ex.Message}";
                lblMessage.CssClass = "error-message";
                lblMessage.Visible = true;
            }

            LoadOrders(); 
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

                var statusCell = e.Row.Cells[3]; 
                switch (status)
                {
                    case "Arrived":
                        statusCell.CssClass = "status-arrived";
                        break;
                    case "Done":
                        statusCell.CssClass = "status-done";
                        break;
                    case "Rejected":
                        statusCell.CssClass = "status-rejected";
                        break;
                }
            }
        }

    }
}