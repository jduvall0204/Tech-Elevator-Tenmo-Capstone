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
        public TransferWithDetails SendMoney(int senderId, int receiverId, decimal amount)
        {
            var transfer = new Transfer();
            var returnTransfer = new TransferWithDetails();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE accounts SET balance = balance - @amount WHERE user_id = @sender_id", conn);
                    cmd.Parameters.AddWithValue("@sender_id", senderId);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("UPDATE accounts SET balance = balance + @amount WHERE user_id = @receiver_id", conn);
                    cmd.Parameters.AddWithValue("@receiver_id", receiverId);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.ExecuteNonQuery();

                    //transfer initial status is "approved"
                    cmd = new SqlCommand("INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) " +
                    "VALUES (2, 2, @sender_id, @receiver_id, @amount)", conn);
                    cmd.Parameters.AddWithValue("@sender_id", senderId);
                    cmd.Parameters.AddWithValue("@receiver_id", receiverId);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT @@IDENTITY", conn);
                    int transferId = Convert.ToInt32(cmd.ExecuteScalar());

                    returnTransfer = GetTransfer(transferId);
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("There was a problem with the database connection.");
            }

            return returnTransfer;
        }

        public List<TransferWithDetails> GetTransferHistory(int userId)
        {
            var allTransfers = new List<TransferWithDetails>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM transfers " +
                    "WHERE account_from = @user_id OR account_to = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Transfer transfer = GetTransferFromReader(reader);
                            TransferWithDetails details = GetDetailsFromTransfer(transfer);

                            allTransfers.Add(details);
                        }
                    }
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("There was a problem with the database connection.");
            }

            return allTransfers;
        }
        public TransferWithDetails GetTransfer(int transferId)
        {
            var transfer = new Transfer();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM transfers " +
                    "WHERE transfer_id = @transfer_id", conn);
                    cmd.Parameters.AddWithValue("@transfer_id", transferId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        transfer = GetTransferFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("There was a problem with the database connection.");
            }

            return GetDetailsFromTransfer(transfer);
        }
        public string GetTypeFromTransfer(int transferTypeId)
        {
            string transferType = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT transfer_type_desc FROM transfer_types " +
                    "WHERE transfer_type_id = @transfer_type_id", conn);
                    cmd.Parameters.AddWithValue("@transfer_type_id", transferTypeId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        transferType = Convert.ToString(reader["transfer_type_desc"]);
                    }
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("There was a problem with the database connection.");
            }
            return transferType;
        }

        public string GetStatusFromTransfer(int transferStatusId)
        {
            string transferStatus = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT transfer_status_desc FROM transfer_statuses " +
                    "WHERE transfer_status_id = @transfer_status_id", conn);
                    cmd.Parameters.AddWithValue("@transfer_status_id", transferStatusId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        transferStatus = Convert.ToString(reader["transfer_status_desc"]);
                    }
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("There was a problem with the database connection.");
            }
            return transferStatus;
        }

        public string GetUsernameFromAccount(int accountId)
        {
            AccountUsername username = new AccountUsername();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT username FROM accounts " +
                    "JOIN users ON accounts.user_id = users.user_id " +
                    "WHERE account_id = @account_id", conn);
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        username.Username = Convert.ToString(reader["username"]);
                    }
                }
            }
            catch (SqlException)
            {
                Console.WriteLine("There was a problem with the database connection.");
            }

            return username.Username;
        }
        private Transfer GetTransferFromReader(SqlDataReader reader)
        {
            Transfer t = new Transfer();

            t.TransferId = Convert.ToInt32(reader["transfer_id"]);
            t.TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]);
            t.TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]);
            t.AccountFrom = Convert.ToInt32(reader["account_from"]);
            t.AccountTo = Convert.ToInt32(reader["account_to"]);
            t.Amount = Convert.ToDecimal(reader["amount"]);

            return t;
        }
        private TransferWithDetails GetDetailsFromTransfer(Transfer transfer)
        {
            var transferDetails = new TransferWithDetails();

            transferDetails.TransferId = transfer.TransferId;
            transferDetails.TransferType = GetTypeFromTransfer(transfer.TransferTypeId);
            transferDetails.TransferStatus = GetStatusFromTransfer(transfer.TransferStatusId);
            transferDetails.FromUser = GetUsernameFromAccount(transfer.AccountFrom);
            transferDetails.ToUser = GetUsernameFromAccount(transfer.AccountTo);
            transferDetails.Amount = transfer.Amount;

            return transferDetails;
        }
    }
}