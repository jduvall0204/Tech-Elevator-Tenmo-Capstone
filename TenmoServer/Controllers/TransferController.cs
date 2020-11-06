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
        private readonly ITransfersDAO TransfersSqlDAO;
        private readonly IAccountsDAO AccountsSqlDAO;
        private readonly IUserDAO UserSqlDAO;

        public TransferController(ITransfersDAO _transferDAO, IAccountsDAO _accountDAO, IUserDAO _userDAO)
        {
            TransfersSqlDAO = _transferDAO;
            AccountsSqlDAO = _accountDAO;
            UserSqlDAO = _userDAO;
        }

        [HttpGet]
        public List<User> ListUsers()
        {
            return UserSqlDAO.GetUsers();
        }

        [HttpGet("{transferId}")]
        public ActionResult<TransferWithDetails> GetTransferById(int transferId)
        {
            TransferWithDetails transfer = TransfersSqlDAO.GetTransfer(transferId);
            if (transfer != null)
            {
                return Ok(transfer);
            }
            else
            {
                return NotFound("Transfer does not exist.");
            }
        }

        [HttpGet("history")]
        public ActionResult<List<TransferWithDetails>> ListTransfers()
        {
            int userId = GetId();
            var transferHistory = TransfersSqlDAO.GetTransferHistory(userId);

            if (transferHistory != null)
            {
                return Ok(transferHistory);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public ActionResult<TransferWithDetails> SendMoney(NewTransfer newTransfer)
        {
            int userId = GetId();
            Account accountFrom = AccountsSqlDAO.GetAccount(userId);

            if (accountFrom == null)
            {
                return NotFound("Account does not exist");
            }
            if (accountFrom.Balance >= newTransfer.Amount)
            {
                TransferWithDetails result = TransfersSqlDAO.SendMoney(userId, newTransfer.ReceiverAccount, newTransfer.Amount);

                return Ok(result);
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
