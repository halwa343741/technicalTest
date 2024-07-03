using VehicleRESTAPI.VehicleModel;

namespace VehicleRESTAPI.Repo.Vehicle.Interface
{
    public interface IvPricelist
    {
        Task<VPricelistModel?> create(VPricelistModel data);
        Task<bool> delete(int id);
        bool isCodeExists(VPricelistModel data);
        IQueryable<VPricelistModel> read();
        Task<VPricelistModel?> update(VPricelistModel data);
    }
}