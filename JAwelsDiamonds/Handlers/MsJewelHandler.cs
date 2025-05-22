using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JAwelsDiamonds.Models;
using JAwelsDiamonds.Module;
using JAwelsDiamonds.Repositories;

namespace JAwelsDiamonds.Handlers
{
    internal class MsJewelHandler
    {
        public static Response<MsJewel> GetJewel(int JewelID)
        {
            MsJewel jewel = MsJewelRepository.getJewel(JewelID);

            if (jewel == null)
            {
                return new Response<MsJewel>
                {
                    Success = false,
                    Message = "Not Found",
                    Payload = null
                };

            }
            return new Response<MsJewel>
            {
                Success = true,
                Message = "Jewel Retrieved Successfully",
                Payload = jewel

            };
        }

        public static Response<MsJewel> UpdateJewel(int JewelID, string jewelName, int categoryId, int brandId, int price, int releaseYear)
        {
            MsJewel jewel = MsJewelRepository.getJewel(JewelID);

            if (jewel == null)
            {
                return new Response<MsJewel>
                {
                    Success = false,
                    Message = "Not Found",
                    Payload = null
                };

            }
            MsJewelRepository.UpdateJewel(jewel, jewelName, categoryId, brandId, price, releaseYear);

            return new Response<MsJewel>
            {
                Success = true,
                Message = "Jewel Updated Successfully",
                Payload = jewel

            };


        }
    }
}