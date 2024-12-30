
using API.Data;
using API1;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly StateRepositary _stateRepositary;

        public StateController(StateRepositary stateRepositary)
        {
            _stateRepositary = stateRepositary;
        }

        [HttpGet]
        public IActionResult GetAllState()
        {
            var state = _stateRepositary.SelectAll();
            return Ok(state);

        }

        [HttpGet("{id}")]
        public IActionResult GetByPK(int id)
        {
            var state = _stateRepositary.SelectByPK(id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }
        
        [HttpPost]
        public IActionResult InsertState([FromBody] StateModel state)
        {
            if (state == null)
            {
                return BadRequest("Invalid country data.");
            }
        
            var isInserted = _stateRepositary.StateInsert(state);
            if (!isInserted)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to insert country.");
            }
        
            return CreatedAtAction(nameof(GetByPK), new { id = state.StateID }, state);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateState(int id, [FromBody] StateModel state)
        {
            if (state == null || id != state.StateID)
            {
                return BadRequest("Invalid country data.");
            }
        
            var isUpdated = _stateRepositary.StateUpdate(state);
            if (!isUpdated)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update country.");
            }
        
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteState(int id)
        {
            var isDeleted = _stateRepositary.StateDelete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
        
            return NoContent();
        }
        
        #region StateDropDown
        [HttpGet]
        public IActionResult StateDropDown()
        {
            var state = _stateRepositary.StateDropDown();
            if (!state.Any())
            {
                return NotFound("No State Found");
            }

            return Ok(state);
        }
        #endregion
    }
}
