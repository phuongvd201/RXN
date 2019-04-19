using System;
using System.Collections.Generic;

using Rxn.Common;
using Rxn.Dtos;
using Rxn.WebApi.Models;

namespace Rxn.WebApi.Helpers
{
    public static class ConvertFromModelHelper
    {
        public static ItemMasterDto ToItemMasterDto(this ItemMasterModel model)
        {
            return model == null
                ? null
                : new ItemMasterDto
                {
                    ItemMasterID = model.ItemMasterID,
                    IMPack = model.IMPack,
                    IMDescription = model.IMDescription,
                    IMImageData = string.IsNullOrWhiteSpace(model.IMImageData) ? null : Convert.FromBase64String(model.IMImageData),
                    IMIsHazardousMaterial = model.IMIsHazardousMaterial.ParseBool(),
                    IMExpirationDate = model.IMExpirationDate.ParseNullableDate(),
                    IMUnitPrice = model.IMUnitPrice.ParseNullableDecimal(),
                    IMWidth = model.IMWidth.ParseNullableDecimal(),
                    IMLength = model.IMLength.ParseNullableDecimal(),
                    IMHeight = model.IMHeight.ParseNullableDecimal(),
                    IMIsPrePack = model.IMIsPrePack.ParseBool(),
                    IMPrePackStyle = model.IMPrePackStyle,
                    IMCostCenterCode = model.IMCostCenterCode,
                };
        }

        public static DictionaryItem ToDictionaryItem(this KeyValuePair<int, string> pair)
        {
            return new DictionaryItem
            {
                Id = pair.Key,
                Name = pair.Value,
            };
        }
    }
}