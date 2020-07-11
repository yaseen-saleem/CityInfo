using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }

        public int NumberOfPointsOfInterest
        {
            get
            {
                return PointsOfInterest.Count;
            }
        }

        //city can have a collection of PointOfInterests we are making PointOfInterestDto object in CityDto Class.
        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; }
               = new List<PointOfInterestDto>();

    }
}
