using System;
using System.Collections.Generic;
using TenmoClient.Data;
using TenmoServer.Models;

namespace TenmoClient
{
    public class ConsoleService
    {
        /// <summary>
        /// Prompts for transfer ID to view, approve, or reject
        /// </summary>
        /// <param name="action">String to print in prompt. Expected values are "Approve" or "Reject" or "View"</param>
        /// <returns>ID of transfers to view, approve, or reject</returns>
        public int PromptForTransferID(string action)
        {
            Console.WriteLine("");
            Console.Write("Please enter transfer ID to " + action + " (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int auctionId))
            {
                Console.WriteLine("Invalid input. Only input a number.");
                return 0;
            }
            else
            {
                return auctionId;
            }
        }

        public Data.LoginUser PromptForLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            string password = GetPasswordFromConsole("Password: ");

            Data.LoginUser loginUser = new Data.LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        private string GetPasswordFromConsole(string displayMessage)
        {
            string pass = "";
            Console.Write(displayMessage);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Remove(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine("");
            return pass;
        }


        //////adding methods 

        public void PrintTransferDetails(Transfer transfer)
        {

        ////    Console.WriteLine("Transfer Details");


        ////    Console.WriteLine($"ID: {transfer.TransferId}");
        ////    Console.WriteLine($"From: {transfer.AccountFrom}");
        ////    Console.WriteLine($"To: {transfer.AccountTo}");
        ////    Console.WriteLine($"Type: {transfer.TransferTypeId.ToString()}");
        ////    Console.WriteLine($"Status: {transfer.TransferStatusId.ToString()}");
        ////    Console.WriteLine($"Amount: {transfer.Amount.ToString("C2")}");

        ////}
        ////public void PrintAllTransfers(List<API_Transfer> transfers)
        ////{

        ////    Console.WriteLine("Transfers");
        ////    Console.Write("ID", ' ');
        ////    Console.Write("From/To", ' ');
        ////    Console.Write("Amount", ' ');
        ////    Console.WriteLine();


        ////    foreach (API_Transfer transfer in transfers)
        ////    {
        ////        string otherMessage;

        ////        if (transfer.FromUserID == UserService.GetUserId()) otherMessage = $"To: {transfer.ToUserName}";
        ////        else otherMessage = $"From: {transfer.FromUserName}";

        ////        Console.Write(transfer.TransferID.ToString(), ' ');
        ////        Console.Write(otherMessage, ' ');
        ////        Console.Write(transfer.TransferAmount.ToString("C2"), ' ');
        ////        Console.WriteLine();
        ////    }

        ////}

        ////public API_Transfer PromptForTransferRequest(TransferType transferType)
        ////{
        ////    bool succeedID = false;
        ////    bool succeedDollarAmount = false;
        ////    var transfer = new API_Transfer();

        ////    int UserIDInput = 0;
        ////    decimal dollarAmountInput = 0;

        ////    string[] userResponseArray;

        ////    do
        ////    {
        ////        Console.WriteLine("Please enter user id and transfer amount respectively.");
        ////        Console.WriteLine("For Example: 1 100");
        ////        var userResponse = Console.ReadLine();
        ////        userResponseArray = userResponse.Split(" ");

        ////        if (userResponseArray.Length != 2) continue;

        ////        succeedID = Int32.TryParse(userResponseArray[0], out UserIDInput);
        ////        succeedDollarAmount = Decimal.TryParse(userResponseArray[1], out dollarAmountInput);
        ////    }
        ////    while (!succeedID || !succeedDollarAmount);

        ////    if (transferType == TransferType.Send)
        ////    {
        ////        transfer.ToUserID = UserIDInput;
        ////    }
        ////    else { transfer.FromUserID = UserIDInput; }
        ////    transfer.TransferAmount = dollarAmountInput;
        ////    transfer.TransferType = transferType;
        ////    return transfer;
        ////}



        ////public void PrintUsers(List<API_User> users)
        ////{

        ////    Console.WriteLine("Users");
        ////    Console.Write("ID", ' ');
        ////    Console.Write("Name", ' ');
        ////    Console.WriteLine();


        ////    foreach (API_User user in users)
        ////    {
        ////        Console.Write(user.UserId.ToString(), ' ');
        ////        Console.Write(user.Username.ToString(), ' ');
        ////        Console.WriteLine();
        ////    }

        ////}










    }
}
