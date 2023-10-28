using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hart_Check_Official.Models;
using Hart_Check_Official.Data;

namespace Hart_Check_Official.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrdersController : ControllerBase
    {
        private readonly datacontext _context;

        public WorkOrdersController(datacontext context)
        {
            _context = context;
        }

        // GET: api/WorkOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkOrder>>> GetWorkOrders([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            return await _context.WorkOrders.Where(e => e.ResourceId != null && !((e.End <= start) || (e.Start >= end))).ToListAsync();
        }

        // GET: api/WorkOrders/Unscheduled
        [HttpGet("Unscheduled")]
        public async Task<ActionResult<IEnumerable<WorkOrder>>> GetWorkOrdersUnscheduled()
        {
            return await _context.WorkOrders.Where(e => e.ResourceId == null).OrderBy(e => e.Ordinal).ThenByDescending(e => e.OrdinalPriority).ToListAsync();
        }

        // GET: api/WorkOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkOrder>> GetWorkOrder(int id)
        {
            var workOrder = await _context.WorkOrders.FindAsync(id);

            if (workOrder == null)
            {
                return NotFound();
            }

            return workOrder;
        }

        // PUT: api/WorkOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkOrder(int id, WorkOrderUpdateParams p)
        {
            WorkOrder? workOrder = await _context.WorkOrders.FindAsync(id);
            if (workOrder == null)
            {
                return Problem("WorkOrder not found " + id);
            }


            if (p.Text != null)
            {
                workOrder.Text = p.Text;
            }

            if (p.Color != null)
            {
                workOrder.Color = p.Color;
            }

            if (p.Resource != null)
            {
                Resource? resource = await _context.Resources.FindAsync(p.Resource);
                if (resource == null)
                {
                    return Problem("Resource not found " + p.Resource);
                }

                workOrder.Resource = resource;
                workOrder.Start = (DateTime)p.Start;
                workOrder.End = (DateTime)p.End;
                await _context.SaveChangesAsync();
            }
            else
            {
                workOrder.ResourceId = null;
                if (p.Duration != null)
                {
                    workOrder.Start = new DateTime(2000, 1, 1);
                    workOrder.End = workOrder.Start.AddMinutes((double)p.Duration);
                }
                if (p.Position != null)
                {
                    workOrder.Ordinal = (int)p.Position;
                    workOrder.OrdinalPriority = DateTime.Now;
                }
                await _context.SaveChangesAsync();
                await CompactOrdinals();
            }


            return Ok(workOrder);

        }

        // POST: api/WorkOrders
        [HttpPost]
        public async Task<ActionResult<WorkOrder>> PostWorkOrder(PostWorkOrderParams p)
        {
            WorkOrder workOrder = new WorkOrder()
            {
                Start = p.Start,
                End = p.End,
                ResourceId = p.Resource,
                Text = p.Text,
                Color = p.Color
            };

            if (p.Resource == null)
            {
                workOrder.Ordinal = 10000;
            }

            _context.WorkOrders.Add(workOrder);
            await _context.SaveChangesAsync();
            await CompactOrdinals();

            return CreatedAtAction("GetWorkOrder", new { id = workOrder.Id }, workOrder);
        }      
        
        // POST: api/WorkOrders/5/Unschedule
        [HttpPost("{id}/Unschedule")]
        public async Task<ActionResult<WorkOrder>> PostWorkOrderUnschedule(int id)
        {
            WorkOrder workOrder = await _context.WorkOrders.FindAsync(id);
            if (workOrder == null)
            {
                return NotFound();
            }

            workOrder.ResourceId = null;
            workOrder.Ordinal = 10000;
            workOrder.OrdinalPriority = new DateTime();
            await _context.SaveChangesAsync();
            
            await CompactOrdinals();
            
            return Ok(workOrder);
        }

        // DELETE: api/WorkOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkOrder(int id)
        {
            var workOrder = await _context.WorkOrders.FindAsync(id);
            if (workOrder == null)
            {
                return NotFound();
            }

            _context.WorkOrders.Remove(workOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkOrderExists(int id)
        {
            return _context.WorkOrders.Any(e => e.Id == id);
        }


        private async Task CompactOrdinals()
        {
            List<WorkOrder> list = await _context.WorkOrders.Where(e => e.ResourceId == null).OrderBy(e => e.Ordinal)
                .ThenByDescending(e => e.OrdinalPriority).ToListAsync();


            Debug.Print("Count " + list.Count);

            int i = 0;
            foreach (WorkOrder workOrder in list)
            {
                workOrder.Ordinal = i;
                i += 1;

                Debug.Print("Id " + workOrder.Id + " ordinal " + i);
            }
            await _context.SaveChangesAsync();
        }
    }


    public class PostWorkOrderParams
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Text { get; set; }
        public int? Resource { get; set; }

        public string? Color { get; set; }
    }


    public class WorkOrderUpdateParams
    {
        public int? Position { get; set; }

        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public int? Resource { get; set; }

        public string? Text { get; set; }

        public string? Color { get; set; }

        public int? Duration { get; set; }
    }

}
