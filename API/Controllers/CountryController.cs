
using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryRepositary _countryRepositary;

        public CountryController(CountryRepositary countryRepositary)
        {
            _countryRepositary = countryRepositary;
        }

        [HttpGet]
        public IActionResult GetAllCountry()
        {
            var country = _countryRepositary.SelectAll();
            return Ok(country);

        }

        [HttpGet("{id}")]
        public IActionResult GetByPK(int id)
        {
            var country = _countryRepositary.SelectByPK(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }
        
        [HttpPost]
        public IActionResult InsertCountry([FromBody] CountryModel country)
        {
            if (country == null)
            {
                return BadRequest("Invalid country data.");
            }
        
            var isInserted = _countryRepositary.CountryInsert(country);
            if (!isInserted)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to insert country.");
            }
        
            return CreatedAtAction(nameof(GetByPK), new { id = country.CountryID }, country);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id, [FromBody] CountryModel country)
        {
            if (country == null || id != country.CountryID)
            {
                return BadRequest("Invalid country data.");
            }
        
            var isUpdated = _countryRepositary.CountryUpdate(country);
            if (!isUpdated)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update country.");
            }
        
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            var isDeleted = _countryRepositary.CountryDelete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
        
            return NoContent();
        }
    }
}
