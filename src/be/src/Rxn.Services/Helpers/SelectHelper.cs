using System.Linq;

using Rxn.Dtos;
using Rxn.EntityFramework.DbEntities;

namespace Rxn.Services.Helpers
{
    internal static class SelectHelper
    {
        public static IQueryable<ItemMasterDto> ProjectToDto(this IQueryable<ItemMaster> query)
        {
            return query.Select(
                x => new ItemMasterDto
                {
                    ItemMasterID = x.ItemMasterID,
                    IMPack = x.IMPack,
                    IMDescription = x.IMDescription,
                    IMImageData = x.IMImageData,
                    IMIsHazardousMaterial = x.IMIsHazardousMaterial,
                    IMExpirationDate = x.IMExpirationDate,
                    IMUnitPrice = x.IMUnitPrice,
                    IMWidth = x.IMWidth,
                    IMLength = x.IMLength,
                    IMHeight = x.IMHeight,
                    IMIsPrePack = x.IMIsPrePack,
                    IMPrePackStyle = x.IMPrePackStyle,
                    IMCostCenterCode = x.IMCostCenterCode,
                });
        }

        public static IQueryable<ItemMasterInventoryDto> ProjectToDto(this IQueryable<ItemMasterInventory> query)
        {
            return query.Select(
                x => new ItemMasterInventoryDto
                {
                    IMIItemMasterID = x.IMIItemMasterID,
                    IMIQtyAllocated = x.IMIQtyAllocated,
                    IMIQtyOnHand = x.IMIQtyOnHand,
                    IMISiteID = x.IMISiteID,
                });
        }
    }
}