using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountsSqlDAO : IAccountsDAO
    {

        private string connectionString;

        public AccountsSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public IList<Accounts> GetAccounts()
        {
            List<Accounts> output = new List<Accounts>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "select * from accounts";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Accounts accounts = GetAccountFromReader(reader);

                        output.Add(accounts);
                    }
                }
                return output;
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
        }

        private Accounts GetAccountFromReader(SqlDataReader reader)
        {
            Accounts accounts = new Accounts();
            accounts.AccountId = Convert.ToInt32(reader["account_id"]);
            accounts.UserId = Convert.ToInt32(reader["user_id"]);
            accounts.Balance = Convert.ToInt32(reader["balance"]);
           
            return accounts;
        }
    }
}

