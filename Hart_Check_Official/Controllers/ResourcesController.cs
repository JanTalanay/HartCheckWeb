using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hart_Check_Official.Models;
using Hart_Check_Official.Data;

namespace Hart_Check_Official.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly datacontext _context;

        public ResourcesController(datacontext context)
        {
            _context = context;
        }

        // GET: api/Resources
        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetResources()
        {
            return await _context.Groups
                .Include(g => g.Resources.OrderBy(e => e.Name))
                .Select(g => new
                {
                    Id = "G" + g.Id, 
                    Expanded = true, 
                    Children = g.Resources, 
                    Name = g.Name,
                    CellsAutoUpdated = true
                })
                .OrderBy(e => e.Name)
                .ToListAsync();
        }

        // GET: api/Resources/Flat
        [HttpGet("Flat")]
        public async Task<ActionResult<IEnumerable>> GetResourcesFlat()
        {
            return await _context.Resources.OrderBy(e => e.Name).ToListAsync();
        }
    }
}

