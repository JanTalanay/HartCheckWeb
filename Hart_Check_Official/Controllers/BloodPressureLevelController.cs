using AutoMapper;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hart_Check_Official.Controllers
{
    public class BloodPressureLevelController : ControllerBase
    {
        private readonly IBloodPressureLevelRepository _bloodPressureLevelRepository;

        private readonly IMapper _mapper;

        public BloodPressureLevelController(IBloodPressureLevelRepository bloodPressureLevelRepository, IMapper mapper)
        {
            _bloodPressureLevelRepository = bloodPressureLevelRepository;
            _mapper = mapper;
        }

        [HttpPut("{usersID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBloodPressureLevel(int usersID, [FromBody] BloodPressureLevelDto updateBloodPressureLevel)
        {
            if (updateBloodPressureLevel == null)
            {
                return BadRequest(ModelState);
            }
            if (usersID != updateBloodPressureLevel.usersID)
            {
                return BadRequest(ModelState);
            }
            if (!_bloodPressureLevelRepository.UserExists(usersID))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var BPlevelMap = _mapper.Map<Users>(updateBloodPressureLevel);

            if (!_bloodPressureLevelRepository.updateBloodPressureLevel(BPlevelMap))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}