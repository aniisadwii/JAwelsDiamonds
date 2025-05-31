using JAwelsDiamonds.Repositories;
using JAwelsDiamonds.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace JAwelsDiamonds.Handlers
{
    public class TransactionHandler
    {
        public static List<TransactionHeader> GetTransactionHeaders()
        {
            return TransactionRepository.GetTransactionHeaders();
        }

        public static List<TransactionHeader> GetAllTransactions()
        {
            using (var db = new DatabaseEntities1())
            {
                return db.TransactionHeaders
                       .Where(th => th.TransactionStatus == "Done")
                       .OrderByDescending(t => t.TransactionDate)
                       .ThenByDescending(t => t.TransactionID)
                       .ToList();
            }
        }

        public static TransactionHeader GetTransactionWithDetails(int transactionId)
        {
            using (var db = new DatabaseEntities1())
            {
                return db.TransactionHeaders
                    .Include("TransactionDetails.MsJewel")
                    .Include("MsUser")
                    .FirstOrDefault(th => th.TransactionID == transactionId);
            }
        }

        public static List<TransactionHeader> GetDoneTransactions()
        {
            return TransactionRepository.GetDoneTransactionsForReport();
        }

        public static List<TransactionHeader> GetTransactionsByDateRange(DateTime startDate, DateTime endDate)
        {
            using (var db = new DatabaseEntities1())
            {
                // Since TransactionDate is DateTime type
                return db.TransactionHeaders
                    .Where(th => th.TransactionStatus == "Done"
                        && th.TransactionDate >= startDate
                        && th.TransactionDate <= endDate)
                    .ToList();
            }
        }

        public static List<TransactionHeader> GetTransactionsByStatus(string status)
        {
            using (var db = new DatabaseEntities1())
            {
                return db.TransactionHeaders
                    .Where(th => th.TransactionStatus == status)
                    .ToList();
            }
        }
    }
}
