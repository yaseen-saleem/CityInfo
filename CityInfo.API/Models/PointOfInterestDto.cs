using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class PointOfInterestDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        [MaxLength(200)] //these are Data Annotations
        public string Description { get; set; }
    }
}
