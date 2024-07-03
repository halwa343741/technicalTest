using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleRESTAPI.Models;

namespace VehicleRESTAPI.VehicleModel
{
    [Table("vehicle_brand")]
    public class VBrandModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; internal set; }
        public string name { get; set; } = string.Empty;
    }
}
