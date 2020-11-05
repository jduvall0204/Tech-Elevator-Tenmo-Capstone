using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserDAO userDAO;
        private readonly IAccountsDAO accountsDAO;

        public UserController(IUserDAO userDAO, IAccountsDAO accountsDAO)
        {
            this.userDAO = userDAO;
            this.accountsDAO = accountsDAO;
        }

        [HttpGet("all")]
        public ActionResult<List<User>> GetUsers()
        {
            var users = userDAO.GetUsers();
            if(users == null)
            {
                NotFound();
            }
            return Ok(users);
        }
            
    }
}
