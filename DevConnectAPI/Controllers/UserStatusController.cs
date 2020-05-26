﻿using System;
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
    public class UserStatusController : ControllerBase
    {
        private readonly DevConnectContext _context;

        public UserStatusController(DevConnectContext context)
        {
            _context = context;
        }

        // GET: api/UserStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserStatus>>> GetUserStatuses()
        {
            return await _context.UserStatuses.ToListAsync();
        }

        // GET: api/UserStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserStatus>> GetUserStatus(int id)
        {
            var userStatus = await _context.UserStatuses.FindAsync(id);

            if (userStatus == null)
            {
                return NotFound();
            }

            return userStatus;
        }

        // PUT: api/UserStatus/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserStatus(int id, UserStatus userStatus)
        {
            if (id != userStatus.status_id)
            {
                return BadRequest();
            }

            _context.Entry(userStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserStatusExists(id))
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

        // POST: api/UserStatus
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserStatus>> PostUserStatus(UserStatus userStatus)
        {
            _context.UserStatuses.Add(userStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserStatus", new { id = userStatus.status_id }, userStatus);
        }

        // DELETE: api/UserStatus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserStatus>> DeleteUserStatus(int id)
        {
            var userStatus = await _context.UserStatuses.FindAsync(id);
            if (userStatus == null)
            {
                return NotFound();
            }

            _context.UserStatuses.Remove(userStatus);
            await _context.SaveChangesAsync();

            return userStatus;
        }

        private bool UserStatusExists(int id)
        {
            return _context.UserStatuses.Any(e => e.status_id == id);
        }
    }
}
