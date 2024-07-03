using VehicleRESTAPI.VehicleModel;

namespace VehicleRESTAPI.Repo.Vehicle.Interface
{
    public interface IvType
    {
        Task<VTypeModel?> create(VTypeModel data);
        Task<bool> delete(int id);
        bool isTypeExists(VTypeModel data);
        IQueryable<VTypeModel> read();
        Task<VTypeModel?> update(VTypeModel data);
    }
}