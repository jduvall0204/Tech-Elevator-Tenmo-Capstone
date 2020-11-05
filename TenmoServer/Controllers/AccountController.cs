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
    //[Authorize]
    public class AccountController : ControllerBase
    {

        //private readonly ITransfersDAO TransfersDAO;
        private readonly IAccountsDAO AccountsDAO;

        public AccountController(IAccountsDAO accountsDAO)
        {
            AccountsDAO = accountsDAO;

        }

        [HttpGet]
        public ActionResult<Account> GetAccount()
        {
            int userId = GetId();

            Account account = AccountsSqlDAO.GetAccount(userId);

            if (account != null)
            {
                return Ok(account);
            }
            else
            {
                return NotFound();
            }

        }

        public int GetId()
        {
            int userId = 0;
            var tokenId = User.FindFirst("sub").Value;

            int.TryParse(tokenId, out userId);

            return userId;
        }
    }
}
