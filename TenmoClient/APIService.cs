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
    class APIService
    {
        private readonly string API_URL = "https://localhost:44315/";
        private readonly RestClient client = new RestClient();


        public List<API_User>  GetAccounts()
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            RestRequest request = new RestRequest(API_URL + "user/all");
            IRestResponse<List<API_User>> response = client.Get<List<API_User>>(request);
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
        public List<API_User> GetOtherAccounts()
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            RestRequest request = new RestRequest(API_URL + "user/others");
            IRestResponse<List<API_User>> response = client.Get<List<API_User>>(request);
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

        //public Accounts UpdateRecipientBalance(User balance, int userId)
        //{
        //    //int result = balance? return result? 
        //    client.Authenticator = new JwtAuthenticator(UserService.GetToken());

        //    RestRequest request = new RestRequest(API_URL + "accounts/" + balance + userId);
        //    request.AddJsonBody(balance);
        //    IRestResponse<Accounts> response = client.Put<Accounts>(request);
        //    if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
        //    {
        //        ProcessErrorResponse(response);
        //    }
        //    else
        //    {
        //        return response.Data;
        //    }
        //        return null;
        //}
        //public Accounts UpdateSenderBalance(User balance, int userId)
        //{
        //    //int result = balance? return result? 
        //    client.Authenticator = new JwtAuthenticator(UserService.GetToken());

        //    RestRequest request = new RestRequest(API_URL + "accounts/" + balance + userId);
        //    request.AddJsonBody(balance);
        //    IRestResponse<Accounts> response = client.Put<Accounts>(request);
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
        //public User Send(string userName, int userId)
        //{
        //    client.Authenticator = new JwtAuthenticator(UserService.GetToken());

        //    RestRequest request = new RestRequest(API_URL + "users/" + userName + userId);
        //    request.AddJsonBody(userId);
        //    IRestResponse<User> response = client.Put<User>(request);
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
        public bool SubmitTransfer(Transfers transfers)
        {
            {
                client.Authenticator = new JwtAuthenticator(UserService.GetToken());

                RestRequest request = new RestRequest(API_URL + "transfer/request");
                request.AddJsonBody(transfers);
                IRestResponse<Transfers> response = client.Post<Transfers>(request);
              
                 
                return true;
            }
        }
        public bool UpdateTransfer(Transfers transfers, int approvalOption)
        {
            if (approvalOption == 1)
            {
               // transfers.TransferStatusId == transfers.TransferStatusId;
            }
            else if (approvalOption == 2)
            {
               // transfers.TransferStatusId;
            }
            else 
                {
                return false;
            }
            
                client.Authenticator = new JwtAuthenticator(UserService.GetToken());

                RestRequest request = new RestRequest(API_URL + "transfer");
                request.AddJsonBody(transfers);
                IRestResponse<Transfers> response = client.Put<Transfers>(request);


                return true;
           
        }
        public List<Transfers> GetTransfer()
        {
            {
                client.Authenticator = new JwtAuthenticator(UserService.GetToken());

                RestRequest request = new RestRequest(API_URL + "transfers"); 
                IRestResponse<List<Transfers>> response = client.Get<List<Transfers>>(request);
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
        }
     

        public decimal? GetBalance ()
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            RestRequest request = new RestRequest(API_URL + "accounts/");
            IRestResponse<decimal> response = client.Get<decimal>(request);

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
