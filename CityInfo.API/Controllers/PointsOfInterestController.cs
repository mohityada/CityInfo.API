
using System.Net.NetworkInformation;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using CityInfo.API.Services;
using AutoMapper;
using CityInfo.API.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService,
            ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            if(!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();
            }

            var PsOIForCity = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);
            

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(PsOIForCity));
        }

        [HttpGet("{pointofinterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointofinterestId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing point of interest.");
                return NotFound();
            }

            var POIForCity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointofinterestId);

            if (POIForCity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(POIForCity));

        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestForUpdateDto
            pointOfInterestForCreationDto)
        {
            if(!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }
            //var city = _cititesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            // to be improved
            //var maxPOI = _cititesDataStore.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            var finalPOI = _mapper.Map<Entities.PointOfInterest>(pointOfInterestForCreationDto);
            //var finalPOI = new PointOfInterestDto()
            //{
            //    Id = ++maxPOI,
            //    Name = pointOfInterestForCreationDto.Name,
            //    Description = pointOfInterestForCreationDto.Description
            //};

            //city.PointsOfInterest.Add(finalPOI);

            await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPOI);
            await _cityInfoRepository.SaveChangesAsync();

            var createdPOIToReturn = _mapper.Map<Models.PointOfInterestDto>(finalPOI);

            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = createdPOIToReturn.Id
                }, createdPOIToReturn
                );
        }

        [HttpPut("{pointofinterestId}")]
        public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointofinterestId, PointOfInterestForUpdateDto
            pointOfInterestForUpdateDto)
        {
            if(!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            //var city = _cititesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            var poiFromDB = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointofinterestId);
            //var poiFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointofinterestId);
            if (poiFromDB == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterestForUpdateDto, poiFromDB);

            //poiFromStore.Name = pointOfInterestForUpdateDto.Name;
            //poiFromStore.Description = pointOfInterestForUpdateDto.Description

            await _cityInfoRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{pointofinterestId}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(int cityId, int pointofinterestId, JsonPatchDocument<PointOfInterestForUpdateDto>
            patchDoc)
        {
            //var city = _cititesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var poiFromDB = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointofinterestId);
            if (poiFromDB == null)
            {
                return NotFound();
            }

            var poiToPatch = _mapper.Map<PointOfInterestForUpdateDto>(poiFromDB);

            //var poiToPatch = new PointOfInterestForUpdateDto()
            //{
            //    Name = poiFromStore.Name,
            //    Description = poiFromStore.Description
            //};

            patchDoc.ApplyTo(poiToPatch, ModelState); // to be studied...

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!TryValidateModel(poiToPatch))
            {
                return BadRequest(ModelState);
            }
            //poiFromDB.Name = poiToPatch.Name;
            //poiFromDB.Description = poiToPatch.Description;

            _mapper.Map(poiToPatch, poiFromDB);
            await _cityInfoRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId, int pointofinterestId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var poiToDelete = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointofinterestId);

            if (poiToDelete == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(poiToDelete);
            await _cityInfoRepository.SaveChangesAsync();
           // city.PointsOfInterest.Remove(pointOfInterest);
            _mailService.Send("Point of interest deleted", $"Deleted point of interest is {poiToDelete.Name} with id {poiToDelete.Id}.");
            return NoContent();
        }

    }
}

