using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Account
    {
        
        public int? UserId { get; set; }

        public User? User { get; set; }

        [Column(TypeName = "decimal(18, 2)")]

        public int Id { get; set; }
        public decimal Balance { get; set; }

        public string AccountNumber { get; set; } = string.Empty;
    }
}