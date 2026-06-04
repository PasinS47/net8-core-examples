using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Accounts;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountControllers : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public AccountControllers(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            var accounts = _context.Accounts.ToList().Select(s => s.ToGetAccountDto());

            return Ok(accounts);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetByID(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if(account == null)
            {
                return NotFound();
            }
            return Ok(account.ToGetAccountDto());
        }

        [HttpPost]

        public IActionResult Create([FromBody] PostAccountRequestDto accountDto)
        {
            var accountModel = accountDto.ToAccountFromPostRequestDto();

            _context.Accounts.Add(accountModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetByID), new { id = accountModel.Id}, accountModel.ToGetAccountDto());
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAccountRequestDto accountDto)
        {
            var accountModel = await _context.Accounts.FindAsync(id);

            if(accountModel == null)
            {
                return NotFound();
            }

            accountModel.Balance = accountDto.Balance;

            await _context.SaveChangesAsync();

            return Ok(accountModel.ToGetAccountDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var accountModel = _context.Accounts.Find(id);

            if(accountModel == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(accountModel);
            _context.SaveChanges();

            return NoContent();
        }
    }
}