using Microsoft.AspNetCore.Mvc;
using VehicleRESTAPI.Attributes.Filter;

namespace VehicleRESTAPI.Attributes
{
    public class JwtAttribute : TypeFilterAttribute
    {
        public JwtAttribute() : base(typeof(JwtFilter)) { }
    }
}
