using VehicleRESTAPI.VehicleModel;

namespace VehicleRESTAPI.Repo.Vehicle.Interface
{
    public interface IvModel
    {
        Task<VModelModel?> create(VModelModel data);
        Task<bool> delete(int id);
        bool isModelExists(VModelModel data);
        IQueryable<VModelModel> read();
        Task<VModelModel?> update(VModelModel data);
    }
}