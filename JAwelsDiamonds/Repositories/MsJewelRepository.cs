using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JAwelsDiamonds.Models;

namespace JAwelsDiamonds.Repositories
{
    public class MsJewelRepository
    {
        private static DatabaseEntities1 db = new DatabaseEntities1();

        public static void UpdateJewel(MsJewel jewel, string jewelName, int categoryId, int brandId, int price, int releaseYear)
        {
            jewel.JewelName = jewelName;
            jewel.CategoryID = categoryId;
            jewel.BrandID = brandId;
            jewel.JewelPrice = price;
            jewel.JewelReleaseYear = releaseYear;
            db.SaveChanges();
        }


        public static MsJewel getJewel(int jewelID)
        {
            return db.MsJewels.Find(jewelID);
        }
    }
}