using Rxn.Dtos;

namespace Rxn.Services
{
    public interface IProductService
    {
        ItemMasterDto GetItemMaster(int id);

        int[] GetProductNumbers();

        ItemMasterInventoryDto[] GetItemMasterInventories(int itemMasterID);

        void CreateOrUpdateItemMaster(ItemMasterDto model);
    }
}