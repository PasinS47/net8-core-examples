using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Users;
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

        [HttpPost]

        public IActionResult Create([FromBody] PostUserRequestDto userDto)
        {
            var userModel = userDto.ToUserFromPostRequestDto();

            _context.Users.Add(userModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetByID), new { id = userModel.Id}, userModel.ToGetUserDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequestDto userDto)
        {
            var userModel = await _context.Users.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }

            userModel.FirstName = userDto.FirstName;
            userModel.LastName = userDto.LastName;
            userModel.Age = userDto.Age;
            userModel.Email = userDto.Email;
            userModel.PhoneNumber = userDto.PhoneNumber;

            await _context.SaveChangesAsync();

            return Ok(userModel.ToGetUserDto());
        }
    }
}