using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSqlDAO : IAccountsDAO
    {
        private readonly string connectionString;

        public AccountSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
<<<<<<< HEAD
=======
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
>>>>>>> 93e87783f04ba695286596bfcb6fe9f5fe6a0054

        public Accounts GetAccount(int userId)
        {
<<<<<<< HEAD
            Accounts account = null;

=======
            Account getBalance = null;
>>>>>>> 93e87783f04ba695286596bfcb6fe9f5fe6a0054
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT account_id, user_id, balance FROM accounts " +
                        "WHERE user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId); // replace @user_id with userId
                    SqlDataReader reader = cmd.ExecuteReader(); // send back a result that i can read

                    if (reader.HasRows && reader.Read())
                    {
                        account = GetAccountFromReader(reader); // reading the data into an account object
                    }
                }
            }
            catch (SqlException)
            { // todo wont write to console--send error codes
                Console.WriteLine("There was a problem with the database connection.");
            }

            return account;
        }

<<<<<<< HEAD
        public Accounts GetAccounts(int id)
=======
        public bool GetTransfer(Transfer transfer)
>>>>>>> 93e87783f04ba695286596bfcb6fe9f5fe6a0054
        {
            throw new NotImplementedException();
        }

        private Accounts GetAccountFromReader(SqlDataReader reader) // making the data into an account object 
        {
            Accounts accounts = new Accounts()
            {
<<<<<<< HEAD
                AccountId = Convert.ToInt32(reader["account_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                Balance = Convert.ToDecimal(reader["balance"]),
            };
=======
                throw new NotImplementedException();
            }
        }
        private Account GetAccountsFromReader(SqlDataReader reader)
        {
            Account accounts = new Account();
            accounts.AccountId = Convert.ToInt32(reader["account_id"]);
            accounts.UserId = Convert.ToInt32(reader["user_id"]);
            accounts.Balance = Convert.ToDecimal(reader["balance"]);
>>>>>>> 93e87783f04ba695286596bfcb6fe9f5fe6a0054

            return accounts;
        }
    }
}

