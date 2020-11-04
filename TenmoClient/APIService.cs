using RestSharp;
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


        //do we need account here and not user??
        public List<User>  GetAccounts(int userId)
        {//what to add with url? and what list are we looking for account?
            RestRequest request = new RestRequest(API_URL + "accounts/" + userId);
            IRestResponse<List<User>> response = client.Get<List<User>>(request);
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
        //userId add ???
        public User UpdateRecipientBalance(User balance, int userId)
        {
            //int result = balance? return result? 

            RestRequest request = new RestRequest(API_URL + "accounts/" + balance + userId);
            request.AddJsonBody(balance);
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
        //userId add ???
        public User UpdateSenderBalance(User balance, int userId)
        {
            //int result = balance? return result? 

            RestRequest request = new RestRequest(API_URL + "accounts/" + balance + userId);
            request.AddJsonBody(balance);
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
        public User Send(string userName, int userId)
        {
            //balance at all? 
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
        public User GetTransfer(int transferId, int accountId)
        {//list of transfers? 
            { 
                RestRequest request = new RestRequest(API_URL + "transfers/" + transferId + accountId);
                IRestResponse<User> response = client.Get<User>(request);
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
