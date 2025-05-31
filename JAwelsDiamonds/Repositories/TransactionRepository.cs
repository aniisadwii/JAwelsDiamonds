using JAwelsDiamonds.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace JAwelsDiamonds.Repositories
{
    public static class TransactionRepository
    {
        public static List<TransactionHeader> GetTransactionHeaders()
        {
            DatabaseEntities1 db = new DatabaseEntities1();
            return db.TransactionHeaders.ToList();
        }
        public static List<TransactionHeader> GetDoneTransactionsForReport()
        {
            using (var db = new DatabaseEntities1())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                return db.TransactionHeaders
                    .Include(th => th.MsUser)
                    .Include(th => th.TransactionDetails.Select(td => td.MsJewel))
                    .Where(th => th.TransactionStatus == "Done")
                    .OrderByDescending(th => th.TransactionDate)
                    .ToList();
            }
        }

        public static TransactionHeader GetUserTransaction(int userId, int transactionId)
        {
            using (var db = new DatabaseEntities1())
            {
                return db.TransactionHeaders
                    .Include(th => th.MsUser)
                    .Include(th => th.TransactionDetails.Select(td => td.MsJewel))
                    .FirstOrDefault(th => th.TransactionID == transactionId && th.UserID == userId);
            }
        }

        public static List<TransactionDetail> GetTransactionDetails(int transactionId)
        {
            using (var db = new DatabaseEntities1())
            {
                return db.TransactionDetails
                    .Include(td => td.MsJewel)
                    .Where(td => td.TransactionID == transactionId)
                    .ToList();
            }
        }

        public static List<TransactionHeader> GetUnfinishedOrders()
        {
            using (var db = new DatabaseEntities1())
            {
                return db.TransactionHeaders
                    .Where(th => th.TransactionStatus != "Done" && th.TransactionStatus != "Rejected")
                    .OrderBy(th => th.TransactionDate)
                    .ToList();
            }
        }

        public static bool UpdateOrderStatus(int transactionId, string newStatus)
        {
            using (var db = new DatabaseEntities1())
            {
                var transaction = db.TransactionHeaders.Find(transactionId);
                if (transaction != null)
                {
                    transaction.TransactionStatus = newStatus;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}