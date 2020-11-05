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
    [Route("[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly IAccountsDAO accountsDAO;
        private readonly ITransfersDAO transfersDAO;

        public TransferControler(IAccountsDAO accountsDAO, ITransfersDAO transfersDAO)
        {
            this.accountsDAO = accountsDAO;
            this.transfersDAO = transfersDAO;
        }

        [HttpGet]
        public ActionResult<List<Transfers>> GetTransfers()
        {
            var transfers = transfersDAO.GetTransfers(User.Identity.Name);
            if(transfers == null)
            {
                return NotFound();
            }
            return Ok(transfers);
        }
        
            
    }
}
