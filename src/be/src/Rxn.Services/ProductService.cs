using System;
using System.Linq;

using Rxn.Common;
using Rxn.Dtos;
using Rxn.EntityFramework.DbEntities;
using Rxn.EntityFramework.Repositories;
using Rxn.Services.Helpers;

namespace Rxn.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<ItemMaster> _itemMasterRepository;
        private readonly IRepository<ItemMasterInventory> _itemMasterInventoryRepository;

        public ProductService(
            IRepository<ItemMaster> itemMasterRepository,
            IRepository<ItemMasterInventory> itemMasterInventoryRepository)
        {
            _itemMasterRepository = itemMasterRepository;
            _itemMasterInventoryRepository = itemMasterInventoryRepository;
        }

        public ItemMasterDto GetItemMaster(int id)
        {
            return _itemMasterRepository
                .All()
                .Where(x => x.ItemMasterID == id)
                .ProjectToDto()
                .FirstOrDefault();
        }

        public int[] GetProductNumbers()
        {
            return _itemMasterRepository
                .All()
                .Select(x => x.ItemMasterID)
                .MakeQueryToDatabase();
        }

        public ItemMasterInventoryDto[] GetItemMasterInventories(int itemMasterID)
        {
            return _itemMasterInventoryRepository
                .All()
                .Where(x => x.IMIItemMasterID == itemMasterID)
                .ProjectToDto()
                .MakeQueryToDatabase()
                .Select(
                    x =>
                    {
                        x.IMISiteName = TempData.Locations.ContainsKey(x.IMISiteID) ? TempData.Locations[x.IMISiteID] : string.Empty;
                        return x;
                    })
                .ToArray();
        }

        public void CreateOrUpdateItemMaster(ItemMasterDto model)
        {
            var itemMasterFromDb = _itemMasterRepository
                                       .All()
                                       .FirstOrDefault(x => x.ItemMasterID == model.ItemMasterID) ?? new ItemMaster();

            itemMasterFromDb.IMPack = model.IMPack;
            itemMasterFromDb.IMDescription = model.IMDescription;
            itemMasterFromDb.IMImageData = model.IMImageData;
            itemMasterFromDb.IMIsHazardousMaterial = model.IMIsHazardousMaterial;
            itemMasterFromDb.IMExpirationDate = model.IMExpirationDate;
            itemMasterFromDb.IMUnitPrice = model.IMUnitPrice;
            itemMasterFromDb.IMWidth = model.IMWidth;
            itemMasterFromDb.IMLength = model.IMLength;
            itemMasterFromDb.IMHeight = model.IMHeight;
            itemMasterFromDb.IMIsPrePack = model.IMIsPrePack;
            itemMasterFromDb.IMPrePackStyle = model.IMPrePackStyle;
            itemMasterFromDb.IMCostCenterCode = model.IMCostCenterCode;

            if (itemMasterFromDb.ItemMasterID <= 0)
            {
                itemMasterFromDb.ItemMasterID = model.ItemMasterID;
                _itemMasterRepository.Add(itemMasterFromDb);
            }

            _itemMasterRepository.UnitOfWork.Commit();
        }
    }
}