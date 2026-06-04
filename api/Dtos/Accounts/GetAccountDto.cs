using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Accounts
{
    public class GetAccountDto
    {
        public int? UserId { get; set; }

        public int Id { get; set; }
        public decimal Balance { get; set; }

        public string AccountNumber { get; set; } = string.Empty;
    }
}