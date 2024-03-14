using System;
using CityInfo.API.Models;

namespace CityInfo.API
{
	public class CititesDataStore
	{
		public List<CityDto> Cities { get; set; }

		public static CititesDataStore Current { get; } = new CititesDataStore();

		public CititesDataStore()
		{
			Cities = new List<CityDto>()
			{
				new CityDto()
				{
					Id = 1,
					Name = "New Delhi",
					Description = "Capital of India",
					PointsOfInterest = new List<PointOfInterestDto>()
					{
						new PointOfInterestDto()
						{
							Id = 1,
							Name = "Lal Quila",
							Description = "Made of red stone"
						},
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Qutub Minar",
                            Description = "Burj khalifa of 17th century"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Lotus Temple",
                            Description = "Temple in shape of lotus flower"
                        }
                    }
				},
				new CityDto()
				{
					Id = 2,
					Name = "Jaipur",
					Description = "Capital of Rajasthan, India",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Albert hall museum",
                            Description = "A museum surrounded by pigeons"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Jai Garh Fort",
                            Description = "Made by Jaisingh and it has Asia's largest canon"
                        }
                    }
                },
				new CityDto()
				{
					Id = 3,
					Name = "Mumbai",
					Description = "Financial capital of India",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "GateWay of India",
                            Description = "The monument was erected to commemorate the landing of King George V and Queen Mary at Apollo Bunder on their visit"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Taj hotel",
                            Description = "Famous 7 star hotel"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Atal Setu",
                            Description = "Newly built tourist place"
                        }
                    }
                }
			};
		}

    }
}

