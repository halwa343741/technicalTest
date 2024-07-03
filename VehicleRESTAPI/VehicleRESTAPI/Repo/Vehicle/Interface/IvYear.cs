using VehicleRESTAPI.VehicleModel;

namespace VehicleRESTAPI.Repo.Vehicle.Interface
{
    public interface IvYear
    {
        Task<VYearModel?> create(VYearModel data);
        Task<bool> delete(int id);
        bool isYearExists(VYearModel data);
        IQueryable<VYearModel> read();
        Task<VYearModel?> update(VYearModel data);
    }
}