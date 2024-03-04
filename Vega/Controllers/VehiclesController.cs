using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vega.Controllers.Resources;
using Vega.Models;
using Vega.Persistance;
using static Vega.Controllers.Resources.VehicleResource;

namespace Vega.Controllers
{
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public VehiclesController(
            IMapper mapper,
            IVehicleRepository repository,
            IUnitOfWork unitOfWork
            )
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            repository.Add(vehicle);
            await unitOfWork.CompleteAsync();

            await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await repository.GetVehicleWithFeatures(id);
            if (vehicle == null)
                return NotFound();

            mapper.Map(saveVehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;
            await unitOfWork.CompleteAsync();

            await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }


        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id, includeRelated: false);
            if (vehicle == null)
                return NotFound();

            repository.Remove(vehicle);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();

            var SaveVehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(SaveVehicleResource);
        }

        [HttpGet]
        public async Task<QueryResultResource<VehicleResource>> GetVehicles(VehicleQueryResource filterResource)
        {
            var filter = mapper.Map<VehicleQueryResource, VehicleQuery>(filterResource);
            var queryResult = await repository.GetVehicles(filter);

            return mapper.Map<QueryResult<Vehicle>,QueryResultResource<VehicleResource>>(queryResult);
        }
    }


}
