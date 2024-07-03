using VehicleRESTAPI.Context;
using VehicleRESTAPI.Models;
using VehicleRESTAPI.Repo.Vehicle.Interface;
using VehicleRESTAPI.VehicleModel;

namespace VehicleRESTAPI.Repo.Vehicle
{
    public class vBrandRepo : IvBrand
    {
        private readonly AppDbContext db;

        public vBrandRepo(AppDbContext db) {
            this.db = db;
        }

        public async Task<VBrandModel?> create(VBrandModel data) {
            try {
                data.created_at = data.updated_at = DateTime.UtcNow;
                db.VBrand.Add(data);
                await db.SaveChangesAsync();
                return data;
            }
            catch {
                return null;
            }
        }

        public IQueryable<VBrandModel> read() {
            return db.VBrand;
        }

        public async Task<VBrandModel?> update(VBrandModel data) {
            try {
                var old = read().SingleOrDefault(d => d.id.Equals(data.id));
                if (old == null) return null;

                old.name = data.name;
                old.updated_at = DateTime.UtcNow;

                db.VBrand.Update(old);
                await db.SaveChangesAsync();
                return old;
            }
            catch {
                return null;
            }
        }

        public async Task<bool> delete(int id) {
            try {
                var old = read().SingleOrDefault(d => d.id.Equals(id));
                if (old == null) return false;

                db.VBrand.Remove(old);
                return await db.SaveChangesAsync() > 0;
            }
            catch {
                return false;
            }
        }

        public bool isBrandExists(VBrandModel data) {
            try {
                var old = read()
                    .SingleOrDefault(d => !d.id.Equals(data.id) && d.name.ToLower().Equals(data.name.ToLower()));
                return old != null;
            }
            catch {
                return true;
            }
        }
    }
}
