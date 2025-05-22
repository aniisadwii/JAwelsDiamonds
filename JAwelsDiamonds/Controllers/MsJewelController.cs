using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using JAwelsDiamonds.Handlers;
using JAwelsDiamonds.Models;
using JAwelsDiamonds.Module;

namespace JAwelsDiamonds.Controllers
{
    public class MsJewelController
    {
        public static Response<MsJewel> GetJewel(int JewelID)
        {
            return MsJewelHandler.GetJewel(JewelID);
        }

        public static Response<MsJewel> UpdateJewel(int JewelID, string jewelName, int categoryId, int brandId, string price, string releaseYear)
        {
            int priceInt;
            int releaseYearInt;

            // Validate Jewel Name
            if (string.IsNullOrWhiteSpace(jewelName))
            {
                return new Response<MsJewel>()
                {
                    Success = false,
                    Message = "Jewel name is required!",
                    Payload = null
                };
            }
            else if (jewelName.Length < 3 || jewelName.Length > 25)
            {

                return new Response<MsJewel>()
                {
                    Success = false,
                    Message = "Jewel name must be between 3-25 characters!",
                    Payload = null
                };
            }

            // Validate Category
            if (categoryId == -1)
            {
                return new Response<MsJewel>()
                {
                    Success = false,
                    Message = "Please select a category!",
                    Payload = null
                };
            }
            // Validate Brand
            if (brandId == -1)
            {

                return new Response<MsJewel>()
                {
                    Success = false,
                    Message = "Please select a brand!",
                    Payload = null
                };
            }
            // Validate Price
            if (!int.TryParse(price, out priceInt))
            {
                return new Response<MsJewel>()
                {
                    Success = false,
                    Message = "Price must be a valid number!",
                    Payload = null
                };
            }
            else if (priceInt <= 25)
            {
                return new Response<MsJewel>()
                {
                    Success = false,
                    Message = "Price must be more than $25!",
                    Payload = null
                };
            }
            // Validate Release Year
            if (!int.TryParse(releaseYear, out releaseYearInt))
            {
                return new Response<MsJewel>()
                {
                    Success = false,
                    Message = "Release year must be a valid number!",
                    Payload = null
                };
            }
            else if (releaseYearInt > DateTime.Now.Year)
            {
                return new Response<MsJewel>()
                {
                    Success = false,
                    Message = $"Release year must be {DateTime.Now.Year} or earlier!",
                    Payload = null
                };
            }
            return MsJewelHandler.UpdateJewel(JewelID, jewelName, categoryId, brandId, priceInt, releaseYearInt);

        }
    }
}