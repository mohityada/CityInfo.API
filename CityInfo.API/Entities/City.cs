using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/* ORM - object relational model
DbConext - session with database & can be used to query and save instances of entities
Use Environment variables foe safer storage of sensitive data
*/

namespace CityInfo.API.Entities
{
	public class City
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        //public int NumberOfPointsOfInterest { get; set; }
        public ICollection<PointOfInterest> PointsOfInterest { get; set; }
             = new List<PointOfInterest>();

        public City(string name)
        {
            Name = name;
        }
    }
}

