using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDAO : ITransfersDAO
    {
        private readonly string connectionString;

        public TransferSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public bool AddTransfer(Transfers transfer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) ";
                    sqlText += "VALUES ((SELECT transfer_type_id FROM transfer_types WHERE transfer_type_desc = @transfertypedesc), ";
                    sqlText += "(SELECT transfer_status_id FROM transfer_statuses WHERE transfer_status_desc = @transferstatusdesc), ";
                    sqlText += "(SELECT account_id FROM accounts WHERE user_id = @user_id_from), ";
                    sqlText += "(SELECT account_id FROM accounts WHERE user_id = @user_id_to), ";
                    sqlText += "@amount)";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@transfertypedesc", transfer.TransferId.ToString());
                    cmd.Parameters.AddWithValue("@transferstatusdesc", transfer.TransferStatusId.ToString());
                    cmd.Parameters.AddWithValue("@user_id_from", transfer.AccountFrom);
                    cmd.Parameters.AddWithValue("@user_id_to", transfer.AccountTo);
                    cmd.Parameters.AddWithValue("@amount", transfer.Amount);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                return false;
            }
            return true;
        }
        public List<Transfers> GetTransfers(string username)
        {
            List<Transfers> returnTransfers = new List<Transfers>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "SELECT transfer_id, users_from.user_id AS user_id_from, users_to.user_id AS user_id_to, ";
                    sqlText += "users_from.username AS username_from, users_to.username AS username_to, transfers.amount, ";
                    sqlText += "transfer_types.transfer_type_desc, transfer_statuses.transfer_status_desc ";
                    sqlText += "FROM transfers ";
                    sqlText += "JOIN accounts as accounts_from ON accounts_from.account_id = transfers.account_from ";
                    sqlText += "JOIN users AS users_from ON users_from.user_id = accounts_from.account_id ";
                    sqlText += "JOIN accounts as accounts_to ON accounts_to.account_id = transfers.account_to ";
                    sqlText += "JOIN users AS users_to ON users_to.user_id = accounts_to.account_id ";
                    sqlText += "JOIN transfer_statuses ON transfer_statuses.transfer_status_id = transfers.transfer_status_id ";
                    sqlText += "JOIN transfer_types ON transfer_types.transfer_type_id = transfers.transfer_type_id ";
                    sqlText += "WHERE users_from.username = @username or users_to.username = @username";

                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Transfers transfer = GetTransferFromReader(reader);
                            returnTransfers.Add(transfer);
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }

            return returnTransfers;
        }
        public Transfers GetTransferFromID(int transferID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "SELECT transfer_id, users_from.user_id AS user_id_from, users_to.user_id AS user_id_to, ";
                    sqlText += "users_from.username AS username_from, users_to.username AS username_to, transfers.amount, ";
                    sqlText += "transfer_types.transfer_type_desc, transfer_statuses.transfer_status_desc ";
                    sqlText += "FROM transfers ";
                    sqlText += "JOIN accounts as accounts_from ON accounts_from.account_id = transfers.account_from ";
                    sqlText += "JOIN users AS users_from ON users_from.user_id = accounts_from.account_id ";
                    sqlText += "JOIN accounts as accounts_to ON accounts_to.account_id = transfers.account_to ";
                    sqlText += "JOIN users AS users_to ON users_to.user_id = accounts_to.account_id ";
                    sqlText += "JOIN transfer_statuses ON transfer_statuses.transfer_status_id = transfers.transfer_status_id ";
                    sqlText += "JOIN transfer_types ON transfer_types.transfer_type_id = transfers.transfer_type_id ";
                    sqlText += "WHERE transfers.transfer_id = @transferid";

                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@transferid", transferID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Transfers transfer = new Transfers();
                        while (reader.Read())
                        {
                            transfer = GetTransferFromReader(reader);
                        }
                        return transfer;
                    }
                }
            }
            catch (SqlException)
            {
                return null;
            }
            return null;
        }
        public bool UpdateTransfer(Transfers transfer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "UPDATE transfers ";
                    sqlText += "SET transfer_status_id = (SELECT transfer_status_id FROM transfer_statuses WHERE transfer_status_desc = @transferstatusdesc) ";
                    sqlText += "WHERE transfer_id = @transferid ";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@transferstatusdesc", transfer.TransferStatusId.ToString());
                    cmd.Parameters.AddWithValue("@transferid", transfer.TransferId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                return false;
            }
            return true;
        }
        private Transfers GetTransferFromReader(SqlDataReader reader)
        {
            Transfers transfers = new Transfers();
            transfers.TransferId = Convert.ToInt32(reader["transfer_id"]);
            transfers.TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]);
            transfers.TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]);
            transfers.AccountFrom = Convert.ToInt32(reader["account_from"]);
            transfers.AccountTo = Convert.ToInt32(reader["account_to"]);
            transfers.Amount = Convert.ToDecimal(reader["amount"]);

            return transfers;
        }
    }
}