using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Index(nameof(AccountNumber), IsUnique = true)]
    public class Account
    {
        
        public int? UserId { get; set; }

        public User? User { get; set; }

        public int Id { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }

        public string AccountNumber { get; set; } = string.Empty;
    }
}