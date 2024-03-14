using System;
using CityInfo.API.Entities;
using CityInfo.API.Models;

// purpose of Async code - freeing up threads so they can be used for other tasks, which improves the scalability of your application.
// although it also has +ve affect on performance.
namespace CityInfo.API.Services
{
	public interface ICityInfoRepository
	{
		Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);

        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);

		Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointofinterestId);

        Task<IEnumerable<PointOfInterest?>> GetPointsOfInterestForCityAsync(int cityId);

		Task<bool> CityExistsAsync(int cityId);

		Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);

		void DeletePointOfInterest(PointOfInterest pointOfInterest);

        Task<bool> SaveChangesAsync();
    }
}

