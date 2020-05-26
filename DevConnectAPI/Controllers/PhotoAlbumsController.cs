using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevConnectAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace DevConnectAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoAlbumsController : ControllerBase
    {
        private readonly DevConnectContext _context;

        public PhotoAlbumsController(DevConnectContext context)
        {
            _context = context;
        }

        // GET: api/PhotoAlbums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoAlbum>>> GetPhotoAlbums()
        {
            return await _context.PhotoAlbums.ToListAsync();
        }

        // GET: api/PhotoAlbums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoAlbum>> GetPhotoAlbum(int id)
        {
            var photoAlbum = await _context.PhotoAlbums.FindAsync(id);

            if (photoAlbum == null)
            {
                return NotFound();
            }

            return photoAlbum;
        }

        // PUT: api/PhotoAlbums/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhotoAlbum(int id, PhotoAlbum photoAlbum)
        {
            if (id != photoAlbum.album_id)
            {
                return BadRequest();
            }

            _context.Entry(photoAlbum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoAlbumExists(id))
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

        // POST: api/PhotoAlbums
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PhotoAlbum>> PostPhotoAlbum(PhotoAlbum photoAlbum)
        {
            _context.PhotoAlbums.Add(photoAlbum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhotoAlbum", new { id = photoAlbum.album_id }, photoAlbum);
        }

        // DELETE: api/PhotoAlbums/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PhotoAlbum>> DeletePhotoAlbum(int id)
        {
            var photoAlbum = await _context.PhotoAlbums.FindAsync(id);
            if (photoAlbum == null)
            {
                return NotFound();
            }

            _context.PhotoAlbums.Remove(photoAlbum);
            await _context.SaveChangesAsync();

            return photoAlbum;
        }

        private bool PhotoAlbumExists(int id)
        {
            return _context.PhotoAlbums.Any(e => e.album_id == id);
        }
    }
}
