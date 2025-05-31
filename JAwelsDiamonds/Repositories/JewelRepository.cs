using JAwelsDiamonds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JAwelsDiamonds.Repositories
{
    public class JewelRepository
    {

        public static MsJewel GetJewelById(int jewelId)
        {
            using (var db = new DatabaseEntities1())
            {
                return db.MsJewels.FirstOrDefault(j => j.JewelID == jewelId);
            }
        }

        public static bool UpdateJewel(MsJewel updatedJewel)
        {
            using (var db = new DatabaseEntities1())
            {
                var jewel = db.MsJewels.FirstOrDefault(j => j.JewelID == updatedJewel.JewelID);
                if (jewel != null)
                {
                    jewel.JewelName = updatedJewel.JewelName;
                    jewel.CategoryID = updatedJewel.CategoryID;
                    jewel.BrandID = updatedJewel.BrandID;
                    jewel.JewelPrice = updatedJewel.JewelPrice;
                    jewel.JewelReleaseYear = updatedJewel.JewelReleaseYear;

                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}