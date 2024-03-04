using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vega.Extensions;
using Vega.Models;

namespace Vega.Persistance
{
    public class VehicleRepository : IVehicleRepository
    {
        public VegaDbContext context { get; }
        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;

        }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
#pragma warning disable CS8603 // Possible null reference return.

            if (!includeRelated)
                return await context.Vehicles.FindAsync(id);

            return await context.Vehicles
            .Include(v => v.Features)
            .ThenInclude(vf => vf.Feature)
            .Include(v => v.Model)
            .ThenInclude(m => m.Make)
            .SingleOrDefaultAsync(v => v.Id == id);

#pragma warning restore CS8603 // Possible null reference return.

        }
        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj)
        {
#pragma warning disable CS8603 // Possible null reference return.
            var result = new QueryResult<Vehicle>();
            var query = context.Vehicles
            .Include(v => v.Features)
            .ThenInclude(vf => vf.Feature)
            .Include(v => v.Model)
            .ThenInclude(m => m.Make)
            .AsQueryable();

            if (queryObj.MakeId.HasValue)
            {
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);
            }
            var columnMap = new Dictionary<string, Expression<Func<Vehicle, object>>>
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"]= v => v.Model.Name,
                ["contactName"]= v => v.ContactName
            };

            query = query.ApplyOrdering(columnMap, queryObj);
            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items = await query.ToListAsync();


            return result;
#pragma warning restore CS8603 // Possible null reference return.

        }


        public async Task<Vehicle> GetVehicleWithFeatures(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.

            return await context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);

#pragma warning restore CS8603 // Possible null reference return.

        }



        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }

        

    }
}
