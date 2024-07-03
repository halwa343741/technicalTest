using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleRESTAPI.Models;

namespace VehicleRESTAPI.VehicleModel
{
    [Table("vehicle_year")]
    public class VYearModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; internal set; }
        public int year { get; set; } = 0;
    }
}
