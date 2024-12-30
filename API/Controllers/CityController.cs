using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityRepositary _cityRepositary;

        public CityController(CityRepositary cityRepositary)
        {
            _cityRepositary = cityRepositary;
        }

        [HttpGet]
        public IActionResult GetAllCity()
        {
            var city = _cityRepositary.SelectAll();
            return Ok(city);

        }

        [HttpGet("{id}")]
        public IActionResult GetByPK(int id)
        {
            var city = _cityRepositary.SelectByPK(id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
        
        [HttpPost]
        public IActionResult InsertCity([FromBody] CityModel city)
        {
            if (city == null)
            {
                return BadRequest("Invalid country data.");
            }
        
            var isInserted = _cityRepositary.CityInsert(city);
            if (!isInserted)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to insert country.");
            }
        
            return CreatedAtAction(nameof(GetByPK), new { id = city.CityID }, city);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateCity(int id, [FromBody] CityModel city)
        {
            if (city == null || id != city.CityID)
            {
                return BadRequest("Invalid country data.");
            }
        
            var isUpdated = _cityRepositary.CityUpdate(city);
            if (!isUpdated)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update country.");
            }
        
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
        {
            var isDeleted = _cityRepositary.CityDelete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
        
            return NoContent();
        }
        
        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            var countries = _cityRepositary.GetCountries();
            if (!countries.Any())
                return NotFound("No countries found.");

            return Ok(countries);
        }
        
        [HttpGet("states/{countryID}")]
        public IActionResult GetStatesByCountryID(int countryID)
        {
            if (countryID <= 0)
                return BadRequest("Invalid CountryID.");

            var states = _cityRepositary.GetStatesByCountryID(countryID);
            if (!states.Any())
                return NotFound("No states found for the given CountryID.");

            return Ok(states);
        }

    }
}
