using System;
using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
	public class PointOfInterestForUpdateDto
	{
        [Required(ErrorMessage = "You should provide a name value.")]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}

