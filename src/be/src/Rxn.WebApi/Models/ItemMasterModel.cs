namespace Rxn.WebApi.Models
{
    public class ItemMasterModel
    {
        public int ItemMasterID { get; set; }

        public int IMPack { get; set; }

        public string IMDescription { get; set; }

        public string IMImageData { get; set; }

        public string IMIsHazardousMaterial { get; set; }

        public string IMExpirationDate { get; set; }

        public string IMUnitPrice { get; set; }

        public string IMWidth { get; set; }

        public string IMLength { get; set; }

        public string IMHeight { get; set; }

        public string IMIsPrePack { get; set; }

        public string IMPrePackStyle { get; set; }

        public string IMCostCenterCode { get; set; }
    }
}