using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vega.Persistance;
using static Vega.Controllers.Resources.VehicleResource;

namespace Vega.Controllers
{
    [ApiController]
    [Route("api/features")]
    public class FeaturesController : ControllerBase
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;

        public FeaturesController(VegaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<KeyValuePairResource> GetFeatures()
        {
            var Features = _context.Features.ToList();
            var FeatureResources = _mapper.Map<List<KeyValuePairResource>>(Features);
            return FeatureResources;
        }
    }


}
