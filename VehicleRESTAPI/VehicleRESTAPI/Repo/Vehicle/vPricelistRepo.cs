using VehicleRESTAPI.Context;
using VehicleRESTAPI.Repo.Vehicle.Interface;
using VehicleRESTAPI.VehicleModel;

namespace VehicleRESTAPI.Repo.Vehicle
{
    public class vPricelistRepo : IvPricelist
    {
        private readonly AppDbContext db;

        public vPricelistRepo(AppDbContext db) {
            this.db = db;
        }

        public async Task<VPricelistModel?> create(VPricelistModel data) {
            try {
                data.created_at = data.updated_at = DateTime.UtcNow;
                db.VPricelist.Add(data);
                await db.SaveChangesAsync();
                return data;
            }
            catch {
                return null;
            }
        }

        public IQueryable<VPricelistModel> read() {
            return db.VPricelist;
        }

        public async Task<VPricelistModel?> update(VPricelistModel data) {
            try {
                var old = read().SingleOrDefault(d => d.id.Equals(data.id));
                if (old == null) return null;

                old.code = data.code;
                old.price = data.price;
                old.year_id = data.year_id;
                old.model_id = data.model_id;
                old.updated_at = DateTime.UtcNow;

                db.VPricelist.Update(old);
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

                db.VPricelist.Remove(old);
                return await db.SaveChangesAsync() > 0;
            }
            catch {
                return false;
            }
        }

        public bool isCodeExists(VPricelistModel data) {
            try {
                var old = read()
                    .SingleOrDefault(d => !d.id.Equals(data.id) && d.code.ToLower().Equals(data.code.ToLower()));
                return old != null;
            }
            catch {
                return true;
            }
        }
    }
}
