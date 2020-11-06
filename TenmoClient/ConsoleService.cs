using System;
using System.Collections.Generic;
using System.Linq;
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


        //adding helper methods to program.cs 

        public static int GetNumberInList(List<int> list)
        {
            string userInput = String.Empty;
            int intValue = 0;
            bool gettingNumberInList = true;

            do
            {
                userInput = Console.ReadLine();
                if (!int.TryParse(userInput, out intValue))
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }
                else
                {
                    if (list.Contains(intValue))
                    {
                        gettingNumberInList = false;
                    }
                    else
                    {
                        Console.WriteLine($"Number you entered is not a valid ID. Try again.");
                    }
                }

            }
            while (gettingNumberInList);

            return intValue;
        }
        public static int GetNumberInRange(int min, int max)
        {
            string userInput = String.Empty;
            int intValue = 0;
            bool gettingNumberInRange = true;

            do
            {
                userInput = Console.ReadLine();
                if (!int.TryParse(userInput, out intValue))
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }
                else
                {
                    if (min <= intValue && intValue <= max)
                    {
                        gettingNumberInRange = false;
                    }
                    else
                    {
                        Console.WriteLine($"Number you entered is not between {min} and {max}. Try again.");
                    }
                }
            }
            while (gettingNumberInRange);

            return intValue;
        }
        public static decimal GetAmount()
        {
            string userInput = String.Empty;
            decimal decimalValue = 0;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Invalid input format. Please try again");
                }
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!decimal.TryParse(userInput, out decimalValue));

            return decimalValue;


        }



        public API_Transfer StartTransfer(List<API_User> users)
        {
            int selection = -1;
            while (selection != 0)
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Users");
                Console.WriteLine("ID\t \tName");
                Console.WriteLine("-------------------------------------");
                foreach (API_User user in users)
                {
                    Console.WriteLine($"{user.UserId}:\t \t{user.Username}");
                }
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("");
                API_Transfer transfer = new API_Transfer()
                {
                    UserFromID = UserService.GetUserId(),
                    UserToID = UserToReceiveTransfer(users),
                    TransferStatusID = 2, //Sending transfers default to approved and sending.
                    TransferTypeID = 2,
                    TransferAmount = AmountToTransfer()
                };
                return transfer;
            }
            return null;
        }

        public int UserToReceiveTransfer(List<API_User> users)
        {
            int inputID = -1;
            bool doneChoosingID = false;
            while (!doneChoosingID)
            {
                Console.WriteLine("Enter ID of user you are sending to (0 to cancel):");

                if (!int.TryParse(Console.ReadLine(), out inputID))
                {
                    Console.WriteLine("Invalid input. Only input a number.");
                    continue;

                }
                if (inputID == 0)
                {
                    doneChoosingID = true;
                    break;
                }

                if (!users.Any((u) => { return u.UserId == inputID; }))
                {
                    Console.WriteLine("");
                    Console.WriteLine("Could not find a user with that ID");
                    Console.WriteLine("");
                    continue;
                }
                doneChoosingID = true;
            }
            return inputID;
        }
        public decimal AmountToTransfer()
        {
            decimal inputAmount = -1;
            bool doneChoosingAmount = false;
            while (!doneChoosingAmount)
            {
                Console.WriteLine("Enter Amount:");
                if (!decimal.TryParse(Console.ReadLine(), out inputAmount))
                {
                    Console.WriteLine("Invalid input. Only input a valid amount.");
                    continue;
                }
                if (inputAmount <= 0)
                {
                    Console.WriteLine("Invalid input. Only input a positive amount.");
                    continue;
                }
                doneChoosingAmount = true;
            }
            return inputAmount;
        }
    }
}
