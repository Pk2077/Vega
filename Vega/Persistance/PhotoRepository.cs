using Microsoft.EntityFrameworkCore;
using System;
using Vega.Models;

namespace Vega.Persistance
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly VegaDbContext context;

        public PhotoRepository(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Photo>> GetPhotos(int VehicleId)
        {
            return await context.Photos.Where(p => p.VehicleId == VehicleId).ToListAsync();

        }
    }
}
