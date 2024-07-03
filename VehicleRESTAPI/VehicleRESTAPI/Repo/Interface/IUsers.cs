using VehicleRESTAPI.Models;

namespace VehicleRESTAPI.Repo.Interface
{
    public interface IUsers
    {
        Task<UsersModel?> create(UsersModel data);
        Task<bool> delete(int id);
        bool isUsersExists(UsersModel data);
        IQueryable<UsersModel> read();
        Task<UsersModel?> update(UsersModel data);
    }
}