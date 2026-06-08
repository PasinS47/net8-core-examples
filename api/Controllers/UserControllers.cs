using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Users;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;

namespace api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserControllers : ControllerBase
    {

        private readonly IUserRepository _userRepo;

        public UserControllers(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll(QueryObject queryObject)
        {
            var users = await _userRepo.GetAllWithAccountAsync(queryObject);
            
            return Ok(users.Select(s => s.ToGetUserDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID (int id)
        {
            var user = await _userRepo.GetUserWithAccountAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToGetUserDto());
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] PostUserRequestDto userDto)
        {
            var userModel = userDto.ToUserFromPostRequestDto();

            var createdUser = await _userRepo.CreateAsync(userModel);

            if(!createdUser)
                return StatusCode(500, "Failed to create account");

            return CreatedAtAction(nameof(GetByID), new { id = userModel.Id}, userModel.ToGetUserDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequestDto userDto)
        {
            var userModel = await _userRepo.GetByIdAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }

            await _userRepo.UpdateUserAsync(userModel, userDto);

            return Ok(userModel.ToGetUserDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var userModel = await _userRepo.GetByIdAsync(id);

            if(userModel == null)
            {
                return NotFound();
            }

            _userRepo.Delete(userModel);
            var success = await _userRepo.SaveChangesAsync();

            if(!success)
                return StatusCode(500, "Unable to save change");

            return NoContent();
        }
    }
}