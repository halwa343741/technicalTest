using Microsoft.EntityFrameworkCore;
using VehicleRESTAPI.Models;
using VehicleRESTAPI.VehicleModel;

namespace VehicleRESTAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<UsersModel> Users { get; set; }
        public DbSet<VBrandModel> VBrand { get; set; }
        public DbSet<VTypeModel> VType { get; set; }
        public DbSet<VModelModel> VModel { get; set; }
        public DbSet<VYearModel> VYear { get; set; }
        public DbSet<VPricelistModel> VPricelist { get; set; }
    }
}
