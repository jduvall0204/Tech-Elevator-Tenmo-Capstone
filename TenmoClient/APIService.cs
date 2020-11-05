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
        private API_User user = new API_User();


        //Logged in?
       // public bool LoggedIn { get { return !string.IsNullOrWhiteSpace(user.Token); } }

        //public APIService(string api_url)
        //{
        //    API_URL = api_url;
        //}


        public List<Accounts>  GetAccounts(int accountId)
        {
            //what to add with url? and what list are we looking for account?
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            RestRequest request = new RestRequest(API_URL + "accounts/" + accountId );
            IRestResponse<List<Accounts>> response = client.Get<List<Accounts>>(request);
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
        public Accounts UpdateRecipientBalance(User balance, int userId)
        {
            //int result = balance? return result? 
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            RestRequest request = new RestRequest(API_URL + "accounts/" + balance + userId);
            request.AddJsonBody(balance);
            IRestResponse<Accounts> response = client.Put<Accounts>(request);
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
        public Accounts UpdateSenderBalance(User balance, int userId)
        {
            //int result = balance? return result? 
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            RestRequest request = new RestRequest(API_URL + "accounts/" + balance + userId);
            request.AddJsonBody(balance);
            IRestResponse<Accounts> response = client.Put<Accounts>(request);
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
        public User Send(string userName, int userId)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            RestRequest request = new RestRequest(API_URL + "users/" + userName + userId);
            request.AddJsonBody(userId);
            IRestResponse<User> response = client.Put<User>(request);
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
        public Transfers GetTransfer(int transferId)//, int accountFrom, int accountTo)
        {
            //list of transfers? 
            {
                client.Authenticator = new JwtAuthenticator(UserService.GetToken());

                RestRequest request = new RestRequest(API_URL + "transfers/" + transferId); //+ accountFrom + accountTo);
                IRestResponse<Transfers> response = client.Get<Transfers>(request);
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

        public Accounts GetBalance (int balance, int userId)
        {

            //userId
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            RestRequest request = new RestRequest(API_URL + "accounts/" + balance);
            IRestResponse<Accounts> response = client.Get<Accounts>(request);

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

        //do we want error messages like in the examples? 
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
