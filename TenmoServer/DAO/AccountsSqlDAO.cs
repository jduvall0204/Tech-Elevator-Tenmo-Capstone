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

        public Account GetAccount(int userId)
        {
            Account account = null;

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

        public Account GetAccounts(int id)
        {
            throw new NotImplementedException();
        }

        private Account GetAccountFromReader(SqlDataReader reader) // making the data into an account object 
        {
            Account a = new Account()
            {
                AccountId = Convert.ToInt32(reader["account_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                Balance = Convert.ToDecimal(reader["balance"]),
            };

            return a;
        }
    }
}

