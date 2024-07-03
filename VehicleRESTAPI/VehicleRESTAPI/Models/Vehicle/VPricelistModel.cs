using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleRESTAPI.Models;

namespace VehicleRESTAPI.VehicleModel
{
    [Table("pricelist")]
    public class VPricelistModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; internal set; }
        public string code { get; set; } = string.Empty;
        public decimal price { get; set; } = 0;
        public int year_id { get; set; } = 0;
        public int model_id { get; set; } = 0;
    }
}
