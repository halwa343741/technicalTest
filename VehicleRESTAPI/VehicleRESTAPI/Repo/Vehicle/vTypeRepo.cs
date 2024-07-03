using VehicleRESTAPI.Context;
using VehicleRESTAPI.Repo.Vehicle.Interface;
using VehicleRESTAPI.VehicleModel;

namespace VehicleRESTAPI.Repo.Vehicle
{
    public class vTypeRepo : IvType
    {
        private readonly AppDbContext db;

        public vTypeRepo(AppDbContext db) {
            this.db = db;
        }

        public async Task<VTypeModel?> create(VTypeModel data) {
            try {
                data.created_at = data.updated_at = DateTime.UtcNow;
                db.VType.Add(data);
                await db.SaveChangesAsync();
                return data;
            }
            catch {
                return null;
            }
        }

        public IQueryable<VTypeModel> read() {
            return db.VType;
        }

        public async Task<VTypeModel?> update(VTypeModel data) {
            try {
                var old = read().SingleOrDefault(d => d.id.Equals(data.id));
                if (old == null) return null;

                old.name = data.name;
                old.brand_id = data.brand_id;
                old.updated_at = DateTime.UtcNow;

                db.VType.Update(old);
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

                db.VType.Remove(old);
                return await db.SaveChangesAsync() > 0;
            }
            catch {
                return false;
            }
        }

        public bool isTypeExists(VTypeModel data) {
            try {
                var old = read()
                    .SingleOrDefault(d => 
                        !d.id.Equals(data.id) && 
                        d.name.ToLower().Equals(data.name.ToLower()) && 
                        d.brand_id.Equals(data.brand_id));
                return old != null;
            }
            catch {
                return true;
            }
        }
    }
}
