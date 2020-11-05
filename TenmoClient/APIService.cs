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
        private readonly string API_URL = "https://localhost:44315/";
        private readonly RestClient client = new RestClient();


        public List<API_User>  GetAccounts()
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());

            RestRequest request = new RestRequest(API_URL + "users");
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

            RestRequest request = new RestRequest(API_URL + "users");
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

        public bool SubmitTransfer(API_Transfer transfers)
        {
            {
                client.Authenticator = new JwtAuthenticator(UserService.GetToken());

                RestRequest request = new RestRequest(API_URL + "transfers");
                request.AddJsonBody(transfers);
                IRestResponse<API_Transfer> response = client.Post<API_Transfer>(request);
              
                 
                return true;
            }
        }
       
        public List<API_Transfer> GetTransfer()
        {
            {
                client.Authenticator = new JwtAuthenticator(UserService.GetToken());

                RestRequest request = new RestRequest(API_URL + "transfers"); 
                IRestResponse<List<API_Transfer>> response = client.Get<List<API_Transfer>>(request);
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

            RestRequest request = new RestRequest(API_URL + "accounts");
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
