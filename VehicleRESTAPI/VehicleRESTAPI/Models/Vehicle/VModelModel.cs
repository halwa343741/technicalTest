using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleRESTAPI.Models;

namespace VehicleRESTAPI.VehicleModel
{
    [Table("vehicle_model")]
    public class VModelModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; internal set; }
        public string name { get; set; } = string.Empty;
        public int type_id { get; set; }
    }

}
