using AutoMapper;
using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Hart_Check_Official.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hart_Check_Official.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodyMassController : ControllerBase
    {
        private readonly IBodyMassRepository _bodyMassRepository;

        private readonly IMapper _mapper;

        public BodyMassController(IBodyMassRepository bodyMassRepository, IMapper mapper)
        {
            _bodyMassRepository = bodyMassRepository;
            _mapper = mapper;
        }

        [HttpGet]//getting list all the data of the BodyMass table
        [ProducesResponseType(200, Type = typeof(IEnumerable<BodyMass>))]
        public IActionResult getBodyMass()
        {
            var user = _mapper.Map<List<BodyMassDto>>(_bodyMassRepository.GetBodies());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }
        [HttpPost]//to be fix
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBodyMass([FromBody] BodyMassDto bodyMassCreate)
        {
            if (bodyMassCreate == null)
            {
                return BadRequest(ModelState);
            }
            var users = _bodyMassRepository.GetBodies();
                //.Where(e => e.firstName.Trim().ToUpper() == userCreate.lastName.TrimEnd().ToUpper())
                //.FirstOrDefault();

            if (users != null)
            {
                ModelState.AddModelError("", "Already Exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var bodyMassMap = _mapper.Map<BodyMass>(bodyMassCreate);

            if (!_bodyMassRepository.CreateBodyMass(bodyMassMap))
            {
                ModelState.AddModelError("", "Something Went Wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpDelete("{bodyMassID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBodyMass(int bodyMassID)
        {
            if (!_bodyMassRepository.BodyMassExist(bodyMassID))
            {
                return NotFound();
            }
            var bodyMassToDelete = _bodyMassRepository.GetBodyMass(bodyMassID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_bodyMassRepository.DeleteBodyMass(bodyMassToDelete))
            {
                ModelState.AddModelError("", "Something Went wrong deleting");
            }
            return NoContent();
        }
        [HttpPut("{bodyMassID}")]//to be fix
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Updatebodymass(int bodyMassID, [FromBody] BodyMassDto updateBodyMass)
        {
            if (updateBodyMass == null)
            {
                return BadRequest(ModelState);
            }
            if (bodyMassID != updateBodyMass.bodyMassID)
            {
                return BadRequest(ModelState);
            }
            if (!_bodyMassRepository.BodyMassExist(bodyMassID))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var bodyMassMap = _mapper.Map<BodyMass>(updateBodyMass);

            if (!_bodyMassRepository.UpdateBodyMass(bodyMassMap))
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
    }
}
