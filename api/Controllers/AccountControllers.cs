using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Accounts;
using api.Interfaces;
using api.Mappers;
using api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountControllers : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        public AccountControllers(IAccountRepository accountRepository)
        {
            _accountRepo = accountRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var accounts = await _accountRepo.GetAllAsync(query);

            return Ok(accounts.Select(a => a.ToGetAccountDto()));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetByID(int id)
        {
            var account = await _accountRepo.GetByIdAsync(id);
            if(account == null)
            {
                return NotFound();
            }
            return Ok(account.ToGetAccountDto());
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] PostAccountRequestDto accountDto)
        {
            var accountModel = accountDto.ToAccountFromPostRequestDto();

            var createdAccount = await _accountRepo.CreateAsync(accountModel);

            if(!createdAccount)
                return StatusCode(500, "Failed to create account");

            return CreatedAtAction(nameof(GetByID), new { id = accountModel.Id}, accountModel.ToGetAccountDto());
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAccountRequestDto accountDto)
        {
            var accountModel = await _accountRepo.GetByIdAsync(id);

            if(accountModel == null)
            {
                return NotFound();
            }

            await _accountRepo.UpdateAccountAsync(accountModel, accountDto);

            return Ok(accountModel.ToGetAccountDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var accountModel = await _accountRepo.GetByIdAsync(id);

            if(accountModel == null)
            {
                return NotFound();
            }

            _accountRepo.Delete(accountModel);
            var success = await _accountRepo.SaveChangesAsync();

            if(!success)
                return StatusCode(500, "Unable to save change");

            return StatusCode(200, "Delete Successfully");
        }
    }
}