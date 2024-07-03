using VehicleRESTAPI.VehicleModel;

namespace VehicleRESTAPI.Repo.Vehicle.Interface
{
    public interface IvBrand
    {
        Task<VBrandModel?> create(VBrandModel data);
        Task<bool> delete(int id);
        bool isBrandExists(VBrandModel data);
        IQueryable<VBrandModel> read();
        Task<VBrandModel?> update(VBrandModel data);
    }
}