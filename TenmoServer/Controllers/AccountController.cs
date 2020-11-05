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
    [Route("/")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {

        private static IUserDAO userDAO;
        private static IAccountsDAO accountsDAO;

        public AccountController(IUserDAO _userDAO)
        {
            userDAO = _userDAO;
        }

        //Get List of all Accounts
        [HttpGet("account")]
        public ActionResult<List<User>> GetAccount() // User should be Account
        {
            return Ok(userDAO.GetUsers());
        }
        // Get Account with ID from List
        [HttpGet("account/{id}")]
        public ActionResult<User> GetAccountId(string username) // User should be Account
        {
            User user = userDAO.GetUser(username);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        //Update Recipient Balance - update Recipients(id) Current Balance
        [HttpPut("account/{id}/balance")]
        public ActionResult<User> UpdateRecipientBalance(int id, double balance) // User should be Account
        {
            User recipientBalance = userDAO.GetBalance(id);
            if (recipientBalance == null)
            {
                return NotFound("Balance does not exist"); //sql command existing balance that belongs to user ID, take that 
            }

            User result = userDAO.Update(id, balance);
            return Ok(result);
        }

        //Update Sender Balance - update current users Current Balance
        [HttpPut("account/{id}/balance")]
        public ActionResult<User> UpdateSenderBalance(int id, User balance) // User should be Account
        {
            User recipientBalance = userDAO.GetBalance(id);
            if (recipientBalance == null)
            {
                return NotFound("Balance does not exist");
            }

            User result = userDAO.Update(id, balance);
            return Ok(result);
        }
        //Get list of all transfers
        [HttpGet("account/transfers")]
        public ActionResult<List<Transfers>> GetTransfers() // User should be Transfer
        {
            return Ok(userDAO.GetTransfers());
        }

        //Get Transfer with Id from List - Past Transfer
        [HttpGet("account/{id}/transfers")]
        public ActionResult<List<Transfers>> GetTransfers(int Id) // User should be Transfer
        {
            return Ok(userDAO.GetTransfers(Id));
        }
    }
}
