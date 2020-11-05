using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TenmoClient.Data;
using TenmoServer.Models;
using NewTransfer = TenmoClient.Data.NewTransfer;
using TransferWithDetails = TenmoClient.Data.TransferWithDetails;

namespace TenmoClient
{
    public class APIService
    {
        private readonly string API_URL = "https://localhost:44315/";
        private readonly RestClient client = new RestClient();


        public API_Account GetAccounts()
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            RestRequest request = new RestRequest(API_URL + "accounts/");
            IRestResponse<API_Account> response = client.Get<API_Account>(request);
            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;


        }
        public TransferWithDetails GetTransferById(int transferId)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            IRestRequest request = new RestRequest(API_URL + "transfer/" + transferId);
            IRestResponse<TransferWithDetails> response = client.Get<TransferWithDetails>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }

            return null;
        }
        public List<TransferWithDetails> GetTransferHistory()
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            IRestRequest request = new RestRequest(API_URL + "transfer/history");
            IRestResponse<List<TransferWithDetails>> response = client.Get<List<TransferWithDetails>>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public List<API_User> ListUsers()
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            var allUsers = new List<API_User>();
            RestRequest request = new RestRequest(API_URL + "transfer");
            IRestResponse<List<API_User>> response = client.Get<List<API_User>>(request);
            allUsers = response.Data;

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return allUsers;
            }
            return null;
        }
        public TransferWithDetails SendMoney(int receiverId, decimal amount)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            NewTransfer nt = new NewTransfer(receiverId, amount);
            RestRequest request = new RestRequest(API_URL + $"transfer");
            request.AddJsonBody(nt);
            IRestResponse<TransferWithDetails> response = client.Post<TransferWithDetails>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }


        private void ProcessErrorResponse(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                Console.WriteLine("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                Console.WriteLine("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
        }
    }
}
