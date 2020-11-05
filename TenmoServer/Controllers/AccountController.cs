using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {

        private readonly ITransfersDAO TransfersDAO;
        private static IAccountsDAO AccountsDAO;

        public AccountController(IAccountsDAO accountsDAO, ITransfersDAO transfersDAO)
        {
            AccountsDAO = accountsDAO;
            TransfersDAO = transfersDAO;
        }

        [HttpGet]
        public ActionResult<decimal> GetAccountBalance()
        {
            string userIDString = (User.FindFirst("sub")?.Value);
            int userId;
            bool successfulParse = Int32.TryParse(userIDString, out userId);
            if(!successfulParse)
            {
                return StatusCode(500);
            }
            decimal? balance = AccountsDAO.GetBalance(userId);

            if(balance == null)
            {
                return NotFound();
            }
            return Ok(balance);
        }
        
    }
}
