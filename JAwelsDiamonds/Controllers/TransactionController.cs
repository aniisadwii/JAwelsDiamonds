using JAwelsDiamonds.Handlers;
using JAwelsDiamonds.Models;
using System;
using System.Collections.Generic;

namespace JAwelsDiamonds.Controllers
{
    public class TransactionController
    {
        public List<TransactionHeader> GetAllTransactions()
        {
            try
            {
                return TransactionHandler.GetAllTransactions();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting transactions: " + ex.Message);
            }
        }

        public TransactionHeader GetTransactionDetails(int transactionId)
        {
            try
            {
                return TransactionHandler.GetTransactionWithDetails(transactionId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting transaction details: " + ex.Message);
            }
        }

        public List<TransactionHeader> GetDoneTransactions()
        {
            try
            {
                return TransactionHandler.GetDoneTransactions();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting done transactions: " + ex.Message);
            }
        }
    }
}