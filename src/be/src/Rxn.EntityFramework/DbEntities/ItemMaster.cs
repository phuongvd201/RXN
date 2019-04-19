using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rxn.EntityFramework.DbEntities
{
    public class ItemMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemMasterID { get; set; }

        public int? IMPack { get; set; }

        public string IMDescription { get; set; }

        public byte[] IMImageData { get; set; }

        public bool IMIsHazardousMaterial { get; set; }

        public DateTime? IMExpirationDate { get; set; }

        public decimal? IMUnitPrice { get; set; }

        public decimal? IMWidth { get; set; }

        public decimal? IMLength { get; set; }

        public decimal? IMHeight { get; set; }

        public bool IMIsPrePack { get; set; }

        public string IMPrePackStyle { get; set; }

        public string IMCostCenterCode { get; set; }
    }
}