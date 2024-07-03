using System.Text.Json.Serialization;

namespace VehicleRESTAPI.Models
{
    public class BaseModel
    {
        public DateTime created_at { get; internal set; } = DateTime.UtcNow;
        public DateTime updated_at { get; internal set; } = DateTime.UtcNow;
    }
}
