using System.ComponentModel.DataAnnotations;

namespace Rxn.EntityFramework.DbEntities
{
    public class ItemMasterInventory
    {
        [Key]
        public int ItemMasterInventoryID { get; set; }

        public int IMIItemMasterID { get; set; }

        public int IMISiteID { get; set; }

        public int IMIQtyOnHand { get; set; }

        public int IMIQtyAllocated { get; set; }
    }
}