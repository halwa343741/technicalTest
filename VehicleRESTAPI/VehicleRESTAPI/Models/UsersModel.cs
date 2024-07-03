using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRESTAPI.Models
{
    [Table("users")]
    public class UsersModel : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; internal set; }
        public string name { get; set; } = string.Empty;
        public bool is_admin { get; set; } = false;
    }
}
