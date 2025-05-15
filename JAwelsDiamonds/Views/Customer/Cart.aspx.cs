using JAwelsDiamonds.Handlers;
using JAwelsDiamonds.Models;
using JAwelsDiamonds.Repositories;
using System;
using System.Linq;
using System.Web.UI;
using System.Collections.Generic;

namespace JAwelsDiamonds.Views.Customer
{
    public partial class Cart : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(rptCartItems);
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Views/Guest/Login.aspx");
                    return;
                }

                LoadCartItems();
            }
        }

        private void LoadCartItems()
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            var db = new DatabaseEntities1();

            var cartItems = (from c in db.Carts
                             join j in db.MsJewels on c.JewelID equals j.JewelID
                             where c.UserID == userId
                             select new
                             {
                                 JewelID = j.JewelID,
                                 JewelName = j.JewelName,
                                 Price = j.JewelPrice,
                                 Quantity = c.Quantity 
                             }).ToList();

            if (cartItems.Any())
            {
                rptCartItems.DataSource = cartItems;
                rptCartItems.DataBind();

                decimal grandTotal = cartItems
                    .Sum(item => Convert.ToDecimal(item.Price) * item.Quantity); // Tidak perlu Convert.ToDecimal untuk Quantity
                lblGrandTotal.Text = $"Grand Total: {grandTotal.ToString("C")}";
                pnlCheckout.Visible = true;
                pnlEmptyCart.Visible = false;
            }
            else
            {
                pnlEmptyCart.Visible = true;
                pnlCheckout.Visible = false;
            }
        }

        protected void rptCartItems_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            int jewelId = Convert.ToInt32(e.CommandArgument);
            var db = new DatabaseEntities1();

            if (e.CommandName == "Update")
            {
                var quantityTextBox = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtQuantity");
                if (int.TryParse(quantityTextBox.Text, out int newQuantity) && newQuantity > 0)
                {
                    var repo = new CartRepository(db);
                    var cartItem = db.Carts.FirstOrDefault(c => c.UserID == userId && c.JewelID == jewelId);

                    if (cartItem != null)
                    {
                        cartItem.Quantity = newQuantity;
                        db.SaveChanges();
                        lblMessage.Text = "Quantity updated successfully!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                }
                else
                {
                    lblMessage.Text = "Please enter a valid quantity (minimum 1)";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else if (e.CommandName == "Remove")
            {
                var repo = new CartRepository(db);
                var cartItem = db.Carts.FirstOrDefault(c => c.UserID == userId && c.JewelID == jewelId);

                if (cartItem != null)
                {
                    db.Carts.Remove(cartItem);
                    db.SaveChanges();
                    Response.Redirect(Request.RawUrl);
                    lblMessage.Text = "Item removed from cart!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
            }

            lblMessage.Visible = true;
            LoadCartItems(); // Refresh the cart
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Views/Guest/Login.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["UserID"]);
            string paymentMethod = ddlPaymentMethod.SelectedValue;

            using (var db = new DatabaseEntities1())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // 1. Get cart items
                        var cartItems = db.Carts
                            .Where(c => c.UserID == userId)
                            .ToList();

                        if (!cartItems.Any())
                        {
                            lblMessage.Text = "Your cart is empty";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Visible = true;
                            return;
                        }

                        // 2. Create transaction header
                        var header = new TransactionHeader
                        {
                            UserID = userId,
                            TransactionDate = DateTime.Now,
                            PaymentMethod = paymentMethod,
                            TransactionStatus = "Pending"
                        };
                        db.TransactionHeaders.Add(header);
                        db.SaveChanges(); // Save to get TransactionID

                        // 3. Create transaction details
                        foreach (var cartItem in cartItems)
                        {
                            db.TransactionDetails.Add(new JAwelsDiamonds.Models.TransactionDetail
                            {
                                TransactionID = header.TransactionID,
                                JewelID = cartItem.JewelID,
                                Quantity = cartItem.Quantity
                            });
                        }

                        // 4. Clear cart
                        db.Carts.RemoveRange(cartItems);

                        // 5. Save all changes
                        db.SaveChanges();
                        transaction.Commit();

                        // 6. Show success
                        lblMessage.Text = "Checkout successful! Transaction ID: " + header.TransactionID;
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        pnlEmptyCart.Visible = true;
                        pnlCheckout.Visible = false;
                        LoadCartItems(); // Refresh cart display
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        lblMessage.Text = "Checkout failed: " + ex.Message;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    finally
                    {
                        lblMessage.Visible = true;
                    }
                }
            }
        }
    }
}