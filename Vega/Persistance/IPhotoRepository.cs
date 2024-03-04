using Vega.Models;

namespace Vega.Persistance
{
    public interface IPhotoRepository
    {
        Task<List<Photo>> GetPhotos(int VehicleId);
    }
}
