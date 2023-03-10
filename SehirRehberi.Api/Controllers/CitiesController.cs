using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SehirRehberi.Api.Dtos;
using SehirRehberi.Business.Abstract;
using SehirRehberi.Entity.Concrete;

namespace SehirRehberi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private ICityService _cityService;
        private IPhotoService _photoService;
        private IMapper _mapper;


        public CitiesController(ICityService cityService,IPhotoService photoService ,IMapper mapper)
        {
            _cityService = cityService;
            _photoService = photoService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetCities()
        {
            var cities = _cityService.GetAllWithPhotos();
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);
            return Ok(citiesToReturn);
        }
        [HttpPost]
        public IActionResult Add([FromBody] City city) 
        {
            _cityService.Add(city);
            return Ok(city);
        }
        [HttpGet("{cityId}")]
        public IActionResult GetCity(int cityId)
        {
            var city = _cityService.GetWithPhotos(c=>c.CityId== cityId);
            var cityToReturn = _mapper.Map<CityForDetailDto>(city);
            return Ok(cityToReturn);
        }
        [HttpGet("{cityId}")]
        public IActionResult GetPhotosByCity(int cityId) 
        {
            var photos = _photoService.GetAll(p=>p.CityId==cityId);
            return Ok(photos);
        }

    }
}
