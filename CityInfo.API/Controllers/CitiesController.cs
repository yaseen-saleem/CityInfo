using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")] //when u use this you have to exclude ("api/cities") from HTTP Get methods.
    public class CitiesController:ControllerBase // if we use controller base class it will also give support of views which is not needed
    {
        private ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository ?? 
                throw new ArgumentNullException(nameof(cityInfoRepository));
        }
        [HttpGet]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();

           
            return Ok(cityEntities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            //find City
            var city = _cityInfoRepository.GetCity(id);
            
            if(city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }
    }
}
