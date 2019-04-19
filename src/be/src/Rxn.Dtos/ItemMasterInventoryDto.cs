namespace Rxn.Dtos
{
    public class ItemMasterInventoryDto
    {
        public int IMIItemMasterID { get; set; }

        public int IMISiteID { get; set; }

        public string IMISiteName { get; set; }

        public int IMIQtyOnHand { get; set; }

        public int IMIQtyAllocated { get; set; }
    }
}