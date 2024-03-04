using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Models;
using Vega.Persistance;
using static Vega.Controllers.Resources.VehicleResource;

namespace Vega.Controllers
{
    [ApiController]
    [Route("api/makes")]
    public class MakeController : ControllerBase
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;

        public MakeController(VegaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IEnumerable<MakeResources> GetMakes()
        {
            var makes = _context.Makes.Include(m => m.Models).ToList();
            var makeResources = _mapper.Map<List<Make>, List<MakeResources>>(makes);
            return makeResources;
        }
    }


}
