using VehicleRESTAPI.Context;
using VehicleRESTAPI.Repo.Vehicle.Interface;
using VehicleRESTAPI.VehicleModel;

namespace VehicleRESTAPI.Repo.Vehicle
{
    public class vModelRepo : IvModel
    {
        private readonly AppDbContext db;

        public vModelRepo(AppDbContext db) {
            this.db = db;
        }

        public async Task<VModelModel?> create(VModelModel data) {
            try {
                data.created_at = data.updated_at = DateTime.UtcNow;
                db.VModel.Add(data);
                await db.SaveChangesAsync();
                return data;
            }
            catch {
                return null;
            }
        }

        public IQueryable<VModelModel> read() {
            return db.VModel;
        }

        public async Task<VModelModel?> update(VModelModel data) {
            try {
                var old = read().SingleOrDefault(d => d.id.Equals(data.id));
                if (old == null) return null;

                old.name = data.name;
                old.type_id = data.type_id;
                old.updated_at = DateTime.UtcNow;

                db.VModel.Update(old);
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

                db.VModel.Remove(old);
                return await db.SaveChangesAsync() > 0;
            }
            catch {
                return false;
            }
        }

        public bool isModelExists(VModelModel data) {
            try {
                var old = read()
                    .SingleOrDefault(d =>
                        !d.id.Equals(data.id) &&
                        d.name.ToLower().Equals(data.name.ToLower()) &&
                        d.type_id.Equals(data.type_id));
                return old != null;
            }
            catch {
                return true;
            }
        }
    }
}
