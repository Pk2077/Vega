using Microsoft.AspNetCore.Mvc;
using Vega.Controllers.Resources;
using Vega.Models;
using Vega.Persistance;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace Vega.Controllers
{
    [Route("api/vehicles/{vehicleId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IVehicleRepository _repository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoRepository photoRepository;
        private readonly PhotoSettings photoSettings;

        public PhotosController(
            IVehicleRepository repository, 
            IWebHostEnvironment hostEnvironment, 
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            IOptionsSnapshot<PhotoSettings> options,
            IPhotoRepository photoRepository
            )
        {
            _repository = repository;
            _hostEnvironment = hostEnvironment;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.photoRepository = photoRepository;
            photoSettings = options.Value;
        }
        [HttpGet]
        public async Task<IEnumerable<PhotosResource>> GetPhoto(int VehicleId)
        {
            var Photo = await photoRepository.GetPhotos(VehicleId);
            return _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotosResource>>(Photo);
        }


        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            if (file == null)
                return BadRequest();
            if (file.Length == 0)
                return BadRequest("Empty");
            if (file.Length > photoSettings.Max_Bytes)
                return BadRequest("Too Large");
            if (!photoSettings.IsSupported(file.FileName))
                return BadRequest("Invalid Type");
            var vehicle = await _repository.GetVehicle(vehicleId, includeRelated: false);

            if (vehicle == null)
            {
                return NotFound();
            }

            var uploadFilePath = Path.Combine(_hostEnvironment.WebRootPath, "Uploads");
            if (!Directory.Exists(uploadFilePath))
            {
                Directory.CreateDirectory(uploadFilePath);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadFilePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<Photo, PhotosResource>(photo));
        }

    }
}
