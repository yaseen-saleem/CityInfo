using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    //this class is used as a child of city child resource. 
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")] //pos not make a sense without city so after city id you have to pass pointofintreerst
    //when u use this you have to exclude ("api/cities") from HTTP Get methods.
    public class PointsOfInterestController: ControllerBase // if we use controller base class it will also give support of views which is not needed
    {
        private ICityInfoRepository _cityInfoRepository;


        //using constructor injection here
        public PointsOfInterestController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository ??
                throw new ArgumentNullException(nameof(cityInfoRepository));
        }

        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            
            try {
                //here a json based object comes in city
                var pointsOfInterestForCity = _cityInfoRepository.GetPointsOfInterestForCity(cityId);

                if (pointsOfInterestForCity == null)
                {

                    return NotFound();
                }
                return Ok(pointsOfInterestForCity);
            }
            catch (Exception)
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

        }

        //this method will also give a specific point of intererst of a city
        //http://localhost:1028/api/cities/1/pointsofinterest/1 //this will give you gulshan park
        [HttpGet("{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointsOfInterest(int cityId,int id)
        {
            if(!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }
            
            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }

        [HttpPost]
        //2nd parameter.. the request will contain data for pointofinterest which we have to convert to CreatePointOfInterest so we desiarilize it
        //Method to add a new PointsOfInterest to a city
        public IActionResult CreatePointOfInterest(int cityId,
         [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            //ModelState is a dictionary containing Both State of the Model and model Binding validations
            //this will alfo false when a model of property type is passed
            //modelstate check the parameters which come in our Action
            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError(
                    "Description",
                    "The Provided Description should not be same as the Name"
                    );
            }

            if (!ModelState.IsValid) //asure data is desirialize in the response body
            {
                return BadRequest(ModelState);
            }
            
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            //we have to calculate the ID of the New Point of interest.our datastore currently working on PointsOfInterestDTO not PointOfInterestForCreationDto
            //loop through all of the ID's of the PointOfInterests of all cities. find the highest id then create an ID for new PointOfInterest by adding 1 in that Highest ID.
            //highest Id come in maxPointOfInteresId
            var maxPointOfInteresId = CitiesDataStore.Current.Cities.SelectMany(
                c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = maxPointOfInteresId + 1,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description

            };
            //insert new PointsOfInterest to the city
            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute(
                "GetPointOfInterest",
                new { cityId = cityId, id = finalPointOfInterest.Id },
                finalPointOfInterest);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            //ModelState is a dictionary containing Both State of the Model and model Binding validations
            //this will alfo false when a model of property type is passed
            //modelstate check the parameters which come in our Action
            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError(
                    "Description",
                    "The Provided Description should not be same as the Name"
                    );
            }
            if (!ModelState.IsValid) //asure data is desirialize in the response body
            {
                return BadRequest(ModelState);
            }

            //check for a user cannot add a PointOfInterest if city is not existing 
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }
            //writing logic to fully update a request according to HTTP principle PUT is used to fully update the resource
            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterestFromStore.Description;
            return NoContent(); //this means request is run properly and no result needed
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdatePointOfInterest (int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            //check for a user cannot add a PointOfInterest if city is not existing 
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            var pointOfInterestFromStore = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            var pointOfInterestToPatch =
                new PointOfInterestForUpdateDto()
                {
                    Name = pointOfInterestFromStore.Name,
                    Description = pointOfInterestFromStore.Description
                };
            patchDoc.ApplyTo(pointOfInterestToPatch,ModelState);
            if (!ModelState.IsValid) //asure data is desirialize in the response body
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError(
                    "Description",
                    "The provided description should be different from the name.");
            }

            //this will trigger when you pass an invalid model e.g without giving name you pass it.
            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }
            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            //check for a user cannot add a PointOfInterest if city is not existing 
            //we check if city exists 
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            //we check if pointOfInterest exists 
            var pointOfInterestFromStore = city.PointsOfInterest
                .FirstOrDefault(p => p.Id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }
            city.PointsOfInterest.Remove(pointOfInterestFromStore);

            
            return NotFound();
        }
        }
}
