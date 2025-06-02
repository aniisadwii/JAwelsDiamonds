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
    }
}
