using JAwelsDiamonds.Models;
using JAwelsDiamonds.Repositories;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JAwelsDiamonds.Views.Admin
{
    public partial class HandleOrders : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserRole"]?.ToString() != "Admin")
            {
                Response.Redirect("~/Views/Home.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadUnfinishedOrders();
            }
            else
            {
                LoadUnfinishedOrders();
            }
        }

        private void LoadUnfinishedOrders()
        {
            try
            {
                using (var db = new DatabaseEntities1())
                {
                    var orders = db.TransactionHeaders
                        .Where(th => th.TransactionStatus != "Done" &&
                                     th.TransactionStatus != "Rejected")
                        .OrderBy(th => th.TransactionDate)
                        .Select(th => new
                        {
                            th.TransactionID,
                            th.UserID,
                            th.TransactionStatus
                        })
                        .ToList();

                    OrdersGridView.DataSource = orders;
                    OrdersGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading orders: " + ex.Message, isSuccess: false);
            }
        }

        protected void OrdersGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var status = DataBinder.Eval(e.Row.DataItem, "TransactionStatus").ToString();
                var actionPlaceholder = (PlaceHolder)e.Row.FindControl("ActionPlaceholder");
                var transactionId = DataBinder.Eval(e.Row.DataItem, "TransactionID").ToString();

                switch (status)
                {
                    case "Payment Pending":
                        var confirmButton = new Button
                        {
                            ID = "btnConfirm_" + transactionId,
                            Text = "Confirm Payment",
                            CssClass = "action-button confirm-button",
                            CommandName = "ConfirmPayment",
                            CommandArgument = transactionId,
                            UseSubmitBehavior = false
                        };
                        confirmButton.Command += Button_Command;
                        actionPlaceholder.Controls.Add(confirmButton);
                        break;

                    case "Shipment Pending":
                        var shipButton = new Button
                        {
                            ID = "btnShip_" + transactionId,
                            Text = "Ship Package",
                            CssClass = "action-button ship-button",
                            CommandName = "ShipPackage",
                            CommandArgument = transactionId,
                            UseSubmitBehavior = false
                        };
                        shipButton.Command += Button_Command;
                        actionPlaceholder.Controls.Add(shipButton);
                        break;

                    case "Arrived":
                        var waitingLabel = new Label
                        {
                            Text = "Waiting for user confirmation...",
                            CssClass = "waiting-text"
                        };
                        actionPlaceholder.Controls.Add(waitingLabel);
                        break;
                }
            }
        }

        protected void Button_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int transactionId = int.Parse(e.CommandArgument.ToString());
                string newStatus = "";
                string successMessage = "";

                switch (e.CommandName)
                {
                    case "ConfirmPayment":
                        newStatus = "Shipment Pending";
                        successMessage = "Payment confirmed successfully!";
                        break;
                    case "ShipPackage":
                        newStatus = "Arrived";
                        successMessage = "Package shipped successfully!";
                        break;
                }

                if (!string.IsNullOrEmpty(newStatus))
                {
                    using (var db = new DatabaseEntities1())
                    {
                        var transaction = db.TransactionHeaders.Find(transactionId);
                        if (transaction != null)
                        {
                            transaction.TransactionStatus = newStatus;
                            db.SaveChanges();

                            ShowMessage(successMessage, true);
                            LoadUnfinishedOrders(); 
                        }
                        else
                        {
                            ShowMessage("Transaction not found.", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error updating order: " + ex.Message, false);
            }
        }

        public string GetStatusCssClass(string status)
        {
            switch (status)
            {
                case "Payment Pending": return "status-pending";
                case "Shipment Pending": return "status-shipping";
                case "Arrived": return "status-arrived";
                default: return "";
            }
        }

        private void ShowMessage(string message, bool isSuccess = true)
        {
            MessageLabel.Text = message;
            MessageLabel.CssClass = isSuccess ? "message success-message" : "message error-message";
            MessageLabel.Visible = true;
        }
    }
}