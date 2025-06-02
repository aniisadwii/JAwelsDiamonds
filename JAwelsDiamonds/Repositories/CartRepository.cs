using JAwelsDiamonds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JAwelsDiamonds.Repositories
{
    public class CartRepository
    {
        private readonly DatabaseEntities1 _db;

        public CartRepository(DatabaseEntities1 db)
        {
            _db = db;
        }

        public void AddOrUpdateCart(int userId, int jewelId, int quantity)
        {
            var existingItem = _db.Carts.FirstOrDefault(c => c.UserID == userId && c.JewelID == jewelId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _db.Carts.Add(new Cart { UserID = userId, JewelID = jewelId, Quantity = quantity });
            }

            _db.SaveChanges();
        }
    }
}