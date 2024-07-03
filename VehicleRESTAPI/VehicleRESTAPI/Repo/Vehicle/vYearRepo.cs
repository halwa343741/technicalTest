using VehicleRESTAPI.Context;
using VehicleRESTAPI.Repo.Vehicle.Interface;
using VehicleRESTAPI.VehicleModel;

namespace VehicleRESTAPI.Repo.Vehicle
{
    public class vYearRepo : IvYear
    {
        private readonly AppDbContext db;

        public vYearRepo(AppDbContext db) {
            this.db = db;
        }

        public async Task<VYearModel?> create(VYearModel data) {
            try {
                data.created_at = data.updated_at = DateTime.UtcNow;
                db.VYear.Add(data);
                await db.SaveChangesAsync();
                return data;
            }
            catch {
                return null;
            }
        }

        public IQueryable<VYearModel> read() {
            return db.VYear;
        }

        public async Task<VYearModel?> update(VYearModel data) {
            try {
                var old = read().SingleOrDefault(d => d.id.Equals(data.id));
                if (old == null) return null;

                old.year = data.year;
                old.updated_at = DateTime.UtcNow;

                db.VYear.Update(old);
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

                db.VYear.Remove(old);
                return await db.SaveChangesAsync() > 0;
            }
            catch {
                return false;
            }
        }

        public bool isYearExists(VYearModel data) {
            try {
                var old = read()
                    .SingleOrDefault(d => !d.id.Equals(data.id) && d.year.Equals(data.year));
                return old != null;
            }
            catch {
                return true;
            }
        }
    }
}
