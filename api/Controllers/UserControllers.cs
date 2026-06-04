using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserControllers : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UserControllers(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var users = _context.Users.ToList().Select(s => s.ToGetUserDto());
            
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID (int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToGetUserDto());
        }
    }
}