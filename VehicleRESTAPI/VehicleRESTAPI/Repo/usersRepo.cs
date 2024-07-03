using VehicleRESTAPI.Context;
using VehicleRESTAPI.Models;
using VehicleRESTAPI.Repo.Interface;

namespace VehicleRESTAPI.Repo
{
    public class usersRepo : IUsers
    {
        private readonly AppDbContext db;

        public usersRepo(AppDbContext db) {
            this.db = db;
        }

        public async Task<UsersModel?> create(UsersModel data) {
            try {
                data.created_at = data.updated_at = DateTime.UtcNow;
                db.Users.Add(data);
                await db.SaveChangesAsync();
                return data;
            }
            catch {
                return null;
            }
        }

        public IQueryable<UsersModel> read() {
            return db.Users;
        }

        public async Task<UsersModel?> update(UsersModel data) {
            try {
                var old = read().SingleOrDefault(d => d.id.Equals(data.id));
                if (old == null) return null;

                old.name = data.name;
                old.is_admin = data.is_admin;
                old.updated_at = DateTime.UtcNow;

                db.Users.Update(old);
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

                db.Users.Remove(old);
                return await db.SaveChangesAsync() > 0;
            }
            catch {
                return false;
            }
        }

        public bool isUsersExists(UsersModel data) {
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
