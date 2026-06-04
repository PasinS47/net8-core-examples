using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Accounts
{
    public class PostAccountRequestDto
    {
        public int UserId { get; set; }

        public string AccountNumber { get; set; } = string.Empty;
    }
}