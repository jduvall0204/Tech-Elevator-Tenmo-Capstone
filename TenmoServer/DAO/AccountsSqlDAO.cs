using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;
using TenmoServer.Controllers;

namespace TenmoServer.DAO
{
    public class AccountsSqlDAO: IAccountsDAO
    {

        private string connectionString;

        public AccountsSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public Account GetAccounts(string username)
        {
            Account getAccount = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "SELECT accounts.account_id, accounts.user_id, balance FROM accounts, users ";
                    sqlText += "WHERE accounts.user_id = users.user_id AND users.username = @username";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        getAccount = GetAccountsFromReader(reader);
                    }
                }
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
            return getAccount;
        }

        public decimal? GetBalance(int userID)
        {
            Account getBalance = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "SELECT account_id, user_id, balance FROM accounts";
                    sqlText = "WHERE user_Id  @user_id";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@user_id", userID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        getBalance = GetAccountsFromReader(reader);
                    }
                }
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
            return getBalance.Balance;
        }

        public bool GetTransfer(Transfer transfer)
        {
            if(!UpdateBalance(transfer.AccountTo, transfer.Amount))
            {
                return false;
            }
            if (!UpdateBalance(transfer.AccountFrom, - transfer.Amount))
            {
                return false;
            }
            return true;
        }

        public bool UpdateBalance(int userId, decimal amount)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "UPDATE accounts SET balance + @amount WHERE user_id = @user_id;";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
        }
        private Account GetAccountsFromReader(SqlDataReader reader)
        {
            Account accounts = new Account();
            accounts.AccountId = Convert.ToInt32(reader["account_id"]);
            accounts.UserId = Convert.ToInt32(reader["user_id"]);
            accounts.Balance = Convert.ToDecimal(reader["balance"]);

            return accounts;
        }
    }
}

