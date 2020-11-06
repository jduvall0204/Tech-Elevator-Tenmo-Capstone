using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TenmoClient.Data;
using TenmoServer.Models;


namespace TenmoClient
{
    public class APIService
    {
        private readonly static string API_URL = "https://localhost:44315/";
        private readonly IRestClient client = new RestClient();

        


        public decimal GetBalance()
        {
            decimal balance = 0;
            RestRequest restRequest = new RestRequest(API_URL + "account/balance");
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<decimal> response = client.Get<decimal>(restRequest);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                balance = response.Data;
                return balance;
            }
            return balance;
        }

        public API_Account GetAccount(int id)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            RestRequest request = new RestRequest($"{API_URL}/account/{id}");
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

        public API_Transfer DoTransfer(API_Transfer transfer)
        {
            RestRequest restRequest = new RestRequest(API_URL + "/transfer");
            restRequest.AddJsonBody(transfer);
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<API_Transfer> response = client.Post<API_Transfer>(restRequest);

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
        public API_Transfer UpdateBalance(API_Transfer transfer)
        {
            RestRequest restRequest = new RestRequest($"{API_URL}/transfer");
            restRequest.AddJsonBody(transfer);
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            IRestResponse<API_Transfer> response = client.Put<API_Transfer>(restRequest);

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
        //public TransferDetails GetTransferById(int transferId)
        //{
        //    client.Authenticator = new JwtAuthenticator(UserService.GetToken());

        //    IRestRequest request = new RestRequest(API_URL + "transfer/" + transferId);
        //    IRestResponse<TransferDetails> response = client.Get<TransferDetails>(request);

        //    if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
        //    {
        //        ProcessErrorResponse(response);
        //    }
        //    else
        //    {
        //        return response.Data;
        //    }

        //    return null;
        //}
        //public List<TransferDetails> GetTransferHistory()
        //{
        //    client.Authenticator = new JwtAuthenticator(UserService.GetToken());

        //    IRestRequest request = new RestRequest(API_URL + "transfer/history");
        //    IRestResponse<List<TransferDetails>> response = client.Get<List<TransferDetails>>(request);

        //    if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
        //    {
        //        ProcessErrorResponse(response);
        //    }
        //    else
        //    {
        //        return response.Data;
        //    }
        //    return null;
        //}

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
        //public TransferDetails SendMoney(int receiverId, decimal amount)
        //{
        //    client.Authenticator = new JwtAuthenticator(UserService.GetToken());

        //    Data.NewTransfer nt = new Data.NewTransfer(receiverId, amount);
        //    RestRequest request = new RestRequest(API_URL + $"transfer");
        //    request.AddJsonBody(nt);
        //    IRestResponse<TransferDetails> response = client.Post<TransferDetails>(request);

        //    if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
        //    {
        //        ProcessErrorResponse(response);
        //    }
        //    else
        //    {
        //        return response.Data;
        //    }
        //    return null;
        //}


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
