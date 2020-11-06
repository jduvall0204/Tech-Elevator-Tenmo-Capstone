using System;
using System.Collections.Generic;
using System.Linq;
using TenmoClient.Data;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoClient
{
    class Program
    {
        private static readonly ConsoleService consoleService = new ConsoleService();
        private static readonly AuthService authService = new AuthService();
        private static APIService apiService = new APIService();

        static void Main(string[] args)
        {
            Run();
        }
        private static void Run()
        {
            int loginRegister = -1;
            while (loginRegister != 1 && loginRegister != 2)
            {
                Console.WriteLine("Welcome to TEnmo!");
                Console.WriteLine("1: Login");
                Console.WriteLine("2: Register");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out loginRegister))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                }
                else if (loginRegister == 1)
                {
                    while (!UserService.IsLoggedIn()) //will keep looping until user is logged in
                    {
                        Data.LoginUser loginUser = consoleService.PromptForLogin();
                        API_User user = authService.Login(loginUser);
                        if (user != null)
                        {
                            UserService.SetLogin(user);
                        }
                    }
                }
                else if (loginRegister == 2)
                {
                    bool isRegistered = false;
                    while (!isRegistered) //will keep looping until user is registered
                    {
                        Data.LoginUser registerUser = consoleService.PromptForLogin();
                        isRegistered = authService.Register(registerUser);
                        if (isRegistered)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Registration successful. You can now log in.");
                            loginRegister = -1; //reset outer loop to allow choice for login
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }

            MenuSelection();
        }

        private static void MenuSelection()
        {
            int menuSelection = -1;
            while (menuSelection != 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Welcome to TEnmo! Please make a selection: ");
                Console.WriteLine("1: View your current balance");
                Console.WriteLine("2: View your past transfers");
                Console.WriteLine("3: View your pending requests"); //optional
                Console.WriteLine("4: Send TE bucks");
                Console.WriteLine("5: Request TE bucks"); //optional 
                Console.WriteLine("6: Log in as different user");
                Console.WriteLine("0: Exit");
                Console.WriteLine("---------");
                Console.Write("Please choose an option: ");

                menuSelection = ConsoleService.GetNumberInRange(0, 6);

                 if (menuSelection == 1) 
                {
                    {
                        Console.WriteLine($"Your current account balance is: ${apiService.GetBalance()}");
                        

                    }
                }
                else if (menuSelection == 2)
                {
                    //List<TransferDetails> transferHistory = apiService.GetTransferHistory();
                    //var allTransferIDs = new List<int>();
                    //allTransferIDs.Add(0);
                    //Console.WriteLine("-------------------------------------------");
                    //Console.WriteLine("Transfer IDs         From/To         Amount");
                    //Console.WriteLine("-------------------------------------------");
                    //foreach (var item in transferHistory)
                    //{
                    //    if (item.TransferId < 10)
                    //    {
                    //        if (item.FromUser == UserService.GetUsername())
                    //        {
                    //            Console.WriteLine($"0{item.TransferId}              To: {item.ToUser}             {item.Amount}");
                    //        }
                    //        else
                    //        {
                    //            Console.WriteLine($"0{item.TransferId}              From: {item.FromUser}             {item.Amount}");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (item.FromUser == UserService.GetUsername())
                    //        {
                    //            Console.WriteLine($"{item.TransferId}              To: {item.ToUser}             {item.Amount}");
                    //        }
                    //        else
                    //        {
                    //            Console.WriteLine($"{item.TransferId}              From: {item.FromUser}             {item.Amount}");
                    //        }
                    //    }
                    //    allTransferIDs.Add(item.TransferId);
                    //}
                    //Console.WriteLine("-------------------------------------------");
                    //Console.WriteLine("Please enter transfer ID to view details (0 to cancel):");
                    //int transferId =ConsoleService.GetNumberInList(allTransferIDs);

                    //if (transferId != 0)
                    //{
                    //    TransferDetails transfer = apiService.GetTransferById(transferId);

                    //    Console.WriteLine("-------------------------------------------");
                    //    Console.WriteLine("Transfer details");
                    //    Console.WriteLine("-------------------------------------------");
                    //    Console.WriteLine($"ID: {transfer.TransferId}");
                    //    Console.WriteLine($"From: {transfer.FromUser}");
                    //    Console.WriteLine($"To: {transfer.ToUser}");
                    //    Console.WriteLine($"Type: {transfer.TransferType}");
                    //    Console.WriteLine($"Status: {transfer.TransferStatus}");
                    //    Console.WriteLine($"Amount: {transfer.Amount}");
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Returning to the main menu.");
                    //}
                }

                else if (menuSelection == 3)
                {
                    Console.WriteLine("Feature unavailable. Returning to main menu.");
                }
                else if (menuSelection == 4)
                {

                    {
                        List<API_User> users = apiService.ListUsers();
                        int userID = UserService.GetUserId();
                        API_Account accountFrom = apiService.GetAccount(userID);
                        if (users != null && users.Count > 0)
                        {
                            API_Transfer transfer = consoleService.StartTransfer(users);
                            if (transfer.TransferAmount > accountFrom.Balance)
                            {
                                Console.WriteLine("Insufficient Funds");
                            }
                            else
                            {
                                API_Transfer updatedTransfer = apiService.DoTransfer(transfer);
                                apiService.UpdateBalance(updatedTransfer);
                                Console.WriteLine("Amount has been transferred");
                            }
                        }

                    }
                    //List<API_User> users = apiService.ListUsers();
                    //var allUserIDs = new List<int>();
                    //int currentUserId = UserService.GetUserId();
                    ////users.RemoveAt(currentUserId - 1);

                    //Console.WriteLine("-------------------------------------------");
                    //Console.WriteLine("User IDs     Names");
                    //Console.WriteLine("-------------------------------------------");
                    //foreach (var item in users)
                    //{
                    //    Console.WriteLine($"{item.UserId}           {item.Username}");
                    //    allUserIDs.Add(item.UserId);
                    //}
                    //Console.WriteLine("-------------------------------------------");
                    //Console.WriteLine("Enter the ID of user you are sending to (0 to cancel):");
                    //int receiverId = ConsoleService.GetNumberInList(allUserIDs);

                    //if (receiverId != 0)
                    //{
                    //    Console.WriteLine("Enter amount to send:");
                    //    decimal amount = ConsoleService.GetAmount();
                    //    TransferDetails result = apiService.SendMoney(receiverId, amount);

                    //    if (result != null)
                    //    {
                    //        Console.WriteLine("Transfer successful! :)");
                    //        Console.WriteLine("-------------------------------------------");
                    //        Console.WriteLine("Transfer Details");
                    //        Console.WriteLine("-------------------------------------------");
                    //        Console.WriteLine($"ID: {result.TransferId}");
                    //        Console.WriteLine($"From: {result.FromUser}");
                    //        Console.WriteLine($"To: {result.ToUser}");
                    //        Console.WriteLine($"Type: {result.TransferType}");
                    //        Console.WriteLine($"Status: {result.TransferStatus}");
                    //        Console.WriteLine($"Amount: {result.Amount}");
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("Transfer not completed");
                    //    }
                    //}
                }

                else if (menuSelection == 5)
                {
                    Console.WriteLine("Feature unavailable. Returning to main menu.");
                }
                else if (menuSelection == 6)
                {
                    Console.WriteLine("");
                    UserService.SetLogin(new API_User()); //wipe out previous login info
                    Run(); //return to entry point
                }
                else
                {
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                }
            }

        }


       
    }
}
