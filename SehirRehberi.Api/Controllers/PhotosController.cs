using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SehirRehberi.Api.Dtos;
using SehirRehberi.Api.Helpers;
using SehirRehberi.Business.Abstract;
using SehirRehberi.Entity.Concrete;
using System.Security.Claims;

namespace SehirRehberi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private IPhotoService _photoService;
        private ICityService _cityService;
        private IMapper _mapper;
        private IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(IPhotoService photoService,ICityService cityService, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _photoService = photoService;
            _cityService = cityService;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;
            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret);
           // _cloudinary = new Cloudinary(account);
        }
        [HttpPost("{cityId}")]
        public IActionResult AddPhotoForCity(int cityId, [FromForm]PhotoForCreationDto photoForCreationDto)
        {
            var city = _cityService.GetWithPhotos(c=>c.CityId==cityId);
            if (city == null) return BadRequest("Could not find the city");
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (currentUserId != city.UserId) return Unauthorized();
            var file = photoForCreationDto.File;
            var uploadResult = new ImageUploadResult();
            if (file.Length>0)
            {
                using (var stream=file.OpenReadStream()) 
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;
            var photo = _mapper.Map<Photo>(photoForCreationDto);
            photo.CityId = city.CityId;
            if (!city.Photos.Any(p => p.IsMain)) photo.IsMain = true;
            _photoService.Add(photo);
            var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
            return CreatedAtRoute("GetPhoto",new {id=photo.PhotoId},photoToReturn);
        }
        [HttpGet("{id}")]
        public IActionResult GetPhoto(int id)
        {
            var photoFromDb = _photoService.Get(p=>p.PhotoId==id);
            var photo = _mapper.Map<PhotoForReturnDto>(photoFromDb);
            return Ok(photo);
        }
    }
}
