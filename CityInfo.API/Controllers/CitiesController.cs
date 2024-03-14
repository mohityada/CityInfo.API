using System;
using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
// 200 - Ok | 201 - Created | 204 - No Content || 400 - Bad Request | 401 - Unauthorized | 403 - Forbidden | 404 - Not Found
// 409 - Conflict || 500 - Internal Server Error
// await async - for I/O operations ensures threads can be freed up faster, resulting in improved scalability
namespace CityInfo.API.Controllers
{
	[ApiController] //automatically returns error codes, routing, binding etc
	[Route("api/cities")]
	public class CitiesController : ControllerBase //provides basic functionality for handling HTTP requests and responses,
    {
		private readonly ICityInfoRepository _cityInfoRepository;
		private readonly IMapper _mapper;
		const int maxPageSize = 10;

		public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
		{
			_cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CityWithoutPOIDto>>> GetCities(string? name, string? searchQuery, int pageNumber = 1,
			int pageSize = 10)
		{
			if(pageSize > maxPageSize)
			{
				pageSize = maxPageSize;
			}

			var (cityEntities, paginationMetadata) = await _cityInfoRepository.GetCitiesAsync(name, searchQuery, pageNumber, pageSize);
			//var results = new List<CityWithoutPOIDto>();
			//foreach(var cityEntity in cityEntities)
			//{
			//	results.Add( new CityWithoutPOIDto
			//	{
			//		Id = cityEntity.Id,
			//		Description = cityEntity.Description,
			//		Name = cityEntity.Name
			//	});
			//}
			//return Ok(results);

			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
		    return Ok(_mapper.Map<IEnumerable<CityWithoutPOIDto>>(cityEntities));
		}

		[HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id, bool includePointsOfInterest = false)
        {
            //var cityToReturn = CititesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            //if(cityToReturn == null)
            //{
            //	return NotFound();
            //}
            var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);

			if(city == null)
			{
				return NotFound();
			}

			if (includePointsOfInterest)
			{
				return Ok(_mapper.Map<CityDto>(city));
			}

			return Ok(_mapper.Map<CityWithoutPOIDto>(city));
		}
    }
}

