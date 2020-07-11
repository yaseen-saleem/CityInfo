using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore(); //we can get current city by reffering to this property. we made it static to make it immutable.
        public List<CityDto> Cities { get; set; } //making objjectof CityDto model in CityDataStore
       

        public CitiesDataStore()
        {
            //initilaize dummy data
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Lahore",
                    Description = "Heart of Pakistan",
                    PointsOfInterest = new List<PointOfInterestDto>()
                     {
                         new PointOfInterestDto() {
                             Id = 1,
                             Name = "Gulshan Park",
                             Description = "The most visited urban park in the Lahore." },
                          new PointOfInterestDto() {
                             Id = 2,
                             Name = "Emporium Mall",
                             Description = "A 4 Story Mall which have nice environment." },
                     }
                },

                new CityDto()
                {
                    Id = 2,
                    Name = "Karachi",
                    Description = "City Of Lights",
                     PointsOfInterest = new List<PointOfInterestDto>()
                     {
                         new PointOfInterestDto() {
                             Id = 1,
                             Name = "Sea View",
                             Description = "The most visited sea point in karachi." },
                          new PointOfInterestDto() {
                             Id = 2,
                             Name = "Dolmen Mall",
                             Description = "A very big mall in karachi" },
                     }
                },

                new CityDto()
                {
                    Id = 3,
                    Name = "IslamAbad",
                    Description = "City of Burgers",
                     PointsOfInterest = new List<PointOfInterestDto>()
                     {
                         new PointOfInterestDto() {
                             Id = 1,
                             Name = "Monal",
                             Description = "The most visited urban resturant in the IslamAbad." },
                          new PointOfInterestDto() {
                             Id = 2,
                             Name = "Centauraus Mall",
                             Description = "A 3 tower building mall very cool." },
                     }
                },
            };
        }
    }
}
