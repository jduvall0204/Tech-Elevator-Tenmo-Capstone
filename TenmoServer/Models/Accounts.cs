using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Accounts
    {
        public int AccountId { get; set; }
        [Required(ErrorMessage = "You need to enter the account ID.")]
        public int UserId { get; set; }
        public decimal Balance { get; set; }


        public override string ToString()
        {
            return AccountId.ToString() + UserId.ToString() + Balance.ToString();
        }
    }
}
