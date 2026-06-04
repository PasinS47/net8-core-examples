using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace api.Dtos.Users
{
    public class GetUserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public List<GetAccountDto> Accounts { get; set; } = new List<GetAccountDto>();
    }
}