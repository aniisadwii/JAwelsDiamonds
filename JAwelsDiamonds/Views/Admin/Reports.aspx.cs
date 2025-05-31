using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using JAwels.Controllers;
using JAwelsDiamonds.Handlers;
using JAwelsDiamonds.Models;
using JAwelsDiamonds.Reports;
using JAwelsDiamonds.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;

namespace JAwelsDiamonds.Views.Admin
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null && Request.Cookies["user_cookie"] == null)
            {
                Response.Redirect("~/Views/Login.aspx");
            }
            else
            {
                MsUser user;
                if (Session["UserID"] == null)
                {
                    var id = Convert.ToInt32(Request.Cookies["user_cookie"].Value);
                    user = UserController.FindbyID(id);
                    Session["user"] = user;
                }
                else
                {
                    var userId = (int)Session["UserID"]; 
                    user = UserController.FindbyID(userId); 
                }

                if (user != null)
                {
                    if (user.UserRole.Equals("Admin") == false)
                    {
                        Response.Redirect("~/Views/Home.aspx");
                    }

                    CrystalReport report = new CrystalReport();
                    JAwelsDiamonds.Dataset.DataSet data = getData(TransactionHandler.GetTransactionHeaders());

                    if (data.Tables["Transactions"].Rows.Count > 0)
                    {
                        CalculateGrandTotals(data.Tables["Transactions"]);
                        report.SetDataSource(data);
                        CrystalReportViewer.ReportSource = report;
                    }
                }
                else
                {
                    Response.Redirect("~/Views/Guest/Login.aspx");
                }
            }
        }

        private void CalculateGrandTotals(DataTable transactionHeaders)
        {
            foreach (DataRow headerRow in transactionHeaders.Rows)
            {
                decimal grandTotal = CalculateGrandTotalForHeader(headerRow);
                headerRow["GrandTotal"] = grandTotal;
            }
        }

        private decimal CalculateGrandTotalForHeader(DataRow headerRow)
        {
            decimal grandTotal = 0;
            foreach (DataRow detailRow in headerRow.GetChildRows("Transactions_TransactionDetails"))
            {
                grandTotal += Convert.ToDecimal(detailRow["Subtotal"]);
            }
            return grandTotal;
        }

        private JAwelsDiamonds.Dataset.DataSet getData(List<TransactionHeader> transactions)
        {
            JAwelsDiamonds.Dataset.DataSet data = new JAwelsDiamonds.Dataset.DataSet();
            var headertable = data.Transactions;
            var detailtable = data.TransactionDetails;

            foreach (TransactionHeader t in transactions)
            {
                var hrow = headertable.NewRow();
                hrow["TransactionID"] = t.TransactionID;
                hrow["UserName"] = t.MsUser.UserName;
                hrow["TransactionDate"] = t.TransactionDate;
                hrow["TransactionStatus"] = t.TransactionStatus;
                headertable.Rows.Add(hrow);

                foreach (JAwelsDiamonds.Models.TransactionDetail d in t.TransactionDetails)
                {
                    var drow = detailtable.NewRow();
                    drow["TransactionID"] = d.TransactionID;
                    drow["JewelID"] = d.MsJewel.JewelID;
                    drow["JewelName"] = d.MsJewel.JewelName;
                    drow["JewelPrice"] = d.MsJewel.JewelPrice;
                    drow["Quantity"] = d.Quantity;
                    drow["Subtotal"] = d.Quantity * d.MsJewel.JewelPrice;
                    detailtable.Rows.Add(drow);
                }
            }
            return data;
        }
    }
}