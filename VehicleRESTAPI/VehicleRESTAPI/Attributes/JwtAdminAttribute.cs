using Microsoft.AspNetCore.Mvc;
using VehicleRESTAPI.Attributes.Filter;

namespace VehicleRESTAPI.Attributes
{
    public class JwtAdminAttribute : TypeFilterAttribute
    {
        public JwtAdminAttribute() : base(typeof(JwtAdminFilter)) {}
    }
}
