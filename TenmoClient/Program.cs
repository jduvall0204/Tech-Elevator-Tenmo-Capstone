﻿using System;
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

                    List<API_Transfer> transfers = apiService.ListTransfers();
                    if (transfers != null && transfers.Count > 0)
                    {
                        consoleService.WriteTransferList(transfers);
                        int id = consoleService.TransferToDetail(transfers);
                        consoleService.WriteTransferDetail(transfers, id);
                        
                    }

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
                            if (transfer.transferAmount > accountFrom.balance)
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
