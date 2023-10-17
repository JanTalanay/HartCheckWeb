using AutoMapper;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hart_Check_Official.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorEditProfileController : ControllerBase
    {
        private readonly IDoctorEditProfileRepository _doctorEditProfile;

        private readonly IMapper _mapper;

        public DoctorEditProfileController(IDoctorEditProfileRepository doctorEditProfile, IMapper mapper)
        {
            _doctorEditProfile = doctorEditProfile;
            _mapper = mapper;
        }

        [HttpPut("{usersID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDoctorProfile(int usersID, [FromBody] DoctorEditProfileDto updateDoctorProfile)
        {
            if (updateDoctorProfile == null)
            {
                return BadRequest(ModelState);
            }
            if (usersID != updateDoctorProfile.usersID)
            {
                return BadRequest(ModelState);
            }
            if (!_doctorEditProfile.UserExists(usersID))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var doctorProfileMap = _mapper.Map<Users>(updateDoctorProfile);

            if (!_doctorEditProfile.UpdateDoctorProfile(doctorProfileMap))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}