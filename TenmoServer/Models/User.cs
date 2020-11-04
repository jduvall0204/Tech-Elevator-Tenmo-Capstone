using Microsoft.AspNetCore.SignalR;

namespace TenmoServer.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return UserId.ToString() + Username;
        }
    }

    /// <summary>
    /// Model to return upon successful login
    /// </summary>
    public class ReturnUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        //public string Role { get; set; }
        public string Token { get; set; }

        //created this method twice as I am unsure currently which one we will want to call on to display user info.  
        //since these are separate classes, easier to put it twice and delete what we dont need.
        public override string ToString()
        {
            return UserId.ToString() + Username;
        }
    }

    

    /// <summary>
    /// Model to accept login parameters
    /// </summary>
    public class LoginUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
