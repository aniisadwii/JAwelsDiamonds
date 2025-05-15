using JAwelsDiamonds.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JAwelsDiamonds.Handlers
{
    // CartHandler.cs
    public class CartHandler
    {
        private readonly CartRepository _cartRepo;

        public CartHandler(CartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public bool AddToCart(int userId, int jewelId, int quantity)
        {
            try
            {
                _cartRepo.AddOrUpdateCart(userId, jewelId, quantity);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}