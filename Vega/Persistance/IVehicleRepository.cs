using Vega.Models;

namespace Vega.Persistance
{
    public interface IVehicleRepository
    {
        Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery filter);
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        Task<Vehicle> GetVehicleWithFeatures(int id);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}
