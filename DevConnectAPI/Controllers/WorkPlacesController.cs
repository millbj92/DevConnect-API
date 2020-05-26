using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevConnectAPI;
using DevConnectAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace DevConnectAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkPlacesController : ControllerBase
    {
        private readonly DevConnectContext _context;

        public WorkPlacesController(DevConnectContext context)
        {
            _context = context;
        }

        // GET: api/WorkPlaces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkPlace>>> GetWorkPlaces()
        {
            return await _context.WorkPlaces.ToListAsync();
        }

        // GET: api/WorkPlaces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkPlace>> GetWorkPlace(int id)
        {
            var workPlace = await _context.WorkPlaces.FindAsync(id);

            if (workPlace == null)
            {
                return NotFound();
            }

            return workPlace;
        }

        // PUT: api/WorkPlaces/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkPlace(int id, WorkPlace workPlace)
        {
            if (id != workPlace.workplace_id)
            {
                return BadRequest();
            }

            _context.Entry(workPlace).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkPlaceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WorkPlaces
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WorkPlace>> PostWorkPlace(WorkPlace workPlace)
        {
            _context.WorkPlaces.Add(workPlace);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkPlace", new { id = workPlace.workplace_id }, workPlace);
        }

        // DELETE: api/WorkPlaces/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkPlace>> DeleteWorkPlace(int id)
        {
            var workPlace = await _context.WorkPlaces.FindAsync(id);
            if (workPlace == null)
            {
                return NotFound();
            }

            _context.WorkPlaces.Remove(workPlace);
            await _context.SaveChangesAsync();

            return workPlace;
        }

        private bool WorkPlaceExists(int id)
        {
            return _context.WorkPlaces.Any(e => e.workplace_id == id);
        }
    }
}
